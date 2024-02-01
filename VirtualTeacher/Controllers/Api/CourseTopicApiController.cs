using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Models;
using VirtualTeacher.Services;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Data.Exceptions;
using VirtualTeacher.Models.DTO;

namespace VirtualTeacher.Controllers.Api
{
    [ApiController]
    [Route("api/courseTopics")]
    public class CourseTopicApiController : ControllerBase
    {
        private readonly ICourseTopicService courseTopicService;
        public CourseTopicApiController(ICourseTopicService courseTopicService)
        {
            this.courseTopicService = courseTopicService;
        }

        [HttpGet("{id}")]
        public ActionResult<CourseTopicDto> GetCourseTopicById(int id) 
        {
            try
            {
                return Ok(courseTopicService.GetCourseTopicById(id));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        } 
        // POST
        [HttpPost]
        public ActionResult<CourseTopicDto> CreateCourseTopic([FromBody] CourseTopicCreationRequest topic)
            // FromQuery has to be tested
        {
            try
            {
                var result = topic.Topic;
                return Ok(courseTopicService.CreateCourseTopic(result));
            }
            catch (DuplicateEntityException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
