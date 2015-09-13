using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Configuration;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace Esoft.Framework.Utility.Configuration
{
    public sealed class CarManageConfig : IConfigurationSection
    {
        private static readonly CarManageConfig instance = 
            new CarManageConfig();

        public event EventHandler<EventArgs> OnSave;


        #region 系统常量

        /// <summary>
        /// 属性契约
        /// </summary>
        public readonly string AttributeContract =
            "Esoft.Framework.DataService.IAttributeFileTransfer";

        /// <summary>
        /// 属性服务地址
        /// </summary>
        public readonly string AttributeAddress = "{0}://{1}:{2}/Design_Time_Addresses/"
            + "Esoft.Framework.DataService/IAttributeFileTransfer/IAttributeFileTransfer";

        /// <summary>
        /// 品牌契约
        /// </summary>
        public readonly string BrandContract =
            "Esoft.Framework.DataService.IBrandFileTransfer";

        /// <summary>
        /// 订单契约
        /// </summary>
        public readonly string OrderContract =
            "Esoft.Framework.DataService.Order.IOrderFileTransfer";

        /// <summary>
        /// 库存契约
        /// </summary>
        public readonly string StorageContract =
            "Esoft.Framework.DataService.Storage.IStorageFileTransfer";

        /// <summary>
        /// 采购订单契约
        /// </summary>
        public readonly string PurchaseContract =
            "Esoft.Framework.DataService.Purchase.IPurchaseFileTransfer";

        /// <summary>
        /// 品牌服务地址
        /// </summary>
        public readonly string BrandAddress = "{0}://{1}:{2}/Design_Time_Addresses/"
            + "Esoft.Framework.DataService/IBrandFileTransfer/IBrandFileTransfer";

        /// <summary>
        /// 产品数据契约
        /// </summary>
        public readonly string ProductContract =
            "Esoft.Framework.DataService.IProductFileTransfer";

        /// <summary>
        /// 产品服务地址
        /// </summary>
        public readonly string ProductAddress = "{0}://{1}:{2}/Design_Time_Addresses/"
            + "Esoft.Framework.DataService/IProductFileTransfer/IProductFileTransfer";

        /// <summary>
        /// 订单服务地址
        /// </summary>
        public readonly string OrderAddress = "{0}://{1}:{2}/Design_Time_Addresses/"
            + "Esoft.Framework.DataService/IOrderFileTransfer/IOrderFileTransfer";

        /// <summary>
        /// 库存服务地址
        /// </summary>
        public readonly string StorageAddress = "{0}://{1}:{2}/Design_Time_Addresses/"
            + "Esoft.Framework.DataService/IStorageFileTransfer/IStorageFileTransfer";

        /// <summary>
        /// 采购订单服务地址
        /// </summary>
        public readonly string PurchaseAddress = "{0}://{1}:{2}/Design_Time_Addresses/"
            + "Esoft.Framework.DataService/IPurchaseFileTransfer/IPurchaseFileTransfer";

        /// <summary>
        /// 供应商系统服务地址
        /// </summary>
        ///public readonly string SupplierServiceAddress = "{0}://{1}/Esoft.Framework.Web.Supplier/SupplierServiceTransfer.svc";
        public readonly string SupplierServiceAddress = "{0}://{1}/SupplierServiceTransfer.svc";

        /// <summary>
        /// 供应商系统Web服务地址
        /// </summary>
        public string SupplierWebServiceUrl { get; set; }

        #endregion

        static CarManageConfig()
        {
            
        }

        public static CarManageConfig Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// 应用程序是否已退出
        /// </summary>
        public bool AppExited { get; set; }

        private string dataAccessProvider;

        /// <summary>
        /// 数据访问提供程序
        /// </summary>
        public string DataAccessProvider
        {
            get
            {
                if (string.IsNullOrEmpty(instance.dataAccessProvider))
                    return "Esoft.Framework.DataAccess";

                return instance.dataAccessProvider;
            }
            set { instance.dataAccessProvider = value; }
        }

        /// <summary>
        /// 服务提供程序
        /// </summary>
        public string ServiceProvider { get; set; }

        /// <summary>
        /// 阿里巴巴服务提供程序
        /// </summary>
        public string ALServiceProvider { get; set; }

        /// <summary>
        /// BabyOnline服务提供程序
        /// </summary>
        public string BLServiceProvider { get; set; }

        /// <summary>
        /// 27dress服务提供程序
        /// </summary>
        public string TSServiceProvider { get; set; }

        /// <summary>
        /// SuZhoudress服务提供程序
        /// </summary>
        public string SZServiceProvider { get; set; }

        /// <summary>
        /// 数据传输提供程序
        /// </summary>
        public string TransportProvider { get; set; }

        /// <summary>
        /// 数据导入提供程序
        /// </summary>
        public string ImportProvider { get; set; }

        /// <summary>
        /// 数据传输服务协议
        /// </summary>
        public string TransportProtocal { get; set; }

        /// <summary>
        /// 数据传输服务地址
        /// </summary>
        public string TransportServer { get; set; }

        /// <summary>
        /// 数据传输服务端口号
        /// </summary>
        public int TransportPort { get; set; }

        /// <summary>
        /// 数据传输服务协议
        /// </summary>
        public string SupplierTransportProtocal { get; set; }

        /// <summary>
        /// 数据传输服务地址
        /// </summary>
        public string SupplierTransportServer { get; set; }

        /// <summary>
        /// 数据传输服务端口号
        /// </summary>
        public int SupplierTransportPort { get; set; }

        /// <summary>
        /// 异常处理策略
        /// </summary>
        public string ExceptionPolicy { get; set; }

        /// <summary>
        /// 是否加载缩略图
        /// </summary>
        public bool IsLoadThumbPic { get; set; }

        /// <summary>
        /// 是否加载产品价格
        /// </summary>
        public bool IsLoadPrice { get; set; }

        /// <summary>
        /// 应用程序根目录
        /// </summary>
        public readonly string AppPath = Path.GetDirectoryName(
            Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName)+"\\";

        /// <summary>
        /// 是否启用缓存
        /// </summary>
        public bool EnableCache { get; set; }

        /// <summary>
        /// 是否启用权限
        /// </summary>
        public bool EnablePermission { get; set; }

        /// <summary>
        /// 应用程序版本号
        /// </summary>
        public string AppVersion
        {
            get
            {
                return Application.ProductVersion;
            }
        }
        /// <summary>
        /// 服务器名称
        /// </summary>
        public String ServerName { get; set; }

        /// <summary>
        /// 数据库连接字符串名称
        /// </summary>
        public string ConnectionStringName { get; set; }

        /// <summary>
        /// 系统语言
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 获取产品列表接口每页数据大小
        /// </summary>
        public int ProductListAPIPageSize 
        {
            get { return 10; }
        }

        /// <summary>
        /// 获取订单列表接口每页数据大小
        /// </summary>
        public int OrderListAPIPageSize
        {
            get { return 20; }
        }

        /// <summary>
        /// 产品名称长度
        /// </summary>
        public int ProductNameLength 
        {
            get { return 140; }
        }

        /// <summary>
        /// 产品文件数据存放路径
        /// </summary>
        public string ProductDataPath { get; set; }

        /// <summary>
        /// 供应商系统文件存放目录
        /// </summary>
        public string SupplierAppPath
        {
            get
            {
                return System.Web.HttpRuntime.AppDomainAppPath;
            }
        }

        /// <summary>
        /// 产品图片扩展名
        /// </summary>
        public string ImageExtension
        {
            get { return ".jpg"; }
        }

        /// <summary>
        /// 采购订单缩略图尺寸
        /// </summary>
        public Size PurchaseOrderImageSize
        {
            get { return new Size(96, 120); }
        }

        /// <summary>
        /// 采购订单缩略图尺寸
        /// </summary>
        public Size SaleOrderImageSize
        {
            get { return new Size(96, 120); }
        }

        /// <summary>
        /// 库存产品缩略图尺寸
        /// </summary>
        public Size StorageProductImageSize
        {
            get { return new Size(96, 120); }
        }

        /// <summary>
        /// Html编辑器
        /// </summary>
        public string HtmlEditorUrl
        {
            get
            {
                return Path.Combine(CarManageConfig.Instance.AppPath,
                    "PlugIn\\Fckeditor\\ProductEditor.htm");
            }
        }

        public void ProcessSection(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode.NodeType.Equals(XmlNodeType.Comment))
                    continue;

                if (childNode.Attributes["key"].Value.Equals("DataAccessProvider"))
                    instance.dataAccessProvider = childNode.Attributes["value"].Value;

                if (childNode.Attributes["key"].Value.Equals("ServiceProvider"))
                    instance.ServiceProvider = childNode.Attributes["value"].Value;

                if (childNode.Attributes["key"].Value.Equals("ALServiceProvider"))
                    instance.ALServiceProvider = childNode.Attributes["value"].Value;

                if (childNode.Attributes["key"].Value.Equals("BLServiceProvider"))
                    instance.BLServiceProvider = childNode.Attributes["value"].Value;

                if (childNode.Attributes["key"].Value.Equals("TSServiceProvider"))
                    instance.TSServiceProvider = childNode.Attributes["value"].Value;

                if (childNode.Attributes["key"].Value.Equals("SZServiceProvider"))
                    instance.SZServiceProvider = childNode.Attributes["value"].Value;

                if (childNode.Attributes["key"].Value.Equals("TransportProvider"))
                    instance.TransportProvider = childNode.Attributes["value"].Value;

                if (childNode.Attributes["key"].Value.Equals("ImportProvider"))
                    instance.ImportProvider = childNode.Attributes["value"].Value;

                if (childNode.Attributes["key"].Value.Equals("TransportProtocal"))
                    instance.TransportProtocal = childNode.Attributes["value"].Value;

                if (childNode.Attributes["key"].Value.Equals("TransportServer"))
                    instance.TransportServer = childNode.Attributes["value"].Value;

                if (childNode.Attributes["key"].Value.Equals("TransportPort"))
                    instance.TransportPort = 
                        int.Parse(childNode.Attributes["value"].Value);

                if (childNode.Attributes["key"].Value.Equals("SupplierTransportProtocal"))
                    instance.SupplierTransportProtocal = childNode.Attributes["value"].Value;

                if (childNode.Attributes["key"].Value.Equals("SupplierTransportServer"))
                    instance.SupplierTransportServer = childNode.Attributes["value"].Value;

                if (childNode.Attributes["key"].Value.Equals("SupplierTransportPort"))
                    instance.SupplierTransportPort =
                        int.Parse(childNode.Attributes["value"].Value);

                if (childNode.Attributes["key"].Value.Equals("SupplierWebServiceUrl"))
                    instance.SupplierWebServiceUrl = childNode.Attributes["value"].Value;

                if (childNode.Attributes["key"].Value.Equals("ExceptionPolicy"))
                    instance.ExceptionPolicy = childNode.Attributes["value"].Value;

                if (childNode.Attributes["key"].Value.Equals("IsLoadThumbPic"))
                    instance.IsLoadThumbPic = childNode.Attributes["value"].Value.Equals("1");

                if (childNode.Attributes["key"].Value.Equals("IsLoadPrice"))
                    instance.IsLoadPrice = childNode.Attributes["value"].Value.Equals("1");

                if (childNode.Attributes["key"].Value.Equals("ConnectionStringName"))
                    instance.ConnectionStringName = childNode.Attributes["value"].Value;

                if (childNode.Attributes["key"].Value.Equals("ServerName"))
                    instance.ServerName = childNode.Attributes["value"].Value;

                if (childNode.Attributes["key"].Value.Equals("PageSize"))
                {
                    int pageSize = 0;

                    if (int.TryParse(childNode.Attributes["value"].Value, out pageSize))
                        instance.PageSize = pageSize;
                    else
                        instance.PageSize = 20;
                }

                if (childNode.Attributes["key"].Value.Equals("Language"))
                    instance.Language = childNode.Attributes["value"].Value;

                if (childNode.Attributes["key"].Value.Equals("ImagePath"))
                    instance.ProductDataPath = childNode.Attributes["value"].Value;

                if (childNode.Attributes["key"].Value.Equals("EnableCache"))
                    instance.EnableCache = bool.Parse(childNode.Attributes["value"].Value);

                if (childNode.Attributes["key"].Value.Equals("EnablePermission"))
                    instance.EnablePermission = bool.Parse(childNode.Attributes["value"].Value);
            }
        }

        //public void Save()
        //{
        //    if (OnSave != null)
        //        OnSave(null, null);
        //}

        public void Save()
        {
            string configPath =
                System.Configuration.ConfigurationManager.AppSettings["ConfigFile"];

            XmlDocument document = new XmlDocument();
            document.Load(configPath);

            XmlNode node = document.SelectSingleNode(
                "descendant::carConfiguration");

            if (node == null)
                return;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (!childNode.NodeType.Equals(XmlNodeType.Element))
                    continue;

                switch (childNode.Attributes["key"].Value)
                {
                    case "ImagePath": childNode.Attributes["value"].Value =
                        CarManageConfig.Instance.ProductDataPath;
                        break;

                    case "PageSize": childNode.Attributes["value"].Value =
                        CarManageConfig.Instance.PageSize.ToString();
                        break;

                    case "TransportServer": childNode.Attributes["value"].Value =
                        CarManageConfig.Instance.TransportServer;
                        break;

                    case "TransportPort": childNode.Attributes["value"].Value =
                        CarManageConfig.Instance.TransportPort.ToString();
                        break;
                }
            }

            document.Save(configPath);
        }

        public string Type
        {
            get { return "carConfiguration"; }
        }
    }
}
