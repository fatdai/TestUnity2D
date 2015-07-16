using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieController : MonoBehaviour {

	private List<Transform> congaLine = new List<Transform> ();

	public float moveSpeed;
	private Vector3 moveDir = Vector3.right;

	public float turnSpeed;


	private int lives = 3;
	private bool isInvincible = false;
	private float timeSpentInvincible;

	private SpriteRenderer render;

	// Use this for initialization
	void Start () {
		render = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		// move
		Vector3 currentPosition = transform.position;

		if(Input.GetButton("Fire1")){
			
			Vector3 moveToward = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			moveDir = moveToward - currentPosition;
			moveDir.z = 0;
			moveDir.Normalize();
		}

		Vector3 target = currentPosition + moveDir * moveSpeed;
		transform.position = Vector3.Lerp(currentPosition,target,Time.deltaTime);

		// rotation
		float targetAngle = Mathf.Atan2(moveDir.y,moveDir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,0,targetAngle),turnSpeed * Time.deltaTime);

		EnforceBounds ();

		// 1
		if (isInvincible) {
			
			// 2
			timeSpentInvincible += Time.deltaTime;

			// 3
			if (timeSpentInvincible < 3f) {
				float remainder = timeSpentInvincible % 0.3f;
				render.enabled = remainder > 0.15f;
			} else {
				render.enabled = true;
				isInvincible = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		//Debug.Log("Enter Hit " + other.gameObject);
		if (other.CompareTag ("cat")) {

			Transform followTarget = congaLine.Count == 0 ? transform : congaLine[congaLine.Count-1];
			other.GetComponent<CatController> ().JoinConga (followTarget,moveSpeed,turnSpeed);
			congaLine.Add (other.transform);
		} else if (other.CompareTag ("enemy") && !isInvincible) {

			isInvincible = true;
			timeSpentInvincible = 0;

			for (int i = 0; i < 2 && congaLine.Count > 0; ++i) {
				int lastIndex = congaLine.Count - 1;
				Transform cat = congaLine [lastIndex];
				congaLine.RemoveAt (lastIndex);
				cat.GetComponent<CatController> ().ExitConga ();
			}

			if (--lives <= 0) {
				Debug.Log ("You lost!");
				Application.LoadLevel ("LoseScene");
			}
		}

		if (congaLine.Count >= 5) {
			Debug.Log ("You win!");
			Application.LoadLevel ("WinScene");
		}
	}

	void OnTriggerExit2D(Collider2D other){
		//Debug.Log("Exit Hit " + other.gameObject);
	}

	private void EnforceBounds(){
		// 1 
		Vector3 newPosition = transform.position;
		Camera mainCamera = Camera.main;
		Vector3 cameraPosition = mainCamera.transform.position;

		// 2
		float xDist = mainCamera.aspect * mainCamera.orthographicSize;
		float xMax = cameraPosition.x + xDist;
		float xMin = cameraPosition.x - xDist;

		// 3
		if(newPosition.x < xMin || newPosition.x > xMax){
			newPosition.x = Mathf.Clamp (newPosition.x,xMin,xMax);
			moveDir.x = -moveDir.x;
		}

		// vertical
		float yMin = cameraPosition.y - mainCamera.orthographicSize;
		float yMax = cameraPosition.y + mainCamera.orthographicSize;
		if (newPosition.y < yMin || newPosition.y > yMax) {
			newPosition.y = Mathf.Clamp (newPosition.y,yMin,yMax);
			moveDir.y = -moveDir.y;
		}

		// 4
		transform.position = newPosition;
	}
}
