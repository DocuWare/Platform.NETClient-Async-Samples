using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DocuWare.Platform.ServerClient;
using DocuWare.Services.Http.Client;

namespace Platform.NETClient_Async_Samples
{
    public class PlatformClient
    {
        public ServiceConnection Conn { get; set; }
        public Uri BaseAddress { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformClient"/> class.
        /// </summary>
        /// <param name="hostAddress">The host address.</param>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <param name="organization">The organization.</param>
        public PlatformClient(Uri hostAddress, string user, string password, string organization = null)
        {
            BaseAddress = new Uri($"{hostAddress}/docuware/platform");
            Conn = ServiceConnection.CreateAsync(BaseAddress, user, password, organization: organization).Result;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformClient"/> class.
        /// </summary>
        /// <param name="hostAddress">The host address.</param>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <param name="httpClientHandler">The HTTP client handler.</param>
        /// <param name="organization">The organization.</param>
        public PlatformClient(Uri hostAddress, string user, string password, HttpClientHandler httpClientHandler, string organization = null)
        {
            BaseAddress = new Uri($"{hostAddress}/docuware/platform");
            Conn = ServiceConnection.CreateAsync(BaseAddress, user, password, organization: organization, httpClientHandler: httpClientHandler).Result;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformClient"/> class.
        /// </summary>
        /// <param name="hostAddress">The host address.</param>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <param name="timeoutValue">The timeout value.</param>
        /// <param name="organization">The organization.</param>
        public PlatformClient(Uri hostAddress, string user, string password, TimeSpan timeoutValue, string organization = null)
        {
            var httpClientHandler = CreateHttpClientHandler();
            BaseAddress = new Uri($"{hostAddress}/docuware/platform");
            Conn = ServiceConnection.CreateAsync(BaseAddress, user, password, organization: organization, httpClientHandler: httpClientHandler).Result;
            SetProxyWithTimeout(timeoutValue, httpClientHandler);
        }

        /// <summary>
        /// Sets the proxy with timeout.
        /// If the WebRequestHandler has a timeout value, the lowest timeout value in HttpClient and WebRequestHandler will be used.
        /// </summary>
        /// <param name="timeSpan">The time span.</param>
        /// <param name="httpClientHandler">The HTTP client handler.</param>
        public void SetProxyWithTimeout(TimeSpan timeSpan, HttpClientHandler httpClientHandler)
        {
            var httpClient = new HttpClient(httpClientHandler)
            {
                BaseAddress = BaseAddress,
                Timeout = timeSpan
            };

            var proxy = new HttpClientProxy(httpClient);
            // Set proxy in service description
            Conn.ServiceDescription.SetProxy(proxy);
        }
        /// <summary>
        /// Creates the HTTP client handler.
        /// </summary>
        public static HttpClientHandler CreateHttpClientHandler()
            => new HttpClientHandler { UseCookies = true };
    }
}