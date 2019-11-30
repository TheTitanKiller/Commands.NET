using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Commands.NET;

namespace ExampleProject
{
    //the class holding the commands, no constructor needed
    class ExampleCommands
    {
        [Command("hello")]
        public async Task Hello(LocalContext ctx)
        {
            await ctx.AnswerAsync("Hello user !\n");
        }

        [Command("add")]
        public async Task Add(LocalContext ctx, int a, int b)
        {
            await ctx.AnswerAsync($"{a} + {b} = {a + b}\n");
        }

        [Command("exit")]
        public async Task Exit(Context ctx)
        {
            Environment.Exit(0);
            await Task.CompletedTask;
        }
    }
}
