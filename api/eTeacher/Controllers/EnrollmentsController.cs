using eTeacher.DTOs.Enrollments;
using eTeacher.Services;
using Microsoft.AspNetCore.Mvc;

namespace eTeacher.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EnrollmentReadDto>> GetAll()
        {
            return Ok(_enrollmentService.GetAllEnrollments());
        }

        [HttpPost]
        public ActionResult<EnrollmentReadDto> Enroll([FromBody] EnrollmentCreateDto dto)
        {
            var result = _enrollmentService.EnrollStudent(dto);
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }

        [HttpGet("enrollment-summary")]
        public ActionResult<IEnumerable<EnrollmentReportDto>> GetEnrollmentReport()
        {
            var report = _enrollmentService.GetEnrollmentReport();
            return Ok(report);
        }
    }
}
