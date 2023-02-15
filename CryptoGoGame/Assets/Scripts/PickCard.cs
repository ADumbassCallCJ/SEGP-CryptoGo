using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickCard : MonoBehaviour, IPointerClickHandler
{
    private GameObject pickCard;
    private GameObject pickCardZone;
    private GameObject handZone; 
    
    // Start is called before the first frame update
    void Start()
    {
        handZone = transform.parent.gameObject;
        pickCard = GameObject.Find("PickCard");
        pickCardZone = GameObject.Find("PickCardZone");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("U click the card");
        Debug.Log(handZone);

        if(transform.parent.gameObject == handZone && pickCard.active == false){
            pickCard.SetActive(true);
            transform.SetParent(pickCardZone.transform);
            transform.localScale = new Vector3(1.4f,1.4f,1);
        }

        
    }

    
}
