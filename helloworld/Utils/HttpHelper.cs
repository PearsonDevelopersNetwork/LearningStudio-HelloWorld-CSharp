#region License Information
// LearningStudio HelloWorld Application & API Explorer  
// 
// Need Help or Have Questions? 
// Please use the PDN Developer Community at https://community.pdn.pearson.com
//
// @category   LearningStudio HelloWorld
// @author     Wes Williams <wes.williams@pearson.com>
// @author     Pearson Developer Services Team <apisupport@pearson.com>
// @copyright  2014 Pearson Education Inc.
// @license    http://www.apache.org/licenses/LICENSE-2.0  Apache 2.0
// @version    1.0
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion License Information

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using Com.Pearson.Pdn.Learningstudio.Core;
using Com.Pearson.Pdn.Learningstudio.Grades;
using Com.Pearson.Pdn.Learningstudio.OAuth;
using Com.Pearson.Pdn.Learningstudio.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Com.Pearson.Pdn.Learningstudio.HelloWorld.WebApp.Utils
{
    public enum HttpMethod
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public class HttpHelper
    {
        public static HttpResponseMessage DoGet(Uri uri, IDictionary<string, string> headers)
        {
            return DoMethod(HttpMethod.GET, uri, headers, null);
        }

        public static HttpResponseMessage DoPost(Uri uri, IDictionary<string, string> headers, string body)
        {
            return DoMethod(HttpMethod.POST, uri, headers, body);
        }

        public static HttpResponseMessage DoPut(Uri uri, IDictionary<string, string> headers, string body)
        {
            return DoMethod(HttpMethod.PUT, uri, headers, body);
        }

        public static HttpResponseMessage DoDelete(Uri uri, IDictionary<string, string> headers)
        {
            return DoMethod(HttpMethod.DELETE, uri, headers, null);
        }

        private static HttpResponseMessage DoMethod(HttpMethod httpMethod, Uri uri, IDictionary<string, string> headers, string body)
        {
            HttpWebResponse httpWebResponse = null;
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            string result;
            bool isXml = uri.ToString().ToLower().EndsWith(".xml");

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                httpWebRequest.Method = httpMethod.ToString();

                foreach (string key in headers.Keys)
                    httpWebRequest.Headers[key] = headers[key];

                httpWebRequest.UserAgent = "LS-HelloWorld";

                if ((httpMethod == HttpMethod.POST || httpMethod == HttpMethod.PUT) && body.Length > 0)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(body);
                    httpWebRequest.ContentLength = bytes.Length;
                    httpWebRequest.ContentType = isXml ? "application/xml" : "application/json";

                    using (Stream stream = httpWebRequest.GetRequestStream())
                    {
                        stream.Write(bytes, 0, bytes.Length);
                    }
                }

                // Response and Status code
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd().Trim();
                }

                // Set content & content type
                httpResponseMessage.StatusCode = httpWebResponse.StatusCode;
                httpResponseMessage.Content = new StringContent(result);
            }
            catch (WebException ex)
            {
                if (httpWebResponse == null)
                {
                    httpWebResponse = (HttpWebResponse)ex.Response;
                    httpResponseMessage.StatusCode = httpWebResponse.StatusCode;
                    httpResponseMessage.Content = new StringContent(httpWebResponse.Headers["Message"]);
                }
            }
            finally
            {
                if (httpWebResponse != null)
                    httpWebResponse.Close();
            }


            return httpResponseMessage;
        }

        public static string GetContent(Stream inputStream)
        {
            string body = string.Empty;

            using (StreamReader streamReader = new StreamReader(inputStream))
            {
                body = streamReader.ReadToEnd().Trim();
            }

            return body;
        }
    }
}