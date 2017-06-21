using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using Common.Tools;
using MyServer.Handler;
using Common;
namespace MyServer
{
   public  class MyClientPeer : ClientPeer
    {
        public string username;

        public float x;
        public float y;
        public float z;

        public   MyClientPeer(InitRequest initrequest) : base(initrequest) {
            MyServer.Instance.PeerList.Add(this);
        }
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            MyServer.Instance.PeerList.Remove(this);
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            //Dictionary<byte, object> data = operationRequest.Parameters;
            //Object intvalue;
            //Object stringvalue;
            //data.TryGetValue(1, out intvalue);
            //data.TryGetValue(2, out stringvalue);
            //MyServer.Log.Info("得到请求的数据" + intvalue.ToString() + "," + stringvalue.ToString());
            //Dictionary<byte, object> responsedata = new Dictionary<byte, object>();
            //responsedata.Add(1, (int)intvalue + 111);
            //responsedata.Add(2, stringvalue.ToString() + "加上服务器响应");
            //OperationResponse operationresponse = new OperationResponse(1);
            //operationresponse.Parameters = responsedata;
            //SendOperationResponse(operationresponse, sendParameters);

            //Dictionary<byte, object> data2 = new Dictionary<byte, object>();
            //data2.Add(1, (int)intvalue + 100000);
            //data2.Add(2, stringvalue.ToString() + "加上事件的数据");
            //EventData eventdata = new EventData(1, data2);
            //SendEvent(eventdata, new SendParameters());
            BaseHandler handler = DictTool.GetValue(MyServer.Instance.HandlerDict, (OperationCode)operationRequest.OperationCode);
            if (handler != default(BaseHandler)) {
                handler.OnOperationRequest(operationRequest, sendParameters, this);
            }
            else
            {
                handler = DictTool.GetValue(MyServer.Instance.HandlerDict, OperationCode.Default);
                handler.OnOperationRequest(operationRequest, sendParameters, this);
            }


        }
    }
}
