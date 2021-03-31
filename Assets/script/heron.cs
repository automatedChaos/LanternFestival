using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heron : MonoBehaviour
{
    public GameObject bird;
    public SpriteRenderer sprite;
    public float moveSpeed;
    bool movingRight;
    // Start is called before the first frame update
    void Start()
    {
        movingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(movingRight == true){
            bird.transform.Translate(moveSpeed,0,0);
            sprite.flipX = true;
        }else if(movingRight == false){
            bird.transform.Translate(-moveSpeed,0,0);
            sprite.flipX = false;
        }


        //float move = Input.GetAxis("Horizontal");
        

        //if (move < 0 && ! sprite.flipX)
           // sprite.flipX = true;
        //if (move > 0 && sprite.flipX)
           // sprite.flipX = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag ("rightBoundary")) {
            movingRight = false;

        }
        if(other.CompareTag ("leftBoundary")){
            movingRight = true;
        }

    }
}
