using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickCard : MonoBehaviour, IPointerClickHandler
{
    public GameObject pickCard;
    private GameObject pickCardZone;
    private GameObject handZone; 
    
    // Start is called before the first frame update
    void Start()
    {
        handZone = transform.parent.gameObject;
        // pickCard = GameObject.Find("PickCard");
        // pickCardZone = GameObject.Find("PickCardZone");
        // Debug.Log(pickCard);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("U click the card");
        Debug.Log(transform.parent.gameObject);
        Debug.Log(PlayerDeck.pickCardZone);
        if(transform.parent.gameObject == handZone){
            PlayerDeck.pickCard.SetActive(true);
            transform.SetParent(PlayerDeck.pickCardZone.transform);
            transform.localScale = new Vector3(1.4f,1.4f,1);
        }

        
    }

    
}
