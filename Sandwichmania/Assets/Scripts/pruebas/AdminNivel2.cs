using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminNivel2 : MonoBehaviour {


	public GameObject[] ingredientes;
	public GameObject[] interzas;

	private GameObject _ingredienteClon; 
	private GameObject[] _destruir;
	private int _contadorCapa, _sandwich, _instruccion;
	private bool _primerPan, _ultimoPan, _jamon, _queso, _jitomate;

	void Awake(){
		interzas [0].gameObject.SetActive (true);
		_contadorCapa = 0;
		_sandwich = 0;
		_primerPan = false;
		_jamon = true;
		_queso = true;
		_jitomate = true;
		_ultimoPan = true;
		_instruccion = 0;

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
		_ingredienteClon = null;
		if (_primerPan == false && _sandwich == 0) {
			Debug.Log ("Entre primer if");
			_primerPan = true;
			_jamon = false;
		}
		if (_ultimoPan == false && _sandwich == 0) {
			Debug.Log ("Entre segundo if");
			_primerPan = true;
			_jamon = true;
			_queso = true;
			_jitomate = true;
			_ultimoPan = true;
		}
	}

	void AgregaJamon(){
		_ingredienteClon = null;
		if (_sandwich == 0) {
			_primerPan = true;
			_jamon = true;
			_queso = false;
		}
	}

	void AgregaQueso(){
		_ingredienteClon = null;
		if (_sandwich == 0) {
			_primerPan = true;
			_jamon = true;
			_queso = true;
			_jitomate = false;
		}
	}

	void AgregaJitomate(){
		_ingredienteClon = null;
		if (_sandwich == 0) {
			_primerPan = true;
			_jamon = true;
			_queso = true;
			_jitomate = true;
			_ultimoPan = false;
		}
	}

	void ActualizaCapa(){
		_contadorCapa += 1;
		_ingredienteClon.gameObject.GetComponent<Renderer>().sortingOrder = _contadorCapa;
	}

	void PanelDeIntrucciones(){
		switch (_instruccion) {
		case 1:
			interzas [0].gameObject.SetActive (false);
			interzas [1].gameObject.SetActive (true);
			_instruccion = 2;
			break;
		case 2:
			interzas [1].gameObject.SetActive (false);
			_instruccion = 3;
			break;
		}
	}

	void LimpiaPlato(){
		_destruir = GameObject.FindGameObjectsWithTag ("Estatico");
		for (int i = 0; i <= _destruir.Length; i++) {
			Destroy (_destruir[i]);
		}
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


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) && (_instruccion == 0 || _instruccion == 1 || _instruccion == 2)){
			if (_instruccion == 0) {
				_instruccion = 1;
				PanelDeIntrucciones ();
			} else
				PanelDeIntrucciones ();
		}
			
		if (Input.GetKeyDown (KeyCode.UpArrow) && _primerPan == false && _instruccion == 3) {
			SpawnPan ();
		} else if (Input.GetKeyDown (KeyCode.RightArrow) && _jamon == false) {
			SpawnJamon ();
		} else if (Input.GetKeyDown (KeyCode.DownArrow) && _queso == false) {
			SpawnQueso ();		
		} else if (Input.GetKeyDown (KeyCode.LeftArrow) && _jitomate == false) {
			SpawnJitomate ();
		} else if (Input.GetKeyDown (KeyCode.UpArrow) && _ultimoPan == false) {
			SpawnPan ();
		}

		if (Input.GetKeyUp (KeyCode.UpArrow) && _ingredienteClon != null && _primerPan == false) {
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
		} else if (Input.GetKeyUp (KeyCode.UpArrow) && _ingredienteClon != null && _ultimoPan == false) {
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
		}
	}
}
