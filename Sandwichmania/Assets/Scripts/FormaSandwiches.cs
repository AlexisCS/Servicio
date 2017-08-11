using UnityEngine;
using System.Collections;

public class FormaSandwiches : MonoBehaviour {

	public GameObject[] textAyuda;
	public GameObject panel;
	private string estadoActual;
	// Use this for initialization
	void Start () {
		estadoActual = "ini";
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.RightArrow)
		     || Input.GetKeyDown (KeyCode.LeftArrow)) && estadoActual == "ini") {
			panel.SetActive (false);
			for (int i = 0; i < textAyuda.Length; i++) {
				textAyuda [i].SetActive (false); 
			}
			NotificationCenter.DefaultCenter ().PostNotification(this,"MuestraIngre");
		}
	}
}
