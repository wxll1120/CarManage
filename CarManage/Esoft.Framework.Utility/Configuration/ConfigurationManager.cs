using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Web;

namespace Esoft.Framework.Utility.Configuration
{
    public sealed class ConfigurationManager
    {
        private List<IConfigurationSection> sections;

        private static readonly ConfigurationManager instance =
            new ConfigurationManager();

        private XmlDocument document = new XmlDocument();

        private ConfigurationManager()
        {
            sections = new List<IConfigurationSection>();
        }

        static ConfigurationManager()
        {

        }

        public static ConfigurationManager Instance
        {
            get
            {
                return instance;
            }
        }

        public void Init()
        {
            UpdateConnectionStrings();

            string configPath =
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                System.Configuration.ConfigurationManager.AppSettings["ConfigFile"]);

            if (!File.Exists(configPath))
                throw new Exception("加载配置文件失败，无法找到文件路径!");

            LoadApplicationConfiguration(configPath);
        }

        private void UpdateConnectionStrings()
        {
            string appType =
                System.Configuration.ConfigurationManager.AppSettings["AppType"];

            System.Configuration.Configuration config;
            string appPath = string.Empty;

            if (appType.Equals(AppType.CS.ToString()))
            {
                appPath = Application.StartupPath + "\\";
                config = System.Configuration.ConfigurationManager.OpenExeConfiguration(
                System.Configuration.ConfigurationUserLevel.None);

                foreach (System.Configuration.ConnectionStringSettings settings in
                config.ConnectionStrings.ConnectionStrings)
                {
                    if (settings.ProviderName.Equals("System.Data.OleDb"))
                    {
                        settings.ConnectionString = string.Format(
                            settings.ConnectionString, appPath);
                    }
                }

                config.Save();
                System.Configuration.ConfigurationManager.RefreshSection(
                    "connectionStrings");
            }
            else
            {
                string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "web.config");

                config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                appPath = AppDomain.CurrentDomain.BaseDirectory;

                System.Configuration.SectionInformation information =
                    config.GetSection("loggingConfiguration").SectionInformation;
                information.SetRawXml(UpdateLoggingFilePath(information.GetRawXml(),
                    appPath));
            }
        }

        private string UpdateLoggingFilePath(string xml, string appPath)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);

            XmlNode node = document.SelectSingleNode("descendant::listeners");
            node.FirstChild.Attributes["fileName"].Value =
                Path.Combine(appPath, "log.xml");

            return document.OuterXml;
        }

        public void LoadApplicationConfiguration(string filePath)
        {
            document = new XmlDocument();
            document.Load(filePath);

            foreach (XmlNode node in document.DocumentElement.ChildNodes)
            {
                if (node.Name.Equals("sections"))
                    LoadSection(node);

                if (node.Name.Equals("configurations"))
                {
                    Trace.WriteLine("configurations--------");
                    LoadConfiguration(node);
                }
            }
        }

        private void LoadSection(XmlNode sections)
        {
            foreach (XmlNode childNode in sections.ChildNodes)
            {
                if (!childNode.Name.Equals("section"))
                    continue;

                string[] assemblyList = childNode.Attributes["handler"].Value.Split(',');
                IConfigurationSection section = (IConfigurationSection)Assembly.Load(
                    assemblyList[1]).CreateInstance(assemblyList[0]);
                this.sections.Add(section);
            }
        }

        private void LoadConfiguration(XmlNode configurations)
        {
            foreach (XmlNode node in configurations.ChildNodes)
            {
                IConfigurationSection config = sections.SingleOrDefault(
                    info => info.Type.Equals(node.LocalName));

                config.OnSave += new EventHandler<EventArgs>(config_Save);

                if (config != null)
                {
                    Trace.WriteLine("config---------");
                    config.ProcessSection(node);
                }
            }
        }

        void config_Save(object sender, EventArgs e)
        {
            string configPath =
                System.Configuration.ConfigurationManager.AppSettings["ConfigFile"];

            document.Save(configPath);
        }
    }
}
