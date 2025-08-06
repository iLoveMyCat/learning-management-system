using eTeacher.DTOs.Courses;
using eTeacher.Services.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace eTeacher.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CourseReadDto>> GetAll()
        {
            return Ok(_courseService.GetAllCourses());
        }

        [HttpGet("{id:guid}")]
        public ActionResult<CourseReadDto> GetById(Guid id)
        {
            var course = _courseService.GetCourseById(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        [HttpPost]
        public ActionResult<CourseReadDto> Create([FromBody] CourseCreateDto dto)
        {
            var created = _courseService.CreateCourse(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] CourseUpdateDto dto)
        {
            var updated = _courseService.UpdateCourse(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var deleted = _courseService.DeleteCourse(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
