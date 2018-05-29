﻿using Microsoft.Bot;
using Microsoft.Bot.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SfBot
{
    public class SfBot : IBot
    {
        public async Task OnTurn(ITurnContext Context)
        {
            if(Context.Activity.Text == "Hi")
            {
               await Context.SendActivity($"Hi {Context.Activity.From.Name}");
            }
        }
    }
}