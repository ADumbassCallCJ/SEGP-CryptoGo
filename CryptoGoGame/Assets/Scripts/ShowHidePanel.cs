using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHidePanel : MonoBehaviour
{
    public GameObject Panel;

    public void hidePanel(){
        Panel.SetActive(false);
    }

    public void showPanel(){
        Panel.SetActive(true);
    }
}
