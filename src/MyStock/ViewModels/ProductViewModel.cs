using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MyStock.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "o campo é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "o campo precisa ter entre {2} e {1} caracteres")]
        public string Name { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "o campo é obrigatório")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "o campo precisa ter entre {2} e {1} caracteres")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Imagem")]
        public string Image { get; set; }

        [Display(Name = "Escolha uma imagem")]
        public IFormFile ImageUpload { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "o campo é obrigatório")]
        [DataType(DataType.Currency)]
        [Range(1.0, 1000000.0, ErrorMessage = "não pode ser nulo")]
        public decimal Value { get; set; }

        [Display(Name = "Data de cadastro")]
        public DateTime Register { get; set; }

        [Display(Name = "Ativo?")]
        public bool Active { get; set; }

        [Display(Name = "Fornecedor")]
        [Required(ErrorMessage = "faça o cadastro de pelo menos um fornecedor")]
        public Guid ProviderId { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "faça o cadastro de pelo menos uma categoria")]
        public Guid CategoryId { get; set; }
        public ProviderViewModel Provider { get; set; }
        public CategoryViewModel Category { get; set; }
        public List<ProviderViewModel> Providers { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }
}