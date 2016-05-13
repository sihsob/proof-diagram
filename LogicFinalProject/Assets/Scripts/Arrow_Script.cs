using UnityEngine;
using System.Collections;

public class Arrow_Script : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//=========================================================================================================
	//Programmer defined functions

	public void scale(Vector3 start, Vector3 end)
	{
		Vector3 midPoint = (start + end) / 2;

		transform.position = new Vector3(midPoint.x, midPoint.y, transform.position.z);
		Vector3 relativePos = end - start;
		float mag = relativePos.magnitude;

		transform.localScale = new Vector3(mag/8, 0.5f, 0);

		float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg - 360;
		Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = rot;
	}
}
