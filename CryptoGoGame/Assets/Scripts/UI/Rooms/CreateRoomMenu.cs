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

    private List<RoomInfo> _roomInfoLists = new List<RoomInfo>(); 

    // Test  Player 
    public void OnClick_CreateRoom(){

        if(!PhotonNetwork.IsConnected){
            return;
        }

        // Create a new room
        // Join or create room
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        // Check if the room existed or not
        int index = _roomInfoLists.FindIndex(x => x.Name == _roomName.text);
        if(index == -1){
            PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, TypedLobby.Default);
        }
        else{
            Debug.Log("The room has already existed");
        }
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
     public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
   
        foreach(RoomInfo info in roomList)
        {   
            if(info.RemovedFromList){
                int index = _roomInfoLists.FindIndex(x => x.Name == info.Name);
                if(index != -1){
           
                    _roomInfoLists.RemoveAt(index);
                }
            }
            else{
                _roomInfoLists.Add(info);
            }
           
        }
    }
}
