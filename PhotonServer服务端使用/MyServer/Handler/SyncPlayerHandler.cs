using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;
using System.Xml.Serialization;
using System.IO;
using Common.Tools;


namespace MyServer.Handler
{
    class SyncPlayerHandler : BaseHandler
    {
       public  SyncPlayerHandler() {
            OpCode = OperationCode.SyncPlayer;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClientPeer peer)
        {
            List<string> usernameList = new List<string>();
            
            foreach (MyClientPeer clientpeer in MyServer.Instance.PeerList) {
                if (!string.IsNullOrEmpty(clientpeer.username)&& clientpeer != peer) {
                    usernameList.Add(clientpeer.username);
                    EventData eventdata = new EventData((byte)EventCode.SyncPlayer);
                    Dictionary<byte, object> eventdict = new Dictionary<byte, object>();                  
                    eventdict.Add((byte)ParameterCode.Username, peer.username);
                    eventdata.Parameters = eventdict;
                    clientpeer.SendEvent(eventdata, sendParameters);
                }
            }
            //StringWriter sw = new StringWriter();
            //XmlSerializer Serializer = new XmlSerializer(typeof(List<string>));
            //Serializer.Serialize(sw, usernameList);
            //string usernameListString;
            //usernameListString = sw.ToString();
            //sw.Dispose();
            byte[] usernameBytes = ProtobufTool.Serialize<List<string>>(usernameList);

            OperationResponse operationresponse = new OperationResponse((byte)OperationCode.SyncPlayer);
            Dictionary<byte, object> dict = new Dictionary<byte, object>();
            dict.Add((byte)ParameterCode.SyncPlayerList, usernameBytes);
            operationresponse.Parameters = dict;
            peer.SendOperationResponse(operationresponse, sendParameters);




        }
    }
}
