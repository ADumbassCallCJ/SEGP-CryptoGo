using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private RoomListing _roomListing;

    [SerializeField]
    private Transform _content;

    private List<RoomListing> _listings = new List<RoomListing>(); 

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomInfo info in roomList)
        {
            if(info.RemovedFromList){
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if(index != -1){
                    Destroy(_listings[index].gameObject);
                    _listings.RemoveAt(index);
                }
            }
            
            else{         
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if(index == -1){
                    RoomListing listing = Instantiate(_roomListing, _content);
                    if(listing != null){
                        listing.SetRoomInfo(info);
                        _listings.Add(listing);
                    }
                }

            }
        }
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
