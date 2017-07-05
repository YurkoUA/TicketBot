namespace TicketBot.Models
{
    public class TotalResponse
    {
        public int TotalTickets { get; set; }
        public int HappyTickets { get; set; }
        public int TotalPackages { get; set; }

        public override string ToString()
        {
            return $"Всього квитків: {TotalTickets}<br/>Щасливих: {HappyTickets}<br/>Пачок: {TotalPackages}";
        }
    }
}