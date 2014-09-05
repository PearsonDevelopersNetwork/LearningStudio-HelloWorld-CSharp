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
using Com.Pearson.Pdn.Learningstudio.OAuth.Config;
using Com.Pearson.Pdn.Learningstudio.OAuth.Request;
using Com.Pearson.Pdn.Learningstudio.Utility;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Com.Pearson.Pdn.Learningstudio.HelloWorld.WebApp.Utils
{
    public enum OAuthType
    {
        NotDefined = 0,
        OAuth1 = 1,
        OAuth2 = 2
    }

    public class CommonHelper
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(OAuth2Controller));
        private OAuthConfig oAuthConfig = null;

        public CommonHelper()
        {
            oAuthConfig = new OAuthConfig();
            oAuthConfig.ApplicationId = ConfigManager.ApplicationId;
            oAuthConfig.ApplicationName = ConfigManager.ApplicationName;
            oAuthConfig.ClientString = ConfigManager.ClientString;
            oAuthConfig.ConsumerKey = ConfigManager.ConsumerKey;
            oAuthConfig.ConsumerSecret = ConfigManager.ConsumerSecret;
        }

        /// <summary>
        /// Do Method
        /// </summary>
        /// <param name="httpMethod">HttpMethod</param>
        /// <returns>HttpResponseMessage</returns>
        public HttpResponseMessage DoMethod(Utils.HttpMethod httpMethod)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            string rawUrl = HttpContext.Current.Request.RawUrl;
            OAuthType oAuthType;
            Uri uri = null;
            string body = string.Empty;
            IDictionary<string, string> headers = null;

            try
            {
                // Identify the OAuth type used in URL
                oAuthType = GetOAuthType(rawUrl);

                // Build the URL to post
                uri = BuildURI(rawUrl);

                // Form the OAuth1 or OAuth2 headers based on OAuthType
                headers = GetOAuthHeaders(oAuthType, uri);

                // Build the request body
                body = Utils.HttpHelper.GetContent(HttpContext.Current.Request.InputStream);

                switch (httpMethod)
                {
                    case Utils.HttpMethod.GET:
                        return Utils.HttpHelper.DoGet(uri, headers);
                    case Utils.HttpMethod.POST:
                        return Utils.HttpHelper.DoPost(uri, headers, body);
                    case Utils.HttpMethod.PUT:
                        return Utils.HttpHelper.DoPut(uri, headers, body);
                    case Utils.HttpMethod.DELETE:
                        return Utils.HttpHelper.DoDelete(uri, headers);
                }
            }
            catch (Exception ex)
            {
                Logger.Debug("Exception from DoMethod: ", ex);
                httpResponseMessage.StatusCode = HttpStatusCode.InternalServerError;
                httpResponseMessage.Content = new StringContent(ex.Message);
            }

            return httpResponseMessage;
        }

        /// <summary>
        /// Build LearningStudio URI
        /// </summary>
        /// <param name="rawUrl">string</param>
        /// <returns>Uri</returns>
        private Uri BuildURI(string rawUrl)
        {
            string lsURI = string.Empty;

            if (rawUrl.Contains("/oauth2"))
                rawUrl = rawUrl.Substring(rawUrl.IndexOf("/oauth2") + 8);

            if (rawUrl.Contains("/oauth1"))
                rawUrl = rawUrl.Substring(rawUrl.IndexOf("/oauth1") + 8);

            return new Uri(ConfigManager.GradeCustomerCategoriesURL + rawUrl);
        }

        /// <summary>
        /// Generate OAuth1 or OAuth2 headers based on OAuth Type
        /// </summary>
        /// <param name="oAuthType">OAuthType</param>
        /// <returns>Dictionary of string, string</returns>
        private IDictionary<string, string> GetOAuthHeaders(OAuthType oAuthType, Uri uri)
        {
            if (oAuthType == OAuthType.OAuth1)
            {
                OAuth1SignatureService oAuth1SignatureService = null;
                OAuth1Request oAuth1Request = null;

                oAuth1SignatureService = new OAuthServiceFactory(oAuthConfig).Build<OAuth1SignatureService>(typeof(OAuth1SignatureService));
                oAuth1Request = oAuth1SignatureService.GenerateOAuth1Request(Utility.HttpMethod.GET, uri, string.Empty);
                return oAuth1Request.Headers;
            }

            if (oAuthType == OAuthType.OAuth2)
            {
                OAuth2AssertionService oAuth2AssertionService = null;
                OAuth2Request oAuth2Request = null;

                oAuth2AssertionService = new OAuthServiceFactory(oAuthConfig).Build<OAuth2AssertionService>(typeof(OAuth2AssertionService));
                oAuth2Request = oAuth2AssertionService.GenerateOAuth2AssertionRequest(ConfigManager.Username);
                return oAuth2Request.Headers;
            }

            return null;
        }

        /// <summary>
        /// Find OAuthType from the URL
        /// </summary>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        private OAuthType GetOAuthType(string rawUrl)
        {
            if (rawUrl.Contains("oauth2"))
                return OAuthType.OAuth2;

            if (rawUrl.Contains("oauth1"))
                return OAuthType.OAuth1;

            return OAuthType.NotDefined;
        }
    }
}