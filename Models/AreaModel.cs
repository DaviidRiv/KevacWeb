using QuevakWeb.Models.AuxModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuevakWeb.Models
{
    public class AreaModel
    {
        [Key]
        [DisplayName("Folio")]
        public int IdArea { get; set; }
        public string? Area { get; set; }
        [DisplayName("Inicio")]
        public string? HorarioI { get; set; }
        [DisplayName("Fin")]
        public string? HorarioF { get; set; }
        public string? Aux { get; set; }
        [DisplayName("Fecha de Creación")]
        public string? Fecha { get; set; }


        [DisplayName("Nombre Última Edición")]
        public string? NameLastEdit { get; set; }
        [DisplayName("Fecha Última Edición")]
        public string? DateLastEdit { get; set; }

        public ICollection<CheckListAreaModel>? CheckListAreas { get; set; }
    }
}
