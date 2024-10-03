using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuevakWeb.Models
{
    public class ClienteModel
    {
        [Key]
        [DisplayName("Folio")]
        public int IdCliente { get; set; }
        public string? RFC { get; set; }
        [DisplayName("Nombre o Razón Social")]
        public string? NombreRS { get; set; }
        public string? Domicilio { get; set; }
        public string? Municipio { get; set; }
        public string? Estado { get; set; }
        public string? Telefono { get; set; }
        [DisplayName("Nombre de Contacto")]
        public string? NombreContacto { get; set; }
        public string? Correo { get; set; }

        [DisplayName("Nombre Última Edición")]
        public string? NameLastEdit { get; set; }
        [DisplayName("Fecha Última Edición")]
        public string? DateLastEdit { get; set; }
    }
}
