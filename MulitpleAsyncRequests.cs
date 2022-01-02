using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestAsyncAwait{

    public class MulitpleAsyncRequests{

        public static async Task RunAsync(){
            var urls = new string[] { "http://webcode.me", "http://example.com",
                "http://httpbin.org", "https://ifconfig.me", "http://termbin.com",
                "https://github.com"
            };

            var rx = new Regex(@"<title>\s*(.+?)\s*</title>",
            RegexOptions.Compiled);

            using var client = new HttpClient();


            var tasks = new List<Task<string>>();
            foreach (var url in urls)
            {
                // call GetStringAsync and then add to task list
                tasks.Add(client.GetStringAsync(url));
            }

            // run await all task in tasklist
            Task.WaitAll(tasks.ToArray());

            var data = new List<string>();

            foreach (var task in tasks)
            {
                data.Add(await task);
            }


            // view result
            foreach (var content in data)
            {
                var matches = rx.Matches(content);

                foreach (var match in matches)
                {
                    Console.WriteLine(match);
                }
            }

        }


        public async void TestJoinStringPerformance(){
            var tasks = new List<Task<int>>();

            tasks.Add(Task.Run(() => DoWork1()));
            tasks.Add(Task.Run(() => DoWork2()));

            await Task.WhenAll(tasks);

            Console.WriteLine(await tasks[0]);
            Console.WriteLine(await tasks[1]);
        }
    
        async Task<int> DoWork1()
        {
            var text = string.Empty;

            for (int i = 0; i < 100_000; i++)
            {
                text += "DoWork";
            }

            Console.WriteLine("concatenation finished");

            return await Task.FromResult(text.Length);
        }

        async Task<int> DoWork2()
        {
            var text = string.Empty;

            for (int i = 0; i < 100_000; i++)
            {
                text = $"{text}DoWork";
            }

            Console.WriteLine("interpolation finished");

            return await Task.FromResult(text.Length);
        }
    }

}