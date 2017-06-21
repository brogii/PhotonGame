using UnityEngine;
using System.Collections;
using Common;
using ExitGames.Client.Photon;

public abstract class Request : MonoBehaviour {
    public OperationCode OpCode;
    public abstract void DefaultRequest();
    public abstract void OnOperationResponse(OperationResponse operationResponse);
    // Use this for initialization
   public virtual void Start () {
        PhotonServerClient.Instance.RequestDict.Add(OpCode, this);
	}

    // Update is called once per frame
    void OnDestroy() {
        PhotonServerClient.Instance.RequestDict.Remove(OpCode);
    }
}
