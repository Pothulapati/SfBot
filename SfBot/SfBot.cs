using Microsoft.Bot;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Ai.LUIS;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Schema;
using Microsoft.ServiceFabric.Client;
using Microsoft.ServiceFabric.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SfBot
{
    public class SfBot : IBot
    {
        public async Task OnTurn(ITurnContext TurnContext)
        {
            if (TurnContext.Activity.Type is ActivityTypes.Message)
            {
                //Find the intent and pass to the specific topic
                //Each Topic is divided into cluster,node,etc like resources and will have methods to do specfic important tasks.
                var Context = new SfContext(TurnContext);
                var result = Context.Services.Get<RecognizerResult>(LuisRecognizerMiddleware.LuisRecognizerResultKey) as RecognizerResult;
                var intent = result.GetTopScoringIntent();
                if (intent.intent == "GetNodes")
                {
                    string x = "";
                    PagedData<NodeInfo> nodes = await Context.Client.Nodes.GetNodeInfoListAsync();
                    foreach (var node in nodes.Data)
                    {
                        x += node.Name + $": {node.NodeStatus}"+"\n";
                    }
                    await Context.SendActivity($"Number of Nodes : {nodes.Data.Count()} \n {x}");
                }
                else if (intent.intent == "GetNodeHealth")
                {
                    var health = await Context.Client.Nodes.GetNodeHealthAsync("_Node_4");
                    await Context.SendActivity($"{health.AggregatedHealthState}");
                }
                else if (intent.intent == "GetNodeInfo")
                {
                    var nodeinfo = await Context.Client.Nodes.GetNodeInfoAsync("_Node_4");
                    await Context.SendActivity($"Node Status: {nodeinfo.NodeStatus}");
                }
                else if (intent.intent == "DeactivateNode")
                {
                    await Context.Client.Nodes.DisableNodeAsync(new NodeName("_Node_4"), new DeactivationIntentDescription(DeactivationIntent.RemoveData));
                    await Context.SendActivity($"Deactivating Node...");
                }
                else if (intent.intent == "ActivateNode")
                {
                    await Context.Client.Nodes.EnableNodeAsync(new NodeName("_Node_4"));
                    await Context.SendActivity("Activating Node...");
                }

            }
        }
    }
}
