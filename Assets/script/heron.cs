using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heron : MonoBehaviour
{
    public GameObject bird;
    //public SpriteRenderer sprite;
    public float moveSpeed;
    bool movingRight;

    public Animator Animator;

    bool walking;
    bool scalingDown;
    bool scalingUp;

    public float scalingSpeed ;
    public Vector3 min = new Vector3(0.06f, 0.06f, 1f);
    public Vector3 max = new Vector3(0.11f, 0.11f, 1f);


// the 'rail'
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    
    
    


    enum ControlState {MoveTo1, MoveTo2, MoveTo3};
	ControlState currentState;

    // Start is called before the first frame update
    void Start()
    {
        movingRight = true;
        Vector3 newScale = bird.gameObject.transform.localScale;
        walking = true;
        currentState = ControlState.MoveTo1;
    }

    // Update is called once per frame
    void Update()
    {
        if(movingRight == true){
           // bird.transform.Translate(moveSpeed,0,0);
           transform.localScale = new Vector3(0.6f,0.6f,0.6f);
            
        }else if(movingRight == false){
            //bird.transform.Translate(-moveSpeed,0,0);
            transform.localScale = new Vector3(-0.6f,0.6f,0.6f);
            
        }


        //move partten

         if(currentState == ControlState.MoveTo1){
       float step = moveSpeed * Time.deltaTime;
        // move sprite towards the target location
        bird.transform.position = Vector2.MoveTowards(bird.transform.position,target1.transform.position,  step);
        }

        if(currentState == ControlState.MoveTo2){
       float step = moveSpeed * Time.deltaTime;
        bird.transform.position = Vector2.MoveTowards(bird.transform.position,target2.transform.position,  step);
        }

         if(currentState == ControlState.MoveTo3){
       float step = moveSpeed * Time.deltaTime;
        bird.transform.position = Vector2.MoveTowards(bird.transform.position,target3.transform.position,  step);
        }

        
        
        

        //float move = Input.GetAxis("Horizontal");
        

        //if (move < 0 && ! sprite.flipX)
           // sprite.flipX = true;
        //if (move > 0 && sprite.flipX)
           // sprite.flipX = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        //if (other.CompareTag ("rightBoundary")) {
            //movingRight = false;

        //}
        //if(other.CompareTag ("leftBoundary")){
            //movingRight = true;
        //}
        if(walking== true){
        if (other.CompareTag ("target1")) {

			
			currentState = ControlState.MoveTo2;

            movingRight = false;
			
		}

        if (other.CompareTag ("target2")) {

			
			currentState = ControlState.MoveTo3;

           // movingRight = true;
			
		}

         if (other.CompareTag ("target3")) {

			
			currentState = ControlState.MoveTo1;

            movingRight = true;
			
		}

       

       

       

        

       
    }else if(walking== true)
    currentState = ControlState.MoveTo1;


        

    }
}
