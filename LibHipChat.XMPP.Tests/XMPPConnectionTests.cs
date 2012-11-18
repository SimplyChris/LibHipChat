using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        [TestFixtureSetUp]
        public void Setup ()
        {
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
           
            Assert.DoesNotThrow(()=>_xmppConnection.OpenConnection(testRoomId));            
            Thread.Sleep(10000);            
        }

        [Test]
        public void should_call_logon_event_event_handler_after_connection_opened()
        {
            _xmppConnection.ClientConnection.OnLogin += ConnectOpenEvent;
            Console.WriteLine("Opening Connection");
            _xmppConnection.OpenConnection(testRoomId);
            Thread.Sleep(2000);
            Assert.That(LogonEventWasCalled, Is.EqualTo(true));
        }


        [Test]
        public void call_tests ()
        {
                         
        }

        public void ConnectOpenEvent(object sender)
        {
            Console.WriteLine("Connection Open Event Fired");
        }
    }

}
