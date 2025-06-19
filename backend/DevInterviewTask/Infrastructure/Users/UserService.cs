using AutoMapper;
using DevInterviewTask.Domain.Users;

namespace DevInterviewTask.Infrastructure.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(User user)
        {
            var userEntity = _mapper.Map<UserEntity>(user);
            return await _userRepository.AddAsync(userEntity);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var userEntity = await _userRepository.FindByEmailAsync(email);

            return _mapper.Map<User>(userEntity);
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            var userEntity = await _userRepository.FindByIdAsync(id);

            return _mapper.Map<User>(userEntity);
        }
    }
}