using gamerszone.Data;
using gamerszone.Iservices;
using MongoDB.Driver;

namespace gamerszone.Services
{
    public class CategoryService : ICategoryService
    {

        private MongoClient? _mongoClient = null;
        private IMongoDatabase? _mongoDatabase = null;
        private IMongoCollection<Category>? _categoryTable = null;
        public CategoryService()
        {
            _mongoClient = new MongoClient("mongodb://127.0.0.1:27017");
            _mongoDatabase = _mongoClient.GetDatabase("trdb");
            _categoryTable = _mongoDatabase.GetCollection<Category>("categories");
        }

        public Category CategoryGetById(string id)
        {
            return _categoryTable.Find(c => c.Id == id && c.active).FirstOrDefault();
        }

        public List<Category> CategoryList()
        {
            return _categoryTable.Find(FilterDefinition<Category>.Empty).ToList();
        }

        public string DeleteCategory(string id)
        {
            try
            {
                if (_categoryTable != null)
                {
                    _categoryTable.DeleteOne(p => p.Id == id);
                    return "Deleted";
                }

            }
            catch (SystemException ex)
            {
                throw ex;
            }
            return "Failed";
        }

        public void SaveOrUpdate(Category category)
        {
            var categoryObj = _categoryTable.Find(c => c.Id == category.Id && c.active).FirstOrDefault();
            if (categoryObj != null)
            {
                if (_categoryTable != null)
                {
                    _categoryTable.ReplaceOne(c => c.Id == category.Id, category);
                }
            }
            else
            {
                if (_categoryTable != null) _categoryTable.InsertOne(category);
            }
        }
    }
}
