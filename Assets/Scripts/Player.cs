using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour 
{
	private Rigidbody board;
	public Text playerScoreText;
	public GameController gameController;

	// Use this for initialization
	void Start () 
	{
		board = GetComponent<Rigidbody>();
		playerScoreText.text = "Player: 0";
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (gameController.gameOn)
		{
			float moveUpOrDown = Input.GetAxis ("Vertical");
			//Debug.Log (moveUpOrDown);

			if (board.transform.position.z > 2.15 && moveUpOrDown > 0.0f || board.transform.position.z < -2.15 && moveUpOrDown < 0.0f)
				moveUpOrDown = 0.0f;

			board.transform.Translate (0, moveUpOrDown * Time.deltaTime * 2.5f, 0);
		}
	}

}
