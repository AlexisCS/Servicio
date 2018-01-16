using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inicio : MonoBehaviour {
	public GameObject creditosPanel;


	public void MostrarCreditos (){
		creditosPanel.gameObject.SetActive (true);
	}

	public void SalirCreditos(){
		creditosPanel.gameObject.SetActive (false);
	}

	public void Jugar() {
		//SceneManager.LoadScene ("Login");
	}

	void Awake(){
		creditosPanel.gameObject.SetActive (false);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
