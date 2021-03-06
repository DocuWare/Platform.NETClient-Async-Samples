﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Platform.NetClientAsyncSamples.Model.Exceptions;
using Platform.NETClient_Async_Samples;

namespace Platform.NetCoreClientAsyncSamples
{
    /// <summary>
    /// This samples depict actions by a single user, thus there is no need for an asynchronous authentication at the platform.
    /// </summary>
    class Program
    {
        const string user = "";
        const string password = "";
        const string organization = "";
        static readonly Uri rootUri = new Uri("{schema}://{authority}");
        static void Main(string[] args) => MainAsync(args).Wait();

        /// <summary>
        /// Annotation to the use of the ConfigureAwait method:
        ///     Because console application by default lack a SynchronizationContext the ConfigureAwait method will have no effect. 
        ///     Nevertheless one should always specify ConfigureAwait to make the code reusable and to prevent exceptions when adding a library that installs a SynchronizationContext.
        /// http://stackoverflow.com/questions/25817703/configureawaitfalse-not-needed-in-console-win-service-apps-right
        /// https://msdn.microsoft.com/en-us/magazine/jj991977.aspx
        /// </summary>
        static async Task MainAsync(string[] args)
        {
            var client = new PlatformClient(rootUri, user, password, TimeSpan.FromMilliseconds(60000), organization);

            var org = client.Conn.Organizations.FirstOrDefault();
            if (org == null)
                throw new OrganizationNotFoundException($"No Organizations found.");

            var fc =
                await
                    org.GetFileCabinetsFromFilecabinetsRelationAsync()
                        .ContinueWith(
                            t => t.Result.Content
                                .FileCabinet.FirstOrDefault(f => !f.IsBasket))
                        .ConfigureAwait(false);
            if (fc == null)
                throw new FileCabinetNotFoundException($"No filecabinet found for user {user}.");


            Console.WriteLine($"Selected FileCabinet: {fc.Name}");
            Console.ReadLine();
        }
    }
}
