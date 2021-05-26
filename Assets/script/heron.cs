using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heron : MonoBehaviour
{
  public Animator animator;
  public Transform[] breadcrumbs;     // points to move between 
  public Transform catchTarget;
  public Transform mouth;
  private int marker = 0;
  private int previousScaleDirection = 1;
  private bool heronActive = false;      // if true then heron is busy doing something and should not be catching crabs
  private GameObject caughtCrab;
  // Start is called before the first frame update
  void Start()
  {
      TweenToNext();
  }

  void Update()
  {
    if (heronActive) return;

    // detect crabs to catch
    Collider2D[] hits = Physics2D.OverlapCircleAll(
        catchTarget.position, 1.2f
    );

    for (int i = 0; i < hits.Length; i++) {
        if (hits[i].tag == "crab") {

            heronActive = true;
            animator.SetTrigger("GrabDown");
            caughtCrab = hits[i].gameObject;

            // Stun crab
            CrabMovement movement = caughtCrab.GetComponent<CrabMovement>();
            movement.setStunnedState(true);

            StartCoroutine("CatchCrab"); 
        }
    }
  }

  IEnumerator CatchCrab () {
    // wait for the grab animation to finish
    yield return new WaitForSeconds(0.4f);
    
    // stop the crab from interacting with other stuff
    Rigidbody2D rb = caughtCrab.GetComponent<Rigidbody2D>();
    Destroy(rb);

    // position crab in mouth and attach to mouth
    caughtCrab.transform.position = mouth.transform.position;
    caughtCrab.transform.SetParent(mouth.transform);
    animator.SetTrigger("GrabUp");

    // set random time to drop crab
    StartCoroutine("DropCrab");
  }

  IEnumerator DropCrab()
  {
    yield return new WaitForSeconds(Random.Range(7, 15));
    caughtCrab.transform.parent = null;
    caughtCrab.transform.localRotation = Quaternion.identity;
    Rigidbody2D rb = caughtCrab.AddComponent<Rigidbody2D>();
    rb.freezeRotation = true;
    rb.isKinematic = false;
    rb.mass = 1;
    rb.drag = 0.25f;
    rb.angularDrag = 0.47f;
    rb.gravityScale = 0;
    heronActive = false;

    Vector3 dropPosition = caughtCrab.transform.position + new Vector3(0, -5, 0);
    LeanTween.move(caughtCrab, dropPosition, 1);

    // unStun crab
    CrabMovement movement = caughtCrab.GetComponent<CrabMovement>();
    movement.setStunnedState(false);

  }
  void TweenToNext()
  {
    // calculate the scale direction to change the bird direction   
    float horizontalDirection = transform.position.x - breadcrumbs[marker].transform.position.x;
    int scaleDirection = horizontalDirection < 0 ? 1 : -1;
    if (scaleDirection != previousScaleDirection) transform.localScale = SetScaleX(transform.localScale, transform.localScale.x * -1);
    
    // trigger tween 
    LeanTween.move(gameObject, breadcrumbs[marker].transform.position, 5).setOnComplete(TweenToNext);
    
    // step through the breadcrumbs
    marker++;
    if (marker >= breadcrumbs.Length) marker = 0;

    // log the direction for the next time around
    previousScaleDirection = scaleDirection;
  }

  Vector3 SetScaleX(Vector3 vector, float x)
  {
    vector.x = x;
    return vector;
  }
}
