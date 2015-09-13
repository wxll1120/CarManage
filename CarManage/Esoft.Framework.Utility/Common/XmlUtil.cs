using System;
using System.Xml;
using System.IO;

namespace Esoft.Framework.Utility.Common
{
    public class XmlUtil
    {
        /// <summary>
        /// 获取指定Xml节点的InnerText值
        /// </summary>
        /// <param name="filePath">Xml文件完整路径</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns>返回InnerText值，如果无匹配节点则返回空串。</returns>
        public static string GetNodeInnerText(string filePath, string nodeName)
        {
            if (!File.Exists(filePath))
                throw new Exception("Xml文件不存在！");

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode node = doc.SelectSingleNode("descendant::{0}");

            if (node == null)
                return string.Empty;

            return node.InnerText;
        }

        /// <summary>
        /// 设置指定Xml节点的InnerText值
        /// </summary>
        /// <param name="filePath">文件完整路径</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="innerText">InnerText值</param>
        public static void SetNodeInnerText(string filePath, string nodeName, 
            string innerText)
        {
            if (!File.Exists(filePath))
                throw new Exception("Xml文件不存在！");

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode node = doc.SelectSingleNode("descendant::{0}");
            node.InnerText = innerText;

            doc.Save(filePath);
        }
    }
}
