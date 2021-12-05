using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
  public Transform destination; 

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "crab"){
      StartCoroutine("teleport", collision.gameObject);
    }
  }

  IEnumerator teleport(GameObject crab)
  {
    // get script
    CrabMovement cm = crab.GetComponent<CrabMovement>();
    Animator ca = crab.GetComponent<Animator>();

    if (cm != null && ca != null){
      cm.setStunnedState(true);
      ca.SetTrigger("crawlingOut");
      yield return new WaitForSeconds(3f);
      crab.transform.position = destination.position;
      ca.SetTrigger("crawlingIn");
      yield return new WaitForSeconds(3f);
      cm.setStunnedState(false);
    } 
  }
}
