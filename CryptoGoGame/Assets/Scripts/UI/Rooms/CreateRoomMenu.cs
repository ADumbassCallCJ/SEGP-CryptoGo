using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _roomName;

    // Test  Player 
    public void OnClick_CreateRoom(){

        if(!PhotonNetwork.IsConnected){
            return;
        }

        // Create a new room
        // Join or create room
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, TypedLobby.Default);
 //       PhotonNetwork.LoadLevel(1);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Created room successfully ", this);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed: " + message, this );
    }
}
