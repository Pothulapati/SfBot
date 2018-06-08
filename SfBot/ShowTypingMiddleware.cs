using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace SfBot
{
    internal class ShowTypingMiddleware : IMiddleware
    {
        public async Task OnTurn(ITurnContext context, MiddlewareSet.NextDelegate next)
        {
            if (context.Activity.Type is ActivityTypes.Message)
            {
                //HEY
                
                var typing = new Activity()
                {
                    RelatesTo = context.Activity.RelatesTo,
                    Type = ActivityTypes.Typing
                };
                await context.SendActivity(typing);
                await next().ConfigureAwait(false);
            }
        }
    }
}