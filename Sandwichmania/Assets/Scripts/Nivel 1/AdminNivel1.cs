﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdminNivel1 : MonoBehaviour {
	public delegate void Audio ();
	public static event Audio Colision;
	public static event Audio MusicaAmbiente;
	public static event Audio AudioExito;

	public GameObject[] ingredientes;
	public GameObject[] interfaz;
	public Text cantidadPanText;
	public Text cantidadJamonText;
	public Text cantidadQuesoText;
	public Text cantidadJitomateText;

	private static bool jugueNivel1;
	public static bool JugueNivel1 {
		get{ return jugueNivel1; }
	}

	private GameObject _ingredienteClon;
	private int __seleccionPanel, _cantidad, _umbral;
	Mano _mano;
	private float _tiempoDeApiladoPan, _tiempoDeApiladoJamon, _tiempoDeApiladoQueso, _tiempoDeApiladoJitomate;
	private bool _panListo, _jamonListo, _quesoListo, _jitomateListo, _iniciaCronometro;

	enum ActivaPanelInteractivo {Bienvenido, Siguiente, Inicio, Juegue, Exito}
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
		interfaz [0].gameObject.SetActive (true);
		interfaz [1].gameObject.SetActive (true);
		interfaz [11].gameObject.SetActive (false);
		PanelActivado = ActivaPanelInteractivo.Bienvenido;
		PanelDedosActivo = ActivaPanelDedos.SinSeleccion;
		//_mano = Mano.Derecha;
		_mano = AdminMenu.datosNivel1.ManoSeleccionada;
		_umbral = 10;
		_panListo = false;
		_jamonListo = true;
		_quesoListo = true;
		_jitomateListo = true;
		_iniciaCronometro = false;
		_cantidad = 0;
		_tiempoDeApiladoPan = 0;
		_tiempoDeApiladoJamon = 0;
		_tiempoDeApiladoQueso = 0;
		_tiempoDeApiladoJitomate = 0;
	}
		
	void ActualizaCantidadPan (){
		Colision ();
		_ingredienteClon = null;
		_cantidad += 1;
		cantidadPanText.text = "Pan:" + _cantidad;
		if (_cantidad == _umbral) {
			PanelDedosActivo = ActivaPanelDedos.Medio;
			MuestraPanelDedos (_mano);
			_cantidad = 0;
			_panListo = true;
			_jamonListo = false;
			_iniciaCronometro = false;
		}
	}

	void ActualizaCantidadJamon (){
		Colision ();
		_ingredienteClon = null;
		_cantidad += 1;
		cantidadJamonText.text = "Jamon:" + _cantidad;
		if (_cantidad == _umbral) {
			PanelDedosActivo = ActivaPanelDedos.Anular;
			MuestraPanelDedos (_mano);
			_cantidad = 0;
			_panListo = true;
			_jamonListo = true;
			_quesoListo = false;
			_iniciaCronometro = false;
		}
	}

	void ActualizaCantidadQueso (){
		Colision ();
		_ingredienteClon = null;
		_cantidad += 1;
		cantidadQuesoText.text = "Queso:" + _cantidad;
		if (_cantidad == _umbral) {
			PanelDedosActivo = ActivaPanelDedos.Meñique;
			MuestraPanelDedos (_mano);
			_cantidad = 0;
			_panListo = true;
			_jamonListo = true;
			_quesoListo = true;
			_jitomateListo = false;
			_iniciaCronometro = false;
		}
	}

	void ActualizaCantidadJitomate (){
		Colision ();
		_ingredienteClon = null;
		_cantidad += 1;
		cantidadJitomateText.text = "Jitomate:" + _cantidad;
		if (_cantidad == _umbral) {
			PanelActivado = ActivaPanelInteractivo.Exito;
			MuestraInstrucciones ();
			_cantidad = 0;
			_panListo = true;
			_jamonListo = true;
			_quesoListo = true;
			_jitomateListo = true;
			_iniciaCronometro = false;
		}
	}
		
	void MuestraPanelDedos(Mano seleccion){
		switch (seleccion) {
		case Mano.Izquierda:
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
		case Mano.Derecha:
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

		
	void MuestraInstrucciones (){ 
		switch (PanelActivado) {
		case ActivaPanelInteractivo.Siguiente:
			PanelDedosActivo = ActivaPanelDedos.Indice;
			MuestraPanelDedos (_mano);
			interfaz [1].gameObject.SetActive (false);
			break;
		case ActivaPanelInteractivo.Inicio:
			DesactivaIngredientes ();
			PanelActivado = ActivaPanelInteractivo.Juegue;
			break;
		case ActivaPanelInteractivo.Exito:
			if (AudioExito != null) {
				AudioExito ();
			}
			DesactivaIngredientes ();
			interfaz [10].gameObject.SetActive (true);
			jugueNivel1 = true;
			AdminMenu.datosNivel1.fecha = System.DateTime.Now.ToString ("yyyy-MM-dd HH:mm:ss");
			break;
		}
	}

	void Exito () {
		SceneManager.LoadScene (7);
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
	

	void Update () {
		if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow)
			|| Input.GetKeyDown(KeyCode.LeftArrow)) && PanelActivado == ActivaPanelInteractivo.Bienvenido && PanelDedosActivo == ActivaPanelDedos.SinSeleccion){
			PanelActivado = ActivaPanelInteractivo.Siguiente;
			MuestraInstrucciones ();
			return;
		}

		if (Input.GetKeyDown (KeyCode.UpArrow) && PanelActivado == ActivaPanelInteractivo.Siguiente) {
			if (MusicaAmbiente != null) {
				MusicaAmbiente ();
			}
			PanelActivado = ActivaPanelInteractivo.Inicio;
			MuestraInstrucciones ();
		}

		if ((Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) 
			|| Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) && PanelActivado == ActivaPanelInteractivo.Exito) {
			Exito ();
		}


		if (_panListo == false  && _iniciaCronometro == true) {
			_tiempoDeApiladoPan += Time.deltaTime;
			AdminMenu.datosNivel1.tiempoDedoIndice = _tiempoDeApiladoPan;
		}

		if (_jamonListo == false  && _iniciaCronometro == true) {
			_tiempoDeApiladoJamon += Time.deltaTime;
			AdminMenu.datosNivel1.tiempoDedoMedio = _tiempoDeApiladoJamon;
		}

		if (_quesoListo == false  && _iniciaCronometro == true) {
			_tiempoDeApiladoQueso += Time.deltaTime;
			AdminMenu.datosNivel1.tiempoDedoAnular = _tiempoDeApiladoQueso;
		}

		if (_jitomateListo == false  && _iniciaCronometro == true) {
			_tiempoDeApiladoJitomate += Time.deltaTime;
			AdminMenu.datosNivel1.tiempoDedoMeñique = _tiempoDeApiladoJitomate;
		}

		if (Input.GetKeyDown(KeyCode.UpArrow) && _panListo == false && PanelActivado == ActivaPanelInteractivo.Juegue){
			_iniciaCronometro = true;
			PanelActivado = ActivaPanelInteractivo.Inicio;
			MuestraInstrucciones ();
			SpawnPan ();
		} else if (Input.GetKeyDown (KeyCode.RightArrow) && _jamonListo == false) {
			_iniciaCronometro = true;
			PanelActivado = ActivaPanelInteractivo.Inicio;
			MuestraInstrucciones ();
			SpawnJamon ();
		} else if (Input.GetKeyDown (KeyCode.DownArrow) && _quesoListo == false) {
			_iniciaCronometro = true;
			PanelActivado = ActivaPanelInteractivo.Inicio;
			MuestraInstrucciones ();
			SpawnQueso ();		
		} else if (Input.GetKeyDown (KeyCode.LeftArrow) && _jitomateListo == false) {
			_iniciaCronometro = true;
			PanelActivado = ActivaPanelInteractivo.Inicio;
			MuestraInstrucciones ();
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
	