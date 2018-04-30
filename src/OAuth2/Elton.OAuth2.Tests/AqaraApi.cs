using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elton.OAuth2.Tests
{
    public class AqaraApi : ApiClient
    {
        public AqaraApi(ApiConfiguration config)
            : base(config)
        {
        }

        public string GetUser(string openId)
        {
            var responseSample = new
            {
                result = new
                {
                    userInfo = new
                    {
                        nickName = "绿藻头"
                    },
                    openId = "xxx"
                },
                code = 0,
                isBytesData = 0,
                requestId = "xxx"
            };

            var response = Post<dynamic>("/user/query",
                headerParams: new[] {
                    new KeyValuePair<string, string>("Appid", config.ApplicationId),
                    new KeyValuePair<string, string>("Appkey", config.ApplicationSecret),
                    new KeyValuePair<string, string>("Openid", openId),
                    new KeyValuePair<string, string>("Access-Token", token),
                },
                contentType: "application/json",
                postBody: JsonConvert.SerializeObject(new { openId, }));

            if (response.code != 0)
                throw new ApiException((int)response.code, (string)response.result);

            return response.result.userInfo.nickName;
        }
    }
}
