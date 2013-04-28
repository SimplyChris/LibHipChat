using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibHipChat.Domain.Constants;
using LibHipChat.Domain.Helpers;
using NUnit;
using NUnit.Framework;
using NUnit.Framework.Constraints;


namespace LibHipChat.Domain.Tests
{
    [TestFixture]
    public class HipChatContextTests
    {
        private QueryStringHelper _queryStringHelper;
        private HipChatContext _context;
        private const string _apiKey = "TheApiKey";
        private const string _baseApiUrl = "https://api.hipchat.com/v1/";

        [SetUp]
        public void Setup ()
        {            
            _queryStringHelper = new QueryStringHelper();
            _context = new HipChatContext(_baseApiUrl, _apiKey, ActionKey.ListUsers,new Dictionary<string, string>());
        }

        [Test]
        public void querystringhelper_should_have_count_set ()
        {
            _queryStringHelper.Add("auth_token", "ApiKey");
            _queryStringHelper.Add("action", "FakeAction");

            Assert.That(_queryStringHelper.Count, Is.EqualTo(2));
        }

        [Test]
        public void querystringhelper_should_throw_exception_when_adding_duplicate_key ()
        {

            _queryStringHelper.Add("auth_token", "TheApiKey");                     
                        
            Assert.Throws(typeof(Exception),() => _queryStringHelper.Add("auth_token", "56789"));
        }

        [Test]
        public void querystringhelper_should_add_multiple_unique_keys ()
        {

            _queryStringHelper.Add("auth_token", "TheApiKey");
            _queryStringHelper.Add("format", "json");
        }

        [Test]
        public void querystringhelper_should_return_correct_query_string_suffix ()
        {
            const string expectedValue = "?auth_token=TheApiKey&format=json";

            _queryStringHelper.Add("auth_token","TheApiKey");
            _queryStringHelper.Add("format", "json");

            Assert.That(_queryStringHelper.HtmlStringValue, Is.EqualTo(expectedValue));

        }

        [Test]
        public void hipchatcontext_should_build_correct_query_string_suffic ()
        {
            const string expectedValue = "?format=json&auth_token=TheApiKey";

            var queryString = _context.BuildQueryString();
            Assert.That(queryString, Is.EqualTo(expectedValue));


        }
    }
}
