﻿using System;
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
            _context = new HipChatContext(_baseApiUrl, _apiKey, ActionKey.ListUsers);
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
            _queryStringHelper.Add("format", "xml");
        }

        [Test]
        public void querystringhelper_should_return_correct_query_string_suffix ()
        {
            const string expectedValue = "?auth_token=TheApiKey&format=xml";

            _queryStringHelper.Add("auth_token","TheApiKey");
            _queryStringHelper.Add("format", "xml");

            Assert.That(_queryStringHelper.HtmlStringValue, Is.EqualTo(expectedValue));

        }

        [Test]
        public void hipchatcontext_should_build_correct_query_string_suffic ()
        {
            const string expectedValue = "?format=xml&auth_token=TheApiKey";

            var queryString = _context.BuildQueryString();
            Assert.That(queryString, Is.EqualTo(expectedValue));


        }
    }
}