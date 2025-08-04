using System;
using System.Collections.Generic;
using eTeacher.DTOs.Courses;
using eTeacher.Models;
using eTeacher.Repositories;
using eTeacher.Services;
using Moq;
using Xunit;

namespace eTeacher.Tests.Services
{
    public class CourseServiceTests
    {
        private readonly Mock<ICourseRepository> _mockRepo;
        private readonly CourseService _service;

        public CourseServiceTests()
        {
            _mockRepo = new Mock<ICourseRepository>();
            _service = new CourseService(_mockRepo.Object);
        }

        [Fact]
        public void CreateCourse_ShouldAddCourse_AndReturnCorrectDto()
        {
            // Arrange
            var createDto = new CourseCreateDto
            {
                Title = "Math 101",
                Description = "Intro to Algebra"
            };

            _mockRepo.Setup(r => r.Add(It.IsAny<Course>())).Returns(true);

            // Act
            var result = _service.CreateCourse(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Math 101", result.Title);
            Assert.Equal("Intro to Algebra", result.Description);
            //Assert.Equal("Wrong Title", result.Title);  // fail

            _mockRepo.Verify(r => r.Add(It.Is<Course>(c => c.Title == "Math 101" && c.Description == "Intro to Algebra")), Times.Once);
        }
    }
}
