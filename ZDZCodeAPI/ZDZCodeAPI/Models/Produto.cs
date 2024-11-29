using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ZDZCodeAPI.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }

        [StringLength(100), Required]
        public string? Nome { get; set; }

        [StringLength(200), Required]
        public string? Descricao { get; set; }

        [StringLength(500), Required]
        public string? ImagemUrl { get; set; }
        public bool Estoque { get; set; }
        public int Quantidade { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }

        public DateTime DataCadastro { get; set; }
        public int CategoriaId { get; set; }

        [JsonIgnore]
        public Categoria? Categoria { get; set; }

    }
}
