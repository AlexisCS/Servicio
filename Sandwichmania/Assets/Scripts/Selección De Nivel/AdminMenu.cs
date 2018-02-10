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
		

	public void SeleccionaNivel1Boton(){
		datosNivel1.nivel = 1;
		panels [0].gameObject.SetActive (false);
		panels [4].gameObject.SetActive (true);
	}

	public void SeleccionaNivel2Boton(){
		Admin_level0.datosNivel2.nivel = 2;
		panels [0].gameObject.SetActive (false);
		panels [1].gameObject.SetActive (true);
	}

	public void SeleccionaNivel3Boton(){
		if ((AdminNivel1.JugueNivel1 && (AdminNivel2.JugueNivel2ConRutina || AdminNivel2SinRutina.JugueNivel2SinRutina)) || InterfazClinica.EntreInterfazClinica) {
			datosNivel3.nivel = 3;
			panels [0].gameObject.SetActive (false);
			panels [7].gameObject.SetActive (true);
			return;
		}
		panels [12].gameObject.SetActive (true);
	}

	public void SeleccionaRutina(){
		if (Admin_level0.RutinaAsignada){
			rutinaSeleccionada = Rutina.ConRutina;
			panels [1].gameObject.SetActive (false);
			panels [4].gameObject.SetActive (true);
		} else {
			panels [11].gameObject.SetActive (true);
		}
	}

	public void SeleccionaSinRutina(){
		rutinaSeleccionada = Rutina.SinRutina;
		eligeRutinaToggle [0].isOn = false;
		eligeRutinaToggle [1].isOn = false;
		eligeRutinaToggle [2].isOn = false;
		eligeRutinaToggle [3].isOn = false;
		panels [1].gameObject.SetActive (false);
		panels [3].gameObject.SetActive (true);
	}

	public void RegresaSeleccionaNivel(){
		datosNivel1.nivel = 0;
		Admin_level0.datosNivel2.nivel = 0;
		datosNivel3.nivel = 0;
		panels [0].gameObject.SetActive (true);
		panels [1].gameObject.SetActive (false);
		panels [4].gameObject.SetActive (false);
		panels [7].gameObject.SetActive (false);
	}

	public void RegresaTipoDeRutinaDos(){
		panels [1].gameObject.SetActive (true);
		panels [3].gameObject.SetActive (false);
		panels [4].gameObject.SetActive (false);
	}

	public void RegresaEligeRutinaDos(){
		eligeRutinaToggle [0].isOn = false;
		eligeRutinaToggle [1].isOn = false;
		eligeRutinaToggle [2].isOn = false;
		eligeRutinaToggle [3].isOn = false;
		panels [3].gameObject.SetActive (true);
		panels [8].gameObject.SetActive (false);
	}

	public void RegresaRepeticionesNivel2(){
		cantidadDeRepeticiones.text = "";
		Advertencia.text = "";
		panels [8].gameObject.SetActive (true);
		panels [4].gameObject.SetActive (false);
	}

	public
	void RegresaInfoNivel3 (){
		cantidadDeRepeticionesNivel3.text = "";
		cantidadDeIngredientesNivel3.text = "";
		advertenciaNivel3.text = "";
		panels [7].gameObject.SetActive (true);
		panels [4].gameObject.SetActive (false);
	}

	public void SeleccionaManoIzquierda(){
		datosNivel1.ManoSeleccionada = Mano.Izquierda;
		Admin_level0.datosNivel2.ManoSeleccionada = Mano.Izquierda;
		datosNivel3.ManoSeleccionada = Mano.Izquierda;
		Calibrar ();
	}

	public void SeleccionaManoDerecha(){
		datosNivel1.ManoSeleccionada = Mano.Derecha;
		Admin_level0.datosNivel2.ManoSeleccionada = Mano.Derecha;
		datosNivel3.ManoSeleccionada = Mano.Derecha;
		Calibrar ();
	}

	public void RegresaDeSeleccionaMano(){
		if(datosNivel1.nivel == 1 && Admin_level0.datosNivel2.nivel == 0 && datosNivel3.nivel == 0){
			RegresaSeleccionaNivel ();
			return;
		}

		if (Admin_level0.datosNivel2.nivel == 2 && rutinaSeleccionada == Rutina.ConRutina) {
			RegresaTipoDeRutinaDos ();
			return;
		}

		if (Admin_level0.datosNivel2.nivel == 2 && rutinaSeleccionada == Rutina.SinRutina) {
			RegresaRepeticionesNivel2 ();
			return;
		}

		if(datosNivel1.nivel == 0 && Admin_level0.datosNivel2.nivel == 0 && datosNivel3.nivel == 3){
			RegresaInfoNivel3 ();
			return;
		}
	}


	public void Volver(){
		SceneManager.LoadScene (11);
	}


	public void DesaJugarRutinaAsignada(){
		Admin_level0.datosNivel2.nivel = 2;
		rutinaSeleccionada = Rutina.ConRutina;
		panels [0].gameObject.SetActive (false);
		panels [9].gameObject.SetActive (false);
		panels [4].gameObject.SetActive (true);
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
		
	public void IngresaRutinaNivel2(string seleccion){
		switch (seleccion) {
		case "Rutina 1":
			_rutina = new List<ActivaPanelDedos> {
				ActivaPanelDedos.Indice,
				ActivaPanelDedos.Medio,
				ActivaPanelDedos.Anular,
				ActivaPanelDedos.Meñique
			};
			break;
		case "Rutina 2":
			_rutina = new List<ActivaPanelDedos> {
				ActivaPanelDedos.Meñique,
				ActivaPanelDedos.Anular,
				ActivaPanelDedos.Medio,
				ActivaPanelDedos.Indice
			};
			break;
		case "Rutina 3":
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
		case "Rutina 4":
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
		panels [8].gameObject.SetActive (false);
		panels [4].gameObject.SetActive (true);
	}

	public void EligeRutina(){
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

	public void LogOutPanel(){
		panels [6].gameObject.SetActive (true);
	}

	public void LogOutYes(){
		SceneManager.LoadScene (1);
	}

	public void LogOutNo(){
		panels [6].gameObject.SetActive (false);
	}

	public void Calibrar(){
		SceneManager.LoadScene (2);
	}

	void ImprimeNombreUsuario(){
		nombreDeUsuario.text = Admin_level0.datos.nombre.ToString ();
	}
}

