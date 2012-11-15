using System.Configuration;
using LibHipChat.WindowsService.Configuration;
using NUnit.Framework;

namespace LibHipChat.WindowsService.Tests
{
    [TestFixture]
    public class ServiceConfigurationManagerTests
    {
        public ServiceConfigurationManager configManager;
        [SetUp]
        public void Setup ()
        {
            configManager = new ServiceConfigurationManager("ProcessorConfiguration.xml");
        }

        [Test]
        public void should_populate_processor_configuration_list ()
        {
            var configs = configManager.ReadConfiguration();

            Assert.That(configs.Count, Is.EqualTo(2));
            Assert.That(configs[0].BufferTime, Is.EqualTo(60));
            Assert.That(configs[0].RoomId, Is.EqualTo("1221"));
            Assert.That(configs[1].RoomId, Is.EqualTo("111221"));
        }
    }
}