using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Controller c = collision.GetComponent<Controller>();
        if (c != null)
        {
            c.SetCheckPoint(transform.position);
        }
    }
}
