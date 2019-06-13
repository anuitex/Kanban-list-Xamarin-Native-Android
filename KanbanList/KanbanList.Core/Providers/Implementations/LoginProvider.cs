using AutoMapper;
using KanbanList.Core.Entities;
using KanbanList.Core.Models;
using KanbanList.Core.Providers.Interfaces;
using KanbanList.Core.Services.Interfaces;
using System.Threading.Tasks;
using KanbanList.Core.Repositories.Interfaces;

namespace KanbanList.Core.Providers.Implementations
{
    public class LoginProvider : ILoginProvider
    {
        private readonly IMapper _mapper;
        private readonly ISecurityService _securityService;
        private readonly IUserRepository<UserModelEntity> _userRepository;

        public LoginProvider(IMapper mapper, ISecurityService securityService, IUserRepository<UserModelEntity> userRepository)
        {
            _mapper = mapper;
            _securityService = securityService;
            _userRepository = userRepository;
        }

        public async Task<UserModelResult> Login(LoginModelResult loginModelResult)
        {
            var hashPassword = _securityService.Encrypte(loginModelResult.Password);
            UserModelEntity userEntity = await _userRepository.GetUser(loginModelResult.Username, hashPassword);
            UserModelResult user = _mapper.Map<UserModelEntity, UserModelResult>(userEntity);
            return user;
        }

    }
}
