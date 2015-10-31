using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Opponent : MonoBehaviour {

	private Rigidbody board;
	public Text opponentScoreText;
	public Ball ball;
	private float speedBefore = 0.0f;
	public GameController gameController;
	private const float accelerationValue = 1.0f;
	
	// Use this for initialization
	void Start () 
	{
		board = GetComponent<Rigidbody>();
		opponentScoreText.text = "CPU: 0";
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (gameController.gameOn)
		{
			float moveUpOrDown = calculatePaddleMovement();
			
			if (board.transform.position.z > 2.15 && moveUpOrDown > 0.0f || board.transform.position.z < -2.15 && moveUpOrDown < 0.0f)
				moveUpOrDown = 0.0f;
			
			board.transform.Translate (0, moveUpOrDown * Time.deltaTime * 2.5f, 0);
		}
	}

	float calculatePaddleMovement()
	{
		float newSpeed = 0.0f, testValue1 = speedBefore + accelerationValue * Time.deltaTime,
		testValue2 = speedBefore - accelerationValue * Time.deltaTime;

		if (testValue2 >= -1.0f && testValue1 <= 1.0f)
		{
			if (ball.transform.position.z > board.transform.position.z)
				newSpeed = testValue1;
			else
				newSpeed = testValue2;
		}
		
		speedBefore = newSpeed;
		return newSpeed;
	}
}
