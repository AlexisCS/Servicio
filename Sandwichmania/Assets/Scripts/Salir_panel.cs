using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salir_panel : MonoBehaviour {
	public GameObject window;

	public void Salir(){
		Application.Quit ();
	}
	public void Exitwindow(){
		window.SetActive (true);
	}
	void Awake(){
		if (window != null)
			window.SetActive (false);
	}



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
