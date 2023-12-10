using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace Cooking.API.Models
{
    public class CookwareForUpdateDto
    {
        [Required(ErrorMessage = "name is required")]
        [MaxLength(50)]
        public string Name { get; set; }
        [EnumDataType(typeof(Color))]
        [Required(ErrorMessage = "color is required")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Color Color { get; set; }
    }
}
