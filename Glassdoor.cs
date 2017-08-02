using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using System.Net;
using System.Net.Sockets;

namespace GlassdoorAPI
{
    public class Glassdoor
    {
        HashSet<string> dataFormats = new HashSet<string>() { "json", "xml" };
        GlassdoorSettings glassdoorSettings;

        /// <summary> default constructor, default format: JSON 
        /// </summary>
        public Glassdoor() {
            this.DataFormat = "json";
        }

        /// <summary> constructor with 1 parameter
        /// <param name="SearchQuery">search query</param>
        /// </summary>
        public Glassdoor(string SearchQuery) {
            this.DataFormat = "json";
            this.SearchQuery = SearchQuery;            
        }

        /// <summary>
        /// Constructor with 2 parameters
        /// </summary>
        /// <param name="SearchQuery"> search query </param>
        /// <param name="DataFormat"> Data format (JSON or XML) </param>
        public Glassdoor(string SearchQuery, string DataFormat) {
            this.SearchQuery = SearchQuery;
            DataFormat = DataFormat.ToLower();
            if (dataFormats.Contains(DataFormat))
                this.DataFormat = DataFormat;
            else throw new ArgumentException("Wrong data format");
        }

        /// <summary>
        /// Search query
        /// </summary>
        public string SearchQuery { get; set; }

        /// <summary>
        /// Data format, JSON or XML
        /// </summary>
        public string DataFormat { get; set; }

        /// <summary>
        /// Make request to the Glassdoor API
        /// </summary>
        /// <returns> The body of the response in plain String format </returns>
        public string Search() {
            if (glassdoorSettings == null)
                throw new NullReferenceException("Glassdoor API settings cannot be null");

            var client = new RestClient(
               baseUrl: string.Format(
                    "http://api.glassdoor.com/api/api.htm?v=1&format={0}&t.p={1}&t.k={2}&action=employers&q={3}&userip={4}&useragent=Mozilla%2F%2F4.0"
                    , DataFormat,
                    glassdoorSettings._partner_id,
                    glassdoorSettings._key,
                    SearchQuery,
                    IPAddress().ToString().Trim()
                )
            );
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        /// <summary>
        /// Get local user IPAddress (TODO: get client IP address instead)
        /// </summary>
        /// <returns></returns>
        IPAddress IPAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                throw new WebException("Client is not connected to the Internet");
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }

        /// <summary>
        /// Glassdoor API credentials
        /// </summary>
        private class GlassdoorSettings
        {
            public string _partner_id { get; private set; }
            public string _key { get; private set; }
            public GlassdoorSettings(string _partner_id, string _key) {
                if (_partner_id.Equals("") || _key.Equals(""))
                    throw new ArgumentException("Partner ID and API Key must not be empty");
                this._key = _key;
                this._partner_id = _partner_id;
            }
            
        }

        /// <summary>
        ///  This method sets the Glassdoor credentials (must be set before the Search() method invokation)
        /// </summary>
        /// <param name="partnerId">Partner ID</param>
        /// <param name="key">API Key</param>
        public void SetCredentials(string partnerId, string key) {
            glassdoorSettings = new GlassdoorSettings(partnerId, key);
        }
    }

    
}
