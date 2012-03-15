using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibHipChat.Contracts;

namespace LibHipChat.Proxy
{
    class JsonModel <T> : IJsonModel<T>
    {
        public T Data { get; set; }

        public void DeserializeList ()
        {
            
        }
    }
}
