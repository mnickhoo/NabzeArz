using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NabzeArz.Models
{
    public class Context : DbContext
    {
        public Context() : base("nabzeArz")
        {
            Database.SetInitializer<Context>(
                new DropCreateDatabaseIfModelChanges<Context>());
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<ChannelModel> Chanel { get; set; }
        public DbSet<PointModel> Points { get; set; }
        public DbSet<TrackingModel> Usertracking { get; set; }
        public DbSet<possensionModel> possensions { get; set; }
        public DbSet<CurrencyModel> Currencies { get; set; }
        //public DbSet<ticketModel> tickets { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //    modelBuilder.Entity<possensionModel>()
            //        //.HasOptional(p => p.user)
            //        //.WithMany(t => t.possension);
        }

        //    //.HasOptional(f => f.user) //Baz is dependent and gets a FK BarId
        //    //.WithRequired(s => s.possension);//Bar is principal
        //    //HasMany(t => t.Cities).WithOptional(t => t.Region);


        //    modelBuilder.Entity<UserModel>()
        //                .HasOptional(f => f.possension) //Bar is dependent and gets a FK BazId
        //                .WithRequired(s => s.user);//Baz is principal
        //}
    }
}