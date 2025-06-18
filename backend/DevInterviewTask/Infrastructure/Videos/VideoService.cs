using AutoMapper;
using DevInterviewTask.Domain.Videos;


namespace DevInterviewTask.Infrastructure.Videos
{
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IMapper _mapper;

        public VideoService(IVideoRepository videoRepository, IMapper mapper)
        {
            _videoRepository = videoRepository;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(Video video)
        {
            var videoEntity = _mapper.Map<VideoEntity>(video);
            return await _videoRepository.AddAsync(videoEntity);
        }

        public async Task<List<Video>> FindAsync()
        {
            var videoEntities = await _videoRepository.FindAsync();

            return _mapper.Map<List<Video>>(videoEntities);
        }


        public async Task<Video?> FindByIdAsync(Guid id)
        {
            var videoEntity = await _videoRepository.FindByIdAsync(id);

            return _mapper.Map<Video>(videoEntity);
        }

        public async Task<List<Video>> FindByUserIdAsync(Guid userId)
        {
            var videoEntity = await _videoRepository.FindByUserIdAsync(userId);

            return _mapper.Map<List<Video>>(videoEntity);
        }

        public async Task UpadateAsync(Video video)
        {
            var newEntity = _mapper.Map<VideoEntity>(video);

            var oldEntity = await _videoRepository.FindByIdAsync(video.Id);
            
            _mapper.Map(newEntity, oldEntity);

            await _videoRepository.UpdateAsync(oldEntity!);
        }


        public async Task DeleteAsync(Guid id)
        {
            await _videoRepository.DeleteAsync(id);
        }
    }
}
