using gamerszone.Data;
using gamerszone.Iservices;
using gamerszone.Utilities;
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

        public async void SaveOrUpdate(Ticket ticket)
        {
            // Logic for uploading file to wwwroot
            var askGPTTool = new AskGPTTool("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");
            var ticketObj = _ticketTable.Find(x => x.Id == ticket.Id && x.active).FirstOrDefault();
            if (ticketObj != null)
            {
                if (_ticketTable != null)
                {
                    _ticketTable.ReplaceOne(t => t.Id == ticket.Id, ticket);
                }
                if(ticket.TicketDescription != null)
                {
                    var gptResponse = await askGPTTool.AskMe(ticket.TicketDescription, "davinci");
                    if (gptResponse != null)
                    {
                        Console.WriteLine(gptResponse);
                    }
                }
                
            }
            else
            {
                if (_ticketTable != null) _ticketTable.InsertOne(ticket);
                if (ticket.TicketDescription != null)
                {
                    var gptResponse = await askGPTTool.AskMe(ticket.TicketDescription, "davinci");
                    if (gptResponse != null)
                    {
                        Console.WriteLine(gptResponse);
                    }
                }
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
