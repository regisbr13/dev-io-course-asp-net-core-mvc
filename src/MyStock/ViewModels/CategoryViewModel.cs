using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyStock.ViewModels
{
    public class CategoryViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "o campo é obrigatório")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "o campo precisa ter entre {2} e {1} caracteres")]
        public string Name { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}