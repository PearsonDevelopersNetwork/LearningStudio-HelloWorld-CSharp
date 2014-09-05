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
using System.Linq;
using System.Web.Http;

namespace Com.Pearson.Pdn.Learningstudio.HelloWorld.WebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "",
                routeTemplate: "{controller}/{param1}/{param2}/{param3}/{param4}/{param5}/{param6}/{param7}/{param8}/{param9}/{param10}/{param11}/{param12}/{param13}/{param14}",
                defaults: new { param1 = RouteParameter.Optional, 
                                param2 = RouteParameter.Optional,
                                param3 = RouteParameter.Optional,
                                param4 = RouteParameter.Optional,
                                param5 = RouteParameter.Optional,
                                param6 = RouteParameter.Optional,
                                param7 = RouteParameter.Optional,
                                param8 = RouteParameter.Optional,
                                param9 = RouteParameter.Optional,
                                param10 = RouteParameter.Optional,
                                param11 = RouteParameter.Optional,
                                param12 = RouteParameter.Optional,
                                param13 = RouteParameter.Optional,
                                param14 = RouteParameter.Optional
                            }
            );

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            //config.EnableSystemDiagnosticsTracing();
        }
    }
}
