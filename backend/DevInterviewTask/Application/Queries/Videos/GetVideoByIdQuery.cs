using DevInterviewTask.Domain.Videos;
using MediatR;

namespace DevInterviewTask.Application.Queries.Videos
{
    public class GetVideoByIdQuery : IRequest<Video?>
    {
        public GetVideoByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}