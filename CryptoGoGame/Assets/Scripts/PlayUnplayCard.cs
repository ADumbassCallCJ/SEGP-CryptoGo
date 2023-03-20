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
    private GameObject auxiliaryZone;
    private GameObject pickCard;
    private GameObject pickCardZone;
    private GameObject playZone;


    private PlayerDeck playerDeck;

    

   
    
    void Start(){
        handZone = GameObject.Find("Hand");
        auxiliaryZone = GameObject.Find("AuxiliaryCards");
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
        GameObject comebackZone;
        if(pickCardZone.transform.GetChild(0).gameObject){
            cardSelected =  pickCardZone.transform.GetChild(0).gameObject;
            
        }
        if(cardSelected.CompareTag("AuxiliaryCard")){
            comebackZone = auxiliaryZone;
            cardSelected.transform.localScale = new Vector3(1.1f,1.1f,1);
        }
        else{
            comebackZone = handZone;
    
        }
        Debug.Log(comebackZone);
        
        cardSelected.transform.SetParent(comebackZone.transform);
        cardSelected.transform.localScale = new Vector3(1f,1f,1);
        if(!playerDeck.PlayCardButton.active){
            playerDeck.PlayCardButton.SetActive(true);
        }
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
            cardSelected.transform.SetParent(playZone.transform);
            cardSelected.transform.localScale = new Vector3(0.8f,0.8f,1);
            Card card = cardSelected.GetComponent<ThisCard>().Card;
            Debug.Log(card.Name);
            pickCard.SetActive(false);
            cardSelected = null;
       //      GameObject.Find("Background Image").GetComponent<PhotonView>().RPC("RPC_PlayCards", RpcTarget.All, playerDeck.GetCurrentPlayerTurn(), card.Id);
    
            GameObject.Find("Background Image").GetComponent<PhotonView>().RPC("RPC_NextTurn", RpcTarget.All, card.Id);

        }


    }


    

}
