using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Examples
{
    class ThreadPoolScaling
    {
        internal static void Run()
        {
            var done = false;

            Console.Clear();

            while (!done)
            {

                Console.WriteLine("Press 'Q' to quit or any other key to continue...");
                var choice = Console.ReadKey();
                if (choice.KeyChar == 'Q' || choice.KeyChar == 'q')
                {
                    done = true;
                    break;
                }

                FetchFiles().Wait();

                Console.WriteLine("\n\n");
            }
        }

        private static async Task FetchFiles()
        {
            var tasks = new List<Task>();

            for (int i = 0; i < 100; i++)
            {
                int fileNum = i;
                tasks.Add(Library.FetchFileAsync(fileNum));
                // tasks.Add(Library.FetchFileAsyncWithAwait(fileNum));
            }

            await Task.WhenAll(tasks);
            Console.WriteLine("All files fetched!");
        }
    }

    // Library
    class Library
    {
        public static async Task FetchFileAsync(int fileNum)
        {
            await Task.Run(() =>
            {
                var contents = IO.DownloadFile();
                Console.WriteLine($"Fetched file # {fileNum}: {contents}");
            });
        }

        public static async Task FetchFileAsyncWithAwait(int fileNum)
        {
            var contents = await IO.DownloadFileAsync();
            Console.WriteLine($"Fetched file # {fileNum}: {contents}");
        }
    }

    class IO
    {
        public static string DownloadFile()
        {
            Thread.Sleep(1000);
            return "file contents";
        }

        public static async Task<string> DownloadFileAsync()
        {
            await Task.Delay(1000);
            return "file contents";
        }
    }
}
