using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace TaxCalculator.Central.WebApplication.Models.APIModels
{
    public abstract class BasePostWebApiModel<InputDTO, ResponseDTO>
    {
        public string RequestUri { get; set; }

        public ResponseDTO PostJsonEncodedContent(string baseUri, InputDTO content, string oAuthToken = "")
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(baseUri);
            httpClient.DefaultRequestHeaders.Accept.Clear();

            if (!string.IsNullOrEmpty(oAuthToken))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", oAuthToken.Replace("Bearer ", ""));

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var task = httpClient.PostAsJsonAsync<InputDTO>(RequestUri, content);

            task.Wait();

            if (task.IsCompleted)
            {
                var response = task.Result;

                if (!response.IsSuccessStatusCode)
                {
                    var responseTaskError = response.Content.ReadAsStringAsync();

                    responseTaskError.Wait();

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        throw new UnauthorizedAccessException($"{response.StatusCode}:{responseTaskError.Result}");
                    else
                        throw new InvalidOperationException($"{response.StatusCode}:{responseTaskError.Result}");
                }

                var responseTask = response.Content.ReadAsStringAsync();

                responseTask.Wait();

                if (responseTask.IsCompleted)
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseDTO>(responseTask.Result);
            }

            return default(ResponseDTO);
        }

    }
}
