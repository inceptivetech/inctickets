using gamerszone.Data;

namespace gamerszone.Iservices
{
    public interface ICategoryService
    {
        void SaveOrUpdate(Category category);
        Category CategoryGetById(string id);

        List<Category> CategoryList();

        string DeleteCategory(string id);
    }
}
