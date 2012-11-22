using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibHipChat.Interfaces;
using LibHipChat.XMPP.Utility;
using StructureMap;
using agsXMPP;
using agsXMPP.protocol.Base;
using agsXMPP.protocol.client;
using agsXMPP.protocol.x.muc;

namespace LibHipChat.XMPP
{
    public delegate void ConnectEventHandler(object sender, EventArgs e);
    public delegate void MessageReceivedEventHandler(object sender, Message message);
    public delegate void DirectMessageReceivedEventHandler(object sender, XmppMessage message);
    public delegate void RoomMessageReceivedEventHandler(object sender, XmppMessage message);

    public class HipChatXMPPConnection : XmppClientConnection
    {
        public event DirectMessageReceivedEventHandler OnDirectMessageReceived;
        public event DirectMessageReceivedEventHandler OnRoomMessageReceived;

        public bool Connected { get; set; }
        private HipChatXmppConnectionSettings ConnectionSettings { get; set; }
        private ILogger<HipChatXMPPConnection> _logger;        

        public IList<HipChatRoom> RoomList { get { return _roomList; } }

        private IList<HipChatRoom> _roomList; 

        public event MessageReceivedEventHandler OnMessageReceived;

        public HipChatXMPPConnection (HipChatXmppConnectionSettings settings) : base (settings.Server)
        {
            _logger = IoC.IocContainer.GetInstance<ILogger<HipChatXMPPConnection>>();
            ConnectionSettings = settings;            
            UseSSL = true;
            UseStartTLS = true;
            AutoRoster = true;
            OnError += ReportError;            
            _roomList = new List<HipChatRoom>();               
        }

        public void OpenConnection ()
        {                        
            if (Connected)
                throw new Exception("Call Close Connection Before Re-Calling Open Connection");
            base.Open(ConnectionSettings.UserName, ConnectionSettings.Password);                  
            base.OnLogin += OnLoginEventHandler;
            base.OnMessage += ClientConnectionOnOnMessage;
        }

        private void ClientConnectionOnOnMessage(object sender, Message msg)
        {
            if (String.IsNullOrWhiteSpace(msg.Body))
                return;

            var xmppMessage = XmppMessageFactory.Create(msg);

            switch (xmppMessage.MessageType)
            {
                case XmppMessageType.DirectMessage:
                    InvokeDirectMessageReceived(xmppMessage);
                    break;

                case XmppMessageType.RoomMessage:
                    InvokeRoomMessageReceived(xmppMessage);
                    break;

                case XmppMessageType.UnKnown:
                    throw new NotImplementedException();
                    break;

            }

            if (OnMessageReceived != null)
                OnMessageReceived(this, msg);
        }

        public void OnLoginEventHandler(object sender)
        {
            _logger.DebugFormat("   OnLoginEventHandler Called");            
            
            foreach (HipChatRoom hipChatRoom in RoomList)
            {
            }

        }



        public void ReportError(object sender, Exception ex)
        {
            Console.WriteLine("XMPP Exception of type {0}. ", ex.GetType());
            Console.WriteLine(ex.Message);
        }

        public void CloseConnection ()
        {
            base.Close();
        }    

        public override void SocketOnConnect(object sender)
        {
            Connected = true;
            base.SocketOnConnect(sender);
        }

        public override void SocketOnDisconnect(object sender)
        {
            Connected = false;
            base.SocketOnDisconnect(sender);
        }


        protected virtual void InvokeRoomMessageReceived(XmppMessage message)
        {
            DirectMessageReceivedEventHandler handler = OnRoomMessageReceived;
            if (handler != null) handler(this, message);
        }

        protected virtual void InvokeDirectMessageReceived(XmppMessage message)
        {
            DirectMessageReceivedEventHandler handler = OnDirectMessageReceived;
            if (handler != null) handler(this, message);
        }
    }
}
