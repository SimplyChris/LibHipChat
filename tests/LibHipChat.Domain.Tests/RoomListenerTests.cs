using System;
using System.Collections.Generic;
using System.Configuration;
using HipChatMessageProcessor.Processors;
using LibHipChat.Domain.Entities;
using LibHipChat.Domain.Interfaces;
using LibHipChat.Domain.Services;
using NUnit.Framework;
using Rhino.Mocks;

namespace LibHipChat.Domain.Tests
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
            _roomListener = new RoomListener(new HipChatProxy(new HipChatConnectionFactory(new HipChatConnectionSettings(apiUrl, apiKey)), new HipChatApiExecutor()));            
            _roomListener.GetHipChatProxy().MessageRoom("52403", "api", "Room Listener Test Message");
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

        [Test]
        public void should_get_only_new_messages ()
        {
            var roomId = "52403";
            _roomListener.SetRoomId(roomId);

            _roomListener.ProcessNewMessages();
            var echoProcessor = new EchoProcessor();
            echoProcessor.SetMessageTypeFilter(new List<RoomMessageType>(){RoomMessageType.ApiMessage});
            _roomListener.AddProcessor(echoProcessor);
            
            _roomListener.GetHipChatProxy().MessageRoom(roomId, "api", "New Message");
            _roomListener.ProcessNewMessages();
            

            //Assert.That(newMessages.Count,Is.EqualTo(1));
        }
    }    
}