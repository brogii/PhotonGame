using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;
using System;

public class SyncPlayerEvent : EventBase {
    Player player;
   public override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }
    public override void OnEvent(EventData eventData)
    {
        player.OnSyncNewPlayerEvent(eventData);
    }




}
