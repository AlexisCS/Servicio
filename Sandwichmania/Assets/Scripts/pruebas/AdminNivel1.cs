using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdminNivel1 : MonoBehaviour {

	//Mensaje de finalizar: Lo que sea, boton escena regresa al menu
	//
	public GameObject[] Ingredientes;
	public GameObject[] Interfaz;
	public Text CantidadPanText;
	public Text CantidadJamonText;
	public Text CantidadQuesoText;
	public Text CantidadJitomateText;

	public int umbral;
	public int mano;

	private int Cantidad;
	private GameObject _ingredienteClon;
	private bool PanListo, JamonListo, QuesoListo, JitomateListo, ExitoBandera;

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
		PanListo = false;
		JamonListo = true;
		QuesoListo = true;
		JitomateListo = true;
		ExitoBandera = false;
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
		}
		//Debug.Log ("Evento Activado");
	}

	void ActualizaCantidadJamon (){
		_ingredienteClon = null;
		Cantidad += 1;
		CantidadJamonText.text = "Jamon:" + Cantidad;
		if (Cantidad == umbral) {
			Cantidad = 0;
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
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow) && PanListo == false){
			SpawnPan ();
		} else if (Input.GetKeyDown (KeyCode.RightArrow) && JamonListo == false) {
			SpawnJamon ();
		} else if (Input.GetKeyDown (KeyCode.DownArrow) && QuesoListo == false ) {
			SpawnQueso ();		
		} else if (Input.GetKeyDown (KeyCode.LeftArrow) && JitomateListo == false) {
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

		if ((Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) && ExitoBandera == true) {
			Exito ();
		}
			
	}
}

/* Indice -> Pan
 * Jamon -> Medio
 * Queso -> Anular
 * Jitomate -> Meñique*/