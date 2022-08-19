namespace Persistence
{
    public class Category
    {
        public int categoryId;
        public string? categoryName;

        public Category()
        {

        }

        public Category(int categoryId, string categoryName)
        {
            this.categoryId = categoryId;
            this.categoryName = categoryName;
        }
    }
}