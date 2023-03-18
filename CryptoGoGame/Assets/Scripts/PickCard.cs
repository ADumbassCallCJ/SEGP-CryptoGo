using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class PickCard : MonoBehaviour, IPointerClickHandler
{
    public GameObject pickCard;
    private GameObject pickCardZone;
    private GameObject handZone; 
    private GameObject playCardButton;
    private PlayerDeck playerDeck;

    // Start is called before the first frame update
    void Start()
    {
        if(transform.CompareTag("AuxiliaryCard")){
            handZone = transform.parent.gameObject;
        }
        else{
            handZone = GameObject.Find("Hand");
        }
        // handZone = transform.parent.gameObject;
        pickCard = GameObject.Find("PickCard");
        
        pickCardZone = GameObject.Find("PickCardZone");
        playerDeck = GameObject.Find("Background Image").GetComponent<PlayerDeck>();
        playCardButton = playerDeck.PlayCardButton;
        // Debug.Log(pickCard);
    }


    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if(playerDeck.GetCurrentPlayerTurn() == PhotonNetwork.LocalPlayer){
//            Debug.Log("U click the card");
  //          Debug.Log(transform.parent.gameObject);
    //        Debug.Log(PlayerDeck.pickCardZone);
            if(transform.parent.gameObject == handZone && PlayerDeck.pickCard.active == false){
                PlayerDeck.pickCard.SetActive(true);
                playCardButton = GameObject.Find("PlayCardButton");
                if(transform.CompareTag("AuxiliaryCard")){
                    playCardButton.SetActive(false);
                }

                transform.SetParent(PlayerDeck.pickCardZone.transform);
               transform.localScale = new Vector3(1f,0.9f,1);
            }
            
        }

        
    }
    private void setActiveButton(GameObject gameObject){
        if(gameObject.active){
            gameObject.SetActive(false);
        }
        else{
            gameObject.SetActive(true);
        }
    }

    
}
