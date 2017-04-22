using UnityEngine;

/// <summary>
/// 【背景のコントロール用クラス】
///		背景は3枚、カメラから見切れたら回り込む
/// </summary>
public class BackGroundController : MonoBehaviour {
	
	// 背景の枚数
	private int bgCount = 3;

	private Transform bgTfm;
	private SpriteRenderer mySpriteRndr;
	private float width;

	void Start () {
		bgTfm = transform;
		mySpriteRndr = GetComponent<SpriteRenderer>();
		width = mySpriteRndr.bounds.size.x;
		float bgWidth = mySpriteRndr.bounds.size.x;
	}
	
	void Update () {
		Vector3 myViewport = Camera.main.WorldToViewportPoint(bgTfm.position);
		
		// 背景の回り込み(X軸プラス方向に移動時)
		if (myViewport.x < -1.0f) {
			bgTfm.position += Vector3.right * (width * bgCount);
		}
		// 背景の回り込み(X軸マイナス方向に移動時)
		else if (myViewport.x > 2.1f) {
			bgTfm.position -= Vector3.right * (width * bgCount);
		}

	}
}
