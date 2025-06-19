using AutoMapper;
using DevInterviewTask.Application.Commands.Videos;
using DevInterviewTask.Application.Queries.Videos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DevInterviewTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhooksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public WebhooksController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("mux")]
        public async Task<IActionResult> GetMuxWebhook([FromBody] JsonElement payload)
        {
            var type = payload.GetProperty("type").GetString();

            switch (type)
            {
                case "video.asset.ready":
                    await VideoAssetReady(payload);
                    break;

                case "video.asset.deleted":
                    await VideoAssetDeleted(payload);
                    break;

                default:
                    break;
            }

            return Ok();
        }

        private async Task VideoAssetDeleted(JsonElement payload)
        {
            var externalId = payload.GetProperty("data")
                                .GetProperty("meta")
                                .GetProperty("external_id")
                                .GetGuid();
            
            await _mediator.Send(new DeleteVideoCommand(externalId));
        }

        private async Task VideoAssetReady(JsonElement payload)
        {
            var externalId = payload.GetProperty("data")
                                .GetProperty("meta")
                                .GetProperty("external_id")
                                .GetGuid();

            var video = await _mediator.Send(new GetVideoByIdQuery(externalId));

            if (video is not null && string.Equals(video!.Status, "Ready"))
            {
                return;
            }

            var uploadId = payload.GetProperty("data")
                                .GetProperty("upload_id")
                                .GetString();

            var assetId = payload.GetProperty("object")
                                .GetProperty("id")
                                .GetString();

            var playbackId = payload.GetProperty("data")
                                .GetProperty("playback_ids")[0]
                                .GetProperty("id")
                                .GetString();

            if (video is not null)
            {
                var command = _mapper.Map<UpdateVideoCommand>(video);

                command.UploadId = uploadId;
                command.AssetId = assetId;
                command.PlaybackId = playbackId;
                command.Status = "Ready";

                await _mediator.Send(command);
            }
        }
    }
}
