using System;
using System.Xml;

namespace Esoft.Framework.Utility.Configuration
{
    public interface IConfigurationSection
    {
        void ProcessSection(XmlNode node);
        string Type { get; }
        event EventHandler<EventArgs> OnSave;
    }
}
