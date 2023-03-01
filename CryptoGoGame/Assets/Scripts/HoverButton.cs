using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverButton : MonoBehaviour
{
    public void PointerEnter(){
        transform.localScale = new Vector2(1.1f, 1.1f);
    }

    public void PointerEnter2(){
        transform.localScale = new Vector2(1.2f, 1.2f);
    }

    public void ExitPointer(){
        transform.localScale = new Vector2(1f, 1f);
    }
    
}
