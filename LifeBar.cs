using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LifeBar : MonoBehaviour
{

	public float speed = 20f;
	private bool gameIsRunning;
	public GameObject[] pickups;
	public GameObject[] plants;

	private AudioSource audioSourceComponent;
	public AudioClip plantSound;
	public AudioClip waterSound;
	public AudioClip water2Sound;
	public AudioClip water3Sound;


	//HP related
	public int life;
	private int maxLife;
	public GameObject lifebarUI;
	private bool lifeDrainON;


	private float movementX;
	private float movementY;

	private Rigidbody rb;
	

	//TEXT related
	// private float speed;
	// public TextMeshProUGUI countText;
	// public GameObject winTextObject;


	 private int count; 
	//# of pickups found


	// Start is called before the first frame update
	void Start()
	{

		lifeDrainON = false;

		maxLife = life;
	//	lifebarUI.transform.localScale = new Vector3((life / 50f), .10f, .40f);

		gameIsRunning = true;

		count = 0;

		rb = GetComponent<Rigidbody>();


		audioSourceComponent = GetComponent<AudioSource>();


		Vector3 movement = new Vector3(movementX, 0.0f, movementY);
		rb.AddForce(movement * speed);

		foreach (GameObject plant in plants)
        {
			plant.SetActive(true);
        }

	}


	void OnTriggerEnter(Collider other)
	{
		//if GameObject has 'Pick Up' tag assigned to it:
		if (other.gameObject.CompareTag("WH_A"))
		{
			//Run the SetCountText() function
			// 	SetCountText();

			life += 100;

			if (life > maxLife)
				life = maxLife;
			lifebarUI.transform.localScale = new Vector3((life / 50f), .10f, .40f);

			audioSourceComponent.PlayOneShot(waterSound);


		}
		if (other.gameObject.CompareTag("WH_B"))
		{
			//Run the SetCountText() function
			// 	SetCountText();

			life += 100;

			if (life > maxLife)
				life = maxLife;
			lifebarUI.transform.localScale = new Vector3((life / 50f), .10f, .40f);

			audioSourceComponent.PlayOneShot(water2Sound);


		}
		if (other.gameObject.CompareTag("WH_C"))
		{
			//Run the SetCountText() function
			// 	SetCountText();

			life += 100;

			if (life > maxLife)
				life = maxLife;
			lifebarUI.transform.localScale = new Vector3((life / 50f), .10f, .40f);

			audioSourceComponent.PlayOneShot(water3Sound);


		}


		if (other.gameObject.CompareTag("Plant"))
        {
			//hide plant
			other.gameObject.SetActive(false);
			
			//add fullhealth
			life = 300;

            //play sound
            audioSourceComponent.PlayOneShot(plantSound);

			count = count + 1;


        }

		else if (count >= 5)
        {
			
			lifeDrainON = false;
			lifebarUI.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);

			life = 0;

			//give "win message" and dont send to main screen 

		}

	}

	private void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
		movementX = v.x;
		movementY = v.y;

    }



    private void LateUpdate()
	{


		if (gameIsRunning == true)
		{

			

			if (!lifeDrainON)
				lifeDrainON = true;
			else
				lifeDrainON = false;
			
			//drain life while keypress WASD
			if (lifeDrainON & (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d")) == true)
			{

				Vector3 movement = new Vector3(movementX, 0.0f, movementY);
				rb.AddForce(movement * speed);


				
				life = (int)(life - (Time.deltaTime * 5));
			
				
				lifebarUI.transform.localScale = new Vector3((life / 50f), .10f, .40f);


				if (life <= 70 == true)
                {	
					//slow down ball
				}
			}

		



        }

		//makes bar stop at 0
		if (life <= 0 && lifeDrainON == true)
		{
			life = 0;
			lifebarUI.transform.localScale = new Vector3((life / 50f), .10f, .40f);
            _ = lifeDrainON == false;
		}
		

		//endsgame
		if (life <= 0 && lifeDrainON && count < 5 == true)
		{
			/*
			life = 0;
			lifebarUI.transform.localScale = new Vector3((life / 50f), .10f, .40f);
			_ = lifeDrainON == false; */


			//make it kill you aAKA "lose message" + return to main screen
			// Code below made restarting game have fucked up camera and would auto-quit again. not patient enough to think this through.
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);


			//Application.Quit();
			//Debug.Log("quit");
		}

		if (Input.GetKey("escape"))
		{
			Application.Quit();
		}






	}



}


