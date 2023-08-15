using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCategoriesApp
{
    public sealed class ManageCategories
    {
        //using singeton Pattern 
        private static ManageCategories instance = null;
        private static readonly object instanceLock = new object();
        private ManageCategories() { }
        public static ManageCategories Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ManageCategories();

                    }
                    return instance;
                }
            }
        }
        public List<Category> GetCategories()
        {
            List<Category> categories;
            try
            {
                using MyStore store = new MyStore();    
                categories = store.Categories.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return categories;
        }//end Getcategories

        /// ------ ham insert category
        public void InsertCategory(Category category)
        {
            try
            {
                using MyStore store = new MyStore();
                store.Categories.Add(category);
                store.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        } // end InsertCategory

        //----    ham update category 
        public void UpdateCategory(Category category)
        {
            try
            {
                using MyStore store = new MyStore();
                store.Entry<Category>(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                store.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        } //end UpdateCategory


        //ham delete category
        public void DeleteCategory(Category category)
        {
            try
            {
                using MyStore store = new MyStore();

                // find category by category ID
                var cate = store.Categories.SingleOrDefault(c=> c.CategoryID == category.CategoryID);

                store.Categories.Remove(cate);
                store.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        } // end deletecategory
    }//end managecategories

}
