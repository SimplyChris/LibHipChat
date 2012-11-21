using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibHipChat.Interfaces;
using StructureMap;
using agsXMPP;
using agsXMPP.protocol.Base;
using agsXMPP.protocol.client;
using agsXMPP.protocol.x.muc;

namespace LibHipChat.XMPP
{
    public delegate void ConnectEventHandler(object sender, EventArgs e);
    public delegate void MessageReceivedEventHandler(object sender, Message message);
    
    public class HipChatXMPPConnection : XmppClientConnection
    {
        public bool Connected { get; set; }
        private HipChatXmppConnectionSettings ConnectionSettings { get; set; }
        private LibHipChat.Interfaces.ILogger<HipChatXMPPConnection> _logger;

        private MucManager _mucManager;
        //public XmppClientConnection ClientConnection { get; set; }

        public IList<HipChatRoom> RoomList { get { return _roomList; } }

        private IList<HipChatRoom> _roomList; 

        public event MessageReceivedEventHandler OnMessageReceived;

        public HipChatXMPPConnection (HipChatXmppConnectionSettings settings) : base (settings.Server)
        {
            _logger = IoC.IocContainer.GetInstance<ILogger<HipChatXMPPConnection>>();
            ConnectionSettings = settings;            
            UseSSL = true;
            UseStartTLS = true;
            OnError += ReportError;            
            _roomList = new List<HipChatRoom>();
        }

        public void OpenConnection ()
        {                        
            base.Open(ConnectionSettings.UserName, ConnectionSettings.Password);                  
            base.OnLogin += OnLoginEventHandler;
            base.OnMessage += ClientConnectionOnOnMessage;
        }

        private void ClientConnectionOnOnMessage(object sender, Message msg)
        {
            if (String.IsNullOrWhiteSpace(msg.Body))
                return;

            if (OnMessageReceived != null)
                OnMessageReceived(this, msg);
        }

        public void OnLoginEventHandler(object sender)
        {
            _logger.DebugFormat("   OnLoginEventHandler Called");
            _mucManager = new MucManager(this);
            
            foreach (HipChatRoom hipChatRoom in RoomList)
            {
                var jid = new Jid(hipChatRoom.Id);
                _mucManager.AcceptDefaultConfiguration(jid);    
                _mucManager.JoinRoom (jid, hipChatRoom.NickName, true);
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
     
        private void AddRosterItem (object source, RosterItem rosterItem )
        {
            
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
    }
}
