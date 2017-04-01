using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private Transform playerTfm;
	private Transform myTfm;
	private Vector3 playerPos;
	private Vector3 myPos;

	void Start () {
		playerTfm = GameObject.FindWithTag("Player").transform;
		playerPos = playerTfm.position;
		myTfm = transform;

	}

	void Update () {
		myTfm.position += Vector3.right * (playerTfm.position.x - playerPos.x);

		playerPos = playerTfm.position;

	}
}
