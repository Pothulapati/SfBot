using Microsoft.Bot.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Client;
using Microsoft.ServiceFabric.Common;
using Microsoft.Bot.Builder.Ai.LUIS;
namespace SfBot
{
    public class SfContext : TurnContextWrapper
    {
        public IServiceFabricClient Client { get; }

        public SfContext(ITurnContext context) : base(context)
        {
            Client = ServiceFabricClientFactory.Create(new Uri("http://localhost:19080"));
        }
    }
}
