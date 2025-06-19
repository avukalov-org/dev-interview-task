using DevInterviewTask.Domain.Videos;
using MediatR;

namespace DevInterviewTask.Application.Queries.Videos
{
    public class GetUserVideosQuery : IRequest<List<Video>>
    {
        public GetUserVideosQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}