using DemoDatabaseFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoDatabaseFirst
{
  class Program
    {
        static void Main(string[] args)
        {
            //MyStoreContext _context = new MyStoreContext(); 
            //var query = _context.Categories.Where(c=>c.CategoryName.Contains(""));
            //foreach (var category in query)
            //{
            //    Console.WriteLine(category.CategoryName);
            //}
            //Console.ReadLine();



            ////create a DbContext object
            //MyStoreContext myStore = new MyStoreContext();
            ////print all Products
            //var products = from p in myStore.Products
            //               select new {p.ProductName, p.CategoryId};
            //foreach(var p in products)
            //{
            //    Console.WriteLine($"ProductName: {p.ProductName}, CategoryID: {p.CategoryId}");

            //}
            //Console.WriteLine("----------------------------------------");
            ////A query to get all Categories and their related Products
            //IQueryable<Category> cats = myStore.Categories.Include(c => c.Products);
            //foreach(Category c in cats)
            //{
            //    Console.WriteLine($"CategoryId: {c.CategoryId} has {c.Products.Count} products.");

            //}
            //Console.ReadLine();


            //FilteredIncludes();

            AggregateProducts();
            Console.ReadLine();


        }//end main

        static void FilteredIncludes()
        {
            using var db = new MyStoreContext();
            Console.Write("Enter a minimum for units in store: ");
            string unitsInStore = Console.ReadLine();
            int store = int.Parse(unitsInStore);
            IQueryable<Category> cats = db.Categories
                .Include(c => c.Products.Where(p => p.UnitslnStock >= store));
            foreach(Category c in cats)
            {
                Console.WriteLine($"{c.CategoryName} has {c.Products.Count} product");
                foreach(Product p in c.Products)
                {
                    Console.WriteLine($" ----> {p.ProductName} has {p.UnitslnStock} units in store");

                }
            }
        } //end filteredIncludes

        //ham aggregateProducts 

        static void AggregateProducts()
        {
            using var db = new MyStoreContext();
            {
                Console.WriteLine("{0,-25} {1,10}" ,arg0: "Product count:", arg1: db.Products.Count());
                Console.WriteLine("{0,-25} {1,10:$#,##0.00}" ,arg0: "Highest product price:", arg1: db.Products.Max(p => p.UnitPrice));
                Console.WriteLine("{0,-25} {1,10:N0}" ,arg0: "Sum of units in store:", arg1: db.Products.Sum(p => p.UnitslnStock));
                Console.WriteLine("{0,-25} {1,10:$#,##0.00}", arg0: "Average unit price:", arg1: db.Products.Average(p => p.UnitPrice));
                Console.WriteLine("{0,-25} {1,10:$#,##0.00}", arg0: "Value of units in store: ",
                     arg1: db.Products.AsEnumerable().Sum(p => p.UnitPrice * p.UnitslnStock));

            }
        } //end AggregateProducts


    } //end class
}

