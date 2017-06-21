using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;
using Common.Tools;

namespace MyServer.Handler
{
     class LoginHandler : BaseHandler
    {
        public LoginHandler() {
            OpCode = OperationCode.Login;
        }
        
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClientPeer peer)
        {
            string username = DictTool.GetValue(operationRequest.Parameters, (byte)ParameterCode.Username) as string;
            string password = DictTool.GetValue(operationRequest.Parameters, (byte)ParameterCode.Password) as string;

            OperationResponse operationresponse = new OperationResponse(operationRequest.OperationCode);
            if (MyServer.Instance.Usermanager.VerifyUser(username, password))
            {
                peer.username = username;
                operationresponse.ReturnCode =(short)ResultCode.Success;
            }
            else {
                operationresponse.ReturnCode = (short)ResultCode.Failed;
            }
            peer.SendOperationResponse(operationresponse, sendParameters);        
        }
    }
}
