using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;
using Common.Tools;
using MyServer.Model;

namespace MyServer.Handler
{
    class RegisterHandler : BaseHandler
    {
        public RegisterHandler() {
            OpCode = OperationCode.Register;
        }
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClientPeer peer)
        {
            string username = DictTool.GetValue(operationRequest.Parameters, (byte)ParameterCode.Username) as string;
            string password = DictTool.GetValue(operationRequest.Parameters, (byte)ParameterCode.Password) as string;

            OperationResponse operationresponse = new OperationResponse(operationRequest.OperationCode);
            User userExist = MyServer.Instance.Usermanager.GetByUsername(username);
            if (userExist==null)
            {
                User user = new User()
                {
                    Username = username,
                    Password = password
                };
                MyServer.Instance.Usermanager.Add(user);
                operationresponse.ReturnCode = (short)ResultCode.Success;
            }
            else
            {
                operationresponse.ReturnCode = (short)ResultCode.Failed;
            }
            peer.SendOperationResponse(operationresponse, sendParameters);
        }
    }
}
