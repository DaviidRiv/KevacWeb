using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuevakWeb.Models;

namespace QuevakWeb.Data
{
    public class QuevakWebContext : DbContext
    {
        public QuevakWebContext (DbContextOptions<QuevakWebContext> options)
            : base(options)
        {
        }

        public DbSet<QuevakWeb.Models.UsuarioModel> UsuarioModel { get; set; } = default!;

        public DbSet<QuevakWeb.Models.ClienteModel>? ClienteModel { get; set; }

        public DbSet<QuevakWeb.Models.TareaModel>? TareaModel { get; set; }

        public DbSet<QuevakWeb.Models.AreaModel>? AreaModel { get; set; }

        public DbSet<QuevakWeb.Models.CheckListModel>? CheckListModel { get; set; }
        public DbSet<QuevakWeb.Models.AuxModels.CheckListTareaModel>? CheckListTareaModel { get; set; }
        public DbSet<QuevakWeb.Models.AuxModels.CheckListAreaModel>? CheckListAreaModel { get; set; }
    }
}
