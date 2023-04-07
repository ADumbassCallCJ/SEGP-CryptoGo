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

    private int numPlayerMode = 2; // number of player allow in the room, by default is 2. cj added
    private int auxiliaryCard = 1;

    [SerializeField]
    private Dropdown dropDownMenu;

    // Test  Player 
    public void OnClick_CreateRoom(){

        if(!PhotonNetwork.IsConnected){
            return;
        }

        // Create a new room
        // Join or create room
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = System.Convert.ToByte(numPlayerMode);
        Debug.Log("number of player: " + options.MaxPlayers); //testing
        ExitGames.Client.Photon.Hashtable setCustomRoomProperty = new ExitGames.Client.Photon.Hashtable();
        Debug.Log(auxiliaryCard);
        setCustomRoomProperty["AuxiliaryNumber"] = auxiliaryCard; 
        options.CustomRoomProperties = setCustomRoomProperty;
        // Check if the room existed or not
        if(options.MaxPlayers > 0){
            int index = _roomInfoLists.FindIndex(x => x.Name == _roomName.text);
            if(index == -1){
                PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, TypedLobby.Default);
            }
            else{
                Debug.Log("The room has already existed");
            }
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

    public void ChooseMaxPlayerRoom(){
        if(dropDownMenu.value == 0){
            numPlayerMode = 2;
            Debug.Log("choose 2");
        }
        if(dropDownMenu.value == 1){
            numPlayerMode = 3;
            Debug.Log("choose 3");
        }
        if(dropDownMenu.value == 2){
            numPlayerMode = 4;
            Debug.Log("choose 4");
        }
    }
        public void ChooseNumberOfAuxiliaryCard(){
        if(dropDownMenu.value == 0){
            auxiliaryCard = 1;
            Debug.Log("choose 1");
        }
        if(dropDownMenu.value == 1){
            numPlayerMode = 2;
            Debug.Log("choose 2");
        }

    }
}
