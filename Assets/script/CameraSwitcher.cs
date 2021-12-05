using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    public Camera camera3;
     
    private void Start()
    {
        // camera1.GetComponent<Camera>().enabled = true;
        // camera2.GetComponent<Camera>().enabled = false;
        // camera3.GetComponent<Camera>().enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown("2"))
        {
            camera1.GetComponent<Camera>().enabled = false;
            camera3.GetComponent<Camera>().enabled = false;
            camera2.GetComponent<Camera>().enabled = true;
            camera2.targetDisplay = 0;
        }
        if (Input.GetKeyDown("1"))
        {
            camera1.GetComponent<Camera>().enabled = true;
            camera1.targetDisplay = 0;
            camera2.GetComponent<Camera>().enabled = false;
            camera3.GetComponent<Camera>().enabled = false;
        }
        if (Input.GetKeyDown("3"))
        {
            camera3.GetComponent<Camera>().enabled = true;
            camera3.targetDisplay = 0;
            camera2.GetComponent<Camera>().enabled = false;
            camera1.GetComponent<Camera>().enabled = false;
        }
    }
}