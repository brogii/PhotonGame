using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using ExitGames.Logging;
using System.IO;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using NHibernate;
using NHibernate.Cfg;
using MyServer.Model;
using MyServer.Manager;
using Common;
using MyServer.Handler;
using MyServer.Threads;

namespace MyServer
{
    public class MyServer : ApplicationBase
    {
        public static MyServer Instance;
        public Dictionary<OperationCode, BaseHandler> HandlerDict = new Dictionary<OperationCode, BaseHandler>();
        public List<MyClientPeer> PeerList = new List<MyClientPeer>();

        private IUserManager usermanager = new UserManager();

        SyncPositionEventThread syncPositionEventThread = new SyncPositionEventThread();

        public  readonly static ILogger Log = LogManager.GetCurrentClassLogger();

        internal IUserManager Usermanager
        {
            get
            {
                return usermanager;
            }

            private set
            {
            }
        }

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            Log.Info("一个连接");
            return new MyClientPeer(initRequest);
        }

        protected override void Setup()
        {
            Instance = this;
            InitHandlerDict();

            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] =
                                                         Path.Combine(Path.Combine(this.ApplicationRootPath, "bin_Win64"),"log");

            string path = Path.Combine(this.BinaryPath, "log4net.config");
            FileInfo configuratorFile = new FileInfo(path);
            if (configuratorFile.Exists)
            {
                LogManager.SetLoggerFactory(ExitGames.Logging.Log4Net.Log4NetLoggerFactory.Instance);
                XmlConfigurator.ConfigureAndWatch(configuratorFile);
            }
            Log.Info("服务器启动");
            
            
            User user = Usermanager.GetById(3);

            Log.Info("username为"+user.Username);

            
            syncPositionEventThread.Run();

        }

        protected override void TearDown()
        {
            Log.Info("服务器关闭");
            syncPositionEventThread.Stop();
        }


        void InitHandlerDict() {
            DefaultHandler defaultHandler = new DefaultHandler();
            LoginHandler loginHandler = new LoginHandler();
            RegisterHandler registerHandler = new RegisterHandler();
            SyncPositionHandler syncPositionHandler = new SyncPositionHandler();
            SyncPlayerHandler syncPlayerHandler = new SyncPlayerHandler();
            HandlerDict.Add(defaultHandler.OpCode, defaultHandler);
            HandlerDict.Add(loginHandler.OpCode, loginHandler);
            HandlerDict.Add(registerHandler.OpCode, registerHandler);
            HandlerDict.Add(syncPositionHandler.OpCode, syncPositionHandler);
            HandlerDict.Add(syncPlayerHandler.OpCode, syncPlayerHandler);
        }
    }
}
