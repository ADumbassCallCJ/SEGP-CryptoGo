using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Realtime;
using Photon.Pun;
using Photon.Realtime;

public class Enemy : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject _kickButton;

    private Player currentPlayer;

    void Awake(){
        if(!PhotonNetwork.IsMasterClient){
            _kickButton.SetActive(false);
        }
    }
    public void SetInfoEnemyPlayer(Player player){
        currentPlayer = player;
        Debug.Log("Set enemy: " + player.NickName);
    }
    public void KickPlayer(){
        if(PhotonNetwork.IsMasterClient){
            Debug.Log("Button clicked " + currentPlayer.NickName);
            PhotonNetwork.CloseConnection(currentPlayer);
            // RemovePlayerInTheList();
        }
    }

}
