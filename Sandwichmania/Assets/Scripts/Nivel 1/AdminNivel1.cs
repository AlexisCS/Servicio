﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdminNivel1 : MonoBehaviour {

	//Evento
	public delegate void Audio ();
	public static event Audio MusicaAmbiente;
	public static event Audio AudioExito;

	public GameObject[] ingredientes;
	public GameObject[] interfaz;
	public AudioClip sonidoColision;
	public Text cantidadPanText;
	public Text cantidadJamonText;
	public Text cantidadQuesoText;
	public Text cantidadJitomateText;

	private GameObject _ingredienteClon;
	private AudioSource _audioSource;
	private int _mano, _seleccionPanel, _cantidad, _umbral;
	private float _tiempoInicio,  _tiempoUltimaActualizacion;
	private bool _panListo, _jamonListo, _quesoListo, _jitomateListo;

	enum ActivaPanelInteractivo {Bienvenido, Siguiente, Inicio, Juegue, ExitoParcial, SegundoInicio, Exito}
	ActivaPanelInteractivo PanelActivado;

	enum ActivaPanelDedos {SinSeleccion, Indice, Medio, Anular, Meñique}
	ActivaPanelDedos PanelDedosActivo;

	void OnEnable(){
		//suscribiendonos al evento
		DetectaColision.OnPanApilado += ActualizaCantidadPan;
		DetectaColision.OnJamonApilado += ActualizaCantidadJamon;
		DetectaColision.OnQuesoApilado += ActualizaCantidadQueso;
		DetectaColision.OnJitomateApilado += ActualizaCantidadJitomate;
	}

	void OnDisable(){
		//nos desuscribimos unicamente cuando este script se desactiva (al cambiar de escena, cerrar la app, etc)
		DetectaColision.OnPanApilado -= ActualizaCantidadPan;
		DetectaColision.OnJamonApilado -= ActualizaCantidadJamon;
		DetectaColision.OnQuesoApilado -= ActualizaCantidadQueso;
		DetectaColision.OnJitomateApilado -= ActualizaCantidadJitomate;
	}

	void Start(){
		
	}

	void Awake () {
		Admin_level0.datos = new InfoPartida ();
		PanelActivado = ActivaPanelInteractivo.Bienvenido;
		PanelDedosActivo = ActivaPanelDedos.SinSeleccion;
		interfaz [0].gameObject.SetActive (true);
		interfaz [1].gameObject.SetActive (true);
		if (interfaz [11].gameObject != null) {
			interfaz [11].gameObject.SetActive (false);
		}
		_audioSource = GetComponent<AudioSource> ();
		_mano = Admin_level0.datos.mano;
		_umbral = 10;
		_panListo = false;
		_jamonListo = true;
		_quesoListo = true;
		_jitomateListo = true;
		_cantidad = 0;
	}
		
	void ActualizaCantidadPan (){
		_audioSource.PlayOneShot (sonidoColision, 1.0F);
		_ingredienteClon = null;
		_cantidad += 1;
		cantidadPanText.text = "Pan:" + _cantidad;
		if (_cantidad == _umbral) {
			_cantidad = 0;
			_panListo = true;
			_jamonListo = false;
		}
	}

	void ActualizaCantidadJamon (){
		_audioSource.PlayOneShot (sonidoColision, 1.0F);
		_ingredienteClon = null;
		_cantidad += 1;
		cantidadJamonText.text = "Jamon:" + _cantidad;
		if (_cantidad == _umbral) {
			_cantidad = 0;
			_panListo = true;
			_jamonListo = true;
			_quesoListo = false;
		}
	}

	void ActualizaCantidadQueso (){
		_audioSource.PlayOneShot (sonidoColision, 1.0F);
		_ingredienteClon = null;
		_cantidad += 1;
		cantidadQuesoText.text = "Queso:" + _cantidad;
		if (_cantidad == _umbral) {
			_cantidad = 0;
			_panListo = true;
			_jamonListo = true;
			_quesoListo = true;
			_jitomateListo = false;
		}
	}

	void ActualizaCantidadJitomate (){
		_audioSource.PlayOneShot (sonidoColision, 1.0F);
		_ingredienteClon = null;
		_cantidad += 1;
		cantidadJitomateText.text = "Jitomate:" + _cantidad;
		if (_cantidad == _umbral) {
			_cantidad = 0;
			_panListo = true;
			_jamonListo = true;
			_quesoListo = true;
			_jitomateListo = true;
		}
	}

	void PanelDedos(int mano){
		switch (mano) {
		case 0:
			if (PanelDedosActivo == ActivaPanelDedos.Indice) {
				interfaz [6].gameObject.SetActive (true);
				interfaz [7].gameObject.SetActive (false);
				interfaz [8].gameObject.SetActive (false);
				interfaz [9].gameObject.SetActive (false);
			} else if (PanelDedosActivo == ActivaPanelDedos.Medio) {
				interfaz [6].gameObject.SetActive (false);
				interfaz [7].gameObject.SetActive (true);
				interfaz [8].gameObject.SetActive (false);
				interfaz [9].gameObject.SetActive (false);
			} else if (PanelDedosActivo == ActivaPanelDedos.Anular) {
				interfaz [6].gameObject.SetActive (false);
				interfaz [7].gameObject.SetActive (false);
				interfaz [8].gameObject.SetActive (true);
				interfaz [9].gameObject.SetActive (false);
			} else if (PanelDedosActivo == ActivaPanelDedos.Meñique) {
				interfaz [6].gameObject.SetActive (false);
				interfaz [7].gameObject.SetActive (false);
				interfaz [8].gameObject.SetActive (false);
				interfaz [9].gameObject.SetActive (true);
			} 
			break;
		case 1:
			if (PanelDedosActivo == ActivaPanelDedos.Indice) {
				interfaz [2].gameObject.SetActive (true);
				interfaz [3].gameObject.SetActive (false);
				interfaz [4].gameObject.SetActive (false);
				interfaz [5].gameObject.SetActive (false);
			} else if (PanelDedosActivo == ActivaPanelDedos.Medio) {
				interfaz [2].gameObject.SetActive (false);
				interfaz [3].gameObject.SetActive (true);
				interfaz [4].gameObject.SetActive (false);
				interfaz [5].gameObject.SetActive (false);
			} else if (PanelDedosActivo == ActivaPanelDedos.Anular) {
				interfaz [2].gameObject.SetActive (false);
				interfaz [3].gameObject.SetActive (false);
				interfaz [4].gameObject.SetActive (true);
				interfaz [5].gameObject.SetActive (false);
			} else if (PanelDedosActivo == ActivaPanelDedos.Meñique) {
				interfaz [2].gameObject.SetActive (false);
				interfaz [3].gameObject.SetActive (false);
				interfaz [4].gameObject.SetActive (false);
				interfaz [5].gameObject.SetActive (true);
			} 
			break;
		}
	}

	void DesactivaIngredientes(){
		interfaz [2].gameObject.SetActive (false);
		interfaz [3].gameObject.SetActive (false);
		interfaz [4].gameObject.SetActive (false);
		interfaz [5].gameObject.SetActive (false);
		interfaz [6].gameObject.SetActive (false);
		interfaz [7].gameObject.SetActive (false);
		interfaz [8].gameObject.SetActive (false);
		interfaz [9].gameObject.SetActive (false);
	}
		
	void PanelInteractivo (){ // Cambiar a verbo
		switch (PanelActivado) {
		case ActivaPanelInteractivo.Siguiente:
			interfaz [1].gameObject.SetActive (false);
			PanelDedosActivo = ActivaPanelDedos.Indice;
			PanelDedos (_mano);
			PanelActivado =  ActivaPanelInteractivo.Inicio;
			break;
		case ActivaPanelInteractivo.Inicio:
			DesactivaIngredientes ();
			PanelActivado = ActivaPanelInteractivo.Juegue;
			break;
		case ActivaPanelInteractivo.Exito:
			break;
		}
	}

	void Exito () {
		SceneManager.LoadScene (1);
	}


		
	void SpawnPan(){
		Vector3 PosicionPan = new Vector3 (6.6f, 7.6f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (ingredientes [0], PosicionPan, Quaternion.identity);
	}

	void SpawnJamon(){
		Vector3 PosicionJamon = new Vector3 (2.2f, 7.7f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (ingredientes [1], PosicionJamon, Quaternion.identity);
	}

	void SpawnQueso(){
		Vector3 PosicionQueso = new Vector3 (-2.0f, 7.9f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (ingredientes [2], PosicionQueso, Quaternion.identity);
	}

	void SpawnJitomate(){
		Vector3 PosicionJitomate = new Vector3 (-7.2f, 7.9f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (ingredientes [3], PosicionJitomate, Quaternion.identity);
	}
		

	public void BotonSi(){
		SceneManager.LoadScene (1);
	}
	
	public void BotonNo(){
		interfaz [11].gameObject.SetActive (false);
	}
	
	public void BotonRegresar(){
		interfaz [11].gameObject.SetActive (true);
	}
	
	
	// Update is called once per frame
	void Update () {
		if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow)
			|| Input.GetKeyDown(KeyCode.LeftArrow)) && (PanelActivado == ActivaPanelInteractivo.Bienvenido
			|| PanelActivado == ActivaPanelInteractivo.Siguiente)){

			if (PanelActivado == ActivaPanelInteractivo.Bienvenido) {
				PanelActivado = ActivaPanelInteractivo.Siguiente;
				PanelInteractivo ();
			}
		}



		if ((Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) 
			|| Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) && PanelActivado == ActivaPanelInteractivo.Exito) {
			Exito ();
		}

		if (Input.GetKeyDown(KeyCode.UpArrow) && _panListo == false && PanelActivado == ActivaPanelInteractivo.Juegue){
			SpawnPan ();
		} else if (Input.GetKeyDown (KeyCode.RightArrow) && _jamonListo == false) {
			SpawnJamon ();
		} else if (Input.GetKeyDown (KeyCode.DownArrow) && _quesoListo == false) {
			SpawnQueso ();		
		} else if (Input.GetKeyDown (KeyCode.LeftArrow) && _jitomateListo == false) {
			SpawnJitomate ();
		}
			
		if (Input.GetKeyUp (KeyCode.UpArrow) && _ingredienteClon != null && _panListo == false) {
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
		} else if (Input.GetKeyUp (KeyCode.RightArrow) && _ingredienteClon != null && _jamonListo == false) {
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
		} else if (Input.GetKeyUp (KeyCode.DownArrow) && _ingredienteClon != null && _quesoListo == false) { 
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
		} else if (Input.GetKeyUp (KeyCode.LeftArrow) && _ingredienteClon != null && _jitomateListo == false) {
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
		}
			
	}
}
	