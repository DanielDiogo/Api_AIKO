using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api_AIKO.Models
{
    public class Stop
    {
        [Key]
        [DisplayName("Id")]
        public long Id { get; set; }

        [Required(ErrorMessage = "O campo Name é obrigatório.")]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A latitude é obrigatória.")]
        [Range(-90, 90, ErrorMessage = "A latitude deve estar entre -90 e 90.")]
        [DisplayName("Latitude")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "A longitude é obrigatória.")]
        [Range(-180, 180, ErrorMessage = "A longitude deve estar entre -180 e 180.")]
        [DisplayName("Longitude")]
        public double Longitude { get; set; }
    }
}
