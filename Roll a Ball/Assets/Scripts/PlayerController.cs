﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text CountText;
	public Text WinText;
	public List<Collider> Colliders = new List<Collider>(); //just for demo
	
	private Rigidbody rb;
	private int count;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		count = 0;
		SetText();
		WinText.text = "";
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Pickable"))
		{
			other.gameObject.SetActive(false);
			count++;
			SetText();
		}
	}

	private void SetText()
	{
		CountText.text = "Count: " + count.ToString();
		if (count >= 10)
		{
			WinText.text = "YOU WIN!";
		}
	}
}
