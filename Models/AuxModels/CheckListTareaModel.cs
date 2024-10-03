using System.ComponentModel.DataAnnotations;

namespace QuevakWeb.Models.AuxModels
{
    public class CheckListTareaModel
    {
        [Key]
        public int IdCheckListTarea { get; set; }

        public int? CheckListId { get; set; }
        public CheckListModel? CheckList { get; set; }

        public int? TareaId { get; set; }
        public TareaModel? Tarea { get; set; }

        public bool? Completada { get; set; }
    }
}
