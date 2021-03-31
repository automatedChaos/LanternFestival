using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carb : MonoBehaviour
{

    public GameObject mainBody;
    public Rigidbody2D carbBody;
    //speed
    public float HorizontalSpeed;
    public float VerticalSpeed;

    //up and down force for rigidbody
    public float pushPower;
   

    //animation control
    public Animator Animator;
    //sprite flip
    public SpriteRenderer sprite;

    //move towards control
    private Vector2 target;
    private Vector2 position;

    //presetted positions
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public GameObject target4;
    public GameObject target5;
    public GameObject target6;
    public GameObject target7;
    public GameObject target8;
    public GameObject target9;
    public GameObject target10;



    //variable of inputs
    [Range (0,100)]public float activityLevel;
    [Range(0,100)]public float windIntensity;

    //suggesting using actual weather data (like wind) to affcet movement speed;


    //state management for crab
    enum ControlState {MoveTo1, MoveTo2, MoveTo3,MoveTo4,MoveTo5,MoveTo6,MoveTo7,MoveTo8,MoveTo9,MoveTo10};
	ControlState currentState;
   

    // Start is called before the first frame update
    void Start()
    {
    currentState = ControlState.MoveTo1;
    //target = new Vector2(0.0f,0.0f);
    //position =  mainBody.transform.position;
    carbBody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(activityLevel > windIntensity){
            carbBody.AddForce(transform.up *  pushPower);
        }
        if(activityLevel < windIntensity){
            carbBody.AddForce(-transform.up *  pushPower);
        }


        //custom made crab route
        if(currentState == ControlState.MoveTo1){
       float step = HorizontalSpeed * Time.deltaTime;
        // move sprite towards the target location
        mainBody.transform.position = Vector2.MoveTowards(mainBody.transform.position,target1.transform.position,  step);
        }

        if(currentState == ControlState.MoveTo2){
       float step = HorizontalSpeed * Time.deltaTime;
        mainBody.transform.position = Vector2.MoveTowards(mainBody.transform.position,target2.transform.position,  step);
        }

         if(currentState == ControlState.MoveTo3){
       float step = HorizontalSpeed * Time.deltaTime;
        mainBody.transform.position = Vector2.MoveTowards(mainBody.transform.position,target3.transform.position,  step);
        }

        if(currentState == ControlState.MoveTo4){
       float step = HorizontalSpeed * Time.deltaTime;
        mainBody.transform.position = Vector2.MoveTowards(mainBody.transform.position,target4.transform.position,  step);
        }

        if(currentState == ControlState.MoveTo5){
       float step = HorizontalSpeed * Time.deltaTime;
        mainBody.transform.position = Vector2.MoveTowards(mainBody.transform.position,target5.transform.position,  step);
        }

        if(currentState == ControlState.MoveTo6){
       float step = HorizontalSpeed * Time.deltaTime;
        mainBody.transform.position = Vector2.MoveTowards(mainBody.transform.position,target6.transform.position,  step);
        }
        
        if(currentState == ControlState.MoveTo7){
       float step = HorizontalSpeed * Time.deltaTime;
        mainBody.transform.position = Vector2.MoveTowards(mainBody.transform.position,target7.transform.position,  step);
        }

        if(currentState == ControlState.MoveTo8){
       float step = HorizontalSpeed * Time.deltaTime;
        mainBody.transform.position = Vector2.MoveTowards(mainBody.transform.position,target8.transform.position,  step);
        }

        if(currentState == ControlState.MoveTo9){
       float step = HorizontalSpeed * Time.deltaTime;
        mainBody.transform.position = Vector2.MoveTowards(mainBody.transform.position,target9.transform.position,  step);
        }

        if(currentState == ControlState.MoveTo10){
       float step = HorizontalSpeed * Time.deltaTime;
        mainBody.transform.position = Vector2.MoveTowards(mainBody.transform.position,target10.transform.position,  step);
        }


        

        //if(Mathf.Abs(HorizontalSpeed)== 0 && Mathf.Abs(VerticalSpeed)== 0 )
        //Animator.SetBool("isWalking",false);
  
        float move = Input.GetAxis("Horizontal");
        //Animator.SetFloat("speed",Mathf.Abs(HorizontalSpeed));

        if (move < 0 && ! sprite.flipX)
            sprite.flipX = true;
        if (move > 0 && sprite.flipX)
            sprite.flipX = false;

        
    }

    //collision detection
    void OnTriggerEnter2D(Collider2D other) {
        
		if (other.CompareTag ("target1")) {

			
			currentState = ControlState.MoveTo2;
			
		}

        if (other.CompareTag ("target2")) {

			
			currentState = ControlState.MoveTo3;
			
		}

         if (other.CompareTag ("target3")) {

			
			currentState = ControlState.MoveTo4;
			
		}

        if (other.CompareTag ("target4")) {

			
			currentState = ControlState.MoveTo5;
			
		}

        if (other.CompareTag ("target5")) {

			
			currentState = ControlState.MoveTo6;
			
		}

        if (other.CompareTag ("target6")) {

			
			currentState = ControlState.MoveTo7;
			
		}

        if (other.CompareTag ("target7")) {

			
			currentState = ControlState.MoveTo8;
			
		}

        if (other.CompareTag ("target8")) {

			
			currentState = ControlState.MoveTo9;
			
		}

        if (other.CompareTag ("target9")) {

			
			currentState = ControlState.MoveTo10;
			
		}

        //slow down the speed when walk through mugpit
        if (other.CompareTag ("mug")) {

			//stranded = true;
			Debug.Log ("slow down");
			//currentSpeed = currentSpeed-1;
			//currentSpeed -= shipAcceleration * Time.deltaTime;
		}
	}

      
}
