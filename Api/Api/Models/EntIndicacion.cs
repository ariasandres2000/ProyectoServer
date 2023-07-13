using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("indicacion", Schema = "web")]
    public class EntIndicacion
    {
        [Key]
        public int id_indicacion { get; set; }
        [Required(ErrorMessage = "Se requiere el usaurio.")]
        public int id_usuario { get; set; }
        [Required(ErrorMessage = "Se requiere el nombre.")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Se requiere el tipo.")]
        public char tipo { get; set; }
        public byte imagen { get; set; }
        public string valor { get; set; }
        public string[] etiqueta { get; set; }
    }
}
