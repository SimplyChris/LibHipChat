using System.Collections.Generic;
using LibHipChat.Domain.Contracts;
using LibHipChat.Domain.Proxy;
using Newtonsoft.Json;

namespace LibHipChat.Domain.Entities
{
    public class JsonErrorModel : IJsonModel <IDictionary<string, string>>
    {
        [JsonProperty("error")]
        public IDictionary<string, string> Data { get; set; }

        public ErrorModel ErrorModel;

        public void DeserializeModel()
        {
            ResultCode resultCode;

            ResultCode.TryParse(Data["code"], out resultCode);
            ErrorModel = new ErrorModel()
            {
                ErrorResult = resultCode,
                Message = Data["message"],
                ErrorType = Data["type"]
            };
        }
    }
}