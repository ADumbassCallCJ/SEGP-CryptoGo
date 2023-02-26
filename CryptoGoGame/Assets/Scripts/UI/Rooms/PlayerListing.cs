using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Realtime;


public class PlayerListing : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    [SerializeField]
    private Image _readyIcon;

    private bool ready = false;

    public Player Player { get; private set; }
    public bool Ready {get{return ready;}}

    public void SetPlayerInfo(Player player){
        Player = player;
        _text.text = Player.NickName;
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
