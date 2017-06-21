using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Photon.SocketServer;

namespace MyServer.Handler
{
    class DefaultHandler : BaseHandler
    {
        public DefaultHandler() {
            OpCode = OperationCode.Default;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClientPeer peer)
        {
            throw new NotImplementedException();
        }
    }
}
