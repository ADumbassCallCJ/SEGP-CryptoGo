using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCardsGenerate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject w = transform.GetChild(0).gameObject;
        GameObject g;
        for(int i=0; i<5; i++){
            g = Instantiate(w, transform);
        }
        Destroy(w);
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
