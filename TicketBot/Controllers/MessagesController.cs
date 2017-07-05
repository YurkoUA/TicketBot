using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using TicketBot.Services;

namespace TicketBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                Activity reply = null;

                try
                {
                    reply = activity.CreateReply(await GetResponse(activity.Text));
                }
                catch (ArgumentException ex)
                {
                    reply = activity.CreateReply(ex.Message);
                }
                catch
                {
                    reply = activity.CreateReply("Помилка.");
                }
                finally
                {
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private async Task<string> GetResponse(string command)
        {
            var service = new TicketService();

            if (command.Equals("/total", StringComparison.CurrentCultureIgnoreCase))
            {
                return (await service.GetTotalTickets()).ToString();
            }
            else if (command.Equals("/random", StringComparison.CurrentCultureIgnoreCase))
            {
                return (await service.GetRandomTicket()).ToString();
            }
            else if (command.Split(' ').First().Equals("/number", StringComparison.CurrentCultureIgnoreCase))
            {
                var number = command.Split(' ').Last();

                if (!Regex.IsMatch(number, @"\d{6}") || string.IsNullOrEmpty(number))
                    return "Необхідно ввести номер квитка.";

                var tickets = await service.GetTickets(number);
                return string.Join("<br/><br/>", tickets.Select(t => t.ToString()));
            }
            return "Даної команди не існує.";
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}