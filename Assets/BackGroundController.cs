using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour {
	
	// 背景の枚数
	private int bgCount = 3;

	private Transform myTfm;
	private SpriteRenderer mySpriteR;
	private float width;


	void Start () {
		myTfm = transform;
		mySpriteR = GetComponent<SpriteRenderer>();
		width = mySpriteR.bounds.size.x;
	}
	
	void Update () {
		Vector3 myViewport = Camera.main.WorldToViewportPoint(myTfm.position);
		//Debug.Log(viewport.x);

		// 背景の回り込み
		if (myViewport.x < -1.0f) {			
			myTfm.position += Vector3.right * (width * bgCount);
		}
		else if (myViewport.x > 2.1f) {
			myTfm.position -= Vector3.right * (width * bgCount);
		}

	}
}
