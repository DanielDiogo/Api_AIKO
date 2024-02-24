using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api_AIKO.Models
{
    public class Vehicle
    {
        [Key]
        [DisplayName("Id")]
        public long Id { get; set; }

        [Required(ErrorMessage = "O campo Name é obrigatório.")]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Model é obrigatório.")]
        [DisplayName("Modelo")]
        public string Model { get; set; }

        [Required(ErrorMessage = "A linha é obrigatória.")]
        [DisplayName("Id da Linha")]
        public long LineId { get; set; }
    }
}
