using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Realtime;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListing : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _text;

    [SerializeField]
    private Image _readyIcon;

    [SerializeField]
    private Image _hostIcon;

    private bool ready = false;

    public Player Player { get; private set; }
    public bool Ready {get{return ready;}}
    

    public void SetPlayerInfo(Player player){
        Player = player;
        _text.text = Player.NickName;
        if(player == PhotonNetwork.MasterClient){
            _readyIcon.gameObject.SetActive(false);
            _hostIcon.gameObject.SetActive(true);
        }
        else{
            _hostIcon.gameObject.SetActive(false);
            _readyIcon.gameObject.SetActive(false);
        }

    
    }
    public void SetReady(bool readyState){
        ready = readyState;
        if(ready){
            _readyIcon.gameObject.SetActive(true);
        }
        else{
            _readyIcon.gameObject.SetActive(false);
        }
    }

}
