using gamerszone.Data;
using gamerszone.Iservices;
using MongoDB.Driver;

namespace gamerszone.Services
{
    public class TicketService : ITicketService
    {
        private MongoClient? _mongoClient = null;
        private IMongoDatabase? _mongoDatabase = null;
        private IMongoCollection<Ticket>? _ticketTable = null;
        public TicketService() 
        {
            _mongoClient = new MongoClient("mongodb://127.0.0.1:27017");
            _mongoDatabase = _mongoClient.GetDatabase("trdb");
            _ticketTable = _mongoDatabase.GetCollection<Ticket>("tickets");
        }

        public string DeleteTicket(string id)
        {
            try
            {
                if (_ticketTable != null)
                {
                    _ticketTable.DeleteOne(t => t.Id == id);
                    return "Deleted";
                }

            }
            catch (SystemException ex)
            {
                throw ex;
            }
            return "Failed";

        }

        public void SaveOrUpdate(Ticket ticket)
        {
            // Logic for uploading file to wwwroot

            var ticketObj = _ticketTable.Find(x => x.Id == ticket.Id && x.active).FirstOrDefault();
            if (ticketObj != null)
            {
                if (_ticketTable != null)
                {
                    _ticketTable.ReplaceOne(t => t.Id == ticket.Id, ticket);
                }
            }
            else
            {
                if (_ticketTable != null) _ticketTable.InsertOne(ticket);
            }
        }

        public Ticket TicketGetById(string id)
        {
            return _ticketTable.Find(t => t.Id == id && t.active).FirstOrDefault();
        }

        public List<Ticket> TicketList()
        {
            return _ticketTable.Find(FilterDefinition<Ticket>.Empty).ToList();
        }
    }
}
