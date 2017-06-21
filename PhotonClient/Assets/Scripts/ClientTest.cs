using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClientTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            sendRequest();
        }
	}

    void sendRequest() {
        Dictionary<byte, object> dict = new Dictionary<byte, object>();
        dict.Add(1, 999);
        dict.Add(2, "请求数据");

        PhotonServerClient.Peer.OpCustom(1, dict, true);
    }
}
