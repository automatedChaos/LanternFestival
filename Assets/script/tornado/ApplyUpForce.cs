using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyUpForce : MonoBehaviour
{
  public float maxThrust = 2f;
  public float minThrust = .5f;
  // Start is called before the first frame update

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "crab"){
      // stop the crab from moving 
      CrabMovement crabMovement = collision.gameObject.GetComponent<CrabMovement>();
      crabMovement.setStunnedState(true);
      
      // apply a a force
      Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
      Vector2 randomForce = new Vector2(Random.Range(-2, 2), Random.Range(-2, 2));
      rb.AddForce(randomForce, ForceMode2D.Impulse);

      // set timer to unStun crab2w
      StartCoroutine("unStunCrab", crabMovement);
    }
  }

  IEnumerator unStunCrab(CrabMovement movement) {
    yield return new WaitForSeconds(5f);
    movement.setStunnedState(false);
  }
}
