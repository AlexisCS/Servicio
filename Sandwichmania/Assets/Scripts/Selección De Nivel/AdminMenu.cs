using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AdminMenu : MonoBehaviour {
	public GameObject[] panels;
	public InputField cantidadDeIngredientes, cantidadDeRepeticiones;
	public Text Advertencia;

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
		//buttons [1].gameObject.SetActive(false);
	}

	public void SelecNivel(int nivel){
		Admin_level0.datos.nivel = nivel;
		switch (nivel) {
		case 1:
			panels [0].gameObject.SetActive (false);
			panels [4].gameObject.SetActive (true);
			break;
		case 2:
			panels [0].gameObject.SetActive (false);
			panels [1].gameObject.SetActive (true);
			break;
		case 3:
			panels [0].gameObject.SetActive (false);
			panels [4].gameObject.SetActive (true);
			break;
		}
	}

	public void SelecMano(int mano){
		Admin_level0.datos.mano = mano;
		Calibrar ();
	}

	public void Regresa(){

	}

	public void DecideTipoDeRutina(int rutina){
		Admin_level0.datos.rutina = rutina;
		switch (rutina) {
		case 0:
			panels [1].gameObject.SetActive (false);
			panels [4].gameObject.SetActive (true);
			break;
		case 1:
			panels [1].gameObject.SetActive (false);
			panels [3].gameObject.SetActive (true);
			break;
		}
	}

	public void IngresaInfoDeRutina(){
		if ((int.Parse (cantidadDeIngredientes.text) < 5) || (int.Parse (cantidadDeIngredientes.text) > 15)) {
			Advertencia.text = "El minimo de ingredientes es 5 y el maximo de 15 ...";
		} else {
			if ((int.Parse (cantidadDeRepeticiones.text) == 0) || (int.Parse (cantidadDeRepeticiones.text) >100)) {
				Advertencia.text = "El minimo de repeticiones es 1 y el maximo de 100 ...";
			} else {
				Admin_level0.datos.numeroDeRepeticiones = int.Parse (cantidadDeRepeticiones.text);
				Admin_level0.datos.numeroDeIngredientes = int.Parse (cantidadDeIngredientes.text);
				panels [3].gameObject.SetActive (false);
				panels [4].gameObject.SetActive (true);
			}
		}
	}

	public void LogOutPanel(){
		panels [6].gameObject.SetActive (true);
	}

	public void LogOutYes(){
		SceneManager.LoadScene (0);
	}

	public void LogOutNo(){
		panels [6].gameObject.SetActive (false);
	}

	public void Calibrar(){
		if (Admin_level0.datos.mano == 1 || Admin_level0.datos.mano == 0) {
			SceneManager.LoadScene (2);
			Debug.Log ("Nivel =" + Admin_level0.datos.nivel + ", Mano (0-> Izq, 1-> Der) =" + Admin_level0.datos.mano);
		}
	}
}

