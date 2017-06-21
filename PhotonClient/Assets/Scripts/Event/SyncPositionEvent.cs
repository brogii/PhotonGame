using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;
using System;
using Common;
using Common.Tools;
using System.Collections.Generic;

public class SyncPositionEvent : EventBase {
    Player player;
    public override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }

    public override void OnEvent(EventData eventData)
    {
        byte[] SyncPositionByte =(byte[])DictTool.GetValue(eventData.Parameters, (byte)ParameterCode.SyncPositionList);
        List<PlayerPosition> playPositionList = (List<PlayerPosition>)ProtobufTool.Deserialize<List<PlayerPosition>>(SyncPositionByte);


        player.OnSyncPositionEvent(playPositionList);
    }

    
}
