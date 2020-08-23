using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rbody;
    private float timeToLive = 5f;
    private bool left;
    public float BulletSpeed = .0025f;

    public void Shoot(Vector3 pos, bool goLeft)
    {
        if (goLeft)
            transform.position = pos + Vector3.left / 5;
        else
            transform.position = pos + Vector3.right / 5;

        timeToLive = 5;
        left = goLeft;
    }

    void FixedUpdate()
    {
        timeToLive -= Time.deltaTime;
        if (timeToLive < 0)
        {
            Destroy(gameObject);
            return;
        }
        if (left)
        {
            rbody.AddForce(Vector3.left * BulletSpeed);
        }
        else
        {
            rbody.AddForce(Vector3.right * BulletSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Controller c = collision.collider.gameObject.GetComponent<Controller>();
        if (c != null) return;
        Enemy e = collision.collider.gameObject.GetComponent<Enemy>();
       Destroy(gameObject);
    }
}