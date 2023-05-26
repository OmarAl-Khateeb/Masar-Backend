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
    public class NoteController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;

        public NoteController(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
        {
            _imageService = imageService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
//no point in this endpoint just use user note one
        // [HttpGet]
        // public async Task<ActionResult<Pagination<NoteDto>>> GetNotes(
        //     [FromQuery] NoteSpecParams NoteParams)
        // {
        //     var spec = new NoteSpecification(NoteParams);
        //     var countSpec = new NoteSpecification(NoteParams);

        //     var totalItems = await _unitOfWork.Repository<Note>().CountAsync(countSpec);
        //     var Notes = await _unitOfWork.Repository<Note>().ListAsync(spec);

        //     var data = _mapper.Map<IReadOnlyList<NoteDto>>(Notes);

        //     return Ok(new Pagination<NoteDto>(NoteParams.PageIndex,
        //         NoteParams.PageSize, totalItems, data));
        // }

        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDto>> GetNote(int id)
        {
            var Note = await _unitOfWork.Repository<Note>().GetByIdAsync(id);

            if (Note == null) return NotFound(new ApiResponse(404));

            var NoteDto = _mapper.Map<Note, NoteDto>(Note);

            return Ok(new ApiResponse(200, NoteDto));
        }

        
        [HttpGet("Student/{id}")]
        public async Task<ActionResult<IReadOnlyList<NoteDto>>> GetStudentNotes(int id)
        {
            var spec = new NoteSpecification(id);
            
            var Note = await _unitOfWork.Repository<Note>().ListAsync(spec);

            if (Note == null) return NotFound(new ApiResponse(404));

            var NoteDto = _mapper.Map<IReadOnlyList<Note>, IReadOnlyList<NoteDto>>(Note);

            return Ok(new ApiResponse(200, NoteDto));
        }

        [HttpPost]
        public async Task<ActionResult<NoteDto>> CreateNote([FromForm] NoteCDto NoteCDto)
        {
            var Note = _mapper.Map<NoteCDto, Note>(NoteCDto);

            Note.Student = await _unitOfWork.Repository<Student>().GetByIdAsync(NoteCDto.StudentId);

            _unitOfWork.Repository<Note>().Add(Note);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Creating Note"));

            var NoteDto = _mapper.Map<Note, NoteDto>(Note);

            return Created("test", new ApiResponse(201, NoteDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Note>> UpdateNote(int id, NoteCDto NoteCDto)
        {
            var Note = await _unitOfWork.Repository<Note>().GetByIdAsync(id);

            if (Note == null) return NotFound(new ApiResponse(404));

            _mapper.Map<NoteCDto, Note>(NoteCDto, Note);

            _unitOfWork.Repository<Note>().Update(Note);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Updating Note"));
            
            var NoteDto = _mapper.Map<Note, NoteDto>(Note);

            return Ok(new ApiResponse(200, NoteDto));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNote(int id)
        {
            var Note = await _unitOfWork.Repository<Note>().GetByIdAsync(id);

            if (Note == null) return NotFound(new ApiResponse(404));

            _unitOfWork.Repository<Note>().Delete(Note);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Deleting Note"));

            return NoContent();
        }
    }

}