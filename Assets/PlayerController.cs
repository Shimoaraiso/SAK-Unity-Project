using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

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

	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}
	
	void Update () {
		inputHrz = Input.GetAxis("Horizontal");
		//inputVtc = Input.GetAxis("Vertical");

		if (Input.GetButtonDown("Jump") && isGround) {
			isJumping = true;
		} else if (Input.GetButtonDown("Jump") && !isGround) {
			isDash = true;
			

		} else if (Input.GetButtonUp("Jump") && !isGround) {
			isDash = false;
		}


		SetAnimator();


		// test
		if (Input.GetKeyDown(KeyCode.Q)) {
			SceneManager.LoadScene(0);
		}
	}

	void SetAnimator() {
		anim.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
		anim.SetFloat("VelocityY", rb.velocity.y);
		anim.SetBool("isJumping", isJumping);
		anim.SetBool("isGround", isGround);
		anim.SetBool("isDead", isDead);
	}

	void FixedUpdate() {
		//rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
		rb.velocity = new Vector2(inputHrz * moveSpeed, rb.velocity.y);
		if (isJumping) {
			isJumping = false;
			isGround = false;
			rb.velocity = new Vector2(rb.velocity.x, 0f);
			rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
		}
		if (isDash) {
			isJumping = false;
			isGround = false;
			anim.SetTrigger("isDash");
			rb.velocity = new Vector2(rb.velocity.x, 0f);
			rb.AddForce(new Vector3(0.2f, 0.2f, 0) * jumpPower, ForceMode2D.Impulse);
			//rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("Ground")) {
			isGround = true;
		}

	}
	//void OnTriggerExit2D(Collider2D col) {
	//	if (col.gameObject.CompareTag("Ground")) {
	//	}
	//}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.CompareTag("Enemy")) {
			rb.simulated = false;
			isDead = true;
		}
	}
}
