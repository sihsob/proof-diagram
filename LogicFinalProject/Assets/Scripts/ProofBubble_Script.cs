using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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
	bool isSubproof = false;

	//material variables
	Material normal;
	Material verified;
	Material active;
	Material subproof;

	//other variables
	GameController_Script gc;

	//==============================================================================================================================
	//Unity defined functions
	
	// Use this for initialization
	void Start () {
		gc = GameObject.Find("GameController").GetComponent<GameController_Script>();
		reference = new List<string>();

		normal = Resources.Load("Normal_mat", typeof(Material)) as Material;
		verified = Resources.Load("Verified_mat", typeof(Material)) as Material;
		active = Resources.Load("Active_mat", typeof(Material)) as Material;
		subproof = Resources.Load("SubProof_mat", typeof(Material)) as Material;
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
				GetComponent<Renderer>().material = active;
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
		//StartCoroutine("connect", json);
	}

	//send info to verification program & get info back
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
			{
				Debug.Log("yay");
				GetComponent<Renderer>().material = verified;
			}
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
		Debug.Log("Justification: " + justification);
	}

	public void addReference(string r)
	{
		if(!reference.Contains(r))
		{
			reference.Add(r);
		}
	}

	public void setAsSubproof()
	{
		isSubproof = true;
	}

	//--------------------------------------------------------------------------------------------------------------------------------------------------------------
	//get functions
	public bool IsSubproof()
	{
		return isSubproof;
	}

	public int getLabel()
	{
		return label;
	}

	public string getSentence()
	{
		return sentence;
	}

	public string getJustification()
	{
		return justification;
	}

	//-------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void deactivatedMat()
	{
		if(isSubproof)
		{
			GetComponent<Renderer>().material = subproof;
		}
		else
		{
			GetComponent<Renderer>().material = normal;
		}
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
