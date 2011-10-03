using System;
using System.IO;

namespace LibHipChat
{
    public class HipChatConnectionReader
    {
        private HipChatConnection _connection;

        public HipChatConnectionReader (HipChatConnection connection)
        {
             _connection = connection;
        }

        public String GetResponseString ()
        {
            var reader = new StreamReader(_connection.GetResponseStream());

            var responseString = reader.ReadToEnd();
            return responseString;
        }
    }
}