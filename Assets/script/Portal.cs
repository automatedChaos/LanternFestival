using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
  public Transform destination; 

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "crab"){
      // stop the crab from moving 
      collision.gameObject.transform.position = destination.position;
    }
  }
}
