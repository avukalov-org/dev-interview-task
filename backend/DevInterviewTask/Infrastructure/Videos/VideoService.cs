using AutoMapper;
using DevInterviewTask.Domain.Payments;
using DevInterviewTask.Domain.Videos;


namespace DevInterviewTask.Infrastructure.Videos
{
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public VideoService(IVideoRepository videoRepository, IMapper mapper, IPaymentService paymentService)
        {
            _videoRepository = videoRepository;

            _mapper = mapper;
            _paymentService = paymentService;
        }

        public async Task<Guid> AddAsync(Video video)
        {
            var videoEntity = _mapper.Map<VideoEntity>(video);
            return await _videoRepository.AddAsync(videoEntity);
        }

        public async Task<List<Video>> FindAsync(Guid userId)
        {
            var videoEntities = await _videoRepository.FindAsync();

            var videos = _mapper.Map<List<Video>>(videoEntities);

            var paidVideos = await _paymentService.FindByUserIdAsync(userId);

            var paidVideoIds = new HashSet<Guid>(paidVideos.Select(p => p.VideoId));

            foreach (var video in videos)
            {
                video.isPurchased = paidVideoIds.Contains(video.Id);
            }

            return videos;

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
