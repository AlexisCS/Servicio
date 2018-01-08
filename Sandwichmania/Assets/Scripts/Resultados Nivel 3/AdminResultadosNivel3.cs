using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class AdminResultadosNivel3 : MonoBehaviour {

	public Text porcentajeDeErrorPan, porcentajeDeErrorJamon, porcentajeDeErrorQueso, porcentajeDeErrorJitomate;
	public Text numeroDeIngredientes, numeroDeRepeticiones;

	public void BotonSiguiente(){
		SceneManager.LoadScene (1);
	}

	private int _cantidadDePan, _cantidadDeJamon, _cantidadDeQueso, _cantidadDeJitomate; 
	private int _erroresPan, _erroresJamon, _erroresQueso, _erroresJitomate;
	private int _repeticiones;

	void Awake(){
		_repeticiones = AdminMenu.datosNivel3.numeroDeRepeticiones;
		_cantidadDePan = 0;
		_cantidadDeJamon = 0;
		_cantidadDeQueso = 0;
		_cantidadDeJitomate = 0;
		_erroresPan = 0;
		_erroresJamon = 0;
		_erroresQueso = 0;
		_erroresJitomate = 0;
		CuentaIngredientesAleatorios ();
		CuentaErrores ();
		EntregaInformacion ();
		EntregaPorcentajeError ();
		RedondeoTiempos ();
		Admin_level0.datos.HistorialPartidasNivel3.Add (AdminMenu.datosNivel3);
		GameSaveLoad.Save (Admin_level0.datos);
		StartCoroutine (GameSaveLoad.IfNewUploadXMLToServer ());
	}

	// Use this for initialization
	void Start () {

	}

	void RedondeoTiempos(){
		for (int i = 0; i <= AdminMenu.datosNivel3.tiempos.Count - 1; i++) {
			AdminMenu.datosNivel3.tiempos[i] = DelimitaSegundos (AdminMenu.datosNivel3.tiempos[i]);
		}
	}

	float DelimitaSegundos(float segundos){
		float segundosConUnDecimal;
		segundosConUnDecimal = Mathf.Round (segundos * 10f) / 10f;
		return segundosConUnDecimal;
	}

	void CuentaIngredientesAleatorios(){
		for (int i = 0; i <= AdminNivel3.IngredientesAleatorios.Count - 1; i++) {
			switch (AdminNivel3.IngredientesAleatorios [i]) {
			case ActivaPanelDedos.Indice:
				_cantidadDePan += 1;
				break;
			case ActivaPanelDedos.Medio:
				_cantidadDeJamon += 1;
				break;
			case ActivaPanelDedos.Anular:
				_cantidadDeQueso += 1;
				break;
			case ActivaPanelDedos.Meñique:
				_cantidadDeJitomate	+= 1;				
				break;
			}
		}
		_cantidadDePan = _cantidadDePan * _repeticiones;
		_cantidadDeJamon = _cantidadDeJamon * _repeticiones;
		_cantidadDeQueso = _cantidadDeQueso * _repeticiones;
		_cantidadDeJitomate = _cantidadDeJitomate * _repeticiones;
	}

	float CalculaPorcentajeError(int cantidadDeIngrediente, int errores){
		float porcentajeDeError;
		porcentajeDeError = (((cantidadDeIngrediente - errores)*1f) / cantidadDeIngrediente)*100;
		porcentajeDeError = 100 - porcentajeDeError;
		return Mathf.Round (porcentajeDeError);
	}

	void CuentaErrores(){
		for (int i = 0; i < AdminNivel3.GuardaResultados.Count; i++) {
			List <ActivaPanelDedos> temp = AdminNivel3.GuardaResultados[i];
			for (int j = 0; j < temp.Count; j++) {
				switch (temp[j]) {
				case ActivaPanelDedos.Indice:
					_erroresPan += 1;
					break;
				case ActivaPanelDedos.Medio:
					_erroresJamon += 1;
					break;
				case ActivaPanelDedos.Anular:
					_erroresQueso += 1;
					break;
				case ActivaPanelDedos.Meñique:
					_erroresJitomate += 1;			
					break;
				}
			}
		}
	}

	void EntregaPorcentajeError(){
		float porcentajeTemp;
		if (_cantidadDePan == 0) {
			AdminMenu.datosNivel3.porcentajDeErrorPan = -1;
			porcentajeDeErrorPan.text = "Pan\n<Indice>\n\nNo generado";
		} else {
			porcentajeTemp = CalculaPorcentajeError (_cantidadDePan, _erroresPan);
			AdminMenu.datosNivel3.porcentajDeErrorPan = porcentajeTemp;
			porcentajeDeErrorPan.text = "Pan\n<Indice>\n\n" + porcentajeTemp + "%";
		}

		if (_cantidadDeJamon == 0) {
			AdminMenu.datosNivel3.porcentajDeErrorJamon = -1;
			porcentajeDeErrorJamon.text = "Jamon\n<Medio>\n\nNo generado";
		} else {
			porcentajeTemp = CalculaPorcentajeError (_cantidadDeJamon, _erroresJamon);
			AdminMenu.datosNivel3.porcentajDeErrorJamon = porcentajeTemp;
			porcentajeDeErrorJamon.text = "Jamon\n<Medio>\n\n" + porcentajeTemp + "%";
		}

		if (_cantidadDeQueso == 0) {
			AdminMenu.datosNivel3.porcentajDeErrorQueso = -1;
			porcentajeDeErrorQueso.text = "Queso\n<Anular>\n\nNo generado";
		} else {
			porcentajeTemp = CalculaPorcentajeError (_cantidadDeQueso, _erroresQueso);
			AdminMenu.datosNivel3.porcentajDeErrorQueso = porcentajeTemp;
			porcentajeDeErrorQueso.text = "Queso\n<Anular>\n\n" + porcentajeTemp + "%";
		}

		if (_cantidadDeJitomate == 0) {
			AdminMenu.datosNivel3.porcentajDeErrorJitomate = -1;
			porcentajeDeErrorJitomate.text = "Jitomate\n<Meñique>\n\nNo generado";
		} else {
			porcentajeTemp = CalculaPorcentajeError (_cantidadDeJitomate, _erroresJitomate);
			AdminMenu.datosNivel3.porcentajDeErrorJitomate = porcentajeTemp;
			porcentajeDeErrorJitomate.text = "Jitomate\n<Meñique>\n\n" + porcentajeTemp + "%";
		}
	}

	void EntregaInformacion(){
		numeroDeRepeticiones.text = "No. Repeticiones:\n" + AdminMenu.datosNivel3.numeroDeRepeticiones;
		numeroDeIngredientes.text = "No. Ingredientes:\n" + AdminMenu.datosNivel3.numeroDeIngredientes;
	}


}
