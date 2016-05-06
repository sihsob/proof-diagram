using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Networking;

[System.Serializable]
public class ProofBubble_Script : MonoBehaviour {

	//public variables
	public int label;
	public string sentence;
	public string justification;
	public List<string> reference;

	//private variables

	//position variables
	Vector3 screenPoint;
	Vector3 offset;
	bool verified;

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
		reference = new List<string>();
	}
	
	// Update is called once per frame
	void Update () {
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
			if(Input.GetMouseButtonDown(0))
			{
				gc.addPoint(gameObject);
			}
		}
	}

	//========================================================================================================================================================
	//Programmer defined functions

	public void toJson()
	{
		string json = JsonUtility.ToJson(this);
		Debug.Log(json);
		StartCoroutine("connect", json);
	}

	IEnumerator connect(string js)
	{
		string url = "http://129.161.89.167:5000";

		Dictionary<string, string> d= new Dictionary<string, string>();
		d.Add("Content-Type", "application/json");

		byte[] data = System.Text.Encoding.UTF8.GetBytes(js);
		WWW request = new WWW(url, data, d);

		yield return request;
	
		if(request.error == null)
		{
			string s = request.text;
			if(s.Contains("true"))
				Debug.Log("yay");
		}
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
		if(!reference.Contains(r))
		{
			reference.Add(r);
		}
	}

	public void setVerified()
	{
		verified = true;
	}

	//functions for testing
	public void printReferences()
	{
		Debug.Log("For node: " + label);
		foreach(string s in reference)
		{
			Debug.Log(s);
		}
	}
}
