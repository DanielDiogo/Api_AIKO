using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api_AIKO.Models
{
    public class Line
    {
        [Key]
        [DisplayName("Id")]
        public long Id { get; set; }

        [Required(ErrorMessage = "O campo Name é obrigatório.")]
        [DisplayName("Nome")]
        public string Name { get; set; }

        // Adicionando validação para garantir que haja pelo menos uma parada
        [Required(ErrorMessage = "É necessário fornecer pelo menos uma parada.")]
        public List<Stop> Stops { get; set; }
    }
}
