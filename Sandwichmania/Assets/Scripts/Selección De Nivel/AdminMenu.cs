using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AdminMenu : MonoBehaviour {

	public static Nivel1 datosNivel1;
	public static Nivel2 datosNivel2;
	public static Nivel3 datosNivel3;

	public GameObject[] panels;
	public InputField cantidadDeRepeticiones, cantidadDeIngredientesNivel3, cantidadDeRepeticionesNivel3;
	public Text Advertencia, advertenciaNivel3, nombreDeUsuario;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Awake(){
		//ImprimeNombreUsuario ();
		datosNivel1 = new Nivel1 ();
		datosNivel2 = new Nivel2 ();
		datosNivel3 = new Nivel3 ();
		panels [0].gameObject.SetActive (true);
		panels [5].gameObject.SetActive (true);
	}

	public void SelecNivel(int nivel){
		switch (nivel) {
		case 1:
			datosNivel1.nivel = nivel;
			panels [0].gameObject.SetActive (false);
			panels [4].gameObject.SetActive (true);
			break;
		case 2:
		datosNivel2.nivel = nivel;
			panels [0].gameObject.SetActive (false);
			panels [1].gameObject.SetActive (true);
			break;
		case 3:
			datosNivel3.nivel = nivel;
			panels [0].gameObject.SetActive (false);
			panels [7].gameObject.SetActive (true);
			break;
		}
	}

	public void SelecMano(int mano){
		switch (mano) {
		case 0:
			if (datosNivel1.nivel == 1) {
				datosNivel1.ManoSeleccionada = Mano.Izquierda;
			}
			if (datosNivel2.nivel == 2) {
				datosNivel2.ManoSeleccionada = Mano.Izquierda;
			}
			if (datosNivel3.nivel == 3) {
				datosNivel3.ManoSeleccionada = Mano.Izquierda;
			}
			break;
		case 1:
			if (datosNivel1.nivel == 1) {
				datosNivel1.ManoSeleccionada = Mano.Derecha;
			}
			if (datosNivel2.nivel == 2) {
				datosNivel2.ManoSeleccionada = Mano.Derecha;
			}
			if (datosNivel3.nivel == 3) {
				datosNivel3.ManoSeleccionada = Mano.Derecha;
			}
			break;
		}
		Calibrar ();
	}



	public void DecideTipoDeRutina(int seleccion){
		switch (seleccion) {
		case 0:
			datosNivel2.RutinaSeleccionada = Rutina.SinRutina;
			panels [1].gameObject.SetActive (false);
			panels [3].gameObject.SetActive (true);
			break;
		case 1:
			datosNivel2.RutinaSeleccionada = Rutina.ConRutina;
			panels [1].gameObject.SetActive (false);
			panels [2].gameObject.SetActive (true);
			break;
		}
	}

	public void IngresaRutina(){
		panels [2].gameObject.SetActive (false);
		panels [4].gameObject.SetActive (true);
	}


	public void IngresaRepeticionesNivel2(){
		if (cantidadDeRepeticiones.text.Length.Equals (0)) {
			Advertencia.text = "El mínimo de repeticiones es 1 y el míximo de 100 ...";
			return;
		}

		int cantidadDeRepeticionesTemp=int.Parse (cantidadDeRepeticiones.text);
		if (cantidadDeRepeticionesTemp == 0 || cantidadDeRepeticionesTemp > 100) {
			Advertencia.text = "El mínimo de repeticiones es 1 y el máximo de 100 ...";
			return;
		} 
		datosNivel2.numeroDeRepeticiones = cantidadDeRepeticionesTemp;
		panels [8].gameObject.SetActive (false);
		panels [4].gameObject.SetActive (true);
	}

	public void EligeRutina(){
		panels [3].gameObject.SetActive (false);
		panels [8].gameObject.SetActive (true);
	}

	public void IngresaInfoDeRutinaNivel3(){
		if (cantidadDeIngredientesNivel3.text.Length.Equals (0)) {
			advertenciaNivel3.text = "El mínimo de ingredientes es 5 y el máximo de 15 ...";
			return;
		}
		if (cantidadDeRepeticionesNivel3.text.Length.Equals (0)) {
			advertenciaNivel3.text = "El mínimo de repeticiones es 1 y el míximo de 100 ...";
			return;
		}
		int cantidadDeIngredientesTemp=int.Parse (cantidadDeIngredientesNivel3.text);
		if (cantidadDeIngredientesTemp < 5 || cantidadDeIngredientesTemp > 15) {
			advertenciaNivel3.text = "El mínimo de ingredientes es 5 y el máximo de 15 ...";
			return;
		}
		int cantidadDeRepeticionesTemp=int.Parse (cantidadDeRepeticionesNivel3.text);
		if (cantidadDeRepeticionesTemp == 0 || cantidadDeRepeticionesTemp > 100) {
			advertenciaNivel3.text = "El mínimo de repeticiones es 1 y el máximo de 100 ...";
			return;
		}
		datosNivel3.numeroDeRepeticiones = cantidadDeRepeticionesTemp;
		datosNivel3.numeroDeIngredientes = cantidadDeIngredientesTemp;
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
			if (datosNivel1.nivel == 1) {
				panels [0].gameObject.SetActive (true);
				panels [4].gameObject.SetActive (false);
			} else if (datosNivel2.nivel == 2 && datosNivel2.RutinaSeleccionada == Rutina.ConRutina) {
				panels [2].gameObject.SetActive (true);
				panels [4].gameObject.SetActive (false);
			} else if (datosNivel2.nivel == 2 && datosNivel2.RutinaSeleccionada == Rutina.SinRutina) {
				panels [3].gameObject.SetActive (true);
				panels [4].gameObject.SetActive (false);
			} else if (datosNivel3.nivel == 3) {
				panels [0].gameObject.SetActive (true);
				panels [4].gameObject.SetActive (false);
			}
			break;
		case 4:
			panels [0].gameObject.SetActive (true);
			panels [7].gameObject.SetActive (false);
			break;
		case 5:
			panels [3].gameObject.SetActive (true);
			panels [8].gameObject.SetActive (false);
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
		if (datosNivel1.ManoSeleccionada == Mano.Derecha || datosNivel1.ManoSeleccionada == Mano.Izquierda ||
			datosNivel2.ManoSeleccionada == Mano.Derecha || datosNivel2.ManoSeleccionada == Mano.Izquierda ||
			datosNivel3.ManoSeleccionada == Mano.Derecha || datosNivel3.ManoSeleccionada == Mano.Izquierda) {
			SceneManager.LoadScene (2);
		}
	}

//	void ImprimeNombreUsuario(){
//		nombreDeUsuario.text = Admin_level0.datos.nombre.ToString ();
//		}
}

