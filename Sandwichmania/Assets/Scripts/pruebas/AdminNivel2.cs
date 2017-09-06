using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminNivel2 : MonoBehaviour {


	public GameObject[] ingredientes;
	public GameObject[] interzas;

	public int mano;

	private GameObject _ingredienteClon; 
	private GameObject[] _destruir;
	private int _contadorCapa, _sandwich, _activaPanel;
	private bool _primerPan, _ultimoPan, _jamon, _queso, _jitomate;

//	enum PanelIngredientes{Pan, Jitomate, Jamon};
//	PanelIngredientes

	enum ActivaPanelInteractivo {Bienvenido, Siguiente, Inicio, Juegue, ExitoPrimerSandwich, SegundoInicio, Exito}
	ActivaPanelInteractivo PanelActivado;

	enum ActivaPanelDedos {SinSeleccion, Indice, Medio, Anular, Meñique}
	ActivaPanelDedos PanelDedosActivo;


	void Awake(){
		interzas [0].gameObject.SetActive (true);
		_contadorCapa = 0;
		_sandwich = 0;
		_primerPan = false;
		_jamon = true;
		_queso = true;
		_jitomate = true;
		_ultimoPan = true;
		_activaPanel = 0;
		PanelActivado = ActivaPanelInteractivo.Bienvenido;
		PanelDedosActivo = ActivaPanelDedos.SinSeleccion;

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
		Debug.Log (_sandwich);
		_ingredienteClon = null;
		if (_primerPan == false && _sandwich == 0) {
			Debug.Log ("Entre primer if");
			PanelDedosActivo = ActivaPanelDedos.Medio;
			_primerPan = true;
			_jamon = false;
			PanelDedos (mano);
		}

		if (_ultimoPan == false && _sandwich == 0) {
			Debug.Log ("Entre tercer if");
			_activaPanel = 8;
			_primerPan = true;
			_jamon = true;
			_queso = true;
			_jitomate = true;
			_ultimoPan = true;
			PanelDedos (mano);
			PanelInteractivo ();
		}

//		if (_primerPan == false && _sandwich == 1) {
//			Debug.Log ("Entre segundo if");
//			_activaPanel = 4;
//			_primerPan = true;
//			_jitomate = false;
//			SegundoPanelDedos (mano);
//		}
//
//		if (_ultimoPan == false && _sandwich == 1 && _activaPanel == 10) {
//			Debug.Log ("Entre tercer if");
//			_activaPanel = 11;
//			_primerPan = true;
//			_jamon = true;
//			_queso = false;
//			_jitomate = true;
//			_ultimoPan = true;
//			SegundoPanelDedos (mano);
//		}
//
//		if (_ultimoPan == false && _sandwich == 1 && _activaPanel == 14) {
//			_activaPanel = 15;
//			_primerPan = true;
//			_jamon = true;
//			_queso = true;
//			_jitomate = true;
//			_ultimoPan = true;
//			SegundoPanelDedos (mano);
//			PanelInteractivo ();
//		}
	}

	void AgregaJamon(){
		Debug.Log (_sandwich);
		_ingredienteClon = null;
		if (_sandwich == 0) {
			PanelDedosActivo = ActivaPanelDedos.Anular;
			_primerPan = true;
			_jamon = true;
			_queso = false;
			PanelDedos (mano);
		}
			
//		if (_sandwich == 1 && _activaPanel == 5) {
//			_activaPanel = 6;
//			_primerPan = true;
//			_jamon = true;
//			_queso = false;	
//			_jitomate = true;
//			SegundoPanelDedos (mano);
//		}
//
//		if (_sandwich == 1 && _activaPanel == 7) {
//			_activaPanel = 10;
//			_primerPan = true;
//			_jamon = true;
//			_queso = true;	
//			_jitomate = true;
//			_ultimoPan = false;
//			SegundoPanelDedos (mano);
//		}
//
//		if (_sandwich == 1 && _activaPanel == 7) {
//			_activaPanel = 10;
//			_primerPan = true;
//			_jamon = true;
//			_queso = true;	
//			_jitomate = true;
//			_ultimoPan = false;
//			SegundoPanelDedos (mano);
//		}
//
//		if (_sandwich == 1 && _activaPanel == 13) {
//			_activaPanel = 14;
//			_primerPan = true;
//			_jamon = true;
//			_queso = true;	
//			_jitomate = true;
//			_ultimoPan = false;
//			SegundoPanelDedos (mano);
//		}
	}

	void AgregaQueso(){
		Debug.Log (_sandwich);
		_ingredienteClon = null;
		if (_sandwich == 0) {
			PanelDedosActivo = ActivaPanelDedos.Meñique;
			_primerPan = true;
			_jamon = true;
			_queso = true;
			_jitomate = false;
			PanelDedos (mano);
		}

//		if (_sandwich == 1 && _activaPanel == 6) {
//			_activaPanel = 7;
//			_primerPan = true;
//			_jamon = false;
//			_queso = true;	
//			_jitomate = true;
//			SegundoPanelDedos (mano);
//		}
//
//		if (_sandwich == 1 && _activaPanel == 11) {
//			_activaPanel = 12;
//			_primerPan = true;
//			_jamon = true;
//			_queso = true;	
//			_jitomate = false;
//			SegundoPanelDedos (mano);
//		}

	}

	void AgregaJitomate(){
		Debug.Log (_sandwich);
		_ingredienteClon = null;
		if (_sandwich == 0) {
			_activaPanel = 7;
			_primerPan = true;
			_jamon = true;
			_queso = true;
			_jitomate = true;
			_ultimoPan = false;
			PanelDedos (mano);
		}

//		if (_sandwich == 1 && _activaPanel == 4) {
//			_activaPanel = 5;
//			_primerPan = true;
//			_jamon = false;
//			//_queso = true;	
//			_jitomate = true;
//			SegundoPanelDedos (mano);
//		}
//
//		if (_sandwich == 1 && _activaPanel == 12) {
//			_activaPanel = 13;
//			_primerPan = true;
//			_jamon = false;
//			_queso = true;	
//			_jitomate = true;
//			SegundoPanelDedos (mano);
//		}
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
			} else if (PanelDedosActivo == ActivaPanelDedos.Medio) {
				interzas [6].gameObject.SetActive (false);
				interzas [7].gameObject.SetActive (true);
			} else if (PanelDedosActivo == ActivaPanelDedos.Anular) {
				interzas [7].gameObject.SetActive (false);
				interzas [8].gameObject.SetActive (true);
			} else if (PanelDedosActivo == ActivaPanelDedos.Meñique) {
				interzas [8].gameObject.SetActive (false);
				interzas [9].gameObject.SetActive (true);
			} /*else if (_activaPanel == 7) {
				interzas [9].gameObject.SetActive (false);
				interzas [6].gameObject.SetActive (true);
			} else if (_activaPanel == 8) {
				interzas[6].gameObject.SetActive (false);	
			}*/
			break;
		case 1:
			if (_activaPanel == 3) {
				interzas [2].gameObject.SetActive (true);
			} else if (_activaPanel == 4) {
				interzas [2].gameObject.SetActive (false);
				interzas [3].gameObject.SetActive (true);
			} else if (_activaPanel == 5) {
				interzas [3].gameObject.SetActive (false);
				interzas [4].gameObject.SetActive (true);
			} else if (_activaPanel == 6) {
				interzas [4].gameObject.SetActive (false);
				interzas [5].gameObject.SetActive (true);
			} else if (_activaPanel == 7) {
				interzas [5].gameObject.SetActive (false);
				interzas [2].gameObject.SetActive (true);
			} else if (_activaPanel == 8) {
				interzas[2].gameObject.SetActive (false);	
			}
			break;

		}
	
	}

