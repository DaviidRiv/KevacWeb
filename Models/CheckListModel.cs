using QuevakWeb.Models.AuxModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuevakWeb.Models
{
    public class CheckListModel
    {
        [Key]
        [DisplayName("Folio")]
        public int IdCheckList{ get; set; }
        [DisplayName("Fecha de Creación")]
        public string? Fecha { get; set; }

        [ForeignKey("UsuarioId")]
        public int? UsuarioId { get; set; }
        public UsuarioModel? Usuario { get; set; }

        [ForeignKey("ClienteId")]
        public int? ClienteId { get; set; }
        public ClienteModel? Cliente { get; set; }

        public ICollection<CheckListTareaModel>? CheckListTareas { get; set; }
        public ICollection<CheckListAreaModel>? CheckListAreas { get; set; }

        public string? Observacion { get; set; }
        [DisplayName("Firma")]
        public string? Firma { get; set; }

        [DisplayName("Nombre Última Edición")]
        public string? NameLastEdit { get; set; }
        [DisplayName("Fecha Última Edición")]
        public string? DateLastEdit { get; set; }
    }
}
