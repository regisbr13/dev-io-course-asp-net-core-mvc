using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyStock.ViewModels
{
    public class ProviderViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "o campo é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "o campo precisa ter entre {2} e {1} caracteres")]
        public string Name { get; set; }

        [Display(Name = "CPF/CNPJ")]
        [Required(ErrorMessage = "o campo é obrigatório")]
        public string DocumentNumber { get; set; }

        [Display(Name = "Tipo")]
        public int ProviderType { get; set; }

        [Display(Name = "Ativo?")]
        public bool Active { get; set; }
        public AddressViewModel Address { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}