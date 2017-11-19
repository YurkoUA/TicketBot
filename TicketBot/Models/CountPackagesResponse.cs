namespace TicketBot.Models
{
    public class CountPackagesResponse
    {
        public int Total { get; set; }
        public int Opened { get; set; }
        public int Special { get; set; }

        public override string ToString()
        {
            return $"Всього пачок: {Total}<br/>Відкритих: {Opened}<br/>Спеціальних: {Special}";
        }
    }
}