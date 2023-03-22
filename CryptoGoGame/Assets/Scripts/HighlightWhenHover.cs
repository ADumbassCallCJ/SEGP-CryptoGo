using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighlightWhenHover : MonoBehaviour
{
    public Button button;
    public TMP_Text buttonText;

    // Start is called before the first frame update
    void Start()
    {
        //block = button.colors;
        //originalColor = block.selectedColor;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeWhenHover(){
        //block.selectedColor = wantedColor;
        //button.color = Color.white;
        buttonText.color = new Color(255, 172, 28);
    }

    public void changeWhenLeaves(){
        //block.selectedColor = originalColor;
        //button.color = Color.white;
        buttonText.color = Color.blue;
    }
}

