using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SiteRONA.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Nom de Catègorie")]

        [MaxLength(30,ErrorMessage = "Max 30 characteres")]
        public string Name { get; set; }
        [DisplayName("Ordre d'affichage")]
        [Range(1,100,ErrorMessage ="L'ordre d'affichage doit être compris entre 1 et 100")]
        public int DisplayOrder { get; set; }
    }
}
