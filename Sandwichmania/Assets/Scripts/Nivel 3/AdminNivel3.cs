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
	private List <Transform> _posicionDeIngredientesClon; 
	private List <ActivaPanelDedos>[] _guardaErrores;
	private List <List<ActivaPanelDedos>> _guardaResultados;
	private List <ActivaPanelDedos> _ingredientesDeUsuario, _ingredientesAleatorios;
	private Dictionary<int, List<ActivaPanelDedos>> _informacionPartida;

	enum ActivaPanelDedos {SinSeleccion, Indice, Medio, Anular, Meñique}
	ActivaPanelDedos PanelDedosActivo;

	enum Teclado {SinPresionar, PresionaPan, PresionaJamon, PresionaQueso, PresionaJitomate, PresionaCualquiera}
	Teclado ActivaTecla;

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
		_posicionDeIngredientesClon = new List<Transform> ();
		_guardaErrores = new List<ActivaPanelDedos>[_numeroDeRepeticiones];
		for (int i = 0; i <= _numeroDeRepeticiones - 1; i++) {
			_guardaErrores[i] = new List<ActivaPanelDedos>();
		}
		_guardaResultados = new List<List<ActivaPanelDedos>> ();
		ActivaTecla = Teclado.PresionaCualquiera;
		GeneraSecuenciaAleatoria ();
		StartCoroutine (GeneraSandwich ());
		_contadorCapa = 1;
		_aciertos = 0;
		_errores = 0;
		_contadorIngredientesDeUsuario = 0;
		_limite = 0;
	}
		

	void AgregaPan(){
		_ingredienteClon = null;
		_contadorIngredientesDeUsuario += 1;
		_ingredientesDeUsuario.Add (ActivaPanelDedos.Indice);
		if (_contadorIngredientesDeUsuario == _numeroDeIngredientes) {
			ComparaIngredientes ();
		}
	}

	void AgregaJamon(){
		_ingredienteClon = null;
		_contadorIngredientesDeUsuario += 1;
		_ingredientesDeUsuario.Add (ActivaPanelDedos.Medio);
		if (_contadorIngredientesDeUsuario == _numeroDeIngredientes) {
			ComparaIngredientes ();
		}
	}

	void AgregaQueso(){
		_ingredienteClon = null;
		_contadorIngredientesDeUsuario += 1;
		_ingredientesDeUsuario.Add (ActivaPanelDedos.Anular);
		if (_contadorIngredientesDeUsuario == _numeroDeIngredientes) {
			ComparaIngredientes ();
		}
	}

	void AgregaJitomate(){
		_ingredienteClon = null;
		_contadorIngredientesDeUsuario += 1;
		_ingredientesDeUsuario.Add (ActivaPanelDedos.Meñique);
		if (_contadorIngredientesDeUsuario == _numeroDeIngredientes) {
			ComparaIngredientes ();
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
		yield return new WaitForSeconds (2.0f);
		//DesactivaTeclas ();
		StartCoroutine (AnimacionSalto ());
	}

	void DesactivaTeclas(Teclado tecla){
		switch (tecla) {
		case Teclado.PresionaPan:
			break;
		case Teclado.PresionaJamon:
			break;
		case Teclado.PresionaQueso:
			break;
		case Teclado.PresionaJitomate:
			break;
		}
	}

	void ActivaTeclas(){
		
	}
		
	void ComparaIngredientes(){
		for (int i = 0; i <= _ingredientesAleatorios.Count - 1; i++) {
			if (_ingredientesAleatorios [i] == _ingredientesDeUsuario [i]) {
				_aciertos += 1;
			} else {
				_guardaErrores [_limite].Add(_ingredientesAleatorios[i]);
				_errores += 1;
			}
		}
		InformacionDePartida (_guardaErrores[_limite]);
		_limite++;
	}

	void InformacionDePartida(List<ActivaPanelDedos> errores){
		_guardaResultados.Add (errores);
		for (int i = 0; i < _guardaResultados.Count; i++) {
			Debug.Log ("Nivel: "+i+"\n");
			List <ActivaPanelDedos> temp = _guardaResultados [i];
			for (int j = 0; j < temp.Count; j++) {
				Debug.Log (temp[j]);
			}
			Debug.Log ("-----------------------------------------------");
		}

	}
		
	IEnumerator AnimacionSalto(){
		yield return new WaitForSeconds (3.0f);
		Vector3 temp;
		for ( int i = 0; i <= _ingredientesAleatorios.Count - 1; i++) {
			temp = _posicionDeIngredientesClon [i].transform.position;
			temp.y += (0.12f)*i;
			_posicionDeIngredientesClon [i].transform.position = temp;
			_posicionDeIngredientesClon [i].GetComponent <Rigidbody2D> ().bodyType = RigidbodyType2D.Kinematic;
		}

		yield return new WaitForSeconds (3.0f);
		for ( int i = 0; i <= _ingredientesAleatorios.Count - 1; i++) {			
			_posicionDeIngredientesClon [i].GetComponent <Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
		}
		StartCoroutine (AnimacionSalto ());
	}

	void Reinicio(){
		if (_limite < _numeroDeRepeticiones) {
			_ingredientesDeUsuario.Clear ();
			_aciertos = 0;
			_errores = 0;
			_contadorIngredientesDeUsuario = 0;
			_destruir = GameObject.FindGameObjectsWithTag ("Estatico");
			for (int i = 0; i <= _destruir.Length - 1; i++) {
				Destroy (_destruir [i]);
			}
		}
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			Reinicio ();
		}
			
		if (Input.GetKeyDown (KeyCode.UpArrow) && (ActivaTecla == Teclado.PresionaPan || ActivaTecla == Teclado.PresionaCualquiera)){
			
			SpawnPan ();
		} else if (Input.GetKeyDown (KeyCode.RightArrow) && (ActivaTecla == Teclado.PresionaJamon || ActivaTecla == Teclado.PresionaCualquiera)){
			
			SpawnJamon ();
		} else if (Input.GetKeyDown (KeyCode.DownArrow) && (ActivaTecla == Teclado.PresionaQueso || ActivaTecla == Teclado.PresionaCualquiera)){
			
			SpawnQueso ();		
		} else if (Input.GetKeyDown (KeyCode.LeftArrow) && (ActivaTecla == Teclado.PresionaJitomate || ActivaTecla == Teclado.PresionaCualquiera)){
			
			SpawnJitomate ();
		}

		if (Input.GetKeyUp (KeyCode.UpArrow) && _ingredienteClon != null && (ActivaTecla == Teclado.PresionaPan || ActivaTecla == Teclado.PresionaCualquiera) ) {
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
		} else if (Input.GetKeyUp (KeyCode.RightArrow) && _ingredienteClon != null && (ActivaTecla == Teclado.PresionaJamon || ActivaTecla == Teclado.PresionaCualquiera)) {
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
		} else if (Input.GetKeyUp (KeyCode.DownArrow) && _ingredienteClon != null && (ActivaTecla == Teclado.PresionaQueso || ActivaTecla == Teclado.PresionaCualquiera)) {
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
		} else if (Input.GetKeyUp (KeyCode.LeftArrow) && _ingredienteClon != null && (ActivaTecla == Teclado.PresionaQueso || ActivaTecla == Teclado.PresionaCualquiera) ) {
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
		} 
	}
}
