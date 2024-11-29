using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ZDZCodeAPI.Models
{
    public class Categoria
    {
        public Categoria() 
        {
            Produtos = new Collection<Produto>();
        }

        public int categoriaId { get; set; }

        [StringLength(100), Required]
        public string? Nome { get; set; }

        [StringLength(200), Required]
        public string? Descricao { get;set; }
        public DateTime DataCadastro { get; set; }

        [JsonIgnore]
        public ICollection<Produto>? Produtos { get; set; }

    }
}
