using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController_Script : MonoBehaviour {

	//public variables
	public GameObject proofBubble;
	public GameObject arrow;

	//private variables
	GameObject activeNode;
	List<GameObject> reasons;
	List<GameObject> all_Nodes;
	List<GameObject> arrows;

	GameObject []points;
	int points_index;
	bool addArrows;

	//===========================================================================================================================================

	// Use this for initialization
	void Start () {
		reasons = new List<GameObject>();
		all_Nodes = new List<GameObject>();
		arrows = new List<GameObject>();
		addArrows = false;

		points = new GameObject[2];
		for(int i = 0; i < 2; i++)
		{
			points[i] = null;
		}
		points_index = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(addArrows && Input.GetKeyDown(KeyCode.A) && !(points[1] == null))
		{
			GameObject newArrow = Instantiate(arrow);
			newArrow.GetComponent<Arrow_Script>().scale(points[0].transform.position, points[1].transform.position);
		}
	}

	//================================================================================================================================================
	//Programmer defined functions

	public void newNode(string sentence, string justification)
	{
		GameObject node = Instantiate(proofBubble);

		node.GetComponent<ProofBubble_Script>().setSentence(sentence);
		node.GetComponent<ProofBubble_Script>().setJustification(justification);
		node.GetComponent<ProofBubble_Script>().setLabel(all_Nodes.Count);

		if(all_Nodes.Count != 0)
		{
			GameObject previous = all_Nodes[all_Nodes.Count-1];
			node.transform.position =  new Vector3(previous.transform.position.x, previous.transform.position.y + 10, previous.transform.position.z);
		}

		all_Nodes.Add(node);
	}

	public void setActiveNode(GameObject active)
	{
		activeNode = active;
	}

	public void addReasons(GameObject a_reason)
	{
		activeNode.GetComponent<ProofBubble_Script>().addReference(a_reason.GetComponent<ProofBubble_Script>().sentence);
		reasons.Add(a_reason);
	}

	public void clearReasons()
	{
		reasons.Clear();
	}

	public void addArrowMode()
	{
		addArrows = !addArrows;
	}

	public bool getAddArrowMode()
	{
		return addArrows;
	}

	public void addPoint(GameObject obj)
	{
		points[points_index] = obj;
		points_index++;

		if(points_index >= 2)
		{
			points_index = 0;
		}
	}
}
