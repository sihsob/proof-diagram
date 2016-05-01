using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Proof_Script : MonoBehaviour {

	//private variables
	public int label;
	public string sentence;
	public string justification;
	public List<string> references;

	//====================================================================================
	// Unity defined functions

	// Use this for initialization
	void Start () {
		label = 0;
		sentence = "";
		justification = "";
		references = new List<string>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	//=======================================================================================
	// Programmer defined functions

	public void to_Json()
	{
		
		Proof_Script obj =  new Proof_Script();
		obj.label = label;
		obj.sentence = sentence;
		obj.justification = justification;
		obj.references = references;

		string json = JsonUtility.ToJson(obj);
		Debug.Log(json);
	}

	//-------------------------------------------------------------------------------------------
	//set functions to set variables

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

	public void setReferences(List<string> a)
	{
		references = a;
	}

	//----------------------------------------------------------------------------------------------
	//get functions

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

	public List<string> getReferences()
	{
		return references;
	}
}
