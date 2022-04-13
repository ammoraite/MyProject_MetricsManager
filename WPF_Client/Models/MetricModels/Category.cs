using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

namespace WPF_Client.Models.MetricModels
{
    internal class Category
    {
        public string CategoryName { get; set; }
        private HttpClient _client { get; set; }
        public InstanceNamesResponse _Instanses { get; set; }=new InstanceNamesResponse();
        //public List<Counters> Counters { get; set; } = new List<Counters>();

        public Category()
         {
            CategoryName = "Процессор";
            _client = new HttpClient();
           GetInstanses();

        }

        public void GetInstanses()
        {
            var a= JsonSerializer.DeserializeAsync<Object>
            (_client.SendAsync(new HttpRequestMessage
                (HttpMethod.Get, new Uri
                    ($"http://localhost:5000/api/Metrics/GetallInstanse?category={CategoryName}"))).Result.Content
                .ReadAsStreamAsync().Result).Result;
            var s=(InstanceNamesResponse)a;
            Console.WriteLine();
        }
        //    public List<Counters> GetCounters()
        //    {


        //    JsonSerializer.
        //        DeserializeAsync<List<Counters>>

        //    (_client.SendAsync(new HttpRequestMessage
        //        (HttpMethod.Get,  new Uri
        //        ($"http://localhost:5000/api/Metrics/GetallCounters?category={CategoryName}&instanse={_Instanses.
        //        _instanses[0]}"))).

        //    Result.
        //        Content.
        //        ReadAsStreamAsync().
        //    Result).
        //    Result;
        //}

    }
}
