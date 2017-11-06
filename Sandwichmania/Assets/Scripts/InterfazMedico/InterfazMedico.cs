using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterfazMedico : MonoBehaviour {

	public GameObject[] interfaz;
	public InputField nombreDeRutina, descripcionDeRutina;
	public InputField muestraRutina;
	public Text advertencia;

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
			interfaz [1].gameObject.SetActive (false);
			interfaz [3].gameObject.SetActive (true);
		}
	}

	public void GuardarRutinaBoton(){
		if (muestraRutina.text.Length.Equals (0)) {
		}
	}

	public void AsignaRutinaBoton(){
		
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
