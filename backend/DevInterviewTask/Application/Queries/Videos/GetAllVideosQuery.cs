using DevInterviewTask.Domain.Videos;
using MediatR;

namespace DevInterviewTask.Application.Queries.Videos
{
    public class GetAllVideosQuery : IRequest<List<Video>>
    {
        // TODO: Add filter parameters

        public Guid UserId { get; set; }

        public GetAllVideosQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}