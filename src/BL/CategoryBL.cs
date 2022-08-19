using Persistence;
using DAL;

namespace BL
{
    public class CategoryBL
    {
        private CategoryDAL categoryDal = new CategoryDAL();

        public Category GetCategoryById(int id)
        {
            return categoryDal.GetCategoryById(DbConfig.OpenConnection(), id);
        }

        public List<Category> GetCategoryByName(string name)
        {
            return categoryDal.GetCategoryByName(DbConfig.OpenConnection(), name);
        }

        public List<Category> GetAllCategory()
        {
            return categoryDal.GetAllCategory(DbConfig.OpenConnection());
        }
    }
}