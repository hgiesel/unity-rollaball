using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;
	public Text infoText;
	public Text infoTextFoo;

	bool pastPointOfNoReturn = false;
	bool wonGame = false;
	int goingBackTo = 0;


	Rigidbody rb;
	int pointCount;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		pointCount = 0;
		SetCountText (pointCount);
		winText.text = "";
	}


	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene().name == "ChooseLevel") {

			if (transform.position.y >= 0) {
				infoTextFoo.text = "← quit game                      first level →";
			} else {
				infoTextFoo.text = "";
			};


			if (transform.position.y < -5) {

				if (transform.position.x > 0) {
					winText.text = "Going to first level";

					if (transform.position.y < -20) {
						SceneManager.LoadScene ("MiniGame");
					}
				} else {

					winText.text = "Goodbye!";

					if (transform.position.y < -20) {
						Application.Quit ();
					}

				}
			}

		} else {

			if (transform.position.z < -10 && transform.position.z > -14.4
				&& transform.position.x > 3 && transform.position.x < 9.68 && transform.position.y >= 0) {
				infoText.text = "Jump off cliff here to return to main menu →";
			} else {
				infoText.text = "";
			}

			if (transform.position.y < -5 && !pastPointOfNoReturn) {

				if (transform.position.x > 5 && transform.position.z < -8 && transform.position.z > -15) {
					pastPointOfNoReturn = true;
					winText.text = "Going to main menu";
					goingBackTo = 0; // ChooseLevel
				} else {
					
					pastPointOfNoReturn = true;

					if (wonGame) {

						winText.text = "You've done great!";
						goingBackTo = 0; // ChooseLevel
					} else {
						winText.text = "Oh no :( Try again";
						goingBackTo = SceneManager.GetActiveScene ().buildIndex;
					}
				}
			}
		    if (transform.position.y < -20) {
				pastPointOfNoReturn = false;
			 	SceneManager.LoadScene (goingBackTo);
			}
		}
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		float altMoveHorizontal = Input.acceleration.x;
		float altMoveVertical = Input.acceleration.y;

		Vector3 movement    = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		Vector3 altMovement = new Vector3 (altMoveHorizontal, 0.0f, altMoveVertical); 

		if (transform.position.x < -9 && transform.position.x > -10 && transform.position.z > 4 && transform.position.z < 5) {
			rb.AddForce (speed * new Vector3(0.0f, 1f, 0.0f));
		}

		rb.AddForce (speed * movement);
		rb.AddForce (speed * altMovement);
	}

	void SetCountText(int count) {
		string myText = count.ToString ();
		countText.text = "Count: " + myText + " / 8";
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			pointCount++;
			SetCountText (pointCount);

			if (pointCount >= 8) {
				winText.text = "YOU WIN! Fall down anywhere to return to main menu.";
				wonGame = true;
			}
		
		}
	}
}