using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Display_Script : MonoBehaviour {

	//public variables
	public Text label;
	public Text sentence;
	public Text entire_label;
	public Text entire_sentence;
	public Text entire_justification;

	public GameObject hoverCanvas;

	// Use this for initialization
	void Start () {
		label.text = gameObject.GetComponent<ProofBubble_Script>().getLabel().ToString();
		sentence.text = gameObject.GetComponent<ProofBubble_Script>().getSentence();

		label.fontSize = 25;
		sentence.fontSize = 25;

		entire_label.text = "Number: " + label.text;
		entire_sentence.text = "Sentence: " + sentence.text;
		entire_justification.text = "Justification" + gameObject.GetComponent<ProofBubble_Script>().getJustification();

		hoverCanvas.SetActive(false);
	}

	void OnMouseEnter()
	{
		hoverCanvas.SetActive(true);
	}

	void OnMouseExit()
	{
		hoverCanvas.SetActive(false);
	}
}
