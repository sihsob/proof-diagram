using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController_Script : MonoBehaviour {


	//private variables
	ProofBubble_Script activeNode;
	List<ProofBubble_Script> reasons;

	// Use this for initialization
	void Start () {
		reasons = new List<ProofBubble_Script>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	//--------------------------------------------------------------------------------------------------------------------------------
	//Programmer defined functions

	public void setActiveNode(ProofBubble_Script active)
	{
		activeNode = active;
	}

	public void addReasons(ProofBubble_Script a_reason)
	{
		reasons.Add(a_reason);
	}

	public void clearReasons()
	{
		reasons.Clear();
	}
}
