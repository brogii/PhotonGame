using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Common;
using ExitGames.Client.Photon;

public class RegisterRequest : Request {
    [HideInInspector]
    public string username;
    [HideInInspector]
    public string password;

    RegisterPanel registerPanel;

    void Start() {
        base.Start();
        registerPanel = GetComponent<RegisterPanel>();
    }

    public override void DefaultRequest()
    {
        Dictionary<byte, object> dict = new Dictionary<byte, object>();
        dict.Add((byte)ParameterCode.Username, username);
        dict.Add((byte)ParameterCode.Password, password);
        PhotonServerClient.Peer.OpCustom((byte)OpCode, dict, true);
    }


    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        registerPanel.OnOperationResponse(operationResponse);
    }
}
