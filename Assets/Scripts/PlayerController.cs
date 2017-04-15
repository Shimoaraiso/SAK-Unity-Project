using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 【プレイヤーのコントロール用クラス】
///		・入力
///		・ジャンプ
///		・アニメーション
///		・地面との当たり判定
///		・敵との当たり判定
/// </summary>
public class PlayerController : MonoBehaviour {

	// 開発用変数
	public bool autoRun;
	//

	private float moveSpeed = 6.0f;
	private float jumpPower = 12.0f;

	private float inputHrz;
	//private float inputVtc;

	private Rigidbody2D rb;
	private Animator anim;

	private bool isJumping;
	private bool isDash;
	private bool isGround;
	private bool isDead;

	/// <summary>
	/// 初期化
	/// </summary>
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}
	
	/// <summary>
	/// 物理演算が必要ない更新処理
	/// </summary>
	void Update () {
		// 入力関連
		inputHrz = Input.GetAxis("Horizontal");
		//inputVtc = Input.GetAxis("Vertical");

		if (Input.GetButtonDown("Jump") && isGround) {
			isJumping = true;
		} else if (Input.GetButtonDown("Jump") && !isGround) {
			isDash = true;
			
		} else if (Input.GetButtonUp("Jump") && !isGround) {
			isDash = false;
		}

		// アニメーション
		SetAnimator();


		// デバック用コマンド【リロード】
		if (Input.GetKeyDown(KeyCode.Q)) {
			SceneManager.LoadScene(0);
		}
	}

	/// <summary>
	/// Animatorに渡すフラグ
	/// </summary>
	void SetAnimator() {
		anim.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
		anim.SetFloat("VelocityY", rb.velocity.y);
		anim.SetBool("isJumping", isJumping);
		anim.SetBool("isGround", isGround);
		anim.SetBool("isDead", isDead);
	}

	/// <summary>
	/// 物理演算を行う更新処理
	/// </summary>
	void FixedUpdate() {

		// 移動（キー入力で移動させているが、後々オートランにする…予定）
		if (autoRun) {
			rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
		} else {
			rb.velocity = new Vector2(inputHrz * moveSpeed, rb.velocity.y);
			//rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
		}
		// ジャンプ
		if (isJumping) {
			isJumping = false;
			isGround = false;
			rb.velocity = new Vector2(rb.velocity.x, 0f);
			rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
		}

		// 空中ダッシュ
		if (isDash) {
			isJumping = false;
			isGround = false;
			anim.SetTrigger("isDash");
			rb.velocity = new Vector2(rb.velocity.x, 0f);
			rb.AddForce(new Vector3(0.2f, 0.2f, 0) * jumpPower, ForceMode2D.Impulse);
		}
	}

	/// <summary>
	/// トリガー突入判定
	/// </summary>
	/// <param name="col"></param>
	void OnTriggerEnter2D(Collider2D col) {
		// 接地判定
		if (col.gameObject.CompareTag("Ground")) {
			isGround = true;
		}
		
		if (col.gameObject.name == "CameraSwitch") {
			Debug.Log("aaaa");
		}


	}

	/// <summary>
	/// トリガー離脱判定
	/// </summary>
	/// <param name="col"></param>
	void OnTriggerExit2D(Collider2D col) {
		//if (col.gameObject.CompareTag("Ground")) {
		//}
	}


	/// <summary>
	/// 当たり判定
	/// </summary>
	/// <param name="col"></param>
	void OnCollisionEnter2D(Collision2D col) {
		// エネミーTagに当たると固まって死ぬ
		if (col.gameObject.CompareTag("Enemy")) {
			rb.simulated = false;
			isDead = true;
		}
	}
}
