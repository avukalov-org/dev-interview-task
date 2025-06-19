using DevInterviewTask.Application.Commands.Videos;
using DevInterviewTask.Application.Queries.Videos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DevInterviewTask.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly IMediator _mediator;


        public VideosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<VideosController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var videos = await _mediator.Send(new GetAllVideosQuery(Guid.Parse(userId!)));

            return Ok(videos);
        }

        // GET api/<VideosController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var video = await _mediator.Send(new GetVideoByIdQuery(id));

            if (video is null)
            {
                return NotFound();
            }

            return Ok(video);
        }

        // GET api/<VideosController>/5
        [HttpGet("user")]
        public async Task<IActionResult> GetByUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var videos = await _mediator.Send(new GetUserVideosQuery(Guid.Parse(userId!)));
            
            return Ok(videos);
        }

        // POST api/<VideosController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddVideoCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            command.UserId = Guid.Parse(userId!);
            var videoId = await _mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { id = userId }, command);
        }

        // PUT api/<VideosController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateVideoCommand command)
        {
            await _mediator.Send(command);
            
            return NoContent();
        }

        // DELETE api/<VideosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteVideoCommand(id));

            return NoContent();
        }
    }
}
