using Microsoft.EntityFrameworkCore;
using Shared.Template.AuthModels;
using Shared.Template.Database;

namespace $safeprojectname$.Data
{
    public partial class MainDbContext : DbContext
    {
        public MainDbContext()
        {

        }

        public MainDbContext(DbContextOptions<MainDbContext> options)
            : base(options)
        {
        }


        public DbSet<Userstbl> Userstbl { get; set; }
        public DbSet<Test> Test { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Userstbl>(entity =>
            {
                entity.HasKey(x => x.UserGuidfld);

            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasKey(x => x.TestGuid);

            });

            //modelBuilder.Entity<Agegrouptbl>(entity =>
            //{
            //    entity.HasKey(x => x.AgeGroupfld);

            //    //entity.ToTable("Customerstbl", tb =>
            //    //{
            //    //    tb.HasTrigger("Customerstbl_SetDateTimeLastUpdatedfld_trg");
            //    //    tb.HasTrigger("tr_Customerstbl_Delete");
            //    //    tb.HasTrigger("tr_Customerstbl_Insert");
            //    //    tb.HasTrigger("tr_Customerstbl_Update");
            //    //});
            //});

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
