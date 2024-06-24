using Domain.ValueObjects;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    [SwaggerSchemaFilter(typeof(ClienteSchemaFilter))]
    public class Cliente
    {
        [JsonProperty("idCliente")]
        public int IdCliente { get; set; }

        [JsonProperty("nome")]
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        [RegularExpression(@"^[a-zA-ZÀ-ú]+", ErrorMessage = "O nome deve conter apenas letras e somente o primeiro nome")]
        public string? Nome { get; set; }

        [JsonProperty("sobrenome")]
        [Required(ErrorMessage = "O sobrenome é obrigatório")]
        [StringLength(50, ErrorMessage = "O sobrenome deve ter no máximo 50 caracteres")]
        public string? Sobrenome { get; set; }

        [JsonProperty("cpf")]
        [Required(ErrorMessage = "O CPF é obrigatório")]
        [CPF(ErrorMessage = "O CPF informado é inválido.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "O CPF deve conter somente números")]
        public string? CPF { get; set; }

        [Required]
        [JsonProperty("email")]
        [EmailAddress(ErrorMessage = "O e-mail informado é inválido")]
        public string? Email { get; set; }

        [JsonProperty("endereco")]
        public string? Endereco { get; set; }

        [JsonProperty("numeroTelefone")]
        public string? NumeroTelefone { get; set; }

        [JsonProperty("ativo")]
        public bool Ativo { get; set; } = true; // Default é true para cliente ativo

        [JsonProperty("dataInativacao")]
        public DateTime? DataInativacao { get; set; }
    }
}
