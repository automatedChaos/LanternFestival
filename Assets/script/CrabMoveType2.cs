using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrabMoveType2 : MonoBehaviour
{
    //crab body related;
    public GameObject mainBody;
    public Rigidbody2D carbBody;
    //public SpriteRenderer sprite;

    //animation control
    public Animator Animator;
    bool walking;
    bool waving;

    //health
    [Range (0,100)]public float HealthAmount=100;
    public Text healthText;

    //speed
    public float HorizontalSpeed;
    public float VerticalSpeed;


    // Start is called before the first frame update
    void Start()
    {
        carbBody = GetComponent<Rigidbody2D>();
        Animator.SetBool("isM1Walking",false);
        walking = false;
        healthText.text = HealthAmount+"";
    }

    // Update is called once per frame
    void Update()
    {
        walking =false;
        waving =false;
         healthText.text = HealthAmount+"";
        
        //movement control
        if(Input.GetKey(KeyCode.A)){
            mainBody.transform.Translate(-HorizontalSpeed,0,0);
           walking =true;
        }
         if(Input.GetKey(KeyCode.D)){
            mainBody.transform.Translate(HorizontalSpeed,0,0);
            walking =true;
        }
        if(Input.GetKey(KeyCode.W)){
            mainBody.transform.Translate(0,VerticalSpeed,0);
            walking =true;
        }
         if(Input.GetKey(KeyCode.S)){
            mainBody.transform.Translate(0,-VerticalSpeed,0);
            walking =true;
        }

        //crab waving when press button
        if(Input.GetKey(KeyCode.Space)){
            waving = true;
        }
        if(waving == true){
            Animator.SetBool("isM1Waving",true);
        }else if(waving == false){
             Animator.SetBool("isM1Waving",false);
        }


        if(walking == true){
            Animator.SetBool("isM1Walking",true);
        }else if(walking == false){
             Animator.SetBool("isM1Walking",false);
        }


        
    }
    public void healthManage(float change){
		HealthAmount += change;
		if (HealthAmount < 0) {
			HealthAmount = 0;
        
		}
        
        
    }

    //collision detection
     void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag ("mug")) {

            HealthAmount -= 50;
            Debug.Log(HealthAmount);
        }

     }
        
}
