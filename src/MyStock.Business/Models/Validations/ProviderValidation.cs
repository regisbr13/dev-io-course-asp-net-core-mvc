using FluentValidation;
using MyStock.Business.Models.Enums;
using MyStock.Business.Models.Validations.Docs;

namespace MyStock.Business.Models.Validations
{
    public class ProviderValidation : AbstractValidator<Provider>
    {
        public ProviderValidation()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio")
                .Length(3, 100).WithMessage("O campo precisa ter entre {MinLength} e {MaxLength} caracteres");

            When(p => p.ProviderType == ProviderType.LegalPerson, () => {
                RuleFor(p => p.DocumentNumber.Length).Equal(CnpjValidation.TamanhoCnpj).WithMessage("O campo precisa ter {ComparisonValue} caracteres");
                RuleFor(p => CnpjValidation.Validar(p.DocumentNumber)).Equal(true).WithMessage("O documento fornecido é inválido");
            });
            When(p => p.ProviderType == ProviderType.PhysicalPerson, () => {
                RuleFor(p => p.DocumentNumber.Length).Equal(CpfValidation.TamanhoCpf).WithMessage("O campo precisa ter {ComparisonValue} caracteres");
                RuleFor(p => CpfValidation.Validar(p.DocumentNumber)).Equal(true).WithMessage("O documento fornecido é inválido");
            });
        }
    }
}