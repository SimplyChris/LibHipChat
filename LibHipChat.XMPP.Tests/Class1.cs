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

        [Test]
        public void should_not_throw_exception_when_opening_connection ()
        {

            Assert.DoesNotThrow(()=>_xmppConnection.OpenConnection());
            _xmppConnection.GetRoster();
            Thread.Sleep(10000);
            _xmppConnection.CloseConnection();
        }
    }

}
