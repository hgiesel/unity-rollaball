using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;
	public Text infoText;
	public Text infoTextFoo;



	bool pastPointOfNoReturn = false;
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

		if (Application.loadedLevelName == "ChooseLevel") {

			if (transform.position.y >= 0) {
				infoTextFoo.text = "← quit game                      first level →";
			} else {
				infoTextFoo.text = "";
			};


			if (transform.position.y < -5) {

				if (transform.position.x > 0) {
					winText.text = "Going to first level";

					if (transform.position.y < -20) {
						Application.LoadLevel ("MiniGame");
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
					winText.text = "Oh no :( Try again";
					goingBackTo = Application.loadedLevel;
				}
			}
		    if (transform.position.y < -20) {
				pastPointOfNoReturn = false;
			 	Application.LoadLevel (goingBackTo);
			}
		}
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (speed * movement);
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
				winText.text = "YOU WIN!";
			}
		
		}
	}
}