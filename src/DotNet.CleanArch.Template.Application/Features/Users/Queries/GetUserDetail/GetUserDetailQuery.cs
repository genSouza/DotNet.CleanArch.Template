using DotNet.CleanArch.Template.Domain.Common.Results;
using MediatR;


namespace DotNet.CleanArch.Template.Application.Features.Users.Queries.GetUserDetail;

public class GetUserDetailQuery: IRequest<Result<UserDetailDto?>>
{
    public int Id { get; set; }
    
}
