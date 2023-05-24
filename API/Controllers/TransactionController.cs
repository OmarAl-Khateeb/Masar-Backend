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
    public class TransactionController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;

        public TransactionController(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
        {
            _imageService = imageService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<TransactionDto>>> GetTransactions(
            [FromQuery] TransactionSpecParams transactionParams)
        {
            var spec = new TransactionSpecification(transactionParams);
            var countSpec = new TransactionSpecification(transactionParams);

            var totalItems = await _unitOfWork.Repository<Transaction>().CountAsync(countSpec);
            var transactions = await _unitOfWork.Repository<Transaction>().ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<TransactionDto>>(transactions);

            return Ok(new Pagination<TransactionDto>(transactionParams.PageIndex,
                transactionParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDto>> GetTransaction(int id)
        {
            var transaction = await _unitOfWork.Repository<Transaction>().GetByIdAsync(id);

            if (transaction == null) return NotFound(new ApiResponse(404));

            return Ok(new ApiResponse(200, _mapper.Map<Transaction, TransactionDto>(transaction)));
        }

        [HttpPost]
        public async Task<ActionResult<TransactionDto>> CreateTransaction([FromForm] TransactionCDto transactionCDto)
        {
            var transaction = _mapper.Map<TransactionCDto, Transaction>(transactionCDto);

            transaction.Student = await _unitOfWork.Repository<Student>().GetByIdAsync(transactionCDto.StudentId);
            //add student id to the post request
            if (transactionCDto.File != null) transaction.Document = await _imageService.UploadDocumentAsync(transactionCDto.File, "students/documents");

            _unitOfWork.Repository<Transaction>().Add(transaction);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Creating Transaction"));

            return Created( "test", new ApiResponse(201,  _mapper.Map<Transaction, TransactionDto>(transaction)));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TransactionDto>> UpdateTransaction(int id, TransactionCDto transactionCDto)
        {
            var transaction = await _unitOfWork.Repository<Transaction>().GetByIdAsync(id);

            if (transaction == null) return NotFound(new ApiResponse(404));

            _mapper.Map<TransactionCDto, Transaction>(transactionCDto, transaction);

            _unitOfWork.Repository<Transaction>().Update(transaction);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Updating Transaction"));

            return Ok(new ApiResponse(200, _mapper.Map<Transaction, TransactionDto>(transaction)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTransaction(int id)
        {
            var transaction = await _unitOfWork.Repository<Transaction>().GetByIdAsync(id);

            if (transaction == null) return NotFound(new ApiResponse(404));

            _unitOfWork.Repository<Transaction>().Delete(transaction);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Deleting Transaction"));

            return NoContent();
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<TransactionTypeDto>>> GetransactionTypes()
        {
            var transactiontype = await _unitOfWork.Repository<TransactionType>().ListAllAsync();
            return Ok(_mapper.Map<IReadOnlyList<TransactionType>, IReadOnlyList<TransactionTypeDto>>(transactiontype));
        }

        [HttpPost("Types")]
        public async Task<ActionResult<TransactionTypeDto>> CreateTransactionType(TransactionTypeCDto transactionTypeCDto)
        {
            var transactionType = _mapper.Map<TransactionTypeCDto, TransactionType>(transactionTypeCDto);

            _unitOfWork.Repository<TransactionType>().Add(transactionType);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Creating Transaction"));

            return Created("test", new ApiResponse(201,  _mapper.Map<TransactionType, TransactionTypeDto>(transactionType)));
        }

        [HttpDelete("Types{id}")]
        public async Task<ActionResult> DeleteTypeTransaction(int id)
        {
            var transactionType = await _unitOfWork.Repository<TransactionType>().GetByIdAsync(id);

            if (transactionType == null) return NotFound(new ApiResponse(404));

            _unitOfWork.Repository<TransactionType>().Delete(transactionType);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Deleting Transaction"));

            return NoContent();
        }
    }
}