﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdminNivel2 : MonoBehaviour {

	public delegate void Audio ();
	public static event Audio AudioDeJuego;
	public static event Audio AudiodeExito;

	public GameObject[] ingredientes;
	public GameObject[] interzas;
	public AudioClip AudioColision;

	public int mano;

	private GameObject _ingredienteClon; 
	private GameObject[] _destruir;
	private AudioSource _audioSource;
	private int _contadorCapa, _sandwich;
	private bool _pan, _jamon, _queso, _jitomate;

	enum ActivaPanelInteractivo {Bienvenido, Siguiente, Inicio, Juegue, ExitoPrimerSandwich, SegundoInicio, Exito}
	ActivaPanelInteractivo PanelActivado;

	enum ActivaPanelDedos {SinSeleccion, Indice, Medio, Anular, Meñique}
	ActivaPanelDedos PanelDedosActivo;

	enum Ingredientes {SinIngredientes, PrimerIngrediente, SegundoIngrediente, TercerIngrediente}
	Ingredientes CantidadDeIngredientes;

	void Awake(){
		PanelActivado = ActivaPanelInteractivo.Bienvenido;
		PanelDedosActivo = ActivaPanelDedos.SinSeleccion;
		CantidadDeIngredientes = Ingredientes.SinIngredientes;
		interzas [0].gameObject.SetActive (true);
		interzas [12].gameObject.SetActive (false);
		_audioSource = GetComponent <AudioSource> ();
		_contadorCapa = 0;
		_sandwich = 0;
		_pan = false;
		_jamon = true;
		_queso = true;
		_jitomate = true;
	}
		
	void OnEnable(){
		DetectaColision.OnPanApilado += AgregaPan;
		DetectaColision.OnJamonApilado += AgregaJamon;
		DetectaColision.OnQuesoApilado += AgregaQueso;
		DetectaColision.OnJitomateApilado += AgregaJitomate;
	}

	void OnDisable(){
		DetectaColision.OnPanApilado -= AgregaPan;
		DetectaColision.OnJamonApilado -= AgregaJamon;
		DetectaColision.OnQuesoApilado -= AgregaQueso;
		DetectaColision.OnJitomateApilado += AgregaJitomate;
	}

	void AgregaPan(){
		_audioSource.PlayOneShot (AudioColision, 1.0f);
		_ingredienteClon = null;
		if (_pan == false && _sandwich == 0 && CantidadDeIngredientes == Ingredientes.PrimerIngrediente) {
			PanelDedosActivo = ActivaPanelDedos.Medio;
			_pan = true;
			_jamon = false;
			PanelDedos (mano);
		} else if (_pan == false && _sandwich == 0 && CantidadDeIngredientes == Ingredientes.SegundoIngrediente) {
			PanelActivado = ActivaPanelInteractivo.ExitoPrimerSandwich;
			_pan = true;
			_jamon = true;
			_queso = true;
			_jitomate = true;
			PanelInteractivo ();
		}

		if (_pan == false && _sandwich == 1 && CantidadDeIngredientes == Ingredientes.PrimerIngrediente) {
			PanelDedosActivo = ActivaPanelDedos.Meñique;
			_pan = true;
			_jitomate = false;
			PanelDedos (mano);
		} else if (_pan == false && _sandwich == 1 && CantidadDeIngredientes == Ingredientes.SegundoIngrediente) {
			PanelDedosActivo = ActivaPanelDedos.Anular;
			_pan = true;
			_jamon = true;
			_queso = false;
			_jitomate = true;
			PanelDedos (mano);
		} else if (_pan == false && _sandwich == 1 && CantidadDeIngredientes == Ingredientes.TercerIngrediente) {
			PanelActivado = ActivaPanelInteractivo.Exito;
			_pan = true;
			_jamon = true;
			_queso = true;
			_jitomate = true;
			PanelInteractivo ();
		}
	}

	void AgregaJamon(){
		_audioSource.PlayOneShot (AudioColision, 1.0f);
		_ingredienteClon = null;
		if (_sandwich == 0) {
			PanelDedosActivo = ActivaPanelDedos.Anular;
			_pan = true;
			_jamon = true;
			_queso = false;
			PanelDedos (mano);
		}
			
		if (_sandwich == 1 && CantidadDeIngredientes == Ingredientes.PrimerIngrediente) {
			PanelDedosActivo = ActivaPanelDedos.Anular;
			_pan = true;
			_jamon = true;
			_queso = false;	
			_jitomate = true;
			PanelDedos (mano);
		} else if (_sandwich == 1 && CantidadDeIngredientes == Ingredientes.SegundoIngrediente) {
			PanelDedosActivo = ActivaPanelDedos.Indice;
			_pan = false;
			_jamon = true;
			_queso = true;	
			_jitomate = true;
			PanelDedos (mano);
		} else if (_sandwich == 1 && CantidadDeIngredientes == Ingredientes.TercerIngrediente) {
			PanelDedosActivo = ActivaPanelDedos.Indice;
			_pan = false;
			_jamon = true;
			_queso = true;	
			_jitomate = true;
			PanelDedos (mano);
		}
	}

	void AgregaQueso(){
		_audioSource.PlayOneShot (AudioColision, 1.0f);
		_ingredienteClon = null;
		if (_sandwich == 0) {
			PanelDedosActivo = ActivaPanelDedos.Meñique;
			_pan = true;
			_jamon = true;
			_queso = true;
			_jitomate = false;
			PanelDedos (mano);
		}

		if (_sandwich == 1 && CantidadDeIngredientes == Ingredientes.PrimerIngrediente) {
			CantidadDeIngredientes = Ingredientes.SegundoIngrediente;
			PanelDedosActivo = ActivaPanelDedos.Medio;
			_pan = true;
			_jamon = false;
			_queso = true;	
			_jitomate = true;
			PanelDedos (mano);
		} else if (_sandwich == 1 && CantidadDeIngredientes == Ingredientes.SegundoIngrediente) {
			PanelDedosActivo = ActivaPanelDedos.Meñique;
			_pan = true;
			_jamon = true;
			_queso = true;	
			_jitomate = false;
			PanelDedos (mano);
		} 
			
	}

	void AgregaJitomate(){
		_audioSource.PlayOneShot (AudioColision, 1.0f);
		_ingredienteClon = null;
		if (_sandwich == 0) {
			CantidadDeIngredientes = Ingredientes.SegundoIngrediente;
			PanelDedosActivo = ActivaPanelDedos.Indice;
			_pan = false;
			_jamon = true;
			_queso = true;
			_jitomate = true;
			PanelDedos (mano);
		}

		if (_sandwich == 1 && CantidadDeIngredientes == Ingredientes.PrimerIngrediente) {
			PanelDedosActivo = ActivaPanelDedos.Medio;
			_pan = true;
			_jamon = false;
			_jitomate = true;
			PanelDedos (mano);
		} else if (_sandwich == 1 && CantidadDeIngredientes == Ingredientes.SegundoIngrediente) {
			CantidadDeIngredientes = Ingredientes.TercerIngrediente;
			PanelDedosActivo = ActivaPanelDedos.Medio;
			_pan = true;
			_jamon = false;
			_queso = true;
			_jitomate = true;
			PanelDedos (mano);
		}
	}

	void ActualizaCapa(){
		_contadorCapa += 1;
		_ingredienteClon.gameObject.GetComponent<Renderer>().sortingOrder = _contadorCapa;
	}

	void PanelDedos(int mano){
		switch (mano) {
		case 0:
			if (PanelDedosActivo == ActivaPanelDedos.Indice) {
				interzas [6].gameObject.SetActive (true);
				interzas [7].gameObject.SetActive (false);
				interzas [8].gameObject.SetActive (false);
				interzas [9].gameObject.SetActive (false);
			} else if (PanelDedosActivo == ActivaPanelDedos.Medio) {
				interzas [6].gameObject.SetActive (false);
				interzas [7].gameObject.SetActive (true);
				interzas [8].gameObject.SetActive (false);
				interzas [9].gameObject.SetActive (false);
			} else if (PanelDedosActivo == ActivaPanelDedos.Anular) {
				interzas [6].gameObject.SetActive (false);
				interzas [7].gameObject.SetActive (false);
				interzas [8].gameObject.SetActive (true);
				interzas [9].gameObject.SetActive (false);
			} else if (PanelDedosActivo == ActivaPanelDedos.Meñique) {
				interzas [6].gameObject.SetActive (false);
				interzas [7].gameObject.SetActive (false);
				interzas [8].gameObject.SetActive (false);
				interzas [9].gameObject.SetActive (true);
			} 
			break;
		case 1:
			if (PanelDedosActivo == ActivaPanelDedos.Indice) {
				interzas [2].gameObject.SetActive (true);
				interzas [3].gameObject.SetActive (false);
				interzas [4].gameObject.SetActive (false);
				interzas [5].gameObject.SetActive (false);
			} else if (PanelDedosActivo == ActivaPanelDedos.Medio) {
				interzas [2].gameObject.SetActive (false);
				interzas [3].gameObject.SetActive (true);
				interzas [4].gameObject.SetActive (false);
				interzas [5].gameObject.SetActive (false);
			} else if (PanelDedosActivo == ActivaPanelDedos.Anular) {
				interzas [2].gameObject.SetActive (false);
				interzas [3].gameObject.SetActive (false);
				interzas [4].gameObject.SetActive (true);
				interzas [5].gameObject.SetActive (false);
			} else if (PanelDedosActivo == ActivaPanelDedos.Meñique) {
				interzas [2].gameObject.SetActive (false);
				interzas [3].gameObject.SetActive (false);
				interzas [4].gameObject.SetActive (false);
				interzas [5].gameObject.SetActive (true);
			} 
			break;
		}
	}
		
	//Contiene los mensajes de instrucciones y de exito
	void PanelInteractivo (){ 
		switch (PanelActivado) {
		case ActivaPanelInteractivo.Siguiente:
			interzas [0].gameObject.SetActive (false);
			interzas [1].gameObject.SetActive (true);
			PanelActivado = ActivaPanelInteractivo.Inicio;
			break;
		case ActivaPanelInteractivo.Inicio:
			interzas [1].gameObject.SetActive (false);
			PanelActivado = ActivaPanelInteractivo.Juegue;
			PanelDedosActivo = ActivaPanelDedos.Indice;
			CantidadDeIngredientes = Ingredientes.PrimerIngrediente;
			PanelDedos (mano);
			break;
		case ActivaPanelInteractivo.ExitoPrimerSandwich:
			interzas [6].gameObject.SetActive (false);
			interzas [2].gameObject.SetActive (false);	
			interzas [10].gameObject.SetActive (true);
			break;
		case ActivaPanelInteractivo.SegundoInicio:
			interzas [10].gameObject.SetActive (false);
			PanelDedosActivo = ActivaPanelDedos.Indice;
			CantidadDeIngredientes = Ingredientes.PrimerIngrediente;
			PanelDedos (mano);
			break;
		case ActivaPanelInteractivo.Exito: 
			interzas [6].gameObject.SetActive (false);
			interzas [2].gameObject.SetActive (false);	
			interzas [11].gameObject.SetActive (true);
			break;
		}
	}


	void Reinicio(){
		_pan = false;
		_jamon = true;
		_queso = true;
		_jitomate = true;
		_sandwich = 1;
		PanelActivado = ActivaPanelInteractivo.Juegue; 
		_destruir = GameObject.FindGameObjectsWithTag ("Estatico");
		for (int i = 0; i <= _destruir.Length - 1; i++) {
			Destroy (_destruir[i]);
		}
	}

	public void BotonRegresar(){
		interzas [12].gameObject.SetActive (true);
	}

	public void BotonSi(){
		SceneManager.LoadScene (1);
	}

	public void BotonNo(){
		interzas [12].gameObject.SetActive (false);
	}

	void Exito(){
		SceneManager.LoadScene (1);
	}

	void SpawnPan(){
		Vector3 PosicionPan = new Vector3 (4.5f, 7.6f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (ingredientes [0], PosicionPan, Quaternion.identity);
		ActualizaCapa ();
	}

	void SpawnJamon(){
		Vector3 PosicionJamon = new Vector3 (4.5f, 7.6f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (ingredientes [1], PosicionJamon, Quaternion.identity);
		ActualizaCapa ();
	}

	void SpawnQueso(){
		Vector3 PosicionQueso = new Vector3 (4.5f, 7.6f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (ingredientes [2], PosicionQueso, Quaternion.identity);
		ActualizaCapa ();
	}

	void SpawnJitomate(){
		Vector3 PosicionJitomate = new Vector3 (4.4f, 7.6f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (ingredientes [3], PosicionJitomate, Quaternion.identity);
		ActualizaCapa ();
	}


	// Update is called once per frame
	void Update () {
		if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) && 
		   (PanelActivado == ActivaPanelInteractivo.Bienvenido || PanelActivado == ActivaPanelInteractivo.Siguiente || PanelActivado == ActivaPanelInteractivo.Inicio || 
			PanelActivado == ActivaPanelInteractivo.ExitoPrimerSandwich)){

			if (PanelActivado == ActivaPanelInteractivo.Bienvenido) {
				PanelActivado = ActivaPanelInteractivo.Siguiente;
				PanelInteractivo ();
			} else 
				PanelInteractivo ();
	
			if (PanelActivado == ActivaPanelInteractivo.ExitoPrimerSandwich && _sandwich == 0) {
				PanelActivado = ActivaPanelInteractivo.SegundoInicio;
				PanelInteractivo ();
				Reinicio ();
			}
		}

		if ((Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) 
			|| Input.GetKeyDown(KeyCode.LeftArrow)) && PanelActivado == ActivaPanelInteractivo.Exito) {
			Exito ();
		}
			
		if (Input.GetKeyDown (KeyCode.UpArrow) && _pan == false && PanelActivado == ActivaPanelInteractivo.Juegue) {
			SpawnPan ();
		} else if (Input.GetKeyDown (KeyCode.RightArrow) && _jamon == false) {
			SpawnJamon ();
		} else if (Input.GetKeyDown (KeyCode.DownArrow) && _queso == false) {
			SpawnQueso ();		
		} else if (Input.GetKeyDown (KeyCode.LeftArrow) && _jitomate == false) {
			SpawnJitomate ();
		}

		if (Input.GetKeyUp (KeyCode.UpArrow) && _ingredienteClon != null && _pan == false) {
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
		} else if (Input.GetKeyUp (KeyCode.RightArrow) && _ingredienteClon != null && _jamon == false) {
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
		} else if (Input.GetKeyUp (KeyCode.DownArrow) && _ingredienteClon != null && _queso == false) { 
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
		} else if (Input.GetKeyUp (KeyCode.LeftArrow) && _ingredienteClon != null && _jitomate == false) {
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
		} 
	}
}
