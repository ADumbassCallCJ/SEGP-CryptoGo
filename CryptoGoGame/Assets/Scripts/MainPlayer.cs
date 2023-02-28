using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Realtime;
[System.Serializable]

public class MainPlayer : MonoBehaviour
{
   private Player player;
   [SerializeField]
   private Text playerName; 
   public Text PlayerName{get{return playerName;}}

   [SerializeField]
   private Image playerImage;
   public Image PlayerImage{get{return playerImage;}}

   [SerializeField]
   private GameObject hand;
   public GameObject Hand{get{return hand;}}

   [SerializeField]
   private GameObject playZone;
   public GameObject PlayZone{get{return playZone;}}

   public Player Player{get {return player;}}
   public void SetPlayerInfo(Player playerInfo){
      player = playerInfo;
      playerName.text = player.NickName;
   }
}
