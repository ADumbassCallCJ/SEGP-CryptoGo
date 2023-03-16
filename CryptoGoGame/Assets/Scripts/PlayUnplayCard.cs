using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class PlayUnplayCard : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    private GameObject cardSelected; 

    private GameObject handZone;
    private GameObject pickCard;
    private GameObject pickCardZone;
    private GameObject playZone;

    private PlayerDeck playerDeck;

    

   
    
    void Start(){
        handZone = GameObject.Find("Hand");
        pickCard = GameObject.Find("PickCard");
        playZone = GameObject.Find("PlayZone");
        pickCardZone = GameObject.Find("PickCardZone");
        playerDeck = GameObject.Find("Background Image").GetComponent<PlayerDeck>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(pickCard.active){
        // //    pickCardZone = GameObject.Find("PickCardZone");
        //    cardSelected =  pickCardZone.transform.GetChild(0).gameObject;
        // }
    }
    public void UnpickCard(){
        if(pickCardZone.transform.GetChild(0).gameObject){
            cardSelected =  pickCardZone.transform.GetChild(0).gameObject;
        }
        cardSelected.transform.localScale = new Vector3(1f,1f,1);
        cardSelected.transform.SetParent(handZone.transform);
        pickCard.SetActive(false);
        cardSelected = null;

    }

    
    public void PlayCard(){
        if(pickCardZone.transform.GetChild(0).gameObject){
            cardSelected =  pickCardZone.transform.GetChild(0).gameObject;
        }

        Debug.Log(playerDeck.GetCurrentPlayerTurn());
        if(playerDeck.GetCurrentPlayerTurn() == PhotonNetwork.LocalPlayer){
            Debug.Log( PhotonNetwork.LocalPlayer + " play card");
    ///      playZone.SetActive(true);
            cardSelected.transform.localScale = new Vector3(0.8f,0.8f,1);
            cardSelected.transform.SetParent(playZone.transform);
            Card card = cardSelected.GetComponent<ThisCard>().Card;
            Debug.Log(card.Name);
            pickCard.SetActive(false);
            cardSelected = null;
            GameObject.Find("Background Image").GetComponent<PhotonView>().RPC("RPC_PlayCards", RpcTarget.AllBuffered, playerDeck.GetCurrentPlayerTurn(), card.Id);
         //   playerDeck.PlayCards(PhotonNetwork.LocalPlayer, card);
            GameObject.Find("Background Image").GetComponent<PhotonView>().RPC("RPC_NextTurn", RpcTarget.All);

        }


    }


    

}
