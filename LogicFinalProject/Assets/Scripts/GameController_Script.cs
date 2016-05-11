using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController_Script : MonoBehaviour {

	//public variables
	public GameObject proofBubble;
	public GameObject subproof;
	public GameObject arrow;
	public GameObject input_canvas;
	public GameObject activeAddarrows;
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
	int subproof_index;
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

		input_canvas.SetActive(false);
		activeAddarrows.SetActive(false);
		activeSubproof.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(addArrows && Input.GetKeyDown(KeyCode.A) && !(points[1] == null))
		{
			GameObject newArrow = Instantiate(arrow);
			newArrow.GetComponent<Arrow_Script>().scale(points[0].transform.position, points[1].transform.position);
			points[1].GetComponent<ProofBubble_Script>().addReference(points[0].GetComponent<ProofBubble_Script>().sentence);
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
		}

		all_Nodes.Add(node);
		input_canvas.SetActive(false);
	}

	public void setActiveNode(GameObject active)
	{
		if(activeNode != null)
		{
			activeNode.GetComponent<ProofBubble_Script>().deactivatedMat();	
		}

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
		activeAddarrows.SetActive(!activeAddarrows.activeInHierarchy);
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

	public void startSubproof()
	{
		inSubproof = true;
		activeSubproof.SetActive(true);
		subproof_index = 1;
	}

	public void endSubproof()
	{
		inSubproof = false;
		activeSubproof.SetActive(false);
		subproof_index = 0;
	}

	public void verify()
	{
		activeNode.GetComponent<ProofBubble_Script>().toJson();
	}

	public void nextStep()
	{
		input_canvas.SetActive(true);
	}
}
