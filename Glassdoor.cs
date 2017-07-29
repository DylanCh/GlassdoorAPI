using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace GlassdoorAPI
{
    public class Glassdoor
    {
        HashSet<string> dataFormats = new HashSet<string>() { "json", "xml" };

        /// <summary> default constructor, default format: JSON 
        /// </summary>
        public Glassdoor() {
            this.DataFormat = "json";
        }

        /// <summary> constructor with 1 parameter
        /// <param name="JobField">search query</param>
        /// </summery>
        public Glassdoor(string JobField) {
            this.DataFormat = "json";
            this.JobField = JobField;            
        }

        /// <summary>
        /// Constructor with 2 parameters
        /// </summary>
        /// <param name="JobField"> search query </param>
        /// <param name="DataFormat"> Data format (JSON or XML) </param>
        public Glassdoor(string JobField, string DataFormat) {
            this.JobField = JobField;
            DataFormat = DataFormat.ToLower();
            if (dataFormats.Contains(DataFormat))
                this.DataFormat = DataFormat;
            else throw new ArgumentException("Wrong data format");
        }

        /// <summary>
        /// Search query
        /// </summary>
        public string JobField { get; set; }

        /// <summary>
        /// Data format, JSON or XML
        /// </summary>
        public string DataFormat { get; set; }

        /// <summary>
        /// Make request to the Glassdoor API
        /// </summary>
        /// <returns> The body of the response in plain String format </returns>
        public string Search() {
            var client = new RestClient(
               baseUrl: string.Format(
                    "http://api.glassdoor.com/api/api.htm?v=1&format={0}&t.p={1}&t.k={2}&action=employers&q={3}&userip=192.168.43.42&useragent=Mozilla%2F%2F4.0"
                    , DataFormat,
                    GlassdoorCred.PARTNER_ID,
                    GlassdoorCred.KEY, 
                    JobField
                    )
            );
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        /// <summary>
        /// Glassdoor API credentials
        /// </summary>
        private static class GlassdoorCred
        {
            public static readonly string PARTNER_ID = "[REPLACE]";
            public static readonly string KEY = "[REPLACE]";
        }
    }

    
}
