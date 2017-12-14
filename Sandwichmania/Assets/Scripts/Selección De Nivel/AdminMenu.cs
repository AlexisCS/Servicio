using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public enum Rutina {SinRutina, ConRutina}

public class AdminMenu : MonoBehaviour {

	public static Nivel1 datosNivel1;
	public static Nivel3 datosNivel3;

	public GameObject[] panels;
	public Button volver, cerrarSesion;
	public InputField cantidadDeRepeticiones, cantidadDeIngredientesNivel3, cantidadDeRepeticionesNivel3;
	public Text Advertencia, advertenciaNivel3, nombreDeUsuario;
	public Text rutinaAsignada;
	public Toggle[] eligeRutinaToggle;

	private static Rutina rutinaSeleccionada;
	public static Rutina RutinaSeleccionada{
		get { return rutinaSeleccionada; }
	}


	private List<ActivaPanelDedos> _rutina;

	void Start () {
		if (InterfazClinica.EntreInterfazClinica) {
			volver.gameObject.SetActive (true);
			cerrarSesion.gameObject.SetActive (false);
		}
			
		if (Admin_level0.RutinaAsignada) {
			rutinaAsignada.text = "Tiene asignada la rutina: " + '"' + Admin_level0.datosNivel2.nombreDeRutina + '"' + ". ¿Desea jugarla?";
			panels [9].gameObject.SetActive (true);
		}

		if (!Admin_level0.RutinaAsignada) {
			panels [11].gameObject.SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void Awake(){
		ImprimeNombreUsuario ();
		_rutina = new List<ActivaPanelDedos> ();
		datosNivel1 = new Nivel1 ();
		datosNivel3 = new Nivel3 ();
		panels [0].gameObject.SetActive (true);
		panels [5].gameObject.SetActive (true);
		AdminMenu.datosNivel1.nivel = 0;
		Admin_level0.datosNivel2.nivel = 0;
		AdminMenu.datosNivel3.nivel = 0;
	}
		
	public void Volver(){
		SceneManager.LoadScene (11);
	}


	public void DesaJugarRutinaAsignada(){
		panels [0].gameObject.SetActive (false);
		panels [9].gameObject.SetActive (false);
		panels [4].gameObject.SetActive (true);
		Admin_level0.datosNivel2.nivel = 2;
		rutinaSeleccionada = Rutina.ConRutina;
	}

	public void NoJugarRutinaAsignada(){
		panels [9].gameObject.SetActive (false);
		panels [10].gameObject.SetActive (true);
	}

	public void QuitarUbicacionDeRutina(){
		panels [10].gameObject.SetActive (false);
	}

	public void OkBoton(){
		panels [11].gameObject.SetActive (false);
		panels [12].gameObject.SetActive (false);
	}

	public void SelecNivel(int nivel){
		switch (nivel) {
		case 1:
			datosNivel1.nivel = nivel;
			panels [0].gameObject.SetActive (false);
			panels [4].gameObject.SetActive (true);
			break;
		case 2:
			Admin_level0.datosNivel2.nivel = nivel;
			panels [0].gameObject.SetActive (false);
			panels [1].gameObject.SetActive (true);
			break;
		case 3:
			if ((AdminNivel1.JugueNivel1 && (AdminNivel2.JugueNivel2ConRutina || AdminNivel2SinRutina.JugueNivel2SinRutina)) || InterfazClinica.EntreInterfazClinica) {
				datosNivel3.nivel = nivel;
				panels [0].gameObject.SetActive (false);
				panels [7].gameObject.SetActive (true);
				return;
			}

			panels [12].gameObject.SetActive (true);
			break;
		}
	}

	public void SelecMano(int mano){
		switch (mano) {
		case 0:
			if (datosNivel1.nivel == 1) {
				datosNivel1.ManoSeleccionada = Mano.Izquierda;
			}
			if (Admin_level0.datosNivel2.nivel == 2) {
				Admin_level0.datosNivel2.ManoSeleccionada = Mano.Izquierda;
			}
			if (datosNivel3.nivel == 3) {
				datosNivel3.ManoSeleccionada = Mano.Izquierda;
			}
			break;
		case 1:
			if (datosNivel1.nivel == 1) {
				datosNivel1.ManoSeleccionada = Mano.Derecha;
			}
			if (Admin_level0.datosNivel2.nivel == 2) {
				Admin_level0.datosNivel2.ManoSeleccionada = Mano.Derecha;
				//datosNivel2.ManoSeleccionada = Mano.Derecha;
			}
			if (datosNivel3.nivel == 3) {
				datosNivel3.ManoSeleccionada = Mano.Derecha;
			}
			break;
		}
		Calibrar ();
	}



	public void DecideTipoDeRutina(int seleccion){
		if (seleccion == 0) {
			rutinaSeleccionada = Rutina.SinRutina;
			//Admin_level0.datosNivel2.RutinaSeleccionada = Rutina.SinRutina;
			//datosNivel2.RutinaSeleccionada = Rutina.SinRutina;
			eligeRutinaToggle [0].isOn = false;
			eligeRutinaToggle [1].isOn = false;
			eligeRutinaToggle [2].isOn = false;
			eligeRutinaToggle [3].isOn = false;
			panels [1].gameObject.SetActive (false);
			panels [3].gameObject.SetActive (true);
		}

		if (seleccion == 1 && Admin_level0.RutinaAsignada) {
			rutinaSeleccionada = Rutina.ConRutina;
			//datosNivel2.RutinaSeleccionada = Rutina.ConRutina;
			panels [1].gameObject.SetActive (false);
			panels [4].gameObject.SetActive (true);
			//panels [2].gameObject.SetActive (true);
		}

		if (seleccion == 1 && !Admin_level0.RutinaAsignada) {
			panels [11].gameObject.SetActive (true);
		}
//		switch (seleccion) {
//		case 0:
//			datosNivel2.RutinaSeleccionada = Rutina.SinRutina;
//			eligeRutinaToggle [0].isOn = false;
//			eligeRutinaToggle [1].isOn = false;
//			eligeRutinaToggle [2].isOn = false;
//			eligeRutinaToggle [3].isOn = false;
//			panels [1].gameObject.SetActive (false);
//			panels [3].gameObject.SetActive (true);
//			break;
//		case 1:
//			datosNivel2.RutinaSeleccionada = Rutina.ConRutina;
//			panels [1].gameObject.SetActive (false);
//			panels [2].gameObject.SetActive (true);
//			break;
//		}
	}

//	public void JugarBoton(){
//		panels [2].gameObject.SetActive (false);
//		panels [4].gameObject.SetActive (true);
//	}

	public void IngresaRutinaNivel2(int seleccion){
		switch (seleccion) {
		case 1:
			_rutina = new List<ActivaPanelDedos> {
				ActivaPanelDedos.Indice,
				ActivaPanelDedos.Medio,
				ActivaPanelDedos.Anular,
				ActivaPanelDedos.Meñique
			};
			break;
		case 2:
			_rutina = new List<ActivaPanelDedos> {
				ActivaPanelDedos.Meñique,
				ActivaPanelDedos.Anular,
				ActivaPanelDedos.Medio,
				ActivaPanelDedos.Indice
			};
			break;
		case 3:
			_rutina = new List<ActivaPanelDedos> {
				ActivaPanelDedos.Indice,
				ActivaPanelDedos.Medio,
				ActivaPanelDedos.Anular,
				ActivaPanelDedos.Meñique,
				ActivaPanelDedos.Anular,
				ActivaPanelDedos.Medio,
				ActivaPanelDedos.Indice
			};
			break;
		case 4:
			_rutina = new List<ActivaPanelDedos> {
				ActivaPanelDedos.Meñique,
				ActivaPanelDedos.Anular,
				ActivaPanelDedos.Medio,
				ActivaPanelDedos.Indice,
				ActivaPanelDedos.Medio,
				ActivaPanelDedos.Anular,
				ActivaPanelDedos.Meñique
			};
			break;
		}
		AdminNivel2SinRutina._secuenciaSinRutina = _rutina;
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
		AdminNivel2SinRutina._numeroDeRepeticiones = cantidadDeRepeticionesTemp;
		//datosNivel2.numeroDeRepeticiones = cantidadDeRepeticionesTemp;
		panels [8].gameObject.SetActive (false);
		panels [4].gameObject.SetActive (true);
	}

	public void EligeRutina(){
		if (eligeRutinaToggle [0].isOn == false || eligeRutinaToggle [1].isOn == false || eligeRutinaToggle [2].isOn == false || eligeRutinaToggle [3].isOn == false) {
		} 
		if (eligeRutinaToggle [0].isOn == true || eligeRutinaToggle [1].isOn == true || eligeRutinaToggle [2].isOn == true || eligeRutinaToggle [3].isOn == true) {
			panels [3].gameObject.SetActive (false);
			panels [8].gameObject.SetActive (true);
		}
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
			Admin_level0.datosNivel2.nivel = 0;
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
				datosNivel1.nivel = 0;
				panels [0].gameObject.SetActive (true);
				panels [4].gameObject.SetActive (false);
				return;
			} 

			if (Admin_level0.datosNivel2.nivel == 2 && rutinaSeleccionada == Rutina.ConRutina) {
				panels [1].gameObject.SetActive (true);
				panels [4].gameObject.SetActive (false);
				return;
			} 

			if (Admin_level0.datosNivel2.nivel == 2 && rutinaSeleccionada == Rutina.SinRutina) {
				panels [8].gameObject.SetActive (true);
				panels [4].gameObject.SetActive (false);
				return;
			} 

			if (datosNivel3.nivel == 3) {
				datosNivel3.nivel = 0;
				panels [7].gameObject.SetActive (true);
				panels [4].gameObject.SetActive (false);
				return;
			}
			break;
		case 4:
			panels [0].gameObject.SetActive (true);
			panels [7].gameObject.SetActive (false);
			break;
		case 5:
			eligeRutinaToggle [0].isOn = false;
			eligeRutinaToggle [1].isOn = false;
			eligeRutinaToggle [2].isOn = false;
			eligeRutinaToggle [3].isOn = false;
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
			Admin_level0.datosNivel2.ManoSeleccionada == Mano.Derecha || Admin_level0.datosNivel2.ManoSeleccionada == Mano.Izquierda ||
			datosNivel3.ManoSeleccionada == Mano.Derecha || datosNivel3.ManoSeleccionada == Mano.Izquierda) {
			SceneManager.LoadScene (2);
		}
	}

	void ImprimeNombreUsuario(){
		nombreDeUsuario.text = Admin_level0.datos.nombre.ToString ();
	}
}

