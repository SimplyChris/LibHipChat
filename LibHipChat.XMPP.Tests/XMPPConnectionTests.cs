using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using LibHipChat.Interfaces;
using LibHipChat.IoC;
using LibHipChat.Services;
using NUnit.Framework;
using agsXMPP.Xml.Dom;
using agsXMPP.protocol.client;

namespace LibHipChat.XMPP.Tests
{
    [TestFixture]
    public partial class XMPPConnectionTests
    {
        private HipChatXMPPConnection _xmppConnection;
        private bool LogonEventWasCalled = false;
        private readonly string testRoomId = "18167_zencode";
        private ILogger<XMPPConnectionTests> _logger;


        [TestFixtureSetUp]
        public void Setup ()
        {
            IocContainer.Configure();
            _logger = IocContainer.GetInstance<ILogger<XMPPConnectionTests>>();
            
            _xmppConnection = new HipChatXMPPConnection(new HipChatXmppConnectionSettings() { UserName = "18167_192253", Password = "botuser123", Server = "chat.hipchat.com" });            
            SetupEvents();
            
        }



        [TestFixtureTearDown]
        public void TearDown()
        {           
            _xmppConnection.CloseConnection();
        }


        [Test]
        public void should_not_throw_exception_when_opening_connection ()
        {           
            Assert.DoesNotThrow(()=>_xmppConnection.OpenConnection());                                    
        }

        [Test]
        public void should_call_logon_event_event_handler_after_connection_opened()
        {
            _xmppConnection.OnLogin += ConnectOpenEvent;
            Console.WriteLine("Opening Connection");
            _xmppConnection.OpenConnection();
            Thread.Sleep(5000);
            Assert.That(LogonEventWasCalled, Is.EqualTo(true));
        }

        [Test]
        public void should_throw_exception_when_reconnection_without_disconnecting()
        {
            
        }

        [Test]
        public void should_auto_join_room()
        {
            _xmppConnection.OpenConnection();
            Thread.Sleep(10000);
        }

        [Test]
        public void call_tests ()
        {
                         
        }

        public void ConnectOpenEvent(object sender)
        {
            Console.WriteLine("Connection Open Event Fired");
            _logger.Debug("Test Event");
        }
    }

}
