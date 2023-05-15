using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Central.WebApplication.Models.APIModels
{
    public abstract class BaseGetWebApiModel<ResponseDTO>
    {
        public string RequestUri { get; set; }

        public ResponseDTO GetEncodedContent(string baseUri, List<KeyValuePair<string, string>> parameters, string oAuthToken = "")
        {
            HttpClient httpClient = new HttpClient();

            var uri = new UriBuilder(baseUri + RequestUri);
            if (parameters != null)
            {
                var encodedContent = new FormUrlEncodedContent(parameters);

                var responseQuery = encodedContent.ReadAsStringAsync();
                responseQuery.Wait();
                uri.Query = responseQuery.Result;
            }

            httpClient.DefaultRequestHeaders.Accept.Clear();

            if (!string.IsNullOrEmpty(oAuthToken))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", oAuthToken.Replace("Bearer ", ""));

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var task = httpClient.GetAsync(uri.ToString());

            task.Wait();

            if (task.IsCompleted)
            {
                var response = task.Result;
                if (!response.IsSuccessStatusCode)
                {
                    var responseTaskError = response.Content.ReadAsStringAsync();

                    responseTaskError.Wait();

                    if (responseTaskError.IsCompleted)
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            throw new UnauthorizedAccessException($"{response.StatusCode}:{responseTaskError.Result}");
                        else
                            throw new InvalidOperationException($"{response.StatusCode}:{responseTaskError.Result}");
                    }
                }
                var responseTask = response.Content.ReadAsStringAsync();

                responseTask.Wait();

                if (responseTask.IsCompleted)
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseDTO>(responseTask.Result);

            }

            return default(ResponseDTO);
        }

        public ResponseDTO GetEncodedContent(string baseUri, string oAuthToken = "")
        {
            HttpClient httpClient = new HttpClient();

            var uri = new UriBuilder(baseUri + RequestUri);

            httpClient.DefaultRequestHeaders.Accept.Clear();

            if (!string.IsNullOrEmpty(oAuthToken))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", oAuthToken);

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var task = httpClient.GetAsync(uri.ToString());

            task.Wait();

            if (task.IsCompleted)
            {
                var response = task.Result;
                if (!response.IsSuccessStatusCode)
                {
                    var responseTaskError = response.Content.ReadAsStringAsync();

                    responseTaskError.Wait();

                    if (responseTaskError.IsCompleted)
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            throw new UnauthorizedAccessException($"{response.StatusCode}:{responseTaskError.Result}");
                        else
                            throw new InvalidOperationException($"{response.StatusCode}:{responseTaskError.Result}");
                    }
                }
                var responseTask = response.Content.ReadAsStringAsync();

                responseTask.Wait();

                if (responseTask.IsCompleted)
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseDTO>(responseTask.Result);

            }

            return default(ResponseDTO);
        }

        public ResponseDTO GetJsonEncodedContent(string baseUri)
        {
            HttpClient httpClient = new HttpClient();

            var uri = new UriBuilder(baseUri + RequestUri);

            httpClient.DefaultRequestHeaders.Accept.Clear();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var task = httpClient.GetAsync(uri.ToString());

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
