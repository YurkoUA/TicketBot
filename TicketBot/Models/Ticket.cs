using System;
using System.Text;

namespace TicketBot.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Number { get; set; }

        public IdName Package { get; set; }

        public IdName Color { get; set; }
        public IdName Serial { get; set; }

        public string SerialNumber { get; set; }
        
        public string Note { get; set; }
        public string Date { get; set; }
        public DateTime AddDate { get; set; }

        public string Url { get; set; }

        public override string ToString()
        {
            var sBuilder = new StringBuilder();

            sBuilder.Append($"ID: {Id}<br/>");
            sBuilder.Append($"Номер: {Number}<br/>");

            if (Package != null)
            {
                sBuilder.Append($"Пачка: {Package}<br/>");
            }

            sBuilder.Append($"Колір: {Color}<br/>");
            sBuilder.Append($"Серія: {Serial}{SerialNumber}<br/>");

            if (!string.IsNullOrEmpty(Date))
            {
                sBuilder.Append($"Дата: {Date}<br/>");
            }

            sBuilder.Append($"Додано: {AddDate.ToString("d")}<br/>");

            if (!string.IsNullOrEmpty(Note))
            {
                sBuilder.Append($"Примітка: {Note}<br/>");
            }

            sBuilder.Append($"URL: {Url}");

            return sBuilder.ToString();
        }
    }
}