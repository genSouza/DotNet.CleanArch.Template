using AutoMapper;
using DotNet.CleanArch.Template.Domain.Common.Repositories;
using DotNet.CleanArch.Template.Domain.Common.Results;
using MediatR;


namespace DotNet.CleanArch.Template.Application.Features.Users.Queries.GetUserDetail;

public class GetUserDetailHandler : IRequestHandler<GetUserDetailQuery, Result<UserDetailDto?>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserDetailHandler(IUserRepository userRepository, IMapper mapper)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    }

    public async Task<Result<UserDetailDto?>> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0) return Result<UserDetailDto?>.Failure("Invalid user ID.");

        var user = await _userRepository.GetByIdAsync(request.Id);

        return user != null
            ? Result<UserDetailDto?>.Success(_mapper.Map<UserDetailDto>(user))
            : Result<UserDetailDto?>.Failure("User not found");
    }
}

