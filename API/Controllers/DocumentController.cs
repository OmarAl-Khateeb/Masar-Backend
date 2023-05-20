using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DocumentController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;

        public DocumentController(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
        {
            _imageService = imageService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<DocumentDto>>> GetDocuments(
            [FromQuery] DocumentSpecParams DocumentParams)
        {
            var spec = new DocumentSpecification(DocumentParams);
            var countSpec = new DocumentSpecification(DocumentParams);

            var totalItems = await _unitOfWork.Repository<Document>().CountAsync(countSpec);
            var Documents = await _unitOfWork.Repository<Document>().ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<DocumentDto>>(Documents);

            return Ok(new Pagination<DocumentDto>(DocumentParams.PageIndex,
                DocumentParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentDto>> GetDocument(int id)
        {
            var Document = await _unitOfWork.Repository<Document>().GetByIdAsync(id);

            if (Document == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Document, DocumentDto>(Document);
        }

        [HttpPost]
        public async Task<ActionResult<DocumentDto>> CreateDocument([FromForm] DocumentCDto DocumentCDto)
        {
            if (DocumentCDto.File == null) return BadRequest(new ApiResponse(400, "Please upload a file"));

            var Document = await _imageService.UploadDocumentAsync(DocumentCDto.File, "students/documents");
            
            // Document = _mapper.Map<DocumentCDto, Document>(DocumentCDto); seems to overwrite instead of add values

            Document.DocumentType = DocumentCDto.DocumentType;
            Document.Tags = DocumentCDto.Tags;
            Document.Note = DocumentCDto.Note;

            Document.Student = await _unitOfWork.Repository<Student>().GetByIdAsync(DocumentCDto.StudentId);

            _unitOfWork.Repository<Document>().Add(Document);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Creating Document"));

            return Created("test", _mapper.Map<Document, DocumentDto>(Document));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DocumentDto>> UpdateDocument(int id, DocumentCDto DocumentCDto)
        {
            var Document = await _unitOfWork.Repository<Document>().GetByIdAsync(id);

            if (Document == null) return NotFound(new ApiResponse(404));

            _mapper.Map<DocumentCDto, Document>(DocumentCDto, Document);

            _unitOfWork.Repository<Document>().Update(Document);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Updating Document"));

            return Ok( _mapper.Map<Document, DocumentDto>(Document));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDocument(int id)
        {
            var Document = await _unitOfWork.Repository<Document>().GetByIdAsync(id);

            if (Document == null) return NotFound(new ApiResponse(404));

            _unitOfWork.Repository<Document>().Delete(Document);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Deleting Document"));

            return NoContent();
        }
    }
}