//	void SegundoPanelDedos(int mano){
//		switch (mano) {
//		case 0:
//			if (_activaPanel == 3) {
//				interzas [6].gameObject.SetActive (true);
//			} else if (_activaPanel == 4) {
//				interzas [6].gameObject.SetActive (false);
//				interzas [9].gameObject.SetActive (true);
//			} else if (_activaPanel == 5) {
//				interzas [9].gameObject.SetActive (false);
//				interzas [7].gameObject.SetActive (true);
//			} else if (_activaPanel == 6) {
//				interzas [7].gameObject.SetActive (false);
//				interzas [8].gameObject.SetActive (true);
//			} else if (_activaPanel == 7) {
//				interzas [8].gameObject.SetActive (false);
//				interzas [7].gameObject.SetActive (true);
//			} else if (_activaPanel == 10) {
//				interzas[7].gameObject.SetActive (false);
//				interzas[6].gameObject.SetActive (true);
//			} else if (_activaPanel == 11) {
//				interzas[6].gameObject.SetActive (false);
//				interzas[8].gameObject.SetActive (true);
//			} else if (_activaPanel == 12) {
//				interzas[8].gameObject.SetActive (false);
//				interzas[9].gameObject.SetActive (true);
//			} else if (_activaPanel == 13) {
//				interzas[9].gameObject.SetActive (false);
//				interzas[7].gameObject.SetActive (true);
//			} else if (_activaPanel == 14) {
//				interzas[7].gameObject.SetActive (false);
//				interzas[6].gameObject.SetActive (true);
//			}
//			break;
//		case 1:
//			if (_activaPanel == 3) {
//				interzas [2].gameObject.SetActive (true);
//			} else if (_activaPanel == 4) {
//				interzas [2].gameObject.SetActive (false);
//				interzas [3].gameObject.SetActive (true);
//			} else if (_activaPanel == 5) {
//				interzas [3].gameObject.SetActive (false);
//				interzas [4].gameObject.SetActive (true);
//			} else if (_activaPanel == 6) {
//				interzas [4].gameObject.SetActive (false);
//				interzas [5].gameObject.SetActive (true);
//			} else if (_activaPanel == 7) {
//				interzas [5].gameObject.SetActive (false);
//				interzas [2].gameObject.SetActive (true);
//			} else if (_activaPanel == 8) {
//				interzas[2].gameObject.SetActive (false);	
//			}
//			break;
//
//		}
//
//	}

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
			PanelDedos (mano);
			break;
		/*case 8:
			interzas [6].gameObject.SetActive (false);
			interzas [2].gameObject.SetActive (false);	
			interzas [10].gameObject.SetActive (true);
			break;
		case 9:
			interzas [10].gameObject.SetActive (false);
			break;
		case 15: 
			interzas [6].gameObject.SetActive (false);
			interzas [2].gameObject.SetActive (false);	
			interzas [11].gameObject.SetActive (true);
			break;*/
		}
	}


	void Reinicio(){
		_primerPan = false;
		_jamon = true;
		_queso = true;
		_jitomate = true;
		_ultimoPan = true;
//		_sandwich = 1;
//		_activaPanel = 3;
		_destruir = GameObject.FindGameObjectsWithTag ("Estatico");
		for (int i = 0; i <= _destruir.Length - 1; i++) {
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
		if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) && (PanelActivado == ActivaPanelInteractivo.Bienvenido || PanelActivado == ActivaPanelInteractivo.Siguiente || PanelActivado == ActivaPanelInteractivo.Inicio || _activaPanel == 8 || _activaPanel == 9 || _activaPanel == 15)){
			if (PanelActivado == ActivaPanelInteractivo.Bienvenido) {
				PanelActivado = ActivaPanelInteractivo.Siguiente;
				PanelInteractivo ();
			} else {
				PanelInteractivo ();
			}

			if (_activaPanel == 8 && _sandwich == 0) {
				_activaPanel = 9;
				PanelDedos (mano);
				PanelInteractivo ();
				Reinicio ();
//				SegundoPanelDedos (mano);
			}

			if (_activaPanel == 14) {
				PanelInteractivo ();
			}
		}
			
		if (Input.GetKeyDown (KeyCode.UpArrow) && _primerPan == false && PanelActivado == ActivaPanelInteractivo.Juegue) {
			//Debug.Log (_activaPanel);
			SpawnPan ();
		} else if (Input.GetKeyDown (KeyCode.RightArrow) && _jamon == false) {
			//Debug.Log (_activaPanel);
			SpawnJamon ();
		} else if (Input.GetKeyDown (KeyCode.DownArrow) && _queso == false) {
			//Debug.Log (_activaPanel);
			SpawnQueso ();		
		} else if (Input.GetKeyDown (KeyCode.LeftArrow) && _jitomate == false) {
			//Debug.Log (_activaPanel);
			SpawnJitomate ();
		} else if (Input.GetKeyDown (KeyCode.UpArrow) && _ultimoPan == false) {
			//(Debug.Log (_activaPanel);
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
