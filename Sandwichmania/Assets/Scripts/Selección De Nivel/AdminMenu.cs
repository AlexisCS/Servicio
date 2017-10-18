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
		panels [0].gameObject.SetActive (true);
		panels [5].gameObject.SetActive (true);
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
			panels [7].gameObject.SetActive (true);
			break;
		}
	}

	public void SelecMano(int mano){
		Admin_level0.datos.mano = mano;
		Calibrar ();
	}
		

	public void DecideTipoDeRutina(int rutina){
		Admin_level0.datos.rutina = rutina;
		switch (rutina) {
		case 0:
			panels [1].gameObject.SetActive (false);
			panels [2].gameObject.SetActive (true);
			break;
		case 1:
			panels [1].gameObject.SetActive (false);
			panels [3].gameObject.SetActive (true);
			break;
		}
	}

	public void IngresaRutina(){
		panels [2].gameObject.SetActive (false);
		panels [4].gameObject.SetActive (true);
	}


	public void IngresaInfoDeRutina(){
		if (cantidadDeIngredientes.text.Length.Equals (0)) {
			Advertencia.text = "El mínimo de ingredientes es 5 y el máximo de 15 ...";
			return;
		}
		if (cantidadDeRepeticiones.text.Length.Equals (0)) {
			Advertencia.text = "El mínimo de repeticiones es 1 y el míximo de 100 ...";
			return;
		}
		int cantidadDeIngredientesTemp=int.Parse (cantidadDeIngredientes.text);
		if (cantidadDeIngredientesTemp < 5 || cantidadDeIngredientesTemp > 15) {
			Advertencia.text = "El mínimo de ingredientes es 5 y el máximo de 15 ...";
			return;
		}
		int cantidadDeRepeticionesTemp=int.Parse (cantidadDeRepeticiones.text);
		if (cantidadDeRepeticionesTemp == 0 || cantidadDeRepeticionesTemp > 100) {
			Advertencia.text = "El mínimo de repeticiones es 1 y el máximo de 100 ...";
			return;
		} 
		Admin_level0.datos.numeroDeRepeticiones = cantidadDeRepeticionesTemp;
		Admin_level0.datos.numeroDeIngredientes = cantidadDeIngredientesTemp;
		panels [3].gameObject.SetActive (false);
		panels [4].gameObject.SetActive (true);
	}

	public void IngresaInfoDeRutinaNivel3(){
		if (cantidadDeIngredientes.text.Length.Equals (0)) {
			Advertencia.text = "El mínimo de ingredientes es 5 y el máximo de 15 ...";
			return;
		}
		if (cantidadDeRepeticiones.text.Length.Equals (0)) {
			Advertencia.text = "El mínimo de repeticiones es 1 y el míximo de 100 ...";
			return;
		}
		int cantidadDeIngredientesTemp=int.Parse (cantidadDeIngredientes.text);
		if (cantidadDeIngredientesTemp < 5 || cantidadDeIngredientesTemp > 15) {
			Advertencia.text = "El mínimo de ingredientes es 5 y el máximo de 15 ...";
			return;
		}
		int cantidadDeRepeticionesTemp=int.Parse (cantidadDeRepeticiones.text);
		if (cantidadDeRepeticionesTemp == 0 || cantidadDeRepeticionesTemp > 100) {
			Advertencia.text = "El mínimo de repeticiones es 1 y el máximo de 100 ...";
			return;
		} 
		Admin_level0.datos.numeroDeRepeticionesNivel3 = cantidadDeRepeticionesTemp;
		Admin_level0.datos.numeroDeIngredientesNivel3 = cantidadDeIngredientesTemp;
		panels [7].gameObject.SetActive (false);
		panels [4].gameObject.SetActive (true);
	}

	public void RegresaBoton (int seccion) {
		switch (seccion) {
		case 0:
			panels [0].gameObject.SetActive (true);
			panels [1].gameObject.SetActive (false);
			break;
		case 1:
			panels [1].gameObject.SetActive (true);
			panels [2].gameObject.SetActive (false);
			break;
		case 2:
			panels [1].gameObject.SetActive (true);
			panels [3].gameObject.SetActive (false);
			break;
		case 3:
			if (Admin_level0.datos.nivel == 1) {
				panels [0].gameObject.SetActive (true);
				panels [4].gameObject.SetActive (false);
			} else if (Admin_level0.datos.nivel == 2 && Admin_level0.datos.rutina == 0) {
				panels [2].gameObject.SetActive (true);
				panels [4].gameObject.SetActive (false);
			} else if (Admin_level0.datos.nivel == 2 && Admin_level0.datos.rutina == 1) {
				panels [3].gameObject.SetActive (true);
				panels [4].gameObject.SetActive (false);
			} else if (Admin_level0.datos.nivel == 3) {
				panels [0].gameObject.SetActive (true);
				panels [4].gameObject.SetActive (false);
			}
			break;
		case 4:
			panels [0].gameObject.SetActive (true);
			panels [7].gameObject.SetActive (false);
			break;
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

