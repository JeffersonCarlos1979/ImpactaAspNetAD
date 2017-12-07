using Loja.Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loja.Mvc.Models
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }

        [Required]
        public String Nome { get; set; }
        [Display(Name ="Categoria")]
        public String CategoriaNome { get; set; }
        [Required]
        [Display(Name ="Preço")]
        public Decimal Preco { get; set; }
        [Required]
        public int Estoque { get; set; }
        public bool Ativo { get; set; }
        [Display(Name ="Categoria")]
        public int? CategoriaId { get; set; }
        public List<SelectListItem> Categorias { get; set; } = new List<SelectListItem>();
        public HttpPostedFileBase imagem { get; set; }
    }
}