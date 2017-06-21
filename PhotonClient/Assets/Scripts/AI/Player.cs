using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Common;
using Common.Tools;

public class Player : MonoBehaviour {

    public Transform tower;

    public bool IsLocalPlayer = true;
    private SyncPositionRequest syncPositionRequest;

    public GameObject playerPreferb;

    public string usernameSelf;

    private Dictionary<string, GameObject> playerDict = new Dictionary<string, GameObject>();

    private Fow fow;

    private Vector3 oldPosition = Vector3.zero;
	void Start () {
        if (IsLocalPlayer) {
            GetComponent<Renderer>().material.color = Color.green;
            syncPositionRequest = GetComponent<SyncPositionRequest>();
            InvokeRepeating("syncPosition", 3, 0.05f);
            GetComponent<SyncPlayerRequest>().DefaultRequest();
            fow =GameObject.FindGameObjectWithTag("Fow").GetComponent<Fow>();
            byte[] byte1=new byte [1024];
            fow.InitMap(200,200,byte1);
            fow.StartFow(this.transform);
         //   fow.StartFow(tower);
        }       
	}

    void syncPosition() {
        if (Vector3.Distance(transform.position, oldPosition) > 0.1) {
            oldPosition = transform.position;
            syncPositionRequest.Pos = transform.position;
            syncPositionRequest.DefaultRequest();
        }
    }

	// Update is called once per frame
	void Update () {
        if (IsLocalPlayer) {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            transform.Translate(new Vector3(-v, 0, h) * 0.5f );
        }
	}

    public void  OnSyncPlayer(List<string> usernameList) {
        foreach (string username in usernameList) {
            CreatePlayer(username);
        }
    }
    public void OnSyncNewPlayerEvent(EventData e) {
        string username = (string)DictTool.GetValue(e.Parameters, (byte)ParameterCode.Username);
        
        CreatePlayer(username);
    }

    void CreatePlayer(string username) {
        GameObject player = Instantiate(playerPreferb);
        Destroy(player.GetComponent<SyncPlayerRequest>());
        Destroy(player.GetComponent<SyncPositionRequest>());
        Destroy(player.GetComponent<SyncPlayerEvent>());
        Destroy(player.GetComponent<SyncPositionEvent>());
        player.GetComponent<Player>().IsLocalPlayer = false;
        player.GetComponent<Player>().usernameSelf = username;
        player.name = username;
        playerDict.Add(username, player);
    }

    public void OnSyncPositionEvent(List<PlayerPosition> playerPosition) {
        foreach (PlayerPosition playerposition in playerPosition) {
             GameObject player = DictTool.GetValue(playerDict, playerposition.Username);
            if (player != null) {
                player.transform.position = new Vector3(playerposition.PositionData.X, playerposition.PositionData.Y, playerposition.PositionData.Z);
            }
        }
    }

}
