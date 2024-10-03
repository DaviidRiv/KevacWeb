using QuevakWeb.Models.AuxModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuevakWeb.Models
{
    public class TareaModel
    {
        [Key]
        [DisplayName("Folio")]
        public int IdTarea { get; set; }
        public string? Tarea { get; set; }
        [DisplayName("Fecha Creación")]
        public string? Fecha { get; set; }
        public string? Aux { get; set; }

        [DisplayName("Nombre Última Edición")]
        public string? NameLastEdit { get; set; }
        [DisplayName("Fecha Última Edición")]
        public string? DateLastEdit { get; set; }

        public ICollection<CheckListTareaModel>? CheckListTareas { get; set; }
    }
}
