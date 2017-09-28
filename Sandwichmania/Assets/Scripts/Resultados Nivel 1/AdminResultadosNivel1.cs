using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdminResultadosNivel1 : MonoBehaviour {

	public Text apiladoPan, apiladoJamon, apiladoQueso, apiladoJitomate;


	public void BotonSiguiente(){
		SceneManager.LoadScene (1);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
