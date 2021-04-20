using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrabMoveType2 : MonoBehaviour
{
  //crab body related;
  public GameObject mainBody;

  // controlling the scale of the crab
  public float scalingSpeed;
  public Vector3 min = new Vector3(0.06f, 0.06f, 1f);
  public Vector3 max = new Vector3(0.11f, 0.11f, 1f);


  public Rigidbody2D carbBody;
  //public SpriteRenderer sprite;

  //animation control
  public Animator Animator;
  bool walking;
  bool waving;

  //telepotation reference objects
  public GameObject reciver1;
  public GameObject reciver2;


  //health
  [Range(0, 100)] public float HealthAmount = 100;
  //public Text healthText;

  //speed
  public float HorizontalSpeed;
  public float VerticalSpeed;

  private int[] networkWASD = { 0,0,0,0,0};


  // Start is called before the first frame update
  void Start()
  {
    carbBody = GetComponent<Rigidbody2D>();
    Animator.SetBool("isM1Walking", false);
    walking = false;
    //healthText.text = HealthAmount+"";

    //scale limits




  }

  // Update is called once per frame
  void FixedUpdate()
  {
    
    //healthText.text = HealthAmount+"";

    // up
    if (Input.GetKey(KeyCode.W) || networkWASD[0] == 1)
    {
      mainBody.transform.Translate(0, VerticalSpeed, 0);
      walking = true;
      mainBody.gameObject.transform.localScale -= new Vector3(0.0001f, 0.0001f, 0);
    }

    // left
    if (Input.GetKey(KeyCode.A) || networkWASD[1] == 1)
    {
      mainBody.transform.Translate(-HorizontalSpeed, 0, 0);
      walking = true;
    }

    // right
    if (Input.GetKey(KeyCode.S) || networkWASD[2] == 1)
    {
      mainBody.transform.Translate(0, -VerticalSpeed, 0);
      walking = true;
      mainBody.gameObject.transform.localScale += new Vector3(0.0001f, 0.0001f, 0);
    }

    // down
    if (Input.GetKey(KeyCode.D) || networkWASD[3] == 1)
    {
      mainBody.transform.Translate(HorizontalSpeed, 0, 0);
      walking = true;
    }


    //limit the crab scale when reach the farthest/closest border

    if (mainBody.gameObject.transform.localScale.x > 0.11f)
    {
      Vector3 l = mainBody.gameObject.transform.localScale;
      l.x = 0.11f;
      mainBody.gameObject.transform.localScale = l;

    }
    if (mainBody.gameObject.transform.localScale.y > 0.11f)
    {
      Vector3 l = mainBody.gameObject.transform.localScale;
      l.y = 0.11f;
      mainBody.gameObject.transform.localScale = l;

    }

    if (mainBody.gameObject.transform.localScale.x < 0.06f)
    {
      Vector3 l = mainBody.gameObject.transform.localScale;
      l.x = 0.06f;
      mainBody.gameObject.transform.localScale = l;

    }
    if (mainBody.gameObject.transform.localScale.y < 0.06f)
    {
      Vector3 l = mainBody.gameObject.transform.localScale;
      l.y = 0.06f;
      mainBody.gameObject.transform.localScale = l;

    }




    //crab waving when press and hold space button
    if (Input.GetKey(KeyCode.Space) || networkWASD[4] == 1 )
    {
      Debug.Log("WAVE");
      waving = true;
      Animator.SetBool("isM1Waving", true);
    }
    else 
    {
      waving = false;
      Animator.SetBool("isM1Waving", false);
    }


    if (walking == true)
    {
      Animator.SetBool("isM1Walking", true);
    }
    else if (walking == false)
    {
      Animator.SetBool("isM1Walking", false);
    }

    resetNetworkWASD();
    walking = false;
    waving = false;
  }
  //public void healthManage(float change){
  //HealthAmount += change;
  //if (HealthAmount < 0) {
  //	HealthAmount = 0;

  //}


  //}

  //collision detection
  void OnTriggerEnter2D(Collider2D other)
  {

    if (other.CompareTag("target1"))
    {

      HealthAmount -= 50;
      Debug.Log(mainBody.gameObject.transform.position.y);
      //telepotation 
      mainBody.gameObject.transform.position = new Vector3(reciver1.gameObject.transform.position.x, reciver1.gameObject.transform.position.y, reciver1.gameObject.transform.position.z);
    }

    if (other.CompareTag("target2"))
    {

      //mainBody.gameObject.transform.position = new Vector3(1.44f,-0.4499999f,-70f);
      mainBody.gameObject.transform.position = new Vector3(reciver2.gameObject.transform.position.x, reciver2.gameObject.transform.position.y, reciver2.gameObject.transform.position.z);


    }

  }

  public void setNetworkWASD(int[] values)
  {
    for (int i = 0; i < networkWASD.Length; i++)
    {
      networkWASD[i] = values[i];
    }
  }

  void resetNetworkWASD()
  {
    for (int i = 0; i < networkWASD.Length; i++)
    {
      networkWASD[i] = 0;
    }
  }
}
