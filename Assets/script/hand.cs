using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hand : MonoBehaviour
{
    public GameObject pinchPoint; 
    public Transform leftMax;
    public Transform rightMax;
    public Transform dropPoint;

    public Animator animator;

    public BoxCollider2D detectorCollider;

    private bool handActive = true;
    private LTDescr handTravelTweenId; 

    private GameObject caughtCrab;

    // Start is called before the first frame update
    void Start()
    {
        startHandMovment();
    }
    IEnumerator deactivateHand () {
        yield return new WaitForSeconds(Random.Range(20, 25));
        handActive = false;
    }

    void startHandMovment () {
        // return to the rightMax and then loop
        LeanTween.move(gameObject, rightMax, 2f).setOnComplete(() => {
            handTravelTweenId = LeanTween.move(gameObject, leftMax, 5f).setLoopPingPong(1).setOnCompleteOnStart(true).setRepeat(-1);
            StartCoroutine("deactivateHand");
        });
    }
    void stopHandMovement () {
        LeanTween.cancel(handTravelTweenId.id);
    }

    // Update is called once per frame
    void Update()
    {
        if (handActive) return;

        // detect crabs to catch
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position, 30f
        );

        // select a crab 
        if (hits.Length > 0 && !handActive){

            int crabSelection = Random.Range(0, hits.Length);

            // Fail states
            if (hits[crabSelection].tag != "crab") return;
            if (hits[crabSelection].transform.position.x <= leftMax.position.x + 15) return;
            
            handActive = true;
            stopHandMovement();
            catchCrab(hits[crabSelection].gameObject);
        }
    }

    void catchCrab(GameObject crab) {

        caughtCrab = crab;

        // Stun crab
        CrabMovement movement = caughtCrab.GetComponent<CrabMovement>();
        movement.setStunnedState(true);

        // lock in positions
        Vector3 offset = caughtCrab.transform.position - pinchPoint.transform.position;
        Vector3 newPosition = (Vector3)transform.position + offset;

        // trigger catch attempt. Note: does not always catch the crab
        LeanTween.move(gameObject, newPosition, 2f).setOnComplete(() =>
            {
                animator.SetBool("CrabCaught", true);
                Rigidbody2D rb = caughtCrab.GetComponent<Rigidbody2D>();
                caughtCrab.transform.position = SetZ(caughtCrab.transform.position, -30);
                Destroy(rb);
                caughtCrab.transform.SetParent(gameObject.transform);
                LeanTween.move(gameObject, dropPoint, 2f).setOnComplete(() =>
                    {
                        dropCaughtCrab();
                    }
                );
            }
        );
    }

    void dropCaughtCrab () {
        // reset the crab
        caughtCrab.transform.parent = null;
        Rigidbody2D rb = caughtCrab.AddComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.isKinematic = false;
        rb.mass = 1;
        rb.drag = 0.25f;
        rb.angularDrag = 0.47f;
        rb.gravityScale = 0;

        // drop the crab
        animator.SetBool("CrabCaught", false);
        Vector3 downForce = new Vector3(0, -3, 0);
        rb.AddForce(downForce, ForceMode2D.Impulse);

        // unStun crab
        CrabMovement movement = caughtCrab.GetComponent<CrabMovement>();
        movement.setStunnedState(false);

        // start the loop again
        startHandMovment();
    }

  Vector3 SetZ(Vector3 vector, float z){
    vector.z = z;
    return vector;
  }
}
