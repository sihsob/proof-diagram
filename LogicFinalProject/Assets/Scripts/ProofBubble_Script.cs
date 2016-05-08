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
	bool incomingArrow;
	bool subproof;
	bool correct;

	List<GameObject> references;
	int subproof_index;

	//other variables
	Material normal;
	Material active;
	Material verified;
	GameController_Script gc;

	//==============================================================================================================================
	//Unity defined functions
	
	// Use this for initialization
	void Start () {
	//	active = false;

		gc = GameObject.Find("GameController").GetComponent<GameController_Script>();
		reference = new List<string>();
		references = new List<GameObject>();
		incomingArrow = false;
		subproof = false;
		correct =false;
		subproof_index = -1;

		normal = Resources.Load("Normal_mat", typeof(Material)) as Material;
		active = Resources.Load("Active_mat", typeof(Material)) as Material;
		verified = Resources.Load("Verified_mat", typeof(Material)) as Material;
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
				gameObject.GetComponent<Renderer>().material = active;
				gc.setActiveNode(gameObject);
				gc.clearReasons();
			}
		}
		else
		{
			if(Input.GetMouseButtonDown(0))
			{
				gc.addPoint(gameObject);
			}
			else if(Input.GetMouseButtonDown(1))
			{
				gc.resertPoints();
			}
		}
	}

	//========================================================================================================================================================
	//Programmer defined functions

	public void toJson()
	{
		//Debug.Log("index: " + subproof_index);
		if(subproof_index != -1)
		{
			string rsn = gc.subproofSen(subproof_index);
			reference.Add(rsn);
		}

		string json = JsonUtility.ToJson(this);
		Debug.Log(json);
		//StartCoroutine("connect", json);
	}

	IEnumerator connect(string js)
	{
		string url = "http://129.161.80.95:5000";

		Dictionary<string, string> d= new Dictionary<string, string>();
		d.Add("Content-Type", "application/json");

		byte[] data = System.Text.Encoding.UTF8.GetBytes(js);
		WWW request = new WWW(url, data, d);

		yield return request;
	
		if(request.error == null)
		{
			string s = request.text;
			//Debug.Log(s);
			if(s.Contains("true"))
			{
				gameObject.GetComponent<Renderer>().material = verified;
				correct = true;
				Debug.Log("yay");
			}
				
		}
		else{
			Debug.Log(request.error);
		}
	}

	public void subProofReference(string end)
	{
		reference.Add(sentence + " -> " + end);
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

	public void addReference(GameObject r)
	{
		if(!references.Contains(r))
		{
			references.Add(r);
			reference.Add(r.GetComponent<ProofBubble_Script>().sentence);
			//Debug.Log(r.GetComponent<ProofBubble_Script>().sentence);
		}
	}

	public void setHasIncoming()
	{
		incomingArrow = true;
	}

	public void setAsSubproof()
	{
		subproof = true;
	}

	//----------------------------------------------------------------------------------------------------------------------------------------------------------------
	// get functions
	public string getSentence()
	{
		return sentence;
	}

	public bool hasIncoming()
	{
		return incomingArrow;
	}

	public bool isSubproof()
	{
		return subproof;
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

	//----------------------------------------------------------------------------------------------------------------------------------------------------------------
	//other

	public void setNormalMat()
	{
		if(!correct)
			gameObject.GetComponent<Renderer>().material = normal;	
		else
			gameObject.GetComponent<Renderer>().material = verified;
	}

	public void addIndex(int i)
	{
		subproof_index = i;
	}
}
