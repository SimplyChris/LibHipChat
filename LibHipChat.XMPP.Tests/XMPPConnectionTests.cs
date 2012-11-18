using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;
using agsXMPP.Xml.Dom;

namespace LibHipChat.XMPP.Tests
{
    [TestFixture]
    public class XMPPConnectionTests
    {
        private HipChatXMPPConnection _xmppConnection;
        private readonly string testRoomId = "18167_zencode";

        [TestFixtureSetUp]
        public void Setup ()
        {
            _xmppConnection = new HipChatXMPPConnection(new HipChatXmppConnectionSettings() { UserName = "13264_192249", Password = "botuser123", Server = "conf.hipchat.com" });            
            _xmppConnection.ClientConnection.OnError += ClientConnectionOnOnError;
            _xmppConnection.ClientConnection.OnAuthError += ClientConnectionOnOnAuthError;
            _xmppConnection.ClientConnection.OnLogin += ClientConnectionOnOnLogin;
            _xmppConnection.ClientConnection.OnSocketError += ClientConnectionOnOnSocketError;
            _xmppConnection.ClientConnection.OnBinded += ClientConnectionOnOnBinded;
            _xmppConnection.ClientConnection.OnRegistered += ClientConnectionOnOnRegistered;
            _xmppConnection.ClientConnection.OnRegisterError += ClientConnectionOnOnRegisterError;
            _xmppConnection.ClientConnection.OnStreamError += ClientConnectionOnOnStreamError;

        }

        private void ClientConnectionOnOnStreamError(object sender, Element element)
        {
            Console.WriteLine("XMPP Stream Error of type {0}. ", element.GetType());
            Console.WriteLine(element.ToString());
        }

        private void ClientConnectionOnOnRegisterError(object sender, Element element)
        {
            Console.WriteLine("XMPP Register Error of type {0}. ", element.GetType());
            Console.WriteLine(element.ToString());
        }

        private void ClientConnectionOnOnRegistered(object sender)
        {
            Console.WriteLine("Registered");
        }

        private void ClientConnectionOnOnBinded(object sender)
        {
            Console.WriteLine("Binded!");
        }

        private void ClientConnectionOnOnSocketError(object sender, Exception ex)
        {
            Console.WriteLine("XMPP Socket Exception of type {0}. ", ex.GetType());
            Console.WriteLine(ex.Message);
        }

        private void ClientConnectionOnOnLogin(object sender)
        {
            Console.WriteLine("Logged In!");
        }

        private void ClientConnectionOnOnAuthError(object sender, Element element)
        {
            Console.WriteLine("XMPP Exception of type {0}. ", element.GetType());
            Console.WriteLine(element.ToString());
        }

        private void ClientConnectionOnOnError(object sender, Exception ex)
        {
            Console.WriteLine("XMPP Exception of type {0}. ", ex.GetType());
            Console.WriteLine(ex.Message);
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
        public void should_call_connect_open_event_handler_after_connection_opened()
        {
            _xmppConnection.ClientConnection.OnLogin += ConnectOpenEvent;
            Console.WriteLine("Opening Connection");
            _xmppConnection.OpenConnection(testRoomId);
            while (0 == 0)
            {
                
            }
        }


        [Test]
        public void call_tests ()
        {
                         
        }

        public void ConnectOpenEvent(object sender)
        {
            
        }
    }

}
