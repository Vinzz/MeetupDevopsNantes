using System;
using System.IO;
using System.Net;
using System.Threading;

namespace FFService
{
    public class JsonRequest
    {

        internal static string GetRestResponse(string requestUrl)
        {
            string json;
            HttpWebResponse response = TryMethod(requestUrl);

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                json = sr.ReadToEnd();
            }

            return json;
        }

        private static HttpWebResponse TryMethod(string requestUrl)
        {
            WebRequest request = CreateRequest(requestUrl);

            int tries = 0;

            while (tries < 5)
            {
                ++tries;
                try
                {
                    return (HttpWebResponse)request.GetResponse();
                }
                catch (WebException e)
                {
                  
                    request = null;
                    Thread.Sleep(2000);
                    request = CreateRequest(requestUrl);
                }
            }

            throw new Exception("Too much network error");
        }

        private static WebRequest CreateRequest(string requestUrl)
        {
            var request = WebRequest.Create(requestUrl);
            request.UseDefaultCredentials = true;
            return request;
        }

        internal static string PostData(string postURL, string payload)
        {
            WebRequest request = CreateRequest(postURL);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = payload.Length;

            int tries = 0;

            while (tries < 5)
            {
                ++tries;
                try
                {
                    using (Stream webStream = request.GetRequestStream())
                    using (StreamWriter requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
                    {
                        requestWriter.Write(payload);
                    }

                    string json;
                    var response = (HttpWebResponse)request.GetResponse();

                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        json = sr.ReadToEnd();
                    }

                    return json;
                }
                catch (WebException e)
                {
                    HttpStatusCode errorCode = ((System.Net.HttpWebResponse)e.Response).StatusCode;

                    if (errorCode != HttpStatusCode.BadRequest)
                    {
                         request = null;
                        Thread.Sleep(2000);
                        request = CreateRequest(postURL);

                        request.Method = "POST";
                        request.ContentType = "application/json";
                        request.ContentLength = payload.Length;
                    }
                    else
                        throw;
                }
            }

            throw new Exception("Too much network error");
        }
    }
}
