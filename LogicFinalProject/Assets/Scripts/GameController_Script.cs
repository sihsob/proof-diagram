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
	int nodeCount;
	int currentSubproof;
	int mainLabelCount;

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
		nodeCount = 0;
		mainLabelCount = 0;

		currentSubproof = -1;

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

		if(inSubproof)
		{
			node.GetComponent<ProofBubble_Script>().setLabel(subproof_index+1);
		}
		else
		{
			node.GetComponent<ProofBubble_Script>().setLabel(mainLabelCount+1);
			mainLabelCount += 1;
		}

		if(all_Nodes.Count != 0)
		{
			GameObject previous = all_Nodes[all_Nodes.Count-1];

			float new_x = 0f;
			float new_y = 0f;

			if(previous.GetComponent<ProofBubble_Script>().IsSubproof())
			{
				new_x = previous.transform.position.x-1f;
				new_y = previous.transform.position.y+1.5f;
			}
			else
			{
				new_x = previous.transform.position.x + 2;
				new_y = previous.transform.position.y - 3;			
			}

			node.transform.position = new Vector3(new_x, new_y, 0);
		}

		nodeCount++;
		if(inSubproof)
		{
			//resize subproof box if about to go out of the box
			Vector3 maxBounds = all_Nodes[currentSubproof].GetComponent<BoxCollider2D>().bounds.max;
			Vector3 bounds = all_Nodes[currentSubproof].transform.position + maxBounds;
			Vector3 placedNode = node.transform.position + node.GetComponent<SpriteRenderer>().sprite.bounds.max;

			if(placedNode.x > bounds.x)
			{
				resizeSubproof(all_Nodes[currentSubproof]);
			}

			subproof_index++;
		}

		all_Nodes.Add(node);
		input_canvas.SetActive(false);
	}

	public void setActiveNode(GameObject active)
	{
		if(activeNode != null)
		{
			activeNode.GetComponent<ProofBubble_Script>().deactivate();	
		}
			
		activeNode = active;
		activeNode.GetComponent<ProofBubble_Script>().activatedColor();
	}

	public void addReasons(GameObject a_reason)
	{
		activeNode.GetComponent<ProofBubble_Script>().addReference(a_reason.GetComponent<ProofBubble_Script>().sentence);

		if(!reasons.Contains(a_reason) && activeNode != a_reason)
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

		GameObject node = Instantiate(subproof);

		if(all_Nodes.Count != 0)
		{
			GameObject previous = all_Nodes[all_Nodes.Count-1];
			node.transform.position =  new Vector3(previous.transform.position.x, previous.transform.position.y - 6, 0);
		}

		node.GetComponent<ProofBubble_Script>().setAsSubproof();
		node.GetComponent<ProofBubble_Script>().setLabel(mainLabelCount+1);

		nodeCount++;
		mainLabelCount += 1;
		all_Nodes.Add(node);

		currentSubproof = all_Nodes.Count - 1;
	}

	public void endSubproof()
	{
		inSubproof = false;
		activeSubproof.SetActive(false);

		string sub_premise = all_Nodes[nodeCount-subproof_index].GetComponent<ProofBubble_Script>().sentence;
		string sub_conclusion = all_Nodes[nodeCount-1].GetComponent<ProofBubble_Script>().sentence;
	
		all_Nodes[nodeCount-subproof_index-1].GetComponent<ProofBubble_Script>().sentence = sub_premise + "->" + sub_conclusion;

		subproof_index = 0;
		currentSubproof = -1;
	}

	public void verify()
	{
		if(activeNode != null)
		{
			activeNode.GetComponent<ProofBubble_Script>().toJson();
		}
	}

	public void nextStep()
	{
		input_canvas.SetActive(true);
	}

	public void clearAll()
	{
		for(int i = 0; i < all_Nodes.Count; i++)
		{
			Destroy(all_Nodes[i]);
		}
		all_Nodes.Clear();
		
		activeNode = null;
		reasons.Clear();

		for(int i = 0; i < arrows.Count; i++)
		{
			Destroy(arrows[i]);
		}
		arrows.Clear();
		
		points[0] = null;
		points[1] = null;
		points_index = 0;
		addArrows = false;
		inSubproof = false;
		nodeCount = 0;
		currentSubproof = 0;
		mainLabelCount = 0;
		activeAddarrows.SetActive(false);
	}

	public void quitGame()
	{
		Application.Quit();
	}

	public void infoScreen()
	{
		gameObject.GetComponent<Information_Script>().makePaused();
	}

	void resizeSubproof(GameObject target)
	{
		target.transform.localScale = new Vector3(1.5f*target.transform.localScale.x,1.5f*target.transform.localScale.y);

		float moveX = target.transform.position.x + 1f;
		float moveY = target.transform.position.y - 1.5f;

		target.transform.position = new Vector3(moveX, moveY);
	}
}
