using AutoMapper;
using DotNet.CleanArch.Template.Domain.Common.Interfaces;
using DotNet.CleanArch.Template.Domain.Common.Results;
using DotNet.CleanArch.Template.Domain.Entities;
using MediatR;



namespace DotNet.CleanArch.Template.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<int>>
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;
    public CreateUserCommandHandler(IIdentityService identityService, IMapper mapper)
    {
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Map CreateUserCommand to User entity
        var user = _mapper.Map<User>(request);

        var identityResult = await _identityService.CreateUserAsync(user.FirstName, user.LastName, user.Email, request.Password);

        return identityResult.Succeeded
            ? Result<int>.Success(identityResult.Value)
            : Result<int>.Failure(identityResult.Errors);
    }
}
