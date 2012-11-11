using System;
using System.Collections;
using System.Collections.Generic;

namespace LibHipChat.Domain.Helpers
{
    public class QueryStringHelper
    {
        private IDictionary _dictionary;

        public int Count
        {
            get { return _dictionary.Count; }
        }

        public String HtmlStringValue
        {
            get { return GetHtmlStringValue(); }
        }

        public String PostStringValue
        {
            get { return GetPostStringValue(); }
        }

        public QueryStringHelper ()
        {
            _dictionary = new Dictionary<string,string>();
        }


        public void Add(string key, string value)
        {                
            if (_dictionary.Contains(key))
            {
                throw new Exception(String.Format(("QueryStringHelper.Add: Key {0} already exists in the dictionary"), key));
            }

            _dictionary.Add(key, value);
        }
        
        private String GetHtmlStringValue ()
        {
            if (_dictionary.Count == 0)
                return String.Empty;

            var htmlString = String.Empty;
            
            foreach (KeyValuePair<String, String> entry in (Dictionary<String, String>)_dictionary)
            {
                if (htmlString.Length == 0)
                    htmlString += "?";
                else
                    htmlString += "&";

                htmlString += String.Format("{0}={1}", entry.Key, entry.Value);                
            }
            return htmlString;
        }

        private String GetPostStringValue()
        {
            if (_dictionary.Count == 0)
                return String.Empty;

            var htmlString = String.Empty;

            foreach (KeyValuePair<String, String> entry in (Dictionary<String, String>)_dictionary)
            {
                if (htmlString.Length > 0)
                    htmlString += "&";

                htmlString += String.Format("{0}={1}", entry.Key, entry.Value);
            }
            return htmlString;
        }
    }
}