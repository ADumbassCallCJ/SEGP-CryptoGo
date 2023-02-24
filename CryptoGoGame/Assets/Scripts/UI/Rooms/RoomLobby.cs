using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomLobby : MonoBehaviourPunCallbacks
{
    // [SerializeField]
    // private PlayerListing _playerListing; 
    [SerializeField]
    private Text playerNameText;

      [SerializeField]
    private Text playerNameText1;

    private List<PlayerListing> _listings = new List<PlayerListing>();


    private void Awake(){
     //   GetCurrentRoomPlayer();
    }

    // private void GetCurrentRoomPlayer(){
    //     foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players){
    //         AddPlayerListing(playerInfo.Value);
    //         Debug.Log(playerInfo.Value);

    //     }
    //   //  _playerListing.SetPlayerInfo(_listings[0].PlayerInfo);
    //   playerNameText.text = _listings[0].PlayerInfo.NickName;
        
    // }
    // private void AddPlayerListing(Player player){
    //     PlayerListing listing = new PlayerListing();
    //     listing.SetPlayerInfo(player);
    //     _listings.Add(listing);

    // }
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.InRoom + " "); 
        Debug.Log(PhotonNetwork.LocalPlayer.NickName);
        playerNameText.text = PhotonNetwork.LocalPlayer.NickName;
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " just joined the room");
        playerNameText1.text = newPlayer.NickName;
//        AddPlayerListing(newPlayer);
    }


}
