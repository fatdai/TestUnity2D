using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour {

	private CircleCollider2D collider;
	private Animator animator;

	private Transform followTarget;
	private float moveSpeed;
	private float turnSpeed;
	private bool isZombie;



	private Vector3 targetPosition;

	void Start(){
		collider = GetComponent<CircleCollider2D> ();
		animator = GetComponent<Animator> ();
		camera = Camera.main;
	}

	void Update(){
		// 1
		if (isZombie) {
			// 2
			Vector3 currentPosition = transform.position;
			Vector3 moveDir = targetPosition - currentPosition;

			// 3
			float targetAngle = Mathf.Atan2(moveDir.y,moveDir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Slerp (transform.rotation,Quaternion.Euler(0,0,targetAngle),turnSpeed * Time.deltaTime);

			// 4
			float distanceToTarget = moveDir.magnitude;
			if (distanceToTarget > 0) {
				// 5
				if(distanceToTarget > moveSpeed){
					distanceToTarget = moveSpeed;
				}

				// 6
				moveDir.Normalize();
				Vector3 target = moveDir * distanceToTarget + currentPosition;
				transform.position = Vector3.Lerp (currentPosition,target,moveSpeed * Time.deltaTime);

			}
		}
	}

	void GrantCatTheSweetReleaseOfDeath()
	{
		DestroyObject( gameObject );
	}

	void OnBecameInvisible() {
		if (!isZombie) {
			Destroy( gameObject );
		} 
	} 


	//1
	public void JoinConga( Transform followTarget, float moveSpeed, float turnSpeed ) {

		//2
		this.followTarget = followTarget;
		this.moveSpeed = moveSpeed * 2;
		this.turnSpeed = turnSpeed;

		targetPosition = followTarget.position;

		//3
		isZombie = true;

		//4
		collider.enabled = false;
		animator.SetBool( "InConga", true );
	}

	public void ExitConga(){
		animator.SetBool ("InConga",false);
	}

	void UpdateTargetPosition()
	{
		targetPosition = followTarget.position;
	}

}
