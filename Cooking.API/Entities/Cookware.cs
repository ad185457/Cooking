using Cooking.API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking.API.Entities
{
    public class Cookware
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [EnumDataType(typeof(Color))]
        public string Color { get; set; }

        public Cookware(string name, string color)
        {
            Name = name;
            Color = color;
        }
    }

    
}
