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

	//appearance variables
	Color normal_color;
	Color active;
	Color verified;
	Color subproof;

	//other variables
	GameController_Script gc;

	//==============================================================================================================================
	//Unity defined functions
	
	// Use this for initialization
	void Start () {
		gc = GameObject.Find("GameController").GetComponent<GameController_Script>();
		reference = new List<string>();

		//define the colors
		normal_color = new Color32(195, 139, 227,255);
		active = new Color32(93, 131, 255,255);
		verified = new Color32(14, 191, 1,255);
		subproof = new Color32(215, 215, 215,255);
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	//------------------------------------------------------------------------------------------------------------------------------------------------------
	//mouse functions
	//when mouse button is pressed down
	void OnMouseDown()
	{
		if(Input.GetMouseButtonDown(0))
		{
			gc.setActiveNode(gameObject);
			gc.clearReasons();
			//gameObject.GetComponent<SpriteRenderer>().color = active;
		}
		else if(Input.GetMouseButtonDown(1))
		{
			gc.addReasons(gameObject);
		}			
	
		if(gc.getAddArrowMode())
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
		StartCoroutine("connect", json);
	}

	//send info to verification program & get info back
	IEnumerator connect(string js)
	{
		string url = "https://tpiazza.me/proof";

		Dictionary<string, string> d= new Dictionary<string, string>();
		d.Add("Content-Type", "application/json");

		byte[] data = System.Text.Encoding.UTF8.GetBytes(js);
		WWW request = new WWW(url, data, d);

		yield return request;
	
		if(request.error == null)
		{
			string s = request.text;
			Debug.Log(s);
			if(s.Contains("true"))
			{
				gameObject.GetComponent<SpriteRenderer>().color = verified;
			}
		}
		else{
			Debug.Log(request.error);
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
	public void activatedColor()
	{
		gameObject.GetComponent<SpriteRenderer>().color = active;
	}

	public void deactivate()
	{
		if(isSubproof)
		{
			gameObject.GetComponent<SpriteRenderer>().color = subproof;
		}
		else
		{
			gameObject.GetComponent<SpriteRenderer>().color = normal_color;
		}
	}
}
