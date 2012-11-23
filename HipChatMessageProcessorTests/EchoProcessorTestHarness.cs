using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using HipChatMessageProcessor.Processors;
using LibHipChat.Domain;
using LibHipChat.Domain.Contracts;
using LibHipChat.Domain.Services;
using LibHipChat.Domain.Services.Interfaces;
using LibHipChat.Proxy;
using LibHipChat.Proxy.Contracts;
using LibHipChat.Services.Contracts;
using NUnit.Framework;
using Rhino.Mocks;

namespace HipChatMessageProcessorTests
{
    [TestFixture]
    public class EchoProcessorTestHarness 
    {
        private IMessageProcessor _messageProcessor;
        private IRoomListener _roomListener;
        private string _roomId = "52403";
        private MockRepository _mockRepository;

        private string apiKey;
        private string apiUrl;
        

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            apiKey = ConfigurationManager.AppSettings["HipChatApiKey"];
            apiUrl = ConfigurationManager.AppSettings["HipChatApiUrl"];
            _mockRepository = new MockRepository();
        }

        [SetUp]
        public void Setup()
        {
            _roomListener = new RoomListener(new HipChatProxy(new HipChatConnectionFactory(new HipChatConnectionSettings(apiUrl, apiKey))));            
            _roomListener.SetRoomId(_roomId);
            _roomListener.GetHipChatProxy().MessageRoom(_roomId, "api", "Echo Processor Test harness Starting");
        }
        

        [Test,Ignore ("This is just a test harness that loops")]
        public void echo_processor_message_loop ()
        {
            var sleep_delay = 3000;
            var echoProcessor =new EchoProcessor();
            echoProcessor.SetMessageTypeFilter(new List<RoomMessageType>()
                                                   {RoomMessageType.UserMessage, RoomMessageType.FileUpload});
            _roomListener.AddProcessor(echoProcessor);

            var _proxy = _roomListener.GetHipChatProxy();
            while (true)
            {
                Thread.Sleep(3000);
                _roomListener.ProcessNewMessages();

                var message = String.Format("@all (boom) Calls Remaining: {0}", _proxy.ApiCallsRemaining);
//                _proxy.MessageRoom(_roomId, "test harness", message, MessageFormat.Text);
                Console.WriteLine(message);//_roomId, "test harness", message, MessageFormat.Text);
            }
        }
    }
}
