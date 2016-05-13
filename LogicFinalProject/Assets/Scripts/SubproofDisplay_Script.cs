using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SubproofDisplay_Script : MonoBehaviour {

	public Text label;

	// Use this for initialization
	void Start () {
		label.text = gameObject.GetComponent<ProofBubble_Script>().getLabel().ToString();
		label.fontSize = 25;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
