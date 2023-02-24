using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class PlayerListingsMenu : MonoBehaviourPunCallbacks
{
   [SerializeField]
    private PlayerListing _playerListing;

    [SerializeField]
    private Transform _content;

    private List<PlayerListing> _listings = new List<PlayerListing>(); 

    private void Awake(){
        GetCurrentRoomPlayers();
    }
    private void GetCurrentRoomPlayers(){
        foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players){
            Debug.Log("Player : " + playerInfo.Value.NickName);
            AddPlayerListing(playerInfo.Value);
        }
    }
    public void AddPlayerListing(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " just joined the room");
        PlayerListing listing = Instantiate(_playerListing,_content);
        if(listing != null){
            listing.SetPlayerInfo(newPlayer);
            _listings.Add(listing);
        
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer){
        int index = _listings.FindIndex(x => x.Player == otherPlayer);
        if(index != -1){
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }
    
}