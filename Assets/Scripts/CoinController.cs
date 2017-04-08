using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 【コインのコントロール用クラス】
///		・プレイヤーが当たったら消える
///		・得点の加算（未実装）
/// </summary>
public class CoinController : MonoBehaviour {
	
	/// <summary>
	/// 非接触コライダー用の当たり判定
	/// </summary>
	/// <param name="col"></param>
	void OnTriggerEnter2D(Collider2D col) {
		// プレイヤーが接触したコインは消える
		if (col.gameObject.CompareTag("Player")) {
			Debug.Log("Get Coin!");
			Destroy(gameObject);
		}
	}
}
