using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class JoinRoom : MonoBehaviourPunCallbacks, IPointerClickHandler
{
    private RoomListing _roomListing; 
    public void setRoomListing(RoomListing roomListing){_roomListing = roomListing;}
    public void OnPointerClick(PointerEventData eventData)
    {   
        Debug.Log("You click the room " + _roomListing.RoomInfo.Name);
        PhotonNetwork.JoinRoom(_roomListing.RoomInfo.Name);
    }
}
