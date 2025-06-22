using AutoMapper;
using DotNet.CleanArch.Template.Application.Features.Users.Commands.CreateUser;
using DotNet.CleanArch.Template.Application.Features.Users.Queries.GetUserDetail;
using DotNet.CleanArch.Template.Domain.Entities;


namespace DotNet.CleanArch.Template.Application.Features.Users.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserCommand, User>();
        CreateMap<User, UserDetailDto>();
    }

}
