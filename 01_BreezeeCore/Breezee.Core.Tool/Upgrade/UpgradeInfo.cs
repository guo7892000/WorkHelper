namespace Breezee.Core.Tool
{
    /// <summary>
    /// 升级信息
    /// </summary>
    public class UpgradeInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="latestVerionConfig"></param>
        public UpgradeInfo(LatestVerionConfig latestVerionConfig) 
        {
            ServerVerCfg = latestVerionConfig;
            Server = new RemoteServer();
            Client = new LocalClient();

            if (!string.IsNullOrEmpty(ServerVerCfg.zipFormats))
            {
                // 设置压缩包后缀格式
                Server.SupportZipFormats = ServerVerCfg.zipFormats.Split(',', '，', '|', ';', '：', ':', '；');
            }
            // 本地文件名
            Server.AppVersion = ServerVerCfg.appName + ServerVerCfg.version;
        }
        
        /// <summary>
        /// 服务器版本配置
        /// </summary>
        public LatestVerionConfig ServerVerCfg { get; set; }

        /// <summary>
        /// 服务器
        /// </summary>
        public RemoteServer Server { get; set; }

        /// <summary>
        /// 客户端
        /// </summary>
        public LocalClient Client { get; set; }
        

        
        
    }
}
