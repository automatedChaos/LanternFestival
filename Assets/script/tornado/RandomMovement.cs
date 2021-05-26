using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
  public Transform[] trans;     // points to move between 
  public GameObject avatar;
  public float maxTime = 10f;
  public float minTime = 2f;
    void Start()
    {
        TweenToRandom();
    }

    void TweenToRandom () {

        int randomPoint = Random.Range(0, trans.Length - 1);
        float randomTime = Random.Range(minTime, maxTime);
        LeanTween.move(avatar, trans[randomPoint].transform.position, maxTime).setEase(LeanTweenType.easeInOutCirc).setOnComplete(TweenToRandom);

    }
}
