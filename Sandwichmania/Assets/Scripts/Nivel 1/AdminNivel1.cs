using System.Collections;
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

	public int umbral;

	private GameObject _ingredienteClon;
	private AudioSource _audioSource;
	private int _mano, _seleccionPanel, _cantidad;
	private bool _panListo, _jamonListo, _quesoListo, _jitomateListo, _exitoBandera, _comienzo;

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
		interfaz [0].gameObject.SetActive (true);
		interfaz [1].gameObject.SetActive (true);
		if (interfaz [11].gameObject != null) {
			interfaz [11].gameObject.SetActive (false);
		}
		_audioSource = GetComponent<AudioSource> ();
		_mano = Admin_level0.datos.mano;
		_panListo = false;
		_jamonListo = true;
		_quesoListo = true;
		_jitomateListo = true;
		_exitoBandera = false;
		_comienzo = true;
		_seleccionPanel = 0;
		_cantidad = 0;
	}
		
	void ActualizaCantidadPan (){
		_audioSource.PlayOneShot (sonidoColision, 1.0F);
		_ingredienteClon = null;
		_cantidad += 1;
		cantidadPanText.text = "Pan:" + _cantidad;
		if (_cantidad == umbral) {
			_cantidad = 0;
			_panListo = true;
			_jamonListo = false;
			_seleccionPanel = 3;
		}
		//Debug.Log ("Evento Activado");
	}

	void ActualizaCantidadJamon (){
		_audioSource.PlayOneShot (sonidoColision, 1.0F);
		_ingredienteClon = null;
		_cantidad += 1;
		cantidadJamonText.text = "Jamon:" + _cantidad;
		if (_cantidad == umbral) {
			_cantidad = 0;
			_seleccionPanel = 5;
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
		if (_cantidad == umbral) {
			_cantidad = 0;
			_panListo = true;
			_jamonListo = true;
			_quesoListo = true;
			_jitomateListo = false;
			_seleccionPanel = 7;
		}
	}

	void ActualizaCantidadJitomate (){
		_audioSource.PlayOneShot (sonidoColision, 1.0F);
		_ingredienteClon = null;
		_cantidad += 1;
		cantidadJitomateText.text = "Jitomate:" + _cantidad;
		if (_cantidad == umbral) {
			_cantidad = 0;
			_panListo = true;
			_jamonListo = true;
			_quesoListo = true;
			_jitomateListo = true;
			interfaz [10].SetActive (true);
			if (AudioExito != null) {
				AudioExito ();
			}
			_exitoBandera = true;
		}
	}

	void Exito () {
		SceneManager.LoadScene (1);
	}

	void ActivaPanel(int mano){
		switch (this._mano) {
		case 0:
			if (_seleccionPanel == 1) {
				interfaz [6].gameObject.SetActive (true);
			} else if (_seleccionPanel == 2) {
				interfaz [6].gameObject.SetActive (false);
			} else if (_seleccionPanel == 3) {
				interfaz [7].gameObject.SetActive (true);
			} else if (_seleccionPanel == 4) {
				interfaz [7].gameObject.SetActive (false);
			} else if (_seleccionPanel == 5) {
				interfaz [8].gameObject.SetActive (true);
			} else if (_seleccionPanel == 6) {
				interfaz [8].gameObject.SetActive (false);
			} else if (_seleccionPanel == 7) {
				interfaz [9].gameObject.SetActive (true);
			} else if (_seleccionPanel == 8) {
				interfaz [9].gameObject.SetActive (false);
			} else if (_seleccionPanel == 9) {
				interfaz [1].gameObject.SetActive (false);
			}
			break; 
		case 1: 
			if (_seleccionPanel == 1) {
				interfaz [2].gameObject.SetActive (true);
			} else if (_seleccionPanel == 2) {
				interfaz [2].gameObject.SetActive (false);
			} else if (_seleccionPanel == 3) {
				interfaz [3].gameObject.SetActive (true);
			} else if (_seleccionPanel == 4) {
				interfaz [3].gameObject.SetActive (false);
			} else if (_seleccionPanel == 5) {
				interfaz [4].gameObject.SetActive (true);
			} else if (_seleccionPanel == 6) {
				interfaz [4].gameObject.SetActive (false);
			} else if (_seleccionPanel == 7) {
				interfaz [5].gameObject.SetActive (true);
			} else if (_seleccionPanel == 8) {
				interfaz [5].gameObject.SetActive (false);
			} else if (_seleccionPanel == 9) {
				interfaz [1].gameObject.SetActive (false);
			}
			break;
		}
		
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
		if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) && _comienzo == true && _seleccionPanel == 0){
			if (MusicaAmbiente != null) {
				MusicaAmbiente ();
			}
			_comienzo = false;
			_seleccionPanel = 9;
		}
		if (_seleccionPanel == 1 || _seleccionPanel == 2 || _seleccionPanel == 3 || _seleccionPanel == 4 || _seleccionPanel == 5  || _seleccionPanel == 6 || _seleccionPanel == 7 || _seleccionPanel == 8 || _seleccionPanel == 9) {
			ActivaPanel (_mano);
		}
		if ((Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) && _exitoBandera == true) {
			Exito ();
		}

		if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) && _comienzo == false){
			_comienzo = true;
			_seleccionPanel = 1;
		} else if (Input.GetKeyDown(KeyCode.UpArrow) && _panListo == false && _comienzo == true) {
			_seleccionPanel = 2;
			SpawnPan ();
		} else if (Input.GetKeyDown (KeyCode.RightArrow) && _jamonListo == false) {
			_seleccionPanel = 4;
			SpawnJamon ();
		} else if (Input.GetKeyDown (KeyCode.DownArrow) && _quesoListo == false) {
			_seleccionPanel = 6;
			SpawnQueso ();		
		} else if (Input.GetKeyDown (KeyCode.LeftArrow) && _jitomateListo == false) {
			_seleccionPanel = 8;
			SpawnJitomate ();
		}
			
		if (Input.GetKeyUp (KeyCode.UpArrow) && _ingredienteClon != null && _panListo == false) {
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
			Debug.Log ("Debo de destruir");
		} else if (Input.GetKeyUp (KeyCode.RightArrow) && _ingredienteClon != null && _jamonListo == false) {
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
			Debug.Log ("Debo de destruir");
		} else if (Input.GetKeyUp (KeyCode.DownArrow) && _ingredienteClon != null && _quesoListo == false) { 
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
			Debug.Log ("Debo de destruir");
		} else if (Input.GetKeyUp (KeyCode.LeftArrow) && _ingredienteClon != null && _jitomateListo == false) {
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