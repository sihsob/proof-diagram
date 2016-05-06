using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Input_Script : MonoBehaviour {

	//public variables
	public InputField sentence;
	public InputField justification;

	//private variables
	GameController_Script gc;

	//====================================================================================

	public void OnConfirm()
	{
		gc = GameObject.Find("GameController").GetComponent<GameController_Script>();
		gc.newNode(sentence.text, justification.text);

		Destroy(gameObject);
	}
}
