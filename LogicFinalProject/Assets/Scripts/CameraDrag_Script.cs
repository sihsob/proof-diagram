using UnityEngine;
using System.Collections;

public class CameraDrag_Script : MonoBehaviour {

	//public variables
	public float dragSpeed = 2f;

	//private variables
	//bool drag = false;
	Vector3 oldPos;
	Vector3 origin;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(Input.GetMouseButtonDown(1))
		{
		//	drag = true;
			oldPos = transform.position;
			origin = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		}

		if(Input.GetMouseButton(1))
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - origin;
			transform.position = oldPos + -pos * dragSpeed;
		}
		/*
		if(Input.GetMouseButtonUp(1))
		{
			drag = false
		}
*/
	}
}
