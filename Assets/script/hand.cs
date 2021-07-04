using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hand : MonoBehaviour
{
    public GameObject pinchPoint; 
    public Transform leftMax;
    public Transform rightMax;
    public Transform dropPoint;
    public List<Transform> detectorNet;

    public Animator animator;

    public BoxCollider2D detectorCollider;

    private bool handActive = true;
    private LTDescr handTravelTweenId; 
    private GameObject caughtCrab;
    private Transform catchPoint; 

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
        LeanTween.move(gameObject, rightMax, 3f).setOnComplete(() => {
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
        
        // sweep across all detectors looking for crabs
        // closest first
        foreach(Transform detector in detectorNet){

            // detect crabs to catch
            Collider2D[] hits = Physics2D.OverlapCircleAll(
                detector.position, 1f
            );
           
            // select a crab 
            if (hits.Length > 0){
                // stop the for loop

                int crabSelection = Random.Range(0, hits.Length);

                // Fail states
                if (hits[crabSelection].tag != "crab") return;
                if (hits[crabSelection].transform.position.x <= leftMax.position.x + 15) return;
                 
                handActive = true;
                stopHandMovement();

                catchCrab(hits[crabSelection].gameObject);
                break;
            }
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
                returnHome();
            }
        );
    }

    void returnHome () {
        // Move uo
        LeanTween.move(gameObject, SetX(leftMax.position, transform.position.x), 2f).setOnComplete(() => {
            // Move across 
            LeanTween.move(gameObject, SetY(dropPoint.position, transform.position.y), 2f).setOnComplete(() => {
                // Move down
                LeanTween.move(gameObject, dropPoint.position, 2f).setOnComplete(() => {
                    dropCaughtCrab();
                });
            });
        });
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
        LeanTween.move(gameObject, gameObject.transform.position, 1f).setOnComplete(() => {
            LeanTween.move(gameObject, SetX(leftMax.position, transform.position.x), 2f).setOnComplete(() => {
                // start the loop again
                startHandMovment();
            });
        });
    }

  Vector3 SetZ(Vector3 vector, float z){
    vector.z = z;
    return vector;
  }

  Vector3 SetY(Vector3 vector, float y){
    vector.y = y;
    return vector;
  }

  Vector3 SetX(Vector3 vector, float x){
    vector.x = x;
    return vector;
  }
}
