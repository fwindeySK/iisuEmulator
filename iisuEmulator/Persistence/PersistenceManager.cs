using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iisuEmulator.Mappings;
using System.Xml;
using iisuEmulator.Emulators;
using System.IO;

namespace iisuEmulator.Persistence
{
    public class PersistenceManager
    {
        public PersistenceManager()
        {

        }

        private String MakeRelativePath(String fromPath, String toPath)
        {
            if (String.IsNullOrEmpty(fromPath)) throw new ArgumentNullException("fromPath");
            if (String.IsNullOrEmpty(toPath)) throw new ArgumentNullException("toPath");

            Uri fromUri = new Uri(fromPath);
            Uri toUri = new Uri(toPath);

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            String relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            return relativePath.Replace('/', Path.DirectorySeparatorChar);
        }

        public void StoreProject(IMapping[] mappings, string IIDProjectPath, IStartStopMapping startStopMapping, string storePath)
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlNode decleration = xmlDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            XmlNode root = xmlDoc.CreateElement("EmulatorProject");
            xmlDoc.AppendChild(decleration);

            IIDProjectPath = MakeRelativePath(Directory.GetCurrentDirectory() + "\\", IIDProjectPath);

            XmlNode iidProjectNode = xmlDoc.CreateElement("IIDProjectPath");
            XmlNode iidProjectPath = xmlDoc.CreateTextNode(IIDProjectPath);
            iidProjectNode.AppendChild(iidProjectPath);
            root.AppendChild(iidProjectNode);

            XmlNode startStopMappingNode = xmlDoc.CreateElement("StartStopMapping");
            XmlNode startStopValue = xmlDoc.CreateTextNode(startStopMapping == null ? "NONE" : startStopMapping.IIDOutputPath);
            startStopMappingNode.AppendChild(startStopValue);
            root.AppendChild(startStopMappingNode);

            XmlNode mappingsCollection = xmlDoc.CreateElement("Mappings");

            foreach (IMapping mapping in mappings)
            {
                //-----Mapping-----
                XmlNode mappingNode = xmlDoc.CreateElement("Mapping");

                //-----IID Output-----
                XmlNode iidOutput = xmlDoc.CreateElement("Name");
                XmlNode iidOutputName = xmlDoc.CreateTextNode(mapping.IIDOutputPath);
                iidOutput.AppendChild(iidOutputName);
                mappingNode.AppendChild(iidOutput);

                //-----Emulator-----
                XmlNode emulator = xmlDoc.CreateElement("Emulator");
                XmlNode emulatorName = null;
                if (mapping.Emulator != null)
                {
                    emulatorName = xmlDoc.CreateTextNode(mapping.Emulator.Name);
                }
                else
                {
                    emulatorName = xmlDoc.CreateTextNode("NONE");
                }
                emulator.AppendChild(emulatorName);
                mappingNode.AppendChild(emulator);

                //-----Emulated data source-----
                XmlNode dataSource = xmlDoc.CreateElement("DataSource");
                XmlNode dataSourceId = null;
                if (mapping.Emulator != null && mapping.DataSourceId >= 0)
                {
                    dataSourceId = xmlDoc.CreateTextNode(mapping.DataSourceId.ToString());
                }
                else
                {
                    dataSourceId = xmlDoc.CreateTextNode("-1");
                }
                dataSource.AppendChild(dataSourceId);
                mappingNode.AppendChild(dataSource);

                mappingsCollection.AppendChild(mappingNode);
            }

            root.AppendChild(mappingsCollection);
            xmlDoc.AppendChild(root);
            xmlDoc.Save(storePath);
        }

        public string GetIIDProjectPath(string emulatorProjectPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(emulatorProjectPath);

            XmlNode iidProjectPath = xmlDoc.SelectSingleNode("/EmulatorProject/IIDProjectPath");
            if (!Path.IsPathRooted(iidProjectPath.InnerText))
            {
                return Directory.GetCurrentDirectory() + "\\" + iidProjectPath.InnerText;
            }
            else
            {
                return iidProjectPath.InnerText;
            }
        }

        public string GetStartStopMappingName(string emulatorProjectPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(emulatorProjectPath);

            XmlNode startStopMappingNode = xmlDoc.SelectSingleNode("/EmulatorProject/StartStopMapping");

            return startStopMappingNode.InnerText;
        }

        public void UpdateMappings(string emulatorProjectPath, MappingManager mappingManager, EmulatorManager emulatorManager)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(emulatorProjectPath);

            foreach (XmlNode mappingNode in xmlDoc.SelectNodes("/EmulatorProject/Mappings/Mapping"))
            {
                string mappingName = mappingNode.SelectSingleNode("Name").InnerText;
                IMapping mapping = mappingManager.GetMapping(mappingName);

                string emulatorName = mappingNode.SelectSingleNode("Emulator").InnerText;
                if(emulatorName != "NONE")
                {
                    IEmulator emulator = emulatorManager.GetEmulator(emulatorName);
                    mapping.Emulator = emulator;
                }
                else
                {
                    mapping.Emulator = null;
                }

                if (mapping.Emulator != null)
                {
                    int dataSource = int.Parse(mappingNode.SelectSingleNode("DataSource").InnerText);
                    mapping.DataSourceId = dataSource;
                }
            }
        }

    }
}
