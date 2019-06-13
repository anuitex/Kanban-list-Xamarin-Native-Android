using AutoMapper;
using KanbanList.Core.Entities;
using KanbanList.Core.Models;
using KanbanList.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using KanbanList.Core.Repositories.Interfaces;

namespace KanbanList.Core.Services.Implementations
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IMapper _mapper;
        private readonly ISecurityService _securityService;
        private readonly IUserRepository<UserModelEntity> _userRepository;

        public RegistrationService(IMapper mapper, ISecurityService securityService, IUserRepository<UserModelEntity> userRepository)
        {
            _mapper = mapper;
            _securityService = securityService;
            _userRepository = userRepository;
        }

        public async Task<ValidationModelResult> Registrate(UserModelResult user)
        {
            UserModelEntity userEntity = await _userRepository.GetUser(user.Email);
            if (userEntity != null)
            {
                var errorMessage = new Dictionary<string, string>();
                errorMessage.Add("Email", "Email is exist");
                return new ValidationModelResult() { IsValid = false, ErrorMessages = errorMessage };
            }

            user.Password = _securityService.Encrypte(user.Password);

            userEntity = _mapper.Map<UserModelResult, UserModelEntity>(user);

            await _userRepository.Create(userEntity);

            return new ValidationModelResult() { IsValid = true };
        }
    }
}
