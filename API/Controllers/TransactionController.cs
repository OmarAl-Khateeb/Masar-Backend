using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TransactionController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        private readonly UserManager<AppUser> _userManager;

        public TransactionController(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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
            var pageData = new Pagination<TransactionDto>(transactionParams.PageIndex,
                transactionParams.PageSize, totalItems, data);

            return Ok(new ApiResponse(200, pageData));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDto>> GetTransaction(int id)
        {
            var transaction = await _unitOfWork.Repository<Transaction>().GetByIdAsync(id);

            if (transaction == null) return NotFound(new ApiResponse(404));

            var TransactionDto = _mapper.Map<Transaction, TransactionDto>(transaction);

            return Ok(new ApiResponse(200, TransactionDto));
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
            
            var TransactionDto = _mapper.Map<Transaction, TransactionDto>(transaction);

            return Created( "test", new ApiResponse(201, TransactionDto));
        }
        
        // [Authorize]
        // [HttpGet("User")]
        // public async Task<ActionResult<TransactionDto>> GetTransactionByUser()
        // {
        //     var user = await _userManager.FindUserByClaimsId(User);
        //     var spec1 = new BaseSpecification<Student>(x=> x.AppUserId == user.Id);

        //     var student = await _unitOfWork.Repository<Student>().GetEntityWithSpec(spec1);
        //     var spec2 = new BaseSpecification<Transaction>(x => x.StudentId == student.Id);

        //     var transaction = await _unitOfWork.Repository<Transaction>().GetEntityWithSpec(spec2);

        //     if (transaction == null) return NotFound(new ApiResponse(404));

        //     var TransactionDto = _mapper.Map<Transaction, TransactionDto>(transaction);

        //     return Ok(new ApiResponse(200, TransactionDto));
        // }

        [Authorize]
        [HttpGet("User")]
        public async Task<ActionResult<Pagination<TransactionDto>>> GetUserTransactions(
            [FromQuery] TransactionSpecParams transactionParams)
        {
            var user = await _userManager.FindUserByClaimsId(User);
            var spec1 = new BaseSpecification<Student>(x=> x.AppUser.Id == user.Id);
            var student = await _unitOfWork.Repository<Student>().GetEntityWithSpec(spec1);

            var spec = new TransactionSpecification(transactionParams, student.Id);
            var countSpec = new TransactionSpecification(transactionParams, student.Id);

            var totalItems = await _unitOfWork.Repository<Transaction>().CountAsync(countSpec);
            var transactions = await _unitOfWork.Repository<Transaction>().ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<TransactionDto>>(transactions);
            var pageData = new Pagination<TransactionDto>(transactionParams.PageIndex,
                transactionParams.PageSize, totalItems, data);

            return Ok(new ApiResponse(200, pageData));
        }
        
        [Authorize]
        [HttpPost("User")]
        public async Task<ActionResult<TransactionDto>> CreateUserTransaction([FromForm] TransactionCUDto transactionCDto)
        {
            var transaction = _mapper.Map<TransactionCUDto, Transaction>(transactionCDto);
            
            var user = await _userManager.FindUserByClaimsId(User);
            var spec = new BaseSpecification<Student>(x=> x.AppUser.Id == user.Id);

            transaction.Student = await _unitOfWork.Repository<Student>().GetEntityWithSpec(spec);
            //add student id to the post request
            if (transactionCDto.File != null) transaction.Document = await _imageService.UploadDocumentAsync(transactionCDto.File, "students/documents");

            _unitOfWork.Repository<Transaction>().Add(transaction);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Creating Transaction"));
            
            var TransactionDto = _mapper.Map<Transaction, TransactionDto>(transaction);

            return Created( "test", new ApiResponse(201, TransactionDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TransactionDto>> UpdateTransaction(int id,[FromForm] TransactionUDto transactionUDto )
        {
            var transaction = await _unitOfWork.Repository<Transaction>().GetByIdAsync(id);

            if (transaction == null) return NotFound(new ApiResponse(404));

            if (transactionUDto.File != null) transaction.Document = await _imageService.UploadDocumentAsync(transactionUDto.File, "students/documents");

            _mapper.Map<TransactionUDto, Transaction>(transactionUDto, transaction);

            _unitOfWork.Repository<Transaction>().Update(transaction);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Updating Transaction"));
            
            var TransactionDto = _mapper.Map<Transaction, TransactionDto>(transaction);

            return Ok(new ApiResponse(200, TransactionDto));
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

            var transactiontypedto = _mapper.Map<IReadOnlyList<TransactionType>, IReadOnlyList<TransactionTypeDto>>(transactiontype);

            return Ok(new ApiResponse(200, transactiontypedto));
        }

        [HttpPost("Types")]
        public async Task<ActionResult<TransactionTypeDto>> CreateTransactionType(TransactionTypeCDto transactionTypeCDto)
        {
            var transactionType = _mapper.Map<TransactionTypeCDto, TransactionType>(transactionTypeCDto);

            _unitOfWork.Repository<TransactionType>().Add(transactionType);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Creating Transaction"));

            var transactionTypeDto =  _mapper.Map<TransactionType, TransactionTypeDto>(transactionType);

            return Created("test", new ApiResponse(201, transactionTypeDto));
        }

        [HttpDelete("Types/{id}")]
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