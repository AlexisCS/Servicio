using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AdminMenu : MonoBehaviour {
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
		if (panels[1] != null && panels[3] != null){
			panels [1].SetActive (false);
			panels [3].SetActive (false);
		}
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
		panels [1].SetActive(false);
		panels [0].SetActive(true);
		buttons [1].gameObject.SetActive(false);
		buttons [0].gameObject.SetActive(true);
	}

	public void LogOutPanel(){
		panels [0].SetActive(false);
		panels [2].SetActive(false);
		panels [3].SetActive(true);
		panels [4].SetActive(false);
	}

	public void LogOutYes(){
		SceneManager.LoadScene (0);
	}

	public void LogOutNo(){
		panels [0].SetActive(true);
		panels [2].SetActive(true);
		panels [3].SetActive(false);
		panels [4].SetActive(false);
	}

	public void Calibrar(){
		if (Admin_level0.datos.mano == 1 || Admin_level0.datos.mano == 0) {
			SceneManager.LoadScene (2);
			Debug.Log ("Nivel =" + Admin_level0.datos.nivel + ", Mano (0-> Izq, 1-> Der) =" + Admin_level0.datos.mano);
		}
	}
}

