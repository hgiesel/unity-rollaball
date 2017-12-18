﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;



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

	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (speed * movement);
	}

	void SetCountText(int count) {
		string myText = count.ToString ();
		countText.text = "Count: " + myText;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			pointCount++;
			SetCountText (pointCount);

			if (pointCount >= 6) {
				winText.text = "YOU WIN!";
			}
		}
	}
}