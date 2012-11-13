using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace LibHipChat.WindowsService.Configuration
{
    public class ServiceConfigurationManager
    {
        public String ConfigurationFileName { get; set; }
        
        public ServiceConfigurationManager (string configFileName)
        {
            ConfigurationFileName = configFileName;
        }

 

        public IList<ProcessorConfiguration> ReadConfiguration ()
        {
            var xdoc = XDocument.Load(ConfigurationFileName);
            var configElement = xdoc.Element("configuration");


            if (!configElement.HasElements)
                return null;

            var configurationList = new List<ProcessorConfiguration>();
            var currentNode = (XElement)configElement.FirstNode;
            
            do
            {
                var configuration = new ProcessorConfiguration();

                configuration.RoomId = currentNode.Attribute("processor.type").Value;
                configuration.RoomId = currentNode.Attribute("room").Value;

                
                var tmpString = currentNode.Attribute("buffer.time.seconds").Value;
                Int32 bufferTime;
                Int32.TryParse(tmpString, out bufferTime);
                configuration.BufferTime = bufferTime;
                configurationList.Add(configuration);
                currentNode = (XElement) currentNode.NextNode;
            } while (currentNode != null);


            return configurationList;
        }
    }
}