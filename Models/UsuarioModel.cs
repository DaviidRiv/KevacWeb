using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuevakWeb.Models
{
    public class UsuarioModel
    {

        [Key]
        [DisplayName("Folio")]
        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        [DisplayName("Apellido Paterno")]
        public string? ApellidoP { get; set; }
        [DisplayName("Apellido Materno")]
        public string? ApellidoM { get; set; }
        public string? Password { get; set; }
        [DisplayName("Rol")]
        public string? RolUser { get; set; }


        [DisplayName("Nombre Última Edición")]
        public string? NameLastEdit { get; set; }
        [DisplayName("Fecha Última Edición")]
        public string? DateLastEdit { get; set; }

        [NotMapped]
        public string InfoUsuario => $"{Nombre} {ApellidoP} {ApellidoM}";
    }
}
