using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using Common;
using System.Collections.Generic;
using ExitGames.Client.Photon;

public class LoginRequest : Request {
    [HideInInspector]
    public string username;
    [HideInInspector]
    public string password;

    LoginPanel loginPanel;

    public override void Start() {
        base.Start();
        loginPanel = GetComponent<LoginPanel>();
    }

    public override void DefaultRequest()
    {        
        Dictionary<byte, object> dict = new Dictionary<byte, object>();
        dict.Add((byte)ParameterCode.Username,username);
        dict.Add((byte)ParameterCode.Password,password);
        PhotonServerClient.Peer.OpCustom((byte)OpCode, dict, true);
    }

   
    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        loginPanel.OnOperationResponse(operationResponse);
    }
}
