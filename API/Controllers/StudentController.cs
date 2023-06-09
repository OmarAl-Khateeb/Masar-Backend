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
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Core.Entities._Enums;

namespace API.Controllers
{
    public class StudentController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        private readonly UserManager<AppUser> _userManager;
        public StudentController(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _imageService = imageService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
        }
        

        [HttpGet]
        public async Task<ActionResult<Pagination<StudentDto>>> GetStudents(
            [FromQuery] StudentSpecParams studentParams)
        {
            var spec = new StudentSpecification(studentParams);
            var countSpec = new StudentSpecification(studentParams);

            var totalItems = await _unitOfWork.Repository<Student>().CountAsync(countSpec);
            var students = await _unitOfWork.Repository<Student>().ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<StudentDto>>(students);
            var pageData = new Pagination<StudentDto>(studentParams.PageIndex,
                studentParams.PageSize, totalItems, data);

            return Ok(new ApiResponse(200, pageData));
        }
        

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudent(int id)
        {
            var student = await _unitOfWork.Repository<Student>().GetByIdAsync(id);

            if (student == null) return NotFound(new ApiResponse(404));

            return  Ok(new ApiResponse(200, _mapper.Map<Student, StudentDto>(student)));
        }
        
        [Authorize]
        [HttpGet("User")]
        public async Task<ActionResult<StudentDto>> GetStudentByUser()
        {
            var user = await _userManager.FindUserByClaimsId(User);
            var spec1 = new BaseSpecification<Student>(x=> x.AppUser.Id == user.Id);
            
            var student = await _unitOfWork.Repository<Student>().GetEntityWithSpec(spec1);

            if (student == null) return NotFound(new ApiResponse(404));

            return  Ok(new ApiResponse(200, _mapper.Map<Student, StudentDto>(student)));
        }
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent([FromForm] StudentCDto studentCDto)
        {

            var student = _mapper.Map<StudentCDto, Student>(studentCDto);

            student.StudentStatus = StudentStatuses.Pending;
            
            if (studentCDto.Photo == null) return BadRequest(new ApiResponse(400, "Missing Student Photo"));

            var uploadFile = await _imageService.AddImageAsync(studentCDto.Photo, "students/photos");
            student.StudentPhotoUrl = uploadFile.SecureUrl.AbsoluteUri;
            
            if (studentCDto.IdCard == null) return BadRequest(new ApiResponse(400, "Missing Student Id"));

            var IdCard = await _imageService.UploadDocumentAsync(studentCDto.IdCard, "students/documents");
            IdCard.DocumentType="IDCard";
            
            if (studentCDto.AddressCard == null) return BadRequest(new ApiResponse(400, "Missing Student AddressCard"));

            var AddressCard = await _imageService.UploadDocumentAsync(studentCDto.AddressCard, "students/documents");
            AddressCard.DocumentType="AddressCard";
            
            if (studentCDto.RationCard == null) return BadRequest(new ApiResponse(400, "Missing Student AddressCard"));

            var RationCard = await _imageService.UploadDocumentAsync(studentCDto.RationCard, "students/documents");
            RationCard.DocumentType="RationCard";

            if (student.AdmissionType == (AdmissionTypes.Direct)) student.IsEvening = true;// evening student check

            _unitOfWork.Repository<Student>().Add(student);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Creating Student"));

            return Created("test", new ApiResponse(200, student));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id, StudentCDto studentCDto)
        {
            var student = await _unitOfWork.Repository<Student>().GetByIdAsync(id);

            if (student == null) return NotFound(new ApiResponse(404));

            _mapper.Map<StudentCDto, Student>(studentCDto, student);

            _unitOfWork.Repository<Student>().Update(student);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Updating Student"));

            return Ok(new ApiResponse(200, student));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var student = await _unitOfWork.Repository<Student>().GetByIdAsync(id);

            if (student == null) return NotFound(new ApiResponse(404));

            _unitOfWork.Repository<Student>().Delete(student);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Deleting Student"));

            return NoContent();
        }

        // [HttpPost("Upload/{id}")]
        // public async Task<ActionResult> Upload(IFormFile file, int id)
        // {
        //     var student = await _unitOfWork.Repository<Student>().GetByIdAsync(id);

        //     if (student == null) return NotFound(new ApiResponse(404));

        //     var uploadFile = await _uploadService.UploadAsync(file, "images/students/photos");

        //     student.StudentPhotoUrl = "images/students/photos/" + uploadFile.FileName;

        //     _unitOfWork.Repository<Student>().Update(student);

        //     var result = await _unitOfWork.Complete();

        //     if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Updating Product"));
            
        //     return Ok(new { uploadFile.FileName });
        // }
    }
}