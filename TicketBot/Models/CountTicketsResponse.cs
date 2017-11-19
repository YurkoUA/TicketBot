namespace TicketBot.Models
{
    public class CountTicketsResponse
    {
        public int Total { get; set; }
        public int Happy { get; set; }

        public override string ToString()
        {
            return $"Всього квитків: {Total}<br/>Щасливих: {Happy}";
        }
    }
}