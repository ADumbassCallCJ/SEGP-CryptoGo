using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private bool isDragging = false;
    private bool isOverDropZone = false;
    private GameObject dropZone; 
    private GameObject handZone;
    private Vector2 startPosition;

    void Start(){
      //  transform.gameObject.SetActive(false);
      dropZone = GameObject.Find("DropZone");
      handZone = GameObject.Find("Hand");
      startPosition = transform.position;
 //    Debug.Log(startPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if(isDragging){
            if(transform.parent == dropZone){

            }
            else{
                transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
            
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
  //      dropZone = collision.gameObject;
        isOverDropZone = true;
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        isOverDropZone = false;
  //      dropZone = null;    
    }
    public void StartDrag(){
    //    Debug.Log(transform.parent);
    //    startPosition = transform.position;
        isDragging = true;

        

    }
    public void EndDrag(){
        isDragging = false;
        if(isOverDropZone){
            transform.SetParent(dropZone.transform);
            transform.localScale = new Vector3(1.4f,1.4f,1);
        }
        else{
       //     Debug.Log("aaaa");
        //    Debug.Log(startPosition);
            transform.position = new Vector2(startPosition.x, startPosition.y);
//            transform.SetParent(handZone.transform);
            transform.localScale = new Vector3(1f,1f,1);
        }

    }
}
