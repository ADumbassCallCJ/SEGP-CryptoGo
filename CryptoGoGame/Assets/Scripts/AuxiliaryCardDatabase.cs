using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuxiliaryCardDatabase : MonoBehaviour
{
    private static List<string> staticAuxiliaryCardList = new List<string>();
    public List<string> StaticAuxiliaryCardList{
        get{
            return staticAuxiliaryCardList;
        }
    }
    void Awake(){
        Debug.Log("auxiliaryCardDatabase.Awake() called");
        AddCards();
    }

    public void AddCards(){
        Debug.Log("auxiliaryCardDatabase.AddCards() called");
        staticAuxiliaryCardList.Add("AuxCards/AE_OM");
        staticAuxiliaryCardList.Add("AuxCards/H_H");
        staticAuxiliaryCardList.Add("AuxCards/MAC_BC");
        staticAuxiliaryCardList.Add("AuxCards/SC_SC");
   
    }


}
