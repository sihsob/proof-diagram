using UnityEngine;
using System.Collections;

public class CameraDrag_Script : MonoBehaviour {

	//public variables
	public float dragSpeed = 2f;
	public float zoomSpeed = 2f;

	public int maxZoom = 500;
	public int minZoom = 100;

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

		float scroll = Input.GetAxis("Mouse ScrollWheel");
		if(scroll < 0)
		{
			Debug.Log("less than 0");
			if(Camera.main.orthographicSize <= maxZoom)
			{
				Camera.main.orthographicSize += zoomSpeed;
			}
		}
		else if(scroll > 0)
		{
			Debug.Log("more than zero");
			if(Camera.main.orthographicSize >= minZoom)
			{
				Debug.Log("change size");
				Camera.main.orthographicSize -= zoomSpeed;
			}
		}
	}
}
