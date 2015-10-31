using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	// Game Objects
	public Player playerBoard; // Player board
	public Opponent opponentBoard;
	public Ball pongBall; // Ball
	public Camera mainCamera;
	public bool gameOn = true;
	public Text winText;

	// Player Goals
	byte playerGoals = 0;
	byte opponentGoals = 0;
	const byte goalsToWin = 5;

	const float displacementFromCentreOfBoard = 2.45f;
	const float maximumTiltAngle = 5.0f;

	// Use this for initialization
	void Start () 
	{
		winText.text = "";
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (gameOn)
		{
			// Rotate the camera
			mainCamera.transform.rotation = Quaternion.Euler (new Vector3(90 - maximumTiltAngle * (pongBall.transform.position.z / displacementFromCentreOfBoard), 0.0f));
			//mainCamera.transform.Rotate(new Vector3(Time.deltaTime * maximumTiltAngle * (pongBall.transform.position.z / displacementFromCentreOfBoard), 0.0f));
		}
	}

	public void incrementPlayerGoals()
	{
		playerGoals++;
		mainCamera.transform.rotation = Quaternion.Euler (new Vector3(90.0f, 0.0f));
		outputPlayerGoals();
	}
	
	public void outputPlayerGoals()
	{
		playerBoard.playerScoreText.text = "Player: " + playerGoals.ToString();
	}

	public void incrementOpponentGoals()
	{
		opponentGoals++;
		mainCamera.transform.rotation = Quaternion.Euler (new Vector3(90.0f, 0.0f));
		outputOpponentGoals();
	}
	
	public void outputOpponentGoals()
	{
		opponentBoard.opponentScoreText.text = "CPU: " + opponentGoals.ToString();
	}

	public bool playerWins()
	{
		return playerGoals == goalsToWin;
	}

	public bool opponentWins()
	{
		return opponentGoals == goalsToWin;
	}
}
