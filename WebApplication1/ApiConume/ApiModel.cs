using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebApplication1.Config;

namespace WebApplication1.ApiConume
{
    public class ApiModel
    {
        public async Task<OutputModel> Post<OutputModel, InputModel>(String endpoint, InputModel model)
        {
          //  var claimsprincipal = new System.Security.Claims.ClaimsPrincipal();
            //WebUser CurrentUser = new WebUser(claimsprincipal);

            using (var client = new HttpClient())
            {
                //setup client
                client.BaseAddress = new Uri(GlobalConstant.ApiDomainUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Add("Authorization", CurrentUser.BearerToken);

                var input = JsonConvert.SerializeObject(model);
                var content = new StringContent(input, Encoding.UTF8, "application/json");

                var response = client.PostAsync(endpoint.ToString(), content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<OutputModel>(result);
                }
                else
                    return default(OutputModel);

            }
        }
    }
}