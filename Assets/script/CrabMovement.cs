using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrabMovement : MonoBehaviour
{
  //animation control
  public Animator Animator;
  public float crabSpeed = 1.5f; 

  public float crabWalkDuration = 1f;
  private int[] networkWASD = { 0,0,0,0,0};
  
  public int crabHealth = 100;

  public bool isStunned = false;

  private Rigidbody2D rigidbody;

  // Start is called before the first frame update
  void Start()
  {
    Animator.SetBool("isWalking", false);
    rigidbody = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    // 
    if (Animator.GetBool("isWalking") == true) return;
    if (isStunned) return;

    if (Input.GetKey(KeyCode.W) || networkWASD[0] == 1)
    {
      StartCoroutine("moveCrab", new Vector3(0, crabSpeed, 0)); 
    }

    // left
    if (Input.GetKey(KeyCode.A) || networkWASD[1] == 1)
    {
      StartCoroutine("moveCrab", new Vector3(-crabSpeed, 0, 0));
    }

    // down 
    // ! order has been muddled for network control
    if (Input.GetKey(KeyCode.S) || networkWASD[3] == 1)
    {
      StartCoroutine("moveCrab", new Vector3(0, -crabSpeed, 0));
    }

    // right
    if (Input.GetKey(KeyCode.D) || networkWASD[2] == 1)
    {
      StartCoroutine("moveCrab", new Vector3(crabSpeed, 0, 0));
    }

    // crab waving when press and hold space button
    if (Input.GetKey(KeyCode.Space) || networkWASD[4] == 1 )
    {
      StartCoroutine("crabWave");
    }

    resetNetworkWASD();
  }

  public void setStunnedState (bool state) 
  {
    isStunned = state;
    if (isStunned && !Animator.GetBool("isWaving")) sendDamage();
    if (rigidbody == null) {
      rigidbody = GetComponent<Rigidbody2D>();
    }
  }
  public void setNetworkWASD(int[] values)
  {
    for (int i = 0; i < networkWASD.Length; i++)
    {
      networkWASD[i] = values[i];
    }
  }

  void sendDamage () {
    Connection network = GameObject.Find("Network").GetComponent<Connection>();
    if (network != null) {
      network.sendDamage(gameObject.name);
    }
  }
  
  IEnumerator moveCrab (Vector3 direction) {
    if (rigidbody != null) {
      rigidbody.velocity = direction;
      Animator.SetBool("isWalking", true);
      yield return new WaitForSeconds(crabWalkDuration);
      Animator.SetBool("isWalking", false);
      rigidbody.velocity = new Vector3(0,0,0);
    }
  }

  IEnumerator crabWave () {
    Animator.SetBool("isWaving", true);
    Animator.SetBool("isWalking", false);
    rigidbody.velocity = new Vector3(0, 0, 0);
    setStunnedState(true);
    yield return new WaitForSeconds(1f);
    Animator.SetBool("isWaving", false);
    setStunnedState(false);
  }

  void resetNetworkWASD()
  {
    for (int i = 0; i < networkWASD.Length; i++)
    {
      networkWASD[i] = 0;
    }
  }
  Vector3 addVectors(Vector3 originalVector, Vector3 destinationVector)
  {
    return originalVector + destinationVector;
  }
}
