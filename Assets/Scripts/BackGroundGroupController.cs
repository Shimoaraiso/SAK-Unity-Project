using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 【背景グループのコントロール用クラス】
///		・プレイヤーの動きに対してゆっくりスクロールする
///		・
/// </summary>
public class BackGroundGroupController : MonoBehaviour {

	private Transform playerTfm;
	private Transform myTfm;
	private Vector3 playerPos;

	//private float scrollDelay = 0.1f;
	private float scrollDelay = 0.9f;

	void Start() {

		playerTfm = GameObject.FindWithTag("Player").transform;
		playerPos = playerTfm.position;
		myTfm = transform;
	}

	void Update() {
		//Vector3 myViewport = Camera.main.WorldToViewportPoint(myTfm.position);
		//Debug.Log(viewport.y);

		float myPosX = (playerTfm.position.x - playerPos.x) * scrollDelay;
		myTfm.position += Vector3.right * myPosX;

		float myPosy = (playerTfm.position.y - playerPos.y );
		myTfm.position += Vector3.up * myPosy;

		playerPos = playerTfm.position;
		

		//Vector3 playerViewport = Camera.main.WorldToViewportPoint(playerTfm.position);

		//if (playerViewport.y > 0.8f) {
		//	Debug.Log("hogehoge");
		//	myTfm.position += Vector3.up * (playerTfm.position.y - playerPos.y);
		//} else if (playerViewport.y < 0.3f) {
		//	myTfm.position += Vector3.up * (playerTfm.position.y - playerPos.y);
		//}



		playerPos = playerTfm.position;
	}
}
