namespace TicketBot.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Color { get; set; }
        public string Serial { get; set; }
        public string Package { get; set; }
        public string Url { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}<br/>Номер: {Number}<br/>Колір: {Color}<br/>Серія: {Serial}<br/>Пачка: {Package}<br/>Посилання: {Url}";
        }
    }
}