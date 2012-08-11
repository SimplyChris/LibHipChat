using LibHipChat.Domain.Contracts;
using LibHipChat.Services.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace LibHipChat.Services.Tests
{
    [TestFixture]
    public class RoomListenerTests
    {
        private IMessageProcessor _messageProcessor;
        private IRoomListener _roomListener;

        private MockRepository _mockRepository;

        [TestFixtureSetUp]
        public void FixtureSetup ()
        {
            _mockRepository= new MockRepository();

        }

        [SetUp]
        public void Setup ()
        {
            _roomListener = new RoomListener();
        }


        [Test]
        public void should_add_new_message_processor ()
        {
            _messageProcessor = _mockRepository.StrictMock<IMessageProcessor>();                     
            _roomListener.AddProcessor(_messageProcessor);

            Assert.That(_roomListener.MessageProcessors.Contains (_messageProcessor), Is.True);
        }
    }    
}