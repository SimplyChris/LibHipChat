using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibHipChat.Interfaces;
using LibHipChat.XMPP.Containers;
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
        private MessageGrabber _messageGrabber;
        private PresenceGrabber _presenceGrabber;
        private HipChatXmppCallBackContainer _callBackContainer;
        private readonly string _roomDomainSuffix = "@conf.hipchat.com";

        public HipChatXMPPRoomManager(HipChatXMPPConnection connection)
        {            
            _logger = ObjectFactory.GetInstance<ILogger<HipChatXMPPRoomManager>>();            
            _hipChatXmppConnection = connection;
            _mucManager  = new MucManager (_hipChatXmppConnection);
            _messageGrabber = new MessageGrabber(connection);
            _presenceGrabber = new PresenceGrabber(connection);
            _callBackContainer = new HipChatXmppCallBackContainer();
        }

        public HipChatXMPPRoomManager (HipChatXMPPConnection connection, HipChatXmppCallBackContainer callBackContainer) : this(connection)
        {
            _callBackContainer = callBackContainer;
        }

        public void JoinRoom(HipChatRoom room)
        {
            _logger.DebugFormat("   JoinRoom: Id: [{0}] Nickname: [{1}]", room.Id, room.NickName);
            
            if (!room.Id.Contains(_roomDomainSuffix))
                room.Id += _roomDomainSuffix;

            var jid = new Jid(room.Id);                                             
            _mucManager.JoinRoom(jid, room.NickName, true);
            AddRoomToGrabbers(jid, null); 
        }

        public void LeaveRoom(HipChatRoom room)
        {
            _logger.DebugFormat("   LeaveRoom: Id: [{0}] Nickname: [{1}]", room.Id, room.NickName);
            var jid = new Jid(room.Id);            
            _mucManager.LeaveRoom(jid, room.NickName);
            RemoveRoomFromGrabbers(jid);
        }      
          
        private void AddRoomToGrabbers (Jid jid, object arg)
        {
            if (_callBackContainer.MessageCallBack != null)
                _messageGrabber.Add(jid, _callBackContainer.MessageCallBack, arg);
            

            if (_callBackContainer.PresenceCallBack != null)           
                _presenceGrabber.Add(jid, _callBackContainer.PresenceCallBack, arg);
        }


        private void RemoveRoomFromGrabbers(Jid jid)
        {
            if (_callBackContainer.MessageCallBack != null)
                _messageGrabber.Remove(jid);
            

            if (_callBackContainer.PresenceCallBack != null)
                _presenceGrabber.Remove(jid);
            
        }
    }
}
