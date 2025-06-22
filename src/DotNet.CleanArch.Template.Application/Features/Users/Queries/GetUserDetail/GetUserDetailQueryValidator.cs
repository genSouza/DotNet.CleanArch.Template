using FluentValidation;


namespace DotNet.CleanArch.Template.Application.Features.Users.Queries.GetUserDetail;

public class GetUserDetailQueryValidator : AbstractValidator<GetUserDetailQuery>
{
    public GetUserDetailQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("User Id must be greater than zero.");
    }
}

