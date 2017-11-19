namespace TicketBot.Models
{
    public class IdName
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}