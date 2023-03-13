using gamerszone.Data;
using gamerszone.Iservices;
using MongoDB.Driver;

namespace gamerszone.Services
{
    public class ProjectService : IProjectService
    {
        private MongoClient? _mongoClient = null;
        private IMongoDatabase? _mongoDatabase = null;
        private IMongoCollection<Project>? _projectTable = null;
        public ProjectService() 
        {
            _mongoClient = new MongoClient("mongodb://127.0.0.1:27017");
            _mongoDatabase = _mongoClient.GetDatabase("trdb");
            _projectTable = _mongoDatabase.GetCollection<Project>("projects");
        }

        public string DeleteProject(string id)
        {
            try
            {
                if (_projectTable != null)
                {
                    _projectTable.DeleteOne(p => p.Id == id);
                    return "Deleted";
                }

            }
            catch (SystemException ex)
            {
                throw ex;
            }
            return "Failed";
        }

        public Project ProjectGetById(string id)
        {
            return _projectTable.Find(p => p.Id == id && p.active).FirstOrDefault();
        }

        public List<Project> ProjectList()
        {
            return _projectTable.Find(FilterDefinition<Project>.Empty).ToList();
        }

        public void SaveOrUpdate(Project project)
        {
            var projectObj = _projectTable.Find(x => x.Id == project.Id && x.active).FirstOrDefault();
            if (projectObj != null)
            {
                if (_projectTable != null)
                {
                    _projectTable.ReplaceOne(p => p.Id == project.Id, project);
                }
            }
            else
            {
                if (_projectTable != null) _projectTable.InsertOne(project);
            }
        }
    }
}
