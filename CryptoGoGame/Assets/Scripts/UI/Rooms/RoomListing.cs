using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;


public class RoomListing : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    [SerializeField]
    private Text _currentPlayerNumber;
    public RoomInfo RoomInfo { get; private set; }

    

    public void SetRoomInfo(RoomInfo roomInfo){
        RoomInfo = roomInfo;
        _text.text = roomInfo.Name + "\tMax players: " + roomInfo.MaxPlayers;
        _currentPlayerNumber.text = "Number of players: "  + "/" + roomInfo.MaxPlayers;

    }

    public void OnClick_Button(){
        PhotonNetwork.JoinRoom(RoomInfo.Name);
    }

}
