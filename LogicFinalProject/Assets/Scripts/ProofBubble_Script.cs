using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ProofBubble_Script : MonoBehaviour {

	//public variables
	public int label;
	public string sentence;
	public string justification;
	public List<string> references;

	//private variables

	//position variables
	Vector3 screenPoint;
	Vector3 offset;
	bool verified;
//	bool active;

	//other variables
	PhysicMaterial current;
	GameController_Script gc;

	//==============================================================================================================================
	//Unity defined functions
	
	// Use this for initialization
	void Start () {

		verified = false;
	//	active = false;

		gc = GameObject.Find("GameController").GetComponent<GameController_Script>();
	}
	
	// Update is called once per frame
	void Update () {
	/*	if(!gc.getAddArrowMode())
		{
			if(Input.GetMouseButtonDown(0))
			{
				gc.setActiveNode(gameObject);
				gc.clearReasons();
			}
			else if(Input.GetMouseButtonDown(1))
			{
				gc.addReasons(gameObject);
			}			
		}
		else
		{
			//Debug.Log("Entered add arrow mode");
			if(Input.GetMouseButtonDown(0))
			{
				gc.addPoint(gameObject);
			}
		}*/
	}
	
	//------------------------------------------------------------------------------------------------------------------------------------------------------
	//mouse functions
	//when mouse button is pressed down
	void OnMouseDown()
	{
		if(!gc.getAddArrowMode())
		{
			if(Input.GetMouseButtonDown(0))
			{
				gc.setActiveNode(gameObject);
				gc.clearReasons();
			}
			else if(Input.GetMouseButtonDown(1))
			{
				gc.addReasons(gameObject);
			}			
		}
		else
		{
			//Debug.Log("Entered add arrow mode");
			if(Input.GetMouseButtonDown(0))
			{
				gc.addPoint(gameObject);
			}
		}
		/*
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		*/
	}
/*
	//when mouse is dragged around screen
	void OnMouseDrag()
	{
		Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint);
		transform.position = currentPosition;
	}
*/
	//========================================================================================================================================================
	//Programmer defined functions

	void toJson()
	{
		string json = JsonUtility.ToJson(this);
	}

	//----------------------------------------------------------------------------------------------------------------------------------------------------------
	//set functions

	public void setLabel(int l)
	{
		label = l;
	}

	public void setSentence(string s)
	{
		sentence = s;
	}

	public void setJustification(string s)
	{
		justification = s;
	}

	public void addReference(string r)
	{
		if(!references.Contains(r))
		{
			references.Add(r);
		}
	}

	//functions for testing
	public void printReferences()
	{
		Debug.Log("For node: " + label);
		foreach(string s in references)
		{
			Debug.Log(s);
		}
	}
}
