using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUnplayCard : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject cardSelected; 

    private GameObject handZone;
    private GameObject pickCard;
    private GameObject pickCardZone;
    private GameObject playZone;
   
    
    void Start(){
        handZone = GameObject.Find("Hand");
        pickCard = GameObject.Find("PickCard");
        playZone = GameObject.Find("PlayZone");
        pickCardZone = GameObject.Find("PickCardZone");
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
        Debug.Log("Play card");
      //  playZone.SetActive(true);
        cardSelected.transform.localScale = new Vector3(0.8f,0.8f,1);
        cardSelected.transform.SetParent(playZone.transform);
        pickCard.SetActive(false);
        cardSelected = null;

    }
}
