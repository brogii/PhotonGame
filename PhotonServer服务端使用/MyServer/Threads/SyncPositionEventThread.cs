using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Tools;
using Photon.SocketServer;

namespace MyServer.Threads
{
   public class SyncPositionEventThread
    {
        Thread T;
        public void Run() {
            T = new Thread(SyncPosition);
            T.IsBackground = true;
            T.Start();
        }
        public void Stop() {
            T.Abort();
        }
        void SyncPosition() {

            Thread.Sleep(5000);
            while (true)
            {
                Thread.Sleep(50);
                SendSyncPositionEvent();
            }
        }
        void SendSyncPositionEvent() {
            List<PlayerPosition> playerPositionList = new List<PlayerPosition>();
            foreach (MyClientPeer clientpeer in MyServer.Instance.PeerList) {
                if (!string.IsNullOrEmpty(clientpeer.username))
                {
                    PlayerPosition playerPosition = new PlayerPosition();
                    playerPosition.Username = clientpeer.username;
                    playerPosition.PositionData = new PositionData() { X = clientpeer.x, Y = clientpeer.y, Z = clientpeer.z };
                    playerPositionList.Add(playerPosition);
                }
            }
             byte[] playerPositionByte =ProtobufTool.Serialize(playerPositionList);
            foreach (MyClientPeer clientpeer in MyServer.Instance.PeerList) {
                if (!string.IsNullOrEmpty(clientpeer.username))
                {
                    EventData eventData = new EventData((byte)EventCode.SyncPosition);
                    Dictionary<byte, object> eventDict = new Dictionary<byte, object>();
                    eventDict.Add((byte)ParameterCode.SyncPositionList, playerPositionByte);
                    eventData.Parameters = eventDict;
                    clientpeer.SendEvent(eventData, new SendParameters());
                }
            }

        }

    }
}
