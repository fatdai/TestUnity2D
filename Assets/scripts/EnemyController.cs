using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float speed = -1;
	private Rigidbody2D rigidbody;

	private Transform spawnPoint;

	private Camera camera;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody2D>();
		rigidbody.velocity = new Vector2(speed,0);
		camera = Camera.main;
		spawnPoint = GameObject.Find ("SpawnPoint").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnBecameInvisible(){

		if (null == camera) {
			return;
		}

		float yMax = camera.orthographicSize - 0.5f;
		transform.position = new Vector3 (spawnPoint.position.x,
			Random.Range (-yMax, yMax), transform.position.z
		);
	}
}
