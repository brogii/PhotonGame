using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using System;
using Common.Tools;
using Common;
using System.Xml.Serialization;
using System.IO;
using ProtoBuf;

public class SyncPlayerRequest : Request {
    Player player;
    void Start() {
        base.Start();
        player = GetComponent<Player>();
    }

    public override void DefaultRequest()
    {
        PhotonServerClient.Peer.OpCustom((byte)OpCode, new Dictionary<byte, object>(), true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
       byte[] SyncPlayerListBytes = (byte[])DictTool.GetValue(operationResponse.Parameters, (byte)ParameterCode.SyncPlayerList);
        List<string> usernameDict  =ProtobufTool.Deserialize<List<string>>(SyncPlayerListBytes);
        player.OnSyncPlayer(usernameDict);
    }


}
