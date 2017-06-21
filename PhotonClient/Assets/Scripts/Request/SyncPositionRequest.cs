using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using Common;

public class SyncPositionRequest : Request {
    [HideInInspector]
    public Vector3 Pos;

    public override void DefaultRequest()
    {
        Dictionary<byte, object> dict = new Dictionary<byte, object>();
        dict.Add((byte)ParameterCode.X, Pos.x);
        dict.Add((byte)ParameterCode.Y, Pos.y);
        dict.Add((byte)ParameterCode.Z, Pos.z);
        PhotonServerClient.Peer.OpCustom((byte)OpCode, dict, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        throw new NotImplementedException();
    }


}
