using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Information_Script : MonoBehaviour {

	//public variables
	public GameObject infoCanvas;
	public GameObject []info_texts;

	//private variables
	bool paused;
	int active_page;

	//=================================================================================================================================

	// Use this for initialization
	void Start () {
		paused = false;
		active_page = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(paused)
		{
			infoCanvas.SetActive(true);
			info_texts[active_page].SetActive(true);
			Time.timeScale = 0f;
		}
		else
		{
			infoCanvas.SetActive(false);
			Time.timeScale = 1f;
		}
	}

	//=================================================================================================================================

	public void next()
	{
		info_texts[active_page].SetActive(false);

		active_page++;
		if(active_page >= info_texts.Length)
		{
			active_page = 0;
		}
	}

	public void previous()
	{
		info_texts[active_page].SetActive(false);

		active_page--;
		if(active_page < 0)
		{
			active_page = info_texts.Length - 1;
		}
	}

	public void close()
	{
		paused = false;
		active_page = 0;

		for(int i = 0; i < info_texts.Length; i++)
		{
			info_texts[i].SetActive(false);
		}
	}

	public void makePaused()
	{
		paused = true;
	}
}
