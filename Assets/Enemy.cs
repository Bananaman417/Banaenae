

using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Enemy : MonoBehaviour {
  public float speed = 25;
  private Rigidbody2D rbody;
  private SpriteRenderer srenderer;
  public int health = 100;
  private float lastDirection = 0;
  public float SpeedMultiplier = 1;
  public float DamageMultiplier = 1;
  public float poisonTime = 0;
  public float fireTime = 0;

  private Color color = new Color32(255, 255, 255, 255);

  private void Start() {
    srenderer = GetComponent<SpriteRenderer>();
    rbody = GetComponent<Rigidbody2D>();
  }

  private void Update() {
    if (poisonTime > 0) {
      poisonTime -= Time.deltaTime;
      srenderer.color = new Color32((byte)(255 * poisonTime / 45f), 255, (byte)(255 * poisonTime / 45f), 255);
      if (poisonTime <= 0) {
        srenderer.color = new Color32(255, 255, 255, 255);
        SpeedMultiplier = 1;
      }
    }
    if (fireTime > 0) {
      fireTime -= Time.deltaTime;
      srenderer.color = new Color32(255, (byte)(255 * poisonTime / 45f), (byte)(255 * poisonTime / 45f), 255);
      if (fireTime <= 0) {
        srenderer.color = new Color32(255, 255, 255, 255);
        DamageMultiplier = 1;
      }
    }
    // Go close to the player if the player is at the same level, but stay on the platform
    if (Mathf.Abs(Controller.thePlayer.transform.position.y - transform.position.y) > .5f) return; // Not on the same level

    float movex = Controller.thePlayer.transform.position.x - transform.position.x;
    if (Mathf.Abs(movex) < .1f) {
      movex *= 20;
    }
    Vector3 move = new Vector3(movex, 0, 0);
    move.Normalize();
    rbody.AddForce(move * speed * SpeedMultiplier);
    lastDirection = move.x;

  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (collision.collider.gameObject.GetComponent<Controller>() != null) {
      Controller.thePlayer.HitPlayer((int)(5 * DamageMultiplier));
      Vector2 bounce = new Vector2(-lastDirection, 0);
      rbody.AddForce(bounce * speed * Random.Range(10f, 20f));
      return;
    }
    if (collision.collider.gameObject.GetComponent<Bullet>() == null) return;

    int damage = 25;
    if (collision.collider.gameObject.CompareTag("Mega Bullet")) damage = 200;
    if (collision.collider.gameObject.CompareTag("Poison")) {
      poisonTime = 45f;
      SpeedMultiplier /= 2;
    }
    if (collision.collider.gameObject.CompareTag("Fire")) {
      fireTime = 45f;
      DamageMultiplier /= 2;
    }
    health -= damage;
    if (health < 0) health = 0;
    color.b = health / 1000f;
    color.g = health / 1000f;
    srenderer.color = color;

    if (health == 0) {
      StartCoroutine(MakeEnemyDie());
    }
  }

  IEnumerator MakeEnemyDie() {
    Controller.thePlayer.GetComponent<Controller>().EnemyKilled(5, Random.Range(1, 3));
    GetComponent<BoxCollider2D>().enabled = false;
    rbody.AddForce(new Vector2(Random.Range(-2, 2), -1000));
    yield return new WaitForSeconds(1);
    Destroy(gameObject);
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    Mine mine = collision.GetComponent<Mine>();
    if (mine != null) {
      mine.Explode();
      color.b = 0;
      color.g = 0;
      srenderer.color = color;
      StartCoroutine(MakeEnemyDie());
    }
  }
}