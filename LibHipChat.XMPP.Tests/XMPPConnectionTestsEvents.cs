using System;
using agsXMPP.Xml.Dom;
using agsXMPP.protocol.client;

namespace LibHipChat.XMPP.Tests
{
    public partial class XMPPConnectionTests
    {

        public void SetupEvents()
        {
            _xmppConnection.ClientConnection.OnError += ClientConnectionOnOnError;
            _xmppConnection.ClientConnection.OnAuthError += ClientConnectionOnOnAuthError;
            _xmppConnection.ClientConnection.OnLogin += ClientConnectionOnOnLogin;
            _xmppConnection.ClientConnection.OnSocketError += ClientConnectionOnOnSocketError;
            _xmppConnection.ClientConnection.OnBinded += ClientConnectionOnOnBinded;
            _xmppConnection.ClientConnection.OnRegistered += ClientConnectionOnOnRegistered;
            _xmppConnection.ClientConnection.OnRegisterError += ClientConnectionOnOnRegisterError;
            _xmppConnection.ClientConnection.OnStreamError += ClientConnectionOnOnStreamError;
            _xmppConnection.ClientConnection.OnClose += ClientConnectionOnOnClose;
            _xmppConnection.ClientConnection.OnIq += ClientConnectionOnOnIq;
            _xmppConnection.ClientConnection.OnMessage += ClientConnectionOnOnMessage;
        }
        private void ClientConnectionOnOnMessage(object sender, Message msg)
        {
            Console.WriteLine("OnMessessage Called -- From {0} Message: {1}", msg.From, msg.Body);
        }

        private void ClientConnectionOnOnIq(object sender, IQ iq)
        {
            Console.WriteLine("ON Iq Called {0}", iq);
        }


        private void ClientConnectionOnOnClose(object sender)
        {
            Console.WriteLine("Closing Client Connection");
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
            LogonEventWasCalled = true;
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
    }
}