using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hand : MonoBehaviour
{
    public GameObject human;
    public float moveSpeed;
    bool movingRight;
    bool onRail;
    public Animator Animator;

    public BoxCollider2D detectorCollider;

    // Start is called before the first frame update
    void Start()
    {
         movingRight = true;
         onRail = true;

         this.detectorCollider = this.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(onRail == true){
            if(movingRight == true){
                human.transform.Translate(moveSpeed,0,0);
          
            
            }else if(movingRight == false){
                human.transform.Translate(-moveSpeed,0,0);
            
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag ("rightBoundary")) {
            movingRight = false;

        }
        if(other.CompareTag ("leftBoundary")){
            movingRight = true;
        }

        if(detectorCollider.CompareTag("crab")){
            Debug.Log("grab");
            onRail =false;
        }
    }
    
}

