using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibHipChat.Interfaces;
using StructureMap;
using agsXMPP;
using agsXMPP.protocol.x.muc;
using log4net.Core;

namespace LibHipChat.XMPP
{
    public class HipChatXMPPRoomManager
    {
        private ILogger<HipChatXMPPRoomManager> _logger; 
        private MucManager _mucManager;
        private HipChatXMPPConnection _hipChatXmppConnection;
    
        public HipChatXMPPRoomManager(HipChatXMPPConnection connection)
        {
            _logger = ObjectFactory.GetInstance<ILogger<HipChatXMPPRoomManager>>();            
            _hipChatXmppConnection = connection;
            _mucManager  = new MucManager (_hipChatXmppConnection);
        }

        public void JoinRoom(HipChatRoom room)
        {
            _logger.DebugFormat("   JoinRoom: Id: [{0}] Nickname: [{1}]", room.Id, room.NickName);
            var jid = new Jid(room.Id);            
            _mucManager.AcceptDefaultConfiguration(jid);            
            _mucManager.JoinRoom(jid, room.NickName, true);            
        }

        public void LeaveRoom(HipChatRoom room)
        {
            _logger.DebugFormat("   LeaveRoom: Id: [{0}] Nickname: [{1}]", room.Id, room.NickName);
            var jid = new Jid(room.Id);
            _mucManager.LeaveRoom(jid, room.NickName);            
        }
    }
}
