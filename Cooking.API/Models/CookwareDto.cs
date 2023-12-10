using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace Cooking.API.Models
{
    public enum Color
    {
        silver,
        black,
        blue,
        red,
        pink,
        white
    }
    public class CookwareDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [EnumDataType(typeof(Color))]
        [JsonConverter(typeof(StringEnumConverter))]
        public Color Color { get; set; }
    }
}
