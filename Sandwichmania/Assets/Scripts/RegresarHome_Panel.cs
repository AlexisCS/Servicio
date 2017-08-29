using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegresarHome_Panel : MonoBehaviour {
	public GameObject window;

	public void BotonSi(){
		Application.Quit ();
	}
	public void BotonNo(){
		window.SetActive (true);
	}
	void Awake(){
		if (window != null)
			window.SetActive (false);
	}


}
