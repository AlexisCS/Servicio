using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminNivel2 : MonoBehaviour {

	public GameObject[] ingredientes;
	public GameObject[] interzas;

	private GameObject _ingredienteClon;
	private int _contadorCapa;

	void Awake(){
		_contadorCapa = 0;
	}

	void OnEnable(){
		DetectaColision.OnPanApilado += ActualizaCapa;
		DetectaColision.OnJamonApilado += ActualizaCapa;
		DetectaColision.OnQuesoApilado += ActualizaCapa;
		DetectaColision.OnJitomateApilado += ActualizaCapa;
	}

	void OnDisable(){
		DetectaColision.OnPanApilado -= ActualizaCapa;
		DetectaColision.OnJamonApilado -= ActualizaCapa;
		DetectaColision.OnQuesoApilado -= ActualizaCapa;
		DetectaColision.OnJitomateApilado -= ActualizaCapa;
	}

	void SpawnPan(){
		Vector3 PosicionPan = new Vector3 (4.5f, 7.6f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (ingredientes [0], PosicionPan, Quaternion.identity);
	}

	void SpawnJamon(){
		Vector3 PosicionJamon = new Vector3 (4.5f, 7.6f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (ingredientes [1], PosicionJamon, Quaternion.identity);
		// Se modifica el ingrediente clon
	}

	void SpawnQueso(){
		Vector3 PosicionQueso = new Vector3 (4.5f, 7.6f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (ingredientes [2], PosicionQueso, Quaternion.identity);
	}

	void SpawnJitomate(){
		Vector3 PosicionJitomate = new Vector3 (4.4f, 7.6f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (ingredientes [3], PosicionJitomate, Quaternion.identity);

	}

	void ActualizaCapa(){
		Debug.Log (_contadorCapa);
		_contadorCapa += 1;
		_ingredienteClon.gameObject.GetComponent<Renderer>().sortingOrder = _contadorCapa;
	}
		

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
		SpawnPan ();
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
		SpawnJamon ();
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
		SpawnQueso ();		
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
		SpawnJitomate ();
		}
	}
}
