using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminNivel3 : MonoBehaviour {

	public GameObject[] interfaz;
	public GameObject[] ingredientes;

	public int _numeroDeIngredientes, _numeroDeRepeticiones;
	public float tiempoDePausaEntreIngredientes;

	private GameObject _ingredienteClon; 
	private GameObject[] _destruir;
	private int _contadorCapa, _contadorIngredientesDeUsuario, _limite, _aciertos, _errores;
	private float _altura; 
	private List <Transform> _posicionDeIngredientesClon; 
	private List <ActivaPanelDedos> _guardaAciertos, _guardaErrores; 
	private List <ActivaPanelDedos> _ingredientesDeUsuario, _ingredientesAleatorios;
	private Dictionary<int, List<ActivaPanelDedos>> _informacionPartida;
	private bool _pan, _jamon, _queso, _jitomate;

	enum ActivaPanelDedos {SinSeleccion, Indice, Medio, Anular, Meñique}
	ActivaPanelDedos PanelDedosActivo;


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
		DetectaColision.OnJitomateApilado -= AgregaJitomate;
	}

	void Awake(){
		_ingredientesDeUsuario = new List<ActivaPanelDedos> ();
		_ingredientesAleatorios = new List<ActivaPanelDedos> ();
		_informacionPartida = new Dictionary<int, List<ActivaPanelDedos>> ();
		_guardaErrores = new List<ActivaPanelDedos> ();
		_posicionDeIngredientesClon = new List<Transform> ();
		GeneraSecuenciaAleatoria ();
		StartCoroutine (GeneraSandwich ());
		_contadorCapa = 1;
		_pan = true;
		_jamon = true;
		_queso = true;
		_jitomate = true;
		_aciertos = 0;
		_errores = 0;
		_contadorIngredientesDeUsuario = 0;
		_limite = 1;
		_altura = 2f;
	}
		

	void AgregaPan(){
		_ingredienteClon = null;
		_contadorIngredientesDeUsuario += 1;
		_ingredientesDeUsuario.Add (ActivaPanelDedos.Indice);
		if (_contadorIngredientesDeUsuario == _numeroDeIngredientes) {
			_pan = false;
			_jamon = false;
			_queso = false;
			_jitomate = false;
		}
	}

	void AgregaJamon(){
		_ingredienteClon = null;
		_contadorIngredientesDeUsuario += 1;
		_ingredientesDeUsuario.Add (ActivaPanelDedos.Medio);
		if (_contadorIngredientesDeUsuario == _numeroDeIngredientes) {
			_pan = false;
			_jamon = false;
			_queso = false;
			_jitomate = false;
		}
	}

	void AgregaQueso(){
		_ingredienteClon = null;
		_contadorIngredientesDeUsuario += 1;
		_ingredientesDeUsuario.Add (ActivaPanelDedos.Anular);
		if (_contadorIngredientesDeUsuario == _numeroDeIngredientes) {
			_pan = false;
			_jamon = false;
			_queso = false;
			_jitomate = false;
		}
	}

	void AgregaJitomate(){
		_ingredienteClon = null;
		_contadorIngredientesDeUsuario += 1;
		_ingredientesDeUsuario.Add (ActivaPanelDedos.Meñique);
		if (_contadorIngredientesDeUsuario == _numeroDeIngredientes) {
			_pan = false;
			_jamon = false;
			_queso = false;
			_jitomate = false;
		}
	}

	void ActualizaCapa(){
		_contadorCapa += 1;
		_ingredienteClon.gameObject.GetComponent<Renderer>().sortingOrder = _contadorCapa;
	}

	void SpawnPan(){
		Vector3 PosicionPan = new Vector3 (4.5f, 7.6f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (ingredientes [0], PosicionPan, Quaternion.identity);
		ActualizaCapa ();
	}
		
	void SpawnJamon(){
		Vector3 PosicionJamon = new Vector3 (4.5f, 7.7f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (ingredientes [1], PosicionJamon, Quaternion.identity);
		ActualizaCapa ();
	}

	void SpawnQueso(){
		Vector3 PosicionQueso = new Vector3 (4.5f, 7.9f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (ingredientes [2], PosicionQueso, Quaternion.identity);
		ActualizaCapa ();
	}

	void SpawnJitomate(){
		Vector3 PosicionJitomate = new Vector3 (4.4f, 7.9f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (ingredientes [3], PosicionJitomate, Quaternion.identity);
		ActualizaCapa ();
	}

	void SpawnPanAleatorio(){
		Vector3 PosicionPanAleatorio = new Vector3 (-4.5f, 7.9f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (ingredientes [4], PosicionPanAleatorio, Quaternion.identity);
		_posicionDeIngredientesClon.Add (_ingredienteClon.transform);
		ActualizaCapa ();
	}

	void SpawnJamonAleatorio(){
		Vector3 PosicionJamonAleatorio = new Vector3 (-4.5f, 7.9f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (ingredientes [5], PosicionJamonAleatorio, Quaternion.identity);
		_posicionDeIngredientesClon.Add (_ingredienteClon.transform);
		ActualizaCapa ();
	}

	void SpawnQuesoAleatorio(){
		Vector3 PosicionQuesoAleatorio = new Vector3 (-4.5f, 7.9f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (ingredientes [6], PosicionQuesoAleatorio, Quaternion.identity);
		_posicionDeIngredientesClon.Add (_ingredienteClon.transform);
		ActualizaCapa ();
	}

	void SpawnJitomateAleatorio(){
		Vector3 PosicionJitomateAleatorio = new Vector3 (-4.5f, 7.9f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (ingredientes [7], PosicionJitomateAleatorio, Quaternion.identity);
		_posicionDeIngredientesClon.Add (_ingredienteClon.transform);
		ActualizaCapa ();
	}

	void GeneraSecuenciaAleatoria(){
		int ingredienteAnterior = 0; 
		int numeroAleatorio = 0; 
		_ingredientesAleatorios.Add (SeleccionaIngredienteAleatorio (0));
		for (int i = 3; i <= _numeroDeIngredientes; i++) {
			if (i == 3 || i == _numeroDeIngredientes) {
				numeroAleatorio = Random.Range (1, 4);
				if (numeroAleatorio == ingredienteAnterior) {
					numeroAleatorio = (numeroAleatorio == ingredienteAnterior) ? (numeroAleatorio + 1) : numeroAleatorio;
				}
			} else {
				numeroAleatorio = Random.Range (0, 4);
				if (numeroAleatorio == ingredienteAnterior) {
					numeroAleatorio = (numeroAleatorio == 0) ? 1 : (numeroAleatorio-1);
				} 			
			}
			_ingredientesAleatorios.Add (SeleccionaIngredienteAleatorio (numeroAleatorio));
			ingredienteAnterior = numeroAleatorio;

		}
		_ingredientesAleatorios.Add (SeleccionaIngredienteAleatorio (0));
	}

	ActivaPanelDedos SeleccionaIngredienteAleatorio(int semilla){
		switch (semilla) {
		case 0:			
			return ActivaPanelDedos.Indice;
		case 1:			
			return ActivaPanelDedos.Medio;
		case 2:			
			return ActivaPanelDedos.Anular;
		case 3:
			return ActivaPanelDedos.Meñique;
		}
		return ActivaPanelDedos.SinSeleccion;
	}

	IEnumerator GeneraSandwich(){
		for (int i = 0; i <= _ingredientesAleatorios.Count - 1; i++) {
			switch (_ingredientesAleatorios [i]) {
			case ActivaPanelDedos.Indice:
				SpawnPanAleatorio ();
				break;
			case ActivaPanelDedos.Medio:
				SpawnJamonAleatorio ();
				break;
			case ActivaPanelDedos.Anular:
				SpawnQuesoAleatorio ();
				break;
			case ActivaPanelDedos.Meñique:
				SpawnJitomateAleatorio ();
				break;
			}
			yield return new WaitForSeconds (tiempoDePausaEntreIngredientes);
		}
	}
		
	void ComparaIngredientes(){
		for (int i = 0; i <= _ingredientesAleatorios.Count - 1; i++) {
			if (_ingredientesAleatorios [i] == _ingredientesDeUsuario [i]) {
				_aciertos += 1;
			} else {
				_guardaErrores.Add (_ingredientesAleatorios[i]);
				_errores += 1;
			}
		}

		for (int i = 0; i <= _guardaErrores.Count - 1; i++) {
			Debug.Log (_guardaErrores[i]);
		}

	}

	void InfoDePartida(int nivel){
		_informacionPartida.Add (nivel, _guardaErrores);
		//for (int i = 0; i <= _informacionPartida.Count; i++) {
		foreach(int val in _informacionPartida.Keys){
			List < ActivaPanelDedos > listTemp= _informacionPartida [val];
			for (int j = 0; j < listTemp.Count; j++){
				Debug.Log (listTemp[j].ToString ());
			}
		}


	}

	IEnumerator AnimacionSalto(){
		Vector3 temp;
		for ( int i = 0; i <= _ingredientesAleatorios.Count - 1; i++) {
			temp = _posicionDeIngredientesClon [i].transform.position;
			temp.y += (0.13f)*i;
			_posicionDeIngredientesClon [i].transform.position = temp;
			Debug.Log (_posicionDeIngredientesClon[i].transform.position);
		}

		yield return new WaitForSeconds (2.0f);

	}

	void Reinicio(){
		if (_limite < _numeroDeRepeticiones) {
			_ingredientesDeUsuario.Clear ();
			_pan = true;
			_jamon = true;
			_queso = true;
			_jitomate = true;
			_aciertos = 0;
			_errores = 0;
			_limite += 1;
			_contadorIngredientesDeUsuario = 0;
			_destruir = GameObject.FindGameObjectsWithTag ("Estatico");
			for (int i = 0; i <= _destruir.Length - 1; i++) {
				Destroy (_destruir [i]);
			}
		}
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			ComparaIngredientes ();
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			StartCoroutine (AnimacionSalto ());
		}

		if (Input.GetKeyDown (KeyCode.UpArrow) && _pan == true) {
			SpawnPan ();
		} else if (Input.GetKeyDown (KeyCode.RightArrow) && _jamon == true) {
			SpawnJamon ();
		} else if (Input.GetKeyDown (KeyCode.DownArrow) && _queso == true) {
			SpawnQueso ();		
		} else if (Input.GetKeyDown (KeyCode.LeftArrow) && _jitomate == true) {
			SpawnJitomate ();
		}

		if (Input.GetKeyUp (KeyCode.UpArrow) && _ingredienteClon != null && _pan == true) {;
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
		} else if (Input.GetKeyUp (KeyCode.RightArrow) && _ingredienteClon != null && _jamon == true) {
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
		} else if (Input.GetKeyUp (KeyCode.DownArrow) && _ingredienteClon != null && _queso == true) { 
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
		} else if (Input.GetKeyUp (KeyCode.LeftArrow) && _ingredienteClon != null && _jitomate == true) {
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
		} 
	}
}
