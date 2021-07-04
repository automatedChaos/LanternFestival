using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Complete : MonoBehaviour
{
    public Connection connection;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "crab"){
            // send complete
            connection.sendComplete(collision.gameObject.name);
        }
    }
}
