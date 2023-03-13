using gamerszone.Data;

namespace gamerszone.Iservices
{
    public interface IProjectService
    {
        void SaveOrUpdate(Project project);
        Project ProjectGetById(string id);

        List<Project> ProjectList();

        string DeleteProject(string id);
    }
}
