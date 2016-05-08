using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController_Script : MonoBehaviour {

	//public variables
	public GameObject proofBubble;
	public GameObject subproofBubble;
	public GameObject arrow;
	public GameObject input_canvas;
	public GameObject activeAdd;
	public GameObject activeSubproof;

	//private variables
	GameObject activeNode;
	List<GameObject> reasons;
	List<GameObject> all_Nodes;
	List<GameObject> arrows;

	GameObject []points;
	int points_index;
	bool addArrows;
	bool inSubproof;
	int subproofBegin_index;
	int subproof_index;
	int subproof_count;
	int index;

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

		inSubproof = false;
		subproof_index = 0;
		index = 0;
		subproofBegin_index = 0;

		input_canvas.SetActive(false);
		activeAdd.SetActive(false);
		activeSubproof.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(addArrows && Input.GetKeyDown(KeyCode.A) && !(points[1] == null))
		{
			GameObject newArrow = Instantiate(arrow);
			newArrow.GetComponent<Arrow_Script>().scale(points[0].transform.position, points[1].transform.position);
			points[1].GetComponent<ProofBubble_Script>().addReference(points[0]);
			points[1].GetComponent<ProofBubble_Script>().setHasIncoming();
			reasons.Add(points[0]);
			/*
			if(points[0].Equals(all_Nodes[subproofBegin_index]))
			{
				Debug.Log(subproofBegin_index + " " + all_Nodes[subproofBegin_index].GetComponent<ProofBubble_Script>().isSubproof());
				Debug.Log("WHAT THE HECK?!");
			}
*/
			if(points[0].GetComponent<ProofBubble_Script>().isSubproof())
			{
				points[1].GetComponent<ProofBubble_Script>().addIndex(subproofBegin_index);
			}

			arrows.Add(newArrow);
		}
	}

	//================================================================================================================================================
	//Programmer defined functions

	public void newNode(string sentence, string justification)
	{
		if(justification == "PREMISE")
		{
			justification = "";
		}

		GameObject node = Instantiate(proofBubble);

		node.GetComponent<ProofBubble_Script>().setSentence(sentence);
		node.GetComponent<ProofBubble_Script>().setJustification(justification);
		node.GetComponent<ProofBubble_Script>().setLabel(all_Nodes.Count);

		if(all_Nodes.Count != 0)
		{
			GameObject previous = all_Nodes[all_Nodes.Count-1];
			node.transform.position =  new Vector3(previous.transform.position.x, previous.transform.position.y - 4, previous.transform.position.z);
		}

		index++;

		if(inSubproof)
		{
			subproof_index++;
			subproof_count++;
		}

		all_Nodes.Add(node);
		input_canvas.SetActive(false);
	}

	public void setActiveNode(GameObject active)
	{
		if(activeNode != null)
		{
			activeNode.GetComponent<ProofBubble_Script>().setNormalMat();
		}

		activeNode = active;
	}

	public void addReasons(GameObject a_reason)
	{
		activeNode.GetComponent<ProofBubble_Script>().addReference(a_reason);
		reasons.Add(a_reason);
	}

	public void clearReasons()
	{
		reasons.Clear();
	}

	public void addArrowMode()
	{
		addArrows = !addArrows;
		activeAdd.SetActive(!activeAdd.activeInHierarchy);
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

	public void resertPoints()
	{
		points[0] = null;
		points[1] = null;
	}

	public void startSubproof()
	{
		activeSubproof.SetActive(true);
		inSubproof = true;
		subproof_index = 1;
		subproof_count = 1;
		GameObject node = Instantiate(subproofBubble);
		node.GetComponent<ProofBubble_Script>().setAsSubproof();
		if(all_Nodes.Count != 0)
		{
			GameObject previous = all_Nodes[all_Nodes.Count-1];
			node.transform.position =  new Vector3(previous.transform.position.x, previous.transform.position.y - 4, previous.transform.position.z);
		}

		subproofBegin_index = index;
		index++;
		all_Nodes.Add(node);
		all_Nodes[subproofBegin_index].GetComponent<ProofBubble_Script>().setAsSubproof();
		//Debug.Log("in start of subproof " + subproofBegin_index);
	}

	public void endSubproof()
	{
		all_Nodes[subproofBegin_index].GetComponent<ProofBubble_Script>().sentence = 
			all_Nodes[subproofBegin_index+1].GetComponent<ProofBubble_Script>().sentence + " -> "
			+ all_Nodes[subproofBegin_index+subproof_count].GetComponent<ProofBubble_Script>().sentence;
		activeSubproof.SetActive(false);
		inSubproof = false;
		//subproof_index = 0;
	}

	public void verify()
	{
		if(!addArrows && !inSubproof)
		{
			if(activeNode.GetComponent<ProofBubble_Script>().hasIncoming())
			{
				activeNode.GetComponent<ProofBubble_Script>().toJson();
			}
		}
	}

	public void nextStep()
	{
		input_canvas.SetActive(true);
	}

	public string subproofSen(int ind)
	{
		//Debug.Log("start index: " + ind+1 + "  end index: " + ind+subproof_count);
		GameObject strt = all_Nodes[ind+1];
		GameObject nd = all_Nodes[ind+subproof_count];
		string rtn = strt.GetComponent<ProofBubble_Script>().getSentence() + " -> " + nd.GetComponent<ProofBubble_Script>().getSentence();
		//Debug.Log(rtn);
		return rtn;
	}
}
