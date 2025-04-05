using FluentValidation;
using Hahn.Domain.Entities;

namespace Hahn.Domain.Validators;

public class DriverValidator : AbstractValidator<Driver>
{
    public DriverValidator()
    {
        RuleFor(o => o.DriverId).NotEmpty().WithMessage("Pedágio é obrigatório");
        RuleFor(o => o.Name).NotEmpty().WithMessage("Nome é obrigatório");
        RuleFor(o => o.Surname).NotEmpty().WithMessage("Sobrenome é obrigatório");
        RuleFor(o => o.Nationality).NotEmpty().WithMessage("Nacionalidade é obrigatório");
        RuleFor(o => o.Birthday).NotEmpty().WithMessage("Data de nascimento é obrigatório");
        RuleFor(o => o.Url).NotEmpty().WithMessage("Url é obrigatório");
    }
}
