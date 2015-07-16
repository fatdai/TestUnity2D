﻿using UnityEngine;
using System.Collections;

public class ScreenRelativePosition : MonoBehaviour {

	public enum ScreenEdge{
		LEFT,
		RIGHT,
		TOP,
		BUTTOM
	};


	public ScreenEdge screenEdge;
	public float yOffset;
	public float xOffset;

	// Use this for initialization
	void Start () {

		// 1
		Vector3 newPosition = transform.position;
		Camera camera = Camera.main;

		// 2
		switch(screenEdge){
		case ScreenEdge.RIGHT:
			newPosition.x = camera.aspect * camera.orthographicSize + xOffset;
			newPosition.y = yOffset;
			break;
		case ScreenEdge.TOP:
			newPosition.x = xOffset;
			newPosition.y = camera.orthographicSize + yOffset;
			break;
		case ScreenEdge.LEFT:
			newPosition.x = -camera.aspect * camera.orthographicSize + xOffset;
			newPosition.y = yOffset;
			break;
		case ScreenEdge.BUTTOM:
			newPosition.y = -camera.orthographicSize + yOffset;
			newPosition.x = xOffset;
			break;
		};

		// 5
		transform.position = newPosition;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
