using Application.Features.UserOperationClaims.Rules;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Security.Hashing;

namespace Application.Features.Users.Commands.Create;

public class CreateUserCommand : IRequest<CreatedUserResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }

    public CreateUserCommand()
    {
        Email = string.Empty;
        Password = string.Empty;
    }

    public CreateUserCommand( string email, string password)
    {
        Email = email;
        Password = password;
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreatedUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules userBusinessRules, IUserOperationClaimRepository userOperationClaimRepository, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
            _userOperationClaimRepository = userOperationClaimRepository;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
        }

        public async Task<CreatedUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await _userBusinessRules.UserEmailShouldNotExistsWhenInsert(request.Email);
            User user = _mapper.Map<User>(request);

            HashingHelper.CreatePasswordHash(
                request.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            User createdUser = await _userRepository.AddAsync(user);

            // Assign OperationClaimId 84 to the new user by manually invoking the CreateUserOperationClaimCommand logic
            var createUserOperationClaimCommand = new Application.Features.UserOperationClaims.Commands.Create.CreateUserOperationClaimCommand
            {
                UserId = createdUser.Id,
                OperationClaimId = 84
            };

            var createUserOperationClaimCommandHandler = new Application.Features.UserOperationClaims.Commands.Create.CreateUserOperationClaimCommand.CreateUserOperationClaimCommandHandler(
                _userOperationClaimRepository,
                _mapper,
                _userOperationClaimBusinessRules
            );

            await createUserOperationClaimCommandHandler.Handle(createUserOperationClaimCommand, cancellationToken);
            
            CreatedUserResponse response = _mapper.Map<CreatedUserResponse>(createdUser);
            return response;
        }
    }
}
