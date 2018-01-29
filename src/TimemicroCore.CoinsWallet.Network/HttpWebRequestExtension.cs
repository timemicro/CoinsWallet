using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TimemicroCore.CoinsWallet.Network
{
    public static class HttpWebRequestExtension
    {
        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        private static HttpWebResponse GetHttpWebResponse(this HttpWebRequest request)
        {
            request.Method = "GET";
            return request.GetResponse() as HttpWebResponse;
        }

        private static HttpWebResponse PostHttpWebResponse(this HttpWebRequest request, string content, string contentType)
        {
            if (request.RequestUri.Scheme.Equals("https", StringComparison.CurrentCultureIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
            }

            var data = Encoding.UTF8.GetBytes(content);

            request.Method = "POST";
            request.ContentType = contentType;
            request.ContentLength = data.Length;

            var requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);

            return request.GetResponse() as HttpWebResponse;
        }

        public static string Get(this HttpWebRequest request)
        {
            using (var response = request.GetHttpWebResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static HttpStatusCode GetHttpStatusCode(this HttpWebRequest request)
        {
            var response = request.GetHttpWebResponse();
            return response.StatusCode;
        }

        public static string Post(this HttpWebRequest request, string content, string contentType)
        {
            using (var response = request.PostHttpWebResponse(content, contentType))
            {
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static string PostForm(this HttpWebRequest request, string content)
        {
            return request.Post(content, "application/x-www-form-urlencoded");
        }

        public static HttpStatusCode PostFormHttpStatusCode(this HttpWebRequest request, string content)
        {
            return request.PostHttpStatusCode(content, "application/x-www-form-urlencoded");
        }

        public static HttpStatusCode PostHttpStatusCode(this HttpWebRequest request, string content, string contentType)
        {
            using (var response = request.PostHttpWebResponse(content, contentType))
            {
                return response.StatusCode;
            }
        }

        public static string PostJson(this HttpWebRequest request, string content)
        {
            return request.Post(content, "application/json");
        }

        public static HttpStatusCode PostJsonHttpStatusCode(this HttpWebRequest request, string content)
        {
            return request.PostHttpStatusCode(content, "application/json");
        }

        public static string PostXml(this HttpWebRequest request, string content)
        {
            return request.Post(content, "text/xml");
        }

        public static HttpStatusCode PostXmlHttpStatusCode(this HttpWebRequest request, string content)
        {
            return request.PostHttpStatusCode(content, "text/xml");
        }
    }
}
