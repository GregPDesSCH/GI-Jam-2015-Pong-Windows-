using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	public GameObject ballGameObject;
	private Rigidbody ball;
	public GameController gameController;

	private const float MAX_DX = 10f;
	private const float MAX_DY = 5f;
	private bool gameOver;
	private float dx;
	private float dy;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine(pause());
		resetMovementComponents();
	}

	void resetMovementComponents()
	{
		ball = GetComponent<Rigidbody>();
		ball.transform.transform.position = new Vector3(0, 0.5f, 0);

	
		float randomizationFactorX = ((float)Random.value) - 0.42f, randomizationFactorY = ((float)Random.value) - 0.32f;
	
		while ((randomizationFactorX < -0.42f || randomizationFactorX > 0.42f) ||
		       (randomizationFactorX > -0.3f && randomizationFactorX < 0.3f))
			randomizationFactorX = ((float)Random.value)  - 0.42f;
		//Debug.Log (randomizationFactorX);
	
		while ((randomizationFactorY < -0.32f || randomizationFactorY > 0.32f) ||
		       (randomizationFactorY > -0.15f && randomizationFactorY > 0.15f))
			randomizationFactorY = ((float)Random.value) - 0.32f;
	
		dx = randomizationFactorX * MAX_DX;
		dy = randomizationFactorY * MAX_DY;

	}



	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (gameController.gameOn)
			ball.transform.Translate (dx * Time.deltaTime, 0, dy * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("North Wall") || other.gameObject.CompareTag("South Wall"))
			dy = dy * -1;
		else if (other.gameObject.CompareTag("Player"))
			dx = dx * -1;
		else if (other.gameObject.CompareTag("Opponent"))
		{
			//Debug.Log ("Hello?");
			dx = dx * -1;
		}
		else if (other.gameObject.CompareTag("Player\'s Goal"))
		{
			gameController.incrementPlayerGoals();
			//Debug.Log ("Now calling pause game (Player).");

			if (gameController.playerWins())
			{
				gameController.gameOn = false;
				gameController.winText.text = "YOU WIN!";
			}

			if (gameController.gameOn)
			{
				StartCoroutine(pause());
				//Debug.Log ("Pause disabled (Player).");
				resetMovementComponents();
			}
		}
		else if (other.gameObject.CompareTag("Opponent\'s Goal"))
		{
			gameController.incrementOpponentGoals();
			//Debug.Log ("Now calling pause game (Opponent).");

			if (gameController.opponentWins())
			{
				gameController.gameOn = false;
				gameController.winText.text = "YOU LOSE!";
			}

			if (gameController.gameOn)
			{
				StartCoroutine(pause());
				//Debug.Log ("Pause disabled (Opponent).");
				resetMovementComponents();
			}
		}
	}

	public IEnumerator pause()
	{
		Time.timeScale = 0.001f;
		yield return new WaitForSeconds(1.25f * Time.timeScale);
		Time.timeScale = 1f;
	}

}
