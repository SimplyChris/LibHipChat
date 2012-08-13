using System;
using System.Configuration;
using LibHipChat.Domain;
using LibHipChat.Domain.Contracts;
using LibHipChat.Domain.Entities;
using LibHipChat.Proxy;
using LibHipChat.Proxy.Contracts;
using LibHipChat.Services.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace LibHipChat.Services.Tests
{
    [TestFixture]
    public class RoomListenerTests
    {
        private IMessageProcessor _messageProcessor;
        private IRoomListener _roomListener;

        private MockRepository _mockRepository;

        private string apiKey;
        private string apiUrl;
        private IHipChatProxy _proxy;

        [TestFixtureSetUp]
        public void FixtureSetup ()
        {
            apiKey = ConfigurationManager.AppSettings["HipChatApiKey"];
            apiUrl = ConfigurationManager.AppSettings["HipChatApiUrl"];
            _mockRepository= new MockRepository();
        }

        [SetUp]
        public void Setup ()
        {
            _roomListener = new RoomListener();
            _roomListener.SetHipChatProxy(new HipChatProxy(new HipChatConnectionFactory(new HipChatConnectionSettings(apiKey, apiUrl))));           
        }

        [Test]
        public void should_add_new_message_processor ()
        {
            _messageProcessor = _mockRepository.StrictMock<IMessageProcessor>();                     
            _roomListener.AddProcessor(_messageProcessor);

            Assert.That(_roomListener.MessageProcessors.Contains (_messageProcessor), Is.True);
        }
        
        [Test]
        public void should_parse_timestamp ()
        {
            var roomMessage = new RoomMessage();
            
            var input = "2010-11-19T15:48:19-0800";
            var expectedDateTime = new DateTime(2010, 11, 19, 15, 48, 19);

            var outDate = roomMessage.ParseTimestamp(input);
            Assert.That(outDate, Is.EqualTo(expectedDateTime));                        
        }


        [Test, Ignore]
        public void should_process_new_message ()
        {            
            _roomListener.SetHipChatProxy(_proxy);
            _roomListener.RetrieveRecentMessages();
            _messageProcessor = _mockRepository.StrictMock<IMessageProcessor>();
            _roomListener.AddProcessor(_messageProcessor);
                      
        }
    }    
}