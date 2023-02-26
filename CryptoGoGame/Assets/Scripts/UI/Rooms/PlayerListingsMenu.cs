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

    [SerializeField]
    private Text _readyUpText;

    [SerializeField]
    private Button _readyButton;

    [SerializeField]
    private Button _startButton;

    [SerializeField]
    private PlayerDeck playerDeck;

    private bool _ready = false;
    private List<PlayerListing> _listings = new List<PlayerListing>(); 

    private void Awake(){
        Debug.Log("Call");
        GetCurrentRoomPlayers();
        ButtonsHide();
    }
    private void ButtonsHide(){
        Debug.Log("ButtonsHide() call");
        Debug.Log(PhotonNetwork.IsMasterClient);
        if(!PhotonNetwork.IsMasterClient){
            _startButton.gameObject.SetActive(false);
        }
        else{
            _readyButton.gameObject.SetActive(false);
            int index = _listings.FindIndex(x => x.Player == PhotonNetwork.MasterClient);
            if(index != -1){
                _listings[index].SetReady(false);

            }
            _readyUpText.text = "Waiting others ready";
        }
    }
    public override void OnEnable()
    {
        base.OnEnable();
        SetReadyUp(false);
    }
    private void SetReadyUp(bool state){
        _ready = state;
        if(!PhotonNetwork.IsMasterClient){
            if(_ready){
                _readyUpText.text = "Ready";
            }
            else{
                _readyUpText.text = "Unready";
            }
            
        }
    }
    public void OnClick_ReadyUp(){
        Debug.Log("Ready button clicked");
        if(!PhotonNetwork.IsMasterClient){
            SetReadyUp(!_ready);
        //    base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, !_ready);
            this.photonView.RPC("RPC_ChangeReadyState", RpcTarget.All, PhotonNetwork.LocalPlayer, _ready);
        }
    }
    public void OnClick_StartGame(){
        if(PhotonNetwork.IsMasterClient){
            foreach(PlayerListing player in _listings){
                if(!player.Ready && player.Player != PhotonNetwork.MasterClient){
                    return;
                }
            }
            playerDeck.StartGame();
            this.photonView.RPC("RPC_StartingGame", RpcTarget.All);
//            _readyUpText.gameObject.SetActive(false);
            _readyButton.gameObject.SetActive(false);
            _startButton.gameObject.SetActive(false);
        }
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
            listing.SetReady(false);
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
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        base.OnMasterClientSwitched(newMasterClient);
    }

    [PunRPC]
    private void RPC_ChangeReadyState(Player player, bool ready){
        Debug.Log("RPC_ChangeReadyState() called");
        Debug.Log(player.NickName + " , Ready: " + ready);
        int index = _listings.FindIndex(x => x.Player == player);
        if(index != -1){
            _listings[index].SetReady(ready);
        }
    }

    [PunRPC]
    private void RPC_StartingGame(){
        _readyUpText.gameObject.SetActive(false);
    }
}