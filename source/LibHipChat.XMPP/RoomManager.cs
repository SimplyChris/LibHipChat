using LibHipChat.Domain.Interfaces;
using LibHipChat.XMPP.Containers;
using StructureMap;
using agsXMPP;
using agsXMPP.protocol.x.muc;

namespace LibHipChat.XMPP
{
    public class RoomManager
    {
        private ILogger<RoomManager> _logger; 
        private MucManager _mucManager;
        private Connection _connection;        
        private MessageGrabber _messageGrabber;
        private PresenceGrabber _presenceGrabber;
        private CallBackContainer _callBackContainer;
        private readonly string _roomDomainSuffix = "@conf.hipchat.com";

        public RoomManager(Connection connection)
        {                        
            _logger = ObjectFactory.GetInstance<ILogger<RoomManager>>();            
            _connection = connection;
            _mucManager  = new MucManager (_connection);
            _messageGrabber = new MessageGrabber(connection);
            _presenceGrabber = new PresenceGrabber(connection);
            _callBackContainer = new CallBackContainer();
        }

        public RoomManager (Connection connection, CallBackContainer callBackContainer) : this(connection)
        {
            _callBackContainer = callBackContainer;
        }

        public void JoinRoom(Room room)
        {
            _logger.DebugFormat("   JoinRoom: Id: [{0}] Nickname: [{1}]", room.Id, room.NickName);
            
            if (!room.Id.Contains(_roomDomainSuffix))
                room.Id += _roomDomainSuffix;

            var jid = new Jid(room.Id);                                             
            _mucManager.JoinRoom(jid, room.NickName, true);
            AddRoomToGrabbers(jid, null); 
        }

        public void LeaveRoom(Room room)
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
