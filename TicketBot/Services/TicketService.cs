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
        private string _baseUrl;
        private bool _isPartialMatchesAllowed;

        public TicketService(string baseUrl, bool isPartialMatchesAllowed)
        {
            _baseUrl = baseUrl;
            _isPartialMatchesAllowed = isPartialMatchesAllowed;
        }

        public async Task<CountTicketsResponse> GetCountOfTickets()
        {
            var response = await GetRequest("Ticket/GetCount");
            return await DeserializeResponse<CountTicketsResponse>(response);
        }

        public async Task<CountPackagesResponse> GetCountOfPackages()
        {
            var response = await GetRequest("Package/GetCount");
            return await DeserializeResponse<CountPackagesResponse>(response);
        }

        public async Task<Ticket> GetRandomTicket()
        {
            var response = await GetRequest("Ticket/GetRandom");

            var ticket = await DeserializeResponse<Ticket>(response);
            ticket.Url = $"{_baseUrl}Details/{ticket.Id}";
            return ticket;
        }

        public async Task<IEnumerable<Ticket>> GetTickets(string number)
        {
            var response = await GetRequest($"Ticket/Search/{number}?partialMatches={_isPartialMatchesAllowed}");

            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new ArgumentException($"Квитки з №{number} не знайдено.");

            var tickets = await DeserializeResponse<IEnumerable<Ticket>>(response);

            tickets.ToList().ForEach(t =>
            {
                t.Url = $"{_baseUrl}Details/{t.Id}";
            });

            return tickets;
        }

        private async Task<HttpResponseMessage> GetRequest(string url)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl + "api/")
            };

            return await client.GetAsync(url);
        }

        private async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }
}