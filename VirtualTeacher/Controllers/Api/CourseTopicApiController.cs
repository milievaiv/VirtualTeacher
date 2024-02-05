using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Attributes;
using VirtualTeacher.Data.Exceptions;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Models.DTO;
using VirtualTeacher.Models;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Constants;

namespace VirtualTeacher.Controllers.Api
{
    [ApiController]
    [Route("api/course-topics")]
    public class CourseTopicController : ControllerBase
    {
        private readonly ICourseTopicService courseTopicService;

        public CourseTopicController(ICourseTopicService courseTopicService)
        {
            this.courseTopicService = courseTopicService;
        }

        //GET: api/course-topics/id
        [AuthorizeApiUsers("teacher, admin")]
        [HttpGet("{id}")]
        public IActionResult GetCourseTopicById(int id)
        {
            try
            {
                return Ok(courseTopicService.GetById(id));
            }
            catch (UnauthorizedOperationException ex)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        //POST: api/course-topics
        [AuthorizeApiUsers("teacher")]
        [HttpPost("")]
        public IActionResult CreateCourseTopic([FromBody] CourseTopicCreationRequest topic)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(topic?.Topic))
                {
                    return this.StatusCode(StatusCodes.Status400BadRequest, Messages.TopicNullOrEmptyMessage);
                }

                var courseTopic = new CourseTopic { Topic = topic.Topic };
                var createdCourseTopic = courseTopicService.Create(courseTopic);

                var responseDto = new CourseTopicDto
                {
                    Topic = courseTopic.Topic
                };

                return Ok(responseDto);
            }
            catch (UnauthorizedOperationException ex)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        //DELETE: api/course-topics/id
        [AuthorizeApiUsers("teacher")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCourseTopic(int id)
        {
            try
            {
                var deletedCourseTopic = courseTopicService.Delete(id);
                return Ok(deletedCourseTopic);
            }
            catch (UnauthorizedOperationException ex)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }

}
