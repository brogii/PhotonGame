using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using System;
using Common;
using Common.Tools;

public class PhotonServerClient : MonoBehaviour ,IPhotonPeerListener{
    public static PhotonServerClient Instance;

    

    public Dictionary<OperationCode, Request> RequestDict = new Dictionary<OperationCode, Request>();
    public Dictionary<EventCode, EventBase> EventDict = new Dictionary<EventCode, EventBase>();

    void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(Instance);
    }

    public static PhotonPeer Peer {
        get {
            return peer;
        }

    }
    private static PhotonPeer peer;

    // Use this for initialization
    void Start () {
        peer = new PhotonPeer(this, ConnectionProtocol.Udp);
        peer.Connect("127.0.0.1:5055", "MyServer");
	}
	
	// Update is called once per frame
	void Update () {
        peer.Service();
	}

    void OnDestroy()
    {
        if (peer.PeerState == PeerStateValue.Connected) {
            peer.Disconnect();
        }
    }


    public void DebugReturn(DebugLevel level, string message)
    {
        throw new NotImplementedException();
    }

    public void OnEvent(EventData eventData)
    {
        //Dictionary<byte, object> data = eventData.Parameters;
        //Debug.Log("服务器的事件int" + data[1].ToString());
        //Debug.Log("服务器的事件string" + data[2].ToString());
        EventCode eventCode = (EventCode)eventData.Code;
        EventBase eventBase = DictTool.GetValue(EventDict, eventCode);
        if (eventBase != null)
        {
            eventBase.OnEvent(eventData);
        }
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        //Dictionary<byte, object> data = operationResponse.Parameters;
        //Debug.Log("服务器的响应int" + data[1].ToString());
        //Debug.Log("服务器的响应string" + data[2].ToString());
        OperationCode opcode = (OperationCode)operationResponse.OperationCode;
        Request request = null;
        if (RequestDict.TryGetValue(opcode, out request)) {
            request.OnOperationResponse(operationResponse);
        }

    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        if (statusCode == StatusCode.Connect) {
            Debug.Log("已连接");
        }
        
    }
}
