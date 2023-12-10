using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace Cooking.API.Models
{
    public class CookwareForCreationDto
    {
        [Required(ErrorMessage = "name is required")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "color is required")]
        [EnumDataType(typeof(Color))]
        [JsonConverter(typeof(StringEnumConverter))]
        public Color Color { get; set; }
    }
}
