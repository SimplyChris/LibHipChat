using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace LibHipChat.XMPP.Tests
{
    [TestFixture]
    public class ConnectionTests
    {
        private HipChatXMPPConnection _xmppConnection;
        

        [TestFixtureSetUp]
        public void Setup ()
        {
            _xmppConnection = new HipChatXMPPConnection(new HipChatXmppConnectionSettings() { UserName = "13264_192249", Password = "botuser123", Server = "conf.hipchat.com" });            
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
            Thread.Sleep(10000);            
        }

        [Test]
        public void should_call_connect_open_event_handler_after_connection_opened()
        {            
            _xmppConnection.OpenConnection();
            Thread.Sleep(2000);
        }


        [Test]
        public void call_tests ()
        {
                         
        }

        public void ConnectOpenEvent(object sender, EventArgs e)
        {            
        }
    }

}
