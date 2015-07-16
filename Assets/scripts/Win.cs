using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour {

	void OnGUI(){

		if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "REPLAY")) {
			Application.LoadLevel ("first");
		}

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
