using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazeInvaders.Client
{
    public class JSKeyHandler
    {
        public static event EventHandler<ConsoleKey> KeyUp;

        public static event EventHandler<ConsoleKey> KeyDown;

        [JSInvokable]
        public static Task<bool> KeyDownFromJS(int e)
        {
            var found = false;
            var consoleKey = default(ConsoleKey);

            try
            {
                consoleKey = (ConsoleKey)e;
                found = true;
            }
            catch
            {
                Console.WriteLine($"Cound not find {nameof(ConsoleKey)} for JS key value {e})");
            }

            if (found)
                KeyDown?.Invoke(null, consoleKey);

            return Task.FromResult(found);
        }

        [JSInvokable]
        public static Task<bool> KeyUpFromJS(int e)
        {
            var found = false;
            var consoleKey = default(ConsoleKey);

            try
            {
                consoleKey = (ConsoleKey)e;
                found = true;
            }
            catch
            {
                Console.WriteLine($"Cound not find {nameof(ConsoleKey)} for JS key value {e})");
            }

            if (found)
                KeyUp?.Invoke(null, consoleKey);

            return Task.FromResult(found);
        }
    }
}
