using UnityEngine;
using System.Collections;
using Common;
using ExitGames.Client.Photon;

public abstract class EventBase : MonoBehaviour {
    public EventCode eventCode;

    public abstract void OnEvent(EventData eventData);

    public virtual void Start () {
        PhotonServerClient.Instance.EventDict.Add(eventCode, this);
	}
    void OnDestroy()
    {
        PhotonServerClient.Instance.EventDict.Remove(eventCode);
    }


}
