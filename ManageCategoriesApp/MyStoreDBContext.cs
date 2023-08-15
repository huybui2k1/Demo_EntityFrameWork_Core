using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCategoriesApp
{
    //declare category entity
    public class Category
    {
        public Category() { }
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

    } //end Categories
    public class MyStore : DbContext
    {
        public MyStore() { }
     // these properties map to tables in the database

        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder() 
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyStoreDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //using fluent API instead of attributes
            //to limit the length of a category name to under 40
            modelBuilder.Entity<Category>()
                .Property(category => category.CategoryName)
                .IsRequired() //not null
                .HasMaxLength(40);
            //insert data for categories table
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryID = 1, CategoryName = "Beverages" },
                new Category { CategoryID = 2, CategoryName = "Condiments" },
                new Category { CategoryID = 3, CategoryName = "Confections" });
        }
    } //end mystore class

}
