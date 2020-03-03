using System;
using System.ComponentModel.DataAnnotations;

namespace MyStock.ViewModels
{
    public class AddressViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Logradouro")]
        [Required(ErrorMessage = "o campo é obrigatório")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "o campo precisa ter entre {2} e {1} caracteres")]
        public string Street { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "o campo é obrigatório")]
        public int Number { get; set; }

        [Display(Name = "Complemento")]
        [Required(ErrorMessage = "o campo é obrigatório")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "o campo precisa ter entre {2} e {1} caracteres")]
        public string Complement { get; set; }

        [Display(Name = "Cep")]
        [Required(ErrorMessage = "o campo é obrigatório")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "o campo precisa ter entre {2} e {1} caracteres")]
        public string Cep { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "o campo é obrigatório")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "o campo precisa ter entre {2} e {1} caracteres")]
        public string District { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "o campo é obrigatório")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "o campo precisa ter entre {2} e {1} caracteres")]
        public string City { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "o campo é obrigatório")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "o campo precisa ter {2} caracteres")]
        public string State { get; set; }
        public Guid ProviderId { get; set; }
    }
}