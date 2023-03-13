using gamerszone.Data;

namespace gamerszone.Iservices
{
    public interface ITicketService
    {
        void SaveOrUpdate(Ticket ticket);
        Ticket TicketGetById(string id);

        List<Ticket> TicketList();

        string DeleteTicket(string id);
    }
}
