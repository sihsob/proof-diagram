using UnityEngine;
using System.Collections;

[System.Serializable]
public class ProofBubble_Script : MonoBehaviour {

	//public variables

	//private variables

	//position variables
	Vector3 screenPoint;
	Vector3 offset;
	Proof_Script proof;
	bool verified;
//	bool active;

	//other variables
	PhysicMaterial current;
	GameController_Script gc;

	//==============================================================================================================================
	//Unity defined functions
	
	// Use this for initialization
	void Start () {
		proof = GetComponent<Proof_Script>();
		verified = false;
	//	active = false;

		gc = GameObject.Find("GameController").GetComponent<GameController_Script>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			gc.setActiveNode(this);
			gc.clearReasons();
			Debug.Log("Active Node: " + proof.getJustification());
		}
		else if(Input.GetMouseButtonDown(1))
		{
			gc.addReasons(this);
			Debug.Log("Reason: " + proof.getJustification());
		}
	}
	
	//------------------------------------------------------------------------------------------------------------------------------------------------------
	//mouse functions
	//when mouse button is pressed down
	void OnMouseDown()
	{
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	//when mouse is dragged around screen
	void OnMouseDrag()
	{
		Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint);
		transform.position = currentPosition;
	}
	
	//================================================================================================================================
	//Programmer defined functions

	public void setProofVars(ProofBubble_Script obj)
	{
		proof.setLabel(obj.getProof().getLabel());
		proof.setSentence(obj.getProof().getSentence());
		proof.setJustification(obj.getProof().getJustification());
		proof.setReferences(obj.getProof().getReferences());
	}
	
	Proof_Script getProof()
	{
		return proof;
	}
}
