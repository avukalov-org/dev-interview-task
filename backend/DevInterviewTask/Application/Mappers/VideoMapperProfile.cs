using AutoMapper;
using DevInterviewTask.Application.Commands.Videos;
using DevInterviewTask.Domain.Videos;

namespace DevInterviewTask.Application.Mappers
{
    public class VideoMapperProfile : Profile
    {
        public VideoMapperProfile()
        {
            CreateMap<AddVideoCommand, Video>();

            CreateMap<UpdateVideoCommand, Video>().ReverseMap();
            CreateMap<Video, VideoEntity>().ReverseMap();

            CreateMap<VideoEntity, VideoEntity>();
        }
    }
}