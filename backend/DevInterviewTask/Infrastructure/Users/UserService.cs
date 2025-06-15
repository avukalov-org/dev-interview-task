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

        public async Task<Guid> Create(User user)
        {
            var userEntity = _mapper.Map<UserEntity>(user);
            return await _userRepository.Create(userEntity);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var userEntity = await _userRepository.FindByEmail(email);

            return _mapper.Map<User>(userEntity);
        }

        public async Task<User?> GetUserById(Guid id)
        {
            var userEntity = await _userRepository.FindById(id);

            return _mapper.Map<User>(userEntity);
        }
    }
}
