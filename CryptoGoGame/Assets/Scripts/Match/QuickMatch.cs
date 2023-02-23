using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class QuickMatch : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private byte maxPlayers = 4;

    private void CreateRoom(){
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(null, roomOptions, null);
    }
    public void CreateQuickMatch(){
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
    }
}
