using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdminNivel1 : MonoBehaviour {

	public GameObject[] Ingredientes;
	public GameObject[] Interfaz;
	public Text CantidadPanText;
	public Text CantidadJamonText;
	public Text CantidadQuesoText;
	public Text CantidadJitomateText;

	public int umbral;
	public int mano;

	private int bandera;
	private int Cantidad;
	private GameObject _ingredienteClon;
	private bool PanListo, JamonListo, QuesoListo, JitomateListo, ExitoBandera, Comienzo;

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

	void Awake () {
		if (Interfaz [11].gameObject != null) {
			Interfaz [11].gameObject.SetActive (false);
		}
		PanListo = false;
		JamonListo = true;
		QuesoListo = true;
		JitomateListo = true;
		ExitoBandera = false;
		Comienzo = true;
		Interfaz [0].gameObject.SetActive (true);
		Interfaz [1].gameObject.SetActive (true);
		bandera = 0;
		Cantidad = 0;
	}
		
	void ActualizaCantidadPan (){
		_ingredienteClon = null;
		Cantidad += 1;
		CantidadPanText.text = "Pan:" + Cantidad;
		if (Cantidad == umbral) {
			Cantidad = 0;
			PanListo = true;
			JamonListo = false;
			bandera = 3;
		}
		//Debug.Log ("Evento Activado");
	}

	void ActualizaCantidadJamon (){
	    _ingredienteClon = null;
		Cantidad += 1;
		CantidadJamonText.text = "Jamon:" + Cantidad;
		if (Cantidad == umbral) {
			Cantidad = 0;
			bandera = 5;
			PanListo = true;
			JamonListo = true;
			QuesoListo = false;
		}
	}

	void ActualizaCantidadQueso (){
		_ingredienteClon = null;
		Cantidad += 1;
		CantidadQuesoText.text = "Queso:" + Cantidad;
		if (Cantidad == umbral) {
			Cantidad = 0;
			PanListo = true;
			JamonListo = true;
			QuesoListo = true;
			JitomateListo = false;
			bandera = 7;
		}
	}

	void ActualizaCantidadJitomate (){
		_ingredienteClon = null;
		Cantidad += 1;
		CantidadJitomateText.text = "Jitomate:" + Cantidad;
		if (Cantidad == umbral) {
			Cantidad = 0;
			PanListo = true;
			JamonListo = true;
			QuesoListo = true;
			JitomateListo = true;
			Interfaz [10].SetActive (true);
			ExitoBandera = true;
		}
	}

	void Exito () {
		SceneManager.LoadScene (1);
	}

	void ActivaPanel(int mano){
		switch (this.mano) {
		case 0:
			if (bandera == 1) {
				Interfaz [6].gameObject.SetActive (true);
			} else if (bandera == 2) {
				Interfaz [6].gameObject.SetActive (false);
			} else if (bandera == 3) {
				Interfaz [7].gameObject.SetActive (true);
			} else if (bandera == 4) {
				Interfaz [7].gameObject.SetActive (false);
			} else if (bandera == 5) {
				Interfaz [8].gameObject.SetActive (true);
			} else if (bandera == 6) {
				Interfaz [8].gameObject.SetActive (false);
			} else if (bandera == 7) {
				Interfaz [9].gameObject.SetActive (true);
			} else if (bandera == 8) {
				Interfaz [9].gameObject.SetActive (false);
			} else if (bandera == 9) {
				Interfaz [1].gameObject.SetActive (false);
			}
			break; 
		case 1: 
			if (bandera == 1) {
				Interfaz [2].gameObject.SetActive (true);
			} else if (bandera == 2) {
				Interfaz [2].gameObject.SetActive (false);
			} else if (bandera == 3) {
				Interfaz [3].gameObject.SetActive (true);
			} else if (bandera == 4) {
				Interfaz [3].gameObject.SetActive (false);
			} else if (bandera == 5) {
				Interfaz [4].gameObject.SetActive (true);
			} else if (bandera == 6) {
				Interfaz [4].gameObject.SetActive (false);
			} else if (bandera == 7) {
				Interfaz [5].gameObject.SetActive (true);
			} else if (bandera == 8) {
				Interfaz [5].gameObject.SetActive (false);
			} else if (bandera == 9) {
				Interfaz [1].gameObject.SetActive (false);
			}
			break;
		}
		
	}
		
	void SpawnPan(){
		Vector3 PosicionPan = new Vector3 (6.6f, 7.6f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (Ingredientes [0], PosicionPan, Quaternion.identity);
	}

	void SpawnJamon(){
		Vector3 PosicionJamon = new Vector3 (2.2f, 7.7f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (Ingredientes [1], PosicionJamon, Quaternion.identity);
	}

	void SpawnQueso(){
		Vector3 PosicionQueso = new Vector3 (-2.0f, 7.9f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (Ingredientes [2], PosicionQueso, Quaternion.identity);
	}

	void SpawnJitomate(){
		Vector3 PosicionJitomate = new Vector3 (-7.2f, 7.9f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (Ingredientes [3], PosicionJitomate, Quaternion.identity);
	}
		

	public void BotonSi(){
		SceneManager.LoadScene (1);
	}
	
	public void BotonNo(){
		Interfaz [11].gameObject.SetActive (false);
	}
	
	public void BotonRegresar(){
		Interfaz [11].gameObject.SetActive (true);
	}
	
	
	// Update is called once per frame
	void Update () {
		if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) && Comienzo == true && bandera == 0){
			Comienzo = false;
			bandera = 9;
		}
		if (bandera == 1 || bandera == 2 || bandera == 3 || bandera == 4 || bandera == 5  || bandera == 6 || bandera == 7 || bandera == 8 || bandera == 9) {
			ActivaPanel (mano);
		}
		if ((Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) && ExitoBandera == true) {
			Exito ();
		}

		if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) && Comienzo == false){
			Comienzo = true;
			bandera = 1;
		} else if (Input.GetKeyDown(KeyCode.UpArrow) && PanListo == false && Comienzo == true) {
			bandera = 2;
			SpawnPan ();
		} else if (Input.GetKeyDown (KeyCode.RightArrow) && JamonListo == false) {
			bandera = 4;
			SpawnJamon ();
		} else if (Input.GetKeyDown (KeyCode.DownArrow) && QuesoListo == false) {
			bandera = 6;
			SpawnQueso ();		
		} else if (Input.GetKeyDown (KeyCode.LeftArrow) && JitomateListo == false) {
			bandera = 8;
			SpawnJitomate ();
		}
			
		if (Input.GetKeyUp (KeyCode.UpArrow) && _ingredienteClon != null && PanListo == false) {
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
			Debug.Log ("Debo de destruir");
		} else if (Input.GetKeyUp (KeyCode.RightArrow) && _ingredienteClon != null && JamonListo == false) {
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
			Debug.Log ("Debo de destruir");
		} else if (Input.GetKeyUp (KeyCode.DownArrow) && _ingredienteClon != null && QuesoListo == false) { 
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
			Debug.Log ("Debo de destruir");
		} else if (Input.GetKeyUp (KeyCode.LeftArrow) && _ingredienteClon != null && JitomateListo == false) {
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
			Debug.Log ("Debo de destruir");
		}
			
	}
}

/* Indice -> Pan
 * Jamon -> Medio
 * Queso -> Anular
 * Jitomate -> Meñique*/