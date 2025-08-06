using eTeacher.DTOs.Students;
using eTeacher.Services;
using eTeacher.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eTeacher.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        //Downloads a mocked students file from S3 and preloads it into the in-memory repository
        public record PreloadRequest(string Key);
        [HttpPost("preload-from-s3")]
        public async Task<IActionResult> PreloadFromS3([FromBody] PreloadRequest req, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(req.Key)) return BadRequest("Key is required.");
            var count = await (_studentService as StudentService)!.PreloadFromS3Async(req.Key, ct);
            return Ok(new { imported = count, key = req.Key });
        }

        [HttpGet]
        public ActionResult<IEnumerable<StudentReadDto>> GetAll()
        {
            return Ok(_studentService.GetAllStudents());
        }

        [HttpGet("{id}")]
        public ActionResult<StudentReadDto> GetById(Guid id)
        {
            var student = _studentService.GetStudentById(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public ActionResult<StudentReadDto> Create([FromBody] StudentCreateDto dto)
        {
            var created = _studentService.CreateStudent(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        [HttpPut("{id}")]
        public ActionResult Update(Guid id, [FromBody] StudentUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest("ID mismatch");

            var success = _studentService.UpdateStudent(dto);
            if (!success) return NotFound();

            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var success = _studentService.DeleteStudent(id);
            if (!success) return NotFound();

            return NoContent(); 
        }
    }
}
