namespace Persistence
{
    public class Category
    {
        private int categoryId;
        private string? categoryName;

        public Category()
        {

        }

        public Category(int categoryId, string? categoryName)
        {
            this.categoryId = categoryId;
            this.categoryName = categoryName;
        }

        public int? getCategoryId()
        {
            return this.categoryId;
        }

        public void setCategoryId(int categoryId)
        {
            this.categoryId = categoryId;
        }

        public string? getCategoryName()
        {
            return this.categoryName;
        }

        public void setCategoryName(string? categoryName)
        {
            this.categoryName = categoryName;
        }
    }
}