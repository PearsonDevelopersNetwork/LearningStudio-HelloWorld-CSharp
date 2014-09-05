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

using System.Net.Http;
using System.Web.Http;
using log4net;
using Com.Pearson.Pdn.Learningstudio.HelloWorld.WebApp.Utils;

namespace Com.Pearson.Pdn.Learningstudio.HelloWorld.WebApp
{
    public class OAuth1Controller : ApiController
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(OAuth2Controller));

        [HttpGet]
        public HttpResponseMessage Get()
        {
            return new CommonHelper().DoMethod(Utils.HttpMethod.GET);
        }

        [HttpPost]
        public HttpResponseMessage Post()
        {
            return new CommonHelper().DoMethod(Utils.HttpMethod.POST);
        }

        [HttpPut]
        public HttpResponseMessage Put()
        {
            return new CommonHelper().DoMethod(Utils.HttpMethod.PUT);
        }

        [HttpDelete]
        public HttpResponseMessage Delete()
        {
            return new CommonHelper().DoMethod(Utils.HttpMethod.DELETE);
        }
    }
}
