using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TicketBot.Models;

namespace TicketBot.Services
{
    public class TicketService
    {
        private const string BASE_URL = "http://ticket-ms.somee.com";

        public async Task<TotalResponse> GetTotalTickets()
        {
            var response = await GetRequest("/Bot/Total");
            return await DeserializeRequest<TotalResponse>(response);
        }

        public async Task<Ticket> GetRandomTicket()
        {
            var response = await GetRequest("/Bot/Random");

            var ticket = await DeserializeRequest<Ticket>(response);
            ticket.Url = ticket.Url.Insert(0, BASE_URL);
            return ticket;
        }

        public async Task<IEnumerable<Ticket>> GetTickets(string number)
        {
            var response = await GetRequest("/Bot/Number/?number=" + number);

            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new ArgumentException($"Квитки з №{number} не знайдено.");

            var tickets = await DeserializeRequest<IEnumerable<Ticket>>(response);

            tickets.ToList().ForEach(t =>
            {
                t.Url = t.Url.Insert(0, BASE_URL);
            });

            return tickets;
        }

        private async Task<HttpResponseMessage> GetRequest(string url)
        {
            var client = new HttpClient();
            return await client.GetAsync(BASE_URL + url);
        }

        private async Task<T> DeserializeRequest<T>(HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }
}