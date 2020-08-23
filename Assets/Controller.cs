using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class Controller : MonoBehaviour
{


	public int isJumping = 0;
	public float speed = 10f;
	private SpriteRenderer spriteRenderer;
	private Rigidbody2D rbody;
	public float jumpStrenght = 10;
	float horizMove = 0;
	float vertMove = 0;
	public Vector2 forces = Vector2.zero;
	public GameObject Bullet;
	public int health = 100;
	public TMPro.TextMeshProUGUI GameOver;
	public Image HealthBar;
	public TMPro.TextMeshProUGUI Score;
	private int score;
	public int money;
	public static Controller thePlayer;
	public GameObject MegaBullet;
	public GameObject FireBullet;
	public GameObject PoisonBullet;
	public float MegaBulletTime = 10;
	public TMPro.TextMeshProUGUI MegaBulletStatus;
	public Animator anim;
	public GameObject Mine;
	private Vector3 lastCheckpoint = Vector3.zero;

	public float shoesSpeed = 20f;
	public float shoesTime = 5f;
	public PlayerInventory inventory;
	public int[] items = new int[5];
	public bool InShop = false;
	private int fireBullet = 0;
	private int poisonBullet = 0;
	private float shieldtime = 0;


	IEnumerator StartGravity()
	{
		yield return new WaitForSeconds(1.1f);
		rbody.gravityScale = 1;
	}

	private void Awake()
	{
		if (thePlayer == null)
		{
			thePlayer = this; // In first scene, make us the singleton.
			DontDestroyOnLoad(gameObject);
		}
		else if (thePlayer != this)
			Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
	}

	private void Start()
	{
		rbody = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		if (GameOver != null)
			GameOver.gameObject.SetActive(false);
		spriteRenderer.color = new Color32(255, 255, 255, 255);
		spriteRenderer.flipY = false;
		score = 0;
		StartCoroutine(StartGravity());
	}


	private void Update()
	{

		horizMove = Input.GetAxis("Horizontal") * speed;
		if (InShop) return;

		if (Input.GetKeyUp(KeyCode.Alpha0) && !InShop)
		{
			lastCheckpoint = transform.position;
			lastScene = SceneManager.GetActiveScene().buildIndex;
			SceneManager.LoadScene("Shop");
			InShop = true;
			return;
		}




		if (Input.GetKeyUp(KeyCode.Alpha1) && inventory.RemoveItem(Item.Shoes, 1))
		{
			shoesTime = 5f;

		}
		if (Input.GetKeyUp(KeyCode.Alpha2) && inventory.RemoveItem(Item.FireGun, 1))
		{
			fireBullet += 5;
		}
		if (Input.GetKeyUp(KeyCode.Alpha3) && inventory.RemoveItem(Item.PoisonGun, 1))
		{
			poisonBullet += 5;
		}
		if (Input.GetKeyUp(KeyCode.Alpha4) && inventory.RemoveItem(Item.Shield, 1))
		{
			shieldtime = 30;
			spriteRenderer.color = new Color32(100, 150, 255, 255);
		}

		if (Input.GetKeyUp(KeyCode.Alpha5) && inventory.RemoveItem(Item.Mine, 1))
		{
			Vector3 minepos = transform.position;
			minepos.y -= .275f;
			Instantiate(Mine, null).transform.position = minepos;
		}

		if (Input.GetKeyUp(KeyCode.Escape))
		{
			transform.position = lastCheckpoint;
			health = 100;
			GameOver.gameObject.SetActive(false);
			spriteRenderer.color = new Color32(255, 255, 255, 255);
			spriteRenderer.flipY = false;
			score = 0;
			Score.text = "0";

		}
		if (shieldtime > 0)
        {
			shieldtime -= Time.deltaTime;
			if (shieldtime <= 0)
            {
				spriteRenderer.color = new Color32(255, 255, 255, 255);
            }
        }
		

		if (health <= 0)
		{
			return;
		}
		shoesTime -= Time.deltaTime;
		horizMove = Input.GetAxis("Horizontal") * speed;
		if (shoesTime > 0) rbody.velocity *= shoesSpeed;

		if (Input.GetKeyUp(KeyCode.Space) && isJumping < 3)
		{
			vertMove = jumpStrenght;
			if (shoesTime >  0) vertMove *= 2;
			isJumping++;
			Debug.Log("A" + vertMove);
		}

		if (Input.GetKeyUp(KeyCode.E))
		{
			Bullet b = null;
			if (poisonBullet > 0)
			{
				poisonBullet--;
				b = Instantiate(PoisonBullet).GetComponent<Bullet>();
			}
			else if (fireBullet > 0)
			{
				fireBullet--;
				b = Instantiate(FireBullet).GetComponent<Bullet>();
			}
			else
			{
				b = Instantiate(Bullet).GetComponent<Bullet>();
			}
			b.Shoot(transform.position, spriteRenderer.flipX);
		}
		if (MegaBulletTime < 0)
			MegaBulletStatus.text = "Mega Bullet: ready!";
		else
			MegaBulletStatus.text = "Mega Bullet: " + (int)MegaBulletTime + " seconds";
		MegaBulletTime -= Time.deltaTime;
		if (Input.GetKeyUp(KeyCode.Q) && MegaBulletTime < 0)
		{
			Bullet b = Instantiate(MegaBullet).GetComponent<Bullet>();
			b.Shoot(transform.position, spriteRenderer.flipX); DontDestroyOnLoad(this);
			MegaBulletTime = 10;
		}

		 HealthBar.transform.localScale = new Vector3(health / 100f, 1, 1);
	}

	private void FixedUpdate()
	{
		forces.x = horizMove;
		forces.y = vertMove;
		if (horizMove != 0)
		{
			spriteRenderer.flipX = (horizMove < 0);
		}
		if (vertMove != 0)
		{
			Debug.Log("B" + vertMove);
			Debug.Log("C" + forces);
			vertMove = 0;
		}

		if (forces != Vector2.zero)
		{
			rbody.AddForce(forces);
		}
		anim.SetBool("Walking", (horizMove != 0));
	}


	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Ground"))
		{
			isJumping = 0;
			forces.y = 0;

		}

		if (other.gameObject.CompareTag("Stairs Right") && forces.x > 0)
		{
			rbody.AddForce(Vector3.up * 100);
		}
		else if (other.gameObject.CompareTag("Stairs Left") && forces.x < 0)
		{
			rbody.AddForce(Vector3.up * 100);
		}
	}

	public void HitPlayer(int force)
	{
		if (shieldtime > 0) return;
		health -= force;
		if (health > 0) return;

		// Handle death
		spriteRenderer.color = new Color32(255, 0, 0, 255);
		spriteRenderer.flipY = true;
		GameOver.gameObject.SetActive(true);
	}
	public void EnemyKilled(int value, int coins)
	{
		score += value;
		money += coins;
		Score.text = "Score: + " + score + "   Money: " + money;

	}




	internal void SetCheckPoint(Vector3 position)
	{
		lastCheckpoint = position;
	}

	private int lastScene = 0;



	public void ExitShop()
	{
		StartCoroutine(LoadLevel());
	}

	IEnumerator LoadLevel()
	{
		AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(lastScene, LoadSceneMode.Single);
		while (!asyncLoadLevel.isDone)
		{
			print("Loading the Scene");
			yield return new WaitForSeconds(.25f);
		}

		transform.position = lastCheckpoint;

		GameObject canvas = GameObject.Find("Canvas");

		Transform tr = canvas.transform.Find("DeathText");
		GameOver = tr.GetComponent<TMPro.TextMeshProUGUI>();

		tr = canvas.transform.Find("Score");
		Score = tr.GetComponent<TMPro.TextMeshProUGUI>();
		tr = canvas.transform.Find("DeathText");


		GameOver = tr.GetComponent<TMPro.TextMeshProUGUI>();
		GameOver.gameObject.SetActive(false);
		tr = canvas.transform.Find("MegaBulletStatus");
		MegaBulletStatus = tr.GetComponent<TMPro.TextMeshProUGUI>();

		tr = canvas.transform.Find("HealthBar");
		HealthBar = tr.GetComponent<Image>();

		tr = canvas.transform.Find("Inventory");
		inventory = tr.GetComponent<PlayerInventory>();

		inventory.SetItems(this);
		InShop = false;
	}
}

