using System;
using System.Configuration;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Xml;
using System.Security.Cryptography.X509Certificates;


namespace HF.Cloud.WcfForm
{
    class Program
    {
        static void Main(string[] args)
        {

           string _BaseUri = ConfigurationManager.AppSettings["BaseUri"];

            Assembly _Assembly = Assembly.Load("HF.Cloud.WcfLibrary");
            Assembly _AssemblyInterface = Assembly.Load("HF.Cloud.WcfService");

            Type[] _Types = _Assembly.GetTypes();

            foreach (Type _Type in _Types)
            {
                if (_Type.DeclaringType != null)
                    continue;

                string _Uri = _BaseUri + (_Type.Name.EndsWith("Service") ? _Type.Name.Replace("Service", "") : _Type.Name);

                Type _TypeInterface = _AssemblyInterface.GetType("HF.Cloud.WcfService.I" + _Type.Name);
                
                WebServiceHost _HostNews = new WebServiceHost(_Type, new Uri(_Uri));
                
                WebHttpBinding _HttpBinding = new WebHttpBinding();
                _HttpBinding.MaxBufferPoolSize = int.MaxValue;
                _HttpBinding.MaxReceivedMessageSize = int.MaxValue;
                _HttpBinding.ReaderQuotas = new XmlDictionaryReaderQuotas();
                _HttpBinding.ReaderQuotas.MaxArrayLength = int.MaxValue;
                _HttpBinding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
                _HttpBinding.ReaderQuotas.MaxDepth = 32;
                _HttpBinding.ReaderQuotas.MaxBytesPerRead = 4096;
                _HttpBinding.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;
                ////////////start-https专用01:告诉传输层是绑定安全的通信,如果是http协议则去掉此段代码即可//////////////
                _HttpBinding.Security = new WebHttpSecurity
                {
                    Mode = WebHttpSecurityMode.Transport,
                    Transport = new HttpTransportSecurity()
                    {
                        ClientCredentialType = HttpClientCredentialType.None
                    }
                };
                ////////////end-https专用01//////////////
                //终结点
                _HostNews.AddServiceEndpoint(_TypeInterface, _HttpBinding, _Uri);
                ServiceEndpoint _point = _HostNews.Description.Endpoints.Find(_TypeInterface);

               ///////////////////haved--start  此段代码可有可无/////////////////////////
                WebHttpBehavior _WebHttpBehavior = new WebHttpBehavior();
                _WebHttpBehavior.HelpEnabled = bool.Parse(ConfigurationManager.AppSettings["IsEnableHelp"]);
                _WebHttpBehavior.DefaultBodyStyle = WebMessageBodyStyle.Bare;
                _WebHttpBehavior.DefaultOutgoingRequestFormat = WebMessageFormat.Json;
                ///////////////////haved--end  此段代码可有可无/////////////////////////

                ////////////start-https专用02:告诉Behaviors证书内容,如果是http协议则去掉此段代码即可//////////////
                ServiceCredentials serviceCredentials = _HostNews.Description.Behaviors.Find<ServiceCredentials>();
                if (null == serviceCredentials)
                {
                    serviceCredentials = new ServiceCredentials();
                    _HostNews.Description.Behaviors.Add(serviceCredentials);
                }
                serviceCredentials.ServiceCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, "shangwulink.com");
                ////////////end-https专用02//////////////


                //检查元数据 是否设置
                ServiceMetadataBehavior behaviour = _HostNews.Description.Behaviors.Find<ServiceMetadataBehavior>();

                if (behaviour == null)
                {
                    behaviour = new ServiceMetadataBehavior();
                    behaviour.HttpsGetEnabled = bool.Parse(ConfigurationManager.AppSettings["IsEnableHelp"]);
                    behaviour.ExternalMetadataLocation = new Uri(_Uri);
                    _HostNews.Description.Behaviors.Add(behaviour);
                }
                else
                {
                    behaviour.HttpsGetEnabled = bool.Parse(ConfigurationManager.AppSettings["IsEnableHelp"]);
                    behaviour.MetadataExporter.PolicyVersion = PolicyVersion.Default;
                }
                _HostNews.Open();
                
            }
            HF.Cloud.BLL.Common.Logger.Info("服务启动");
            Console.ReadLine();
        }

    }
}
