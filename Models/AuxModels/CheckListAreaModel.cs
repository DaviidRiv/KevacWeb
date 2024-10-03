using System.ComponentModel.DataAnnotations;

namespace QuevakWeb.Models.AuxModels
{
    public class CheckListAreaModel
    {
        [Key]
        public int IdCheckListArea { get; set; }

        public int? CheckListId { get; set; }
        public CheckListModel? CheckList { get; set; }

        public int? AreaId { get; set; }
        public AreaModel? Area { get; set; }

        public bool? Completada { get; set; }
    }
}
