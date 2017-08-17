using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Admin : MonoBehaviour {
	public GameObject[] panels;
	public Button[] buttons;


	// Use this for initialization
	void Start () {
		//solo para probar   !!!!!!!!!!!!!!!!!!!!!!!!
		Admin_level0.datos = new InfoPartida ();
		//BORRAR!!!!!!!!!!!!!!!!!!!!!!!!
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Awake(){
		if (panels[1] != null)
			panels[1].SetActive (false);
		buttons [1].gameObject.SetActive(false);
	}

	public void SelecNivel(int nivel){
		Admin_level0.datos.nivel = nivel;
		panels [0].SetActive(false);
		panels [1].SetActive(true);
		buttons [0].gameObject.SetActive(false);
		buttons [1].gameObject.SetActive(true);
	}

	public void SelecMano(int mano){
		Admin_level0.datos.mano = mano;
		Calibrar ();
	}

	public void Regresa(){
		
	}
		
	public void Calibrar(){
		if (Admin_level0.datos.mano == 1 || Admin_level0.datos.mano == 0) {
			SceneManager.LoadScene (2);
		}
	}
}

