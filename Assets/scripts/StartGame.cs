using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	void OnGUI(){
		if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "PLAY")) {
			Application.LoadLevel ("first");
		}
	}


	// Use this for initialization
//	void Start () {
//		Invoke ("LoadLevel",3f);
//	}
//	
//
//	void LoadLevel(){
//		Application.LoadLevel ("first");
//	}

}
