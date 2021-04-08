using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class management : MonoBehaviour
{
    public GameObject spawnPoint;
    //prefabs
    public GameObject mod1CrabPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.I)) 
       Instantiate (mod1CrabPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

       
    }
}
