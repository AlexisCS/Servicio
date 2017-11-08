using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Collections;
using UnityEngine.SceneManagement;

public class InterfazMedico : MonoBehaviour {

	public GameObject[] interfaz;
	public InputField nombreDeRutina, descripcionDeRutina;
	public InputField muestraRutina;
	public Text advertencia;

	private int count = 0;
	private List<ActivaPanelDedos> _rutina = new List<ActivaPanelDedos> ();
	enum FlechasTeclado {Ninguna, Cualquiera, Arriba, Derecha, Abajo, Izquierda}
	FlechasTeclado ElementoRutina;

	ActivaPanelDedos Rutina;

	public void SeleccionaCategoria (int categoria){
		switch (categoria) {
		case 0: // Crear Rutina
			nombreDeRutina.text = "";
			descripcionDeRutina.text = "";
			advertencia.text = "";
			interfaz [0].gameObject.SetActive (false);
			interfaz [1].gameObject.SetActive (true);
			break;
		case 1: // Asignar Rutina
			interfaz [0].gameObject.SetActive (false);
			interfaz[4].gameObject.SetActive (true);
			break;
		case 2: // Ver Resultados
			//SceneManager.LoadScene (); // Escena Resultados
			break;
		}
	}

	public void CancelarBoton (int seleccion){
		switch (seleccion) {
		case 0:
			interfaz [0].gameObject.SetActive (true);
			interfaz [1].gameObject.SetActive (false);
			break;
		case 1:
			interfaz [0].gameObject.SetActive (true);
			interfaz [3].gameObject.SetActive (false);
			break;
		case 2:
			interfaz [0].gameObject.SetActive (true);
			interfaz[4].gameObject.SetActive (false);
			break;
		}
	}

	public void ContinuarRutina(){
		if (nombreDeRutina.text.Length.Equals (0)) {
			advertencia.text = "Por favor, ingrese el nombre de la rutina";
		} else {
			advertencia.text = "";
			interfaz [2].gameObject.SetActive (true);
		}
	}

	public void GuardarRutinaBoton(){
		if (muestraRutina.text.Length.Equals (0)) {
			interfaz [5].gameObject.SetActive (true);
		} else {
			interfaz [6].gameObject.SetActive (true);
		}
	}

	public void AsignaRutinaBoton(){
		interfaz [7].gameObject.SetActive (true);
	}

	public void BotonSiContinuarRutina(){
		interfaz [1].gameObject.SetActive (false);
		interfaz [2].gameObject.SetActive (false);
		interfaz [3].gameObject.SetActive (true);
		ElementoRutina = FlechasTeclado.Cualquiera;
	}

	public void BotonNoContinuarRutina(){
		interfaz [2].gameObject.SetActive (false);
	}

	public void BotonSiGuardaRutina(){
		interfaz [8].gameObject.SetActive (true);
		interfaz [6].gameObject.SetActive (false);
	}

	public void BotonNoGuardaRutina(){
		interfaz [6].gameObject.SetActive (false);
	}

	public void BotonOk(int seccion){
		switch (seccion) {
		case 0:
			interfaz [5].gameObject.SetActive (false);
			break;
		case 1:
			interfaz [0].gameObject.SetActive (true);
			interfaz [3].gameObject.SetActive (false);
			interfaz [8].gameObject.SetActive (false);
			break;
		case 2:
			interfaz [0].gameObject.SetActive (true);
			interfaz [4].gameObject.SetActive (false);
			interfaz [9].gameObject.SetActive (false);
			break;
		}
	}

	public void BotonSiAsignaRutina(){
		interfaz [9].gameObject.SetActive (true);
	}

	public void BotonNoAsignaRutina(){
		interfaz [7].gameObject.SetActive (false);
	}

	// Use this for initialization
	void Start () {
	}

	void ImprimeRutina(){
		muestraRutina.text = nombreDeRutina.text+": {";
		for (int i = 0; i <= _rutina.Count - 1; i++) {
			muestraRutina.text+= _rutina[i] + ",";
		}
		muestraRutina.text+= "}";
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow) && ElementoRutina == FlechasTeclado.Cualquiera) {
			_rutina.Add ( ActivaPanelDedos.Indice);
			ImprimeRutina ();
		}

		if (Input.GetKeyUp (KeyCode.RightArrow) &&ElementoRutina == FlechasTeclado.Cualquiera) {
			_rutina.Add ( ActivaPanelDedos.Medio);
			ImprimeRutina ();
		}

		if (Input.GetKeyUp (KeyCode.DownArrow) && ElementoRutina == FlechasTeclado.Cualquiera) {
			_rutina.Add ( ActivaPanelDedos.Anular);
			ImprimeRutina ();
		}

		if (Input.GetKeyUp (KeyCode.LeftArrow) &&  ElementoRutina == FlechasTeclado.Cualquiera) {
			_rutina.Add ( ActivaPanelDedos.Meñique);
			ImprimeRutina ();
		}
	}
}
