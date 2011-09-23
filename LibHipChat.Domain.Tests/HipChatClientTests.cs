using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibHipChat.Helpers;
using NUnit;
using NUnit.Framework;
using NUnit.Framework.Constraints;


namespace LibHipChat.Domain.Tests
{
    [TestFixture]
    public class HipChatClientTests
    {
        private QueryStringHelper _queryStringHelper;

        [SetUp]
        public void Setup ()
        {
            _queryStringHelper = new QueryStringHelper();
        }

        [Test]
        public void querystringhelper_should_throw_exception_when_adding_duplicate_key ()
        {

            _queryStringHelper.Add("auth_token", "12345");           
                        
            Assert.Throws(typeof(Exception),() => _queryStringHelper.Add("auth_token", "56789"));
        }

        [Test]
        public void querystringhelper_should_add_multiple_unique_keys ()
        {

            _queryStringHelper.Add("auth_token", "1234");
            _queryStringHelper.Add("format", "xml");
        }

        [Test]
        public void querystringhelper_should_return_correct_query_string_suffix ()
        {
            const string expectedValue = "?auth_token=12345&format=xml";

            _queryStringHelper.Add("auth_token","12345");
            _queryStringHelper.Add("format", "xml");

            Assert.That(_queryStringHelper.HtmlStringValue, Is.EqualTo(expectedValue));

        }
    }
}
