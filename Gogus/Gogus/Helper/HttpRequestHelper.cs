using Gogus.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace Gogus.Helper
{
    public class HttpRequestHelper
    {
        private static HttpRequestHelper _instance { get; set; }
        public static HttpRequestHelper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HttpRequestHelper();

                return _instance;
            }
        }

        public HttpRequestHelper()
        {

        }

        public async Task<List<T>> FillListObjectWithJson<T>(List<T> obToFill, String uri)
        {
            List<T> returnList = new List<T>();
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(new Uri(uri));
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                returnList = JsonConvert.DeserializeObject<List<T>>(responseString);
            }

            return returnList;
        }

        public async Task<T> FillObjectWithJson<T>(T obToFill, String uri)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(new Uri(uri));
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseString);
            }

            return obToFill;
        }
    }
}
