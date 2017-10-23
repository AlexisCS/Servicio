using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Collections;

public class AdminNivel2SinRutina : MonoBehaviour {

	public delegate void Audio ();
	public static event Audio Colision;
	public static event Audio AudioDeJuego;
	public static event Audio AudiodeExito;

	public GameObject[] ingredientes;
	public GameObject[] interzas;
	public Text mensajeFelicitacion;

	//public int _numeroDeRepeticiones, _numeroDeIngredientes, _mano;

	private GameObject _ingredienteClon; 
	private GameObject[] _destruir;
	private List<ActivaPanelDedos> _secuencia;
	private List<ActivaPanelDedos> _guardaIngredientes;
	private int _contadorCapa, _count, _limite, _mano, _numeroDeRepeticiones, _numeroDeIngredientes;
	private bool _pan, _jamon, _queso, _jitomate;

	enum ActivaPanelInteractivo {Bienvenido, Siguiente, Inicio, Juegue, ExitoParcial, SegundoInicio, Exito}
	ActivaPanelInteractivo PanelActivado;

	enum ActivaPanelDedos {SinSeleccion, Indice, Medio, Anular, Meñique}
	ActivaPanelDedos PanelDedosActivo;

	void Awake(){
		_secuencia = new List<ActivaPanelDedos> ();
		PanelActivado = ActivaPanelInteractivo.Bienvenido;
		PanelDedosActivo = ActivaPanelDedos.SinSeleccion;
		interzas [0].gameObject.SetActive (true);
		interzas [12].gameObject.SetActive (false);
		interzas [13].gameObject.SetActive (true);
		_mano = Admin_level0.datos.mano;
		_numeroDeIngredientes = Admin_level0.datos.numeroDeIngredientes;
		_numeroDeRepeticiones = Admin_level0.datos.numeroDeRepeticiones;
		_contadorCapa = 0;
		_count = 0;
		_limite = 1;
		_pan = true;
		_jamon = true;
		_queso = true;
		_jitomate = true;
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
		DetectaColision.OnJitomateApilado -= AgregaJitomate;
	}


	void GeneraSecuenciaAleatoria(){
		int ingredienteAnterior = 0; 
		int numeroAleatorio = 0; 
		_secuencia.Add (SeleccionaIngredienteAleatorio (0));
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
			_secuencia.Add (SeleccionaIngredienteAleatorio (numeroAleatorio));
			ingredienteAnterior = numeroAleatorio;
		}
		_secuencia.Add (SeleccionaIngredienteAleatorio (0));
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


	void DecideSecuencia(){
		if(_count == _secuencia.Count && _limite == _numeroDeRepeticiones){ 
			PanelActivado = ActivaPanelInteractivo.Exito;
			PanelInteractivo ();
			return;
		}

		if(_count == _secuencia.Count && _limite < _numeroDeRepeticiones){
			PanelActivado = ActivaPanelInteractivo.ExitoParcial;
			PanelInteractivo ();
			return;
		}
		switch (_secuencia[_count]) {
		case ActivaPanelDedos.Indice:
			PanelDedosActivo = ActivaPanelDedos.Indice;
			PanelDedos (_mano);
			_pan = false;
			_jamon = true;
			_queso = true;
			_jitomate = true;
			break;
		case ActivaPanelDedos.Medio:
			PanelDedosActivo = ActivaPanelDedos.Medio;
			PanelDedos (_mano);
			_pan = true;
			_jamon = false;
			_queso = true;
			_jitomate = true;
			break;
		case ActivaPanelDedos.Anular:
			PanelDedosActivo = ActivaPanelDedos.Anular;
			PanelDedos (_mano);
			_pan = true;
			_jamon = true;
			_queso = false;
			_jitomate = true;
			break;
		case ActivaPanelDedos.Meñique:
			PanelDedosActivo = ActivaPanelDedos.Meñique;
			PanelDedos (_mano);
			_pan = true;
			_jamon = true;
			_queso = true;
			_jitomate = false;
			break;
		}
		if(_count <_secuencia.Count){
			_count += 1;
		}

	}

	void AgregaPan(){
		Colision ();
		_ingredienteClon = null;
		DecideSecuencia ();
	}

	void AgregaJamon(){
		Colision ();
		_ingredienteClon = null;
		DecideSecuencia ();
	}

	void AgregaQueso(){
		Colision ();
		_ingredienteClon = null;
		DecideSecuencia ();
	}

	void AgregaJitomate(){
		Colision ();
		_ingredienteClon = null;
		DecideSecuencia ();
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
				interzas [7].gameObject.SetActive (false);
				interzas [8].gameObject.SetActive (false);
				interzas [9].gameObject.SetActive (false);
			} else if (PanelDedosActivo == ActivaPanelDedos.Medio) {
				interzas [6].gameObject.SetActive (false);
				interzas [7].gameObject.SetActive (true);
				interzas [8].gameObject.SetActive (false);
				interzas [9].gameObject.SetActive (false);
			} else if (PanelDedosActivo == ActivaPanelDedos.Anular) {
				interzas [6].gameObject.SetActive (false);
				interzas [7].gameObject.SetActive (false);
				interzas [8].gameObject.SetActive (true);
				interzas [9].gameObject.SetActive (false);
			} else if (PanelDedosActivo == ActivaPanelDedos.Meñique) {
				interzas [6].gameObject.SetActive (false);
				interzas [7].gameObject.SetActive (false);
				interzas [8].gameObject.SetActive (false);
				interzas [9].gameObject.SetActive (true);
			} 
			break;
		case 1:
			if (PanelDedosActivo == ActivaPanelDedos.Indice) {
				interzas [2].gameObject.SetActive (true);
				interzas [3].gameObject.SetActive (false);
				interzas [4].gameObject.SetActive (false);
				interzas [5].gameObject.SetActive (false);
			} else if (PanelDedosActivo == ActivaPanelDedos.Medio) {
				interzas [2].gameObject.SetActive (false);
				interzas [3].gameObject.SetActive (true);
				interzas [4].gameObject.SetActive (false);
				interzas [5].gameObject.SetActive (false);
			} else if (PanelDedosActivo == ActivaPanelDedos.Anular) {
				interzas [2].gameObject.SetActive (false);
				interzas [3].gameObject.SetActive (false);
				interzas [4].gameObject.SetActive (true);
				interzas [5].gameObject.SetActive (false);
			} else if (PanelDedosActivo == ActivaPanelDedos.Meñique) {
				interzas [2].gameObject.SetActive (false);
				interzas [3].gameObject.SetActive (false);
				interzas [4].gameObject.SetActive (false);
				interzas [5].gameObject.SetActive (true);
			} 
			break;
		}
	}


	void DesactivaIngredientes(){
		interzas [2].gameObject.SetActive (false);
		interzas [3].gameObject.SetActive (false);
		interzas [4].gameObject.SetActive (false);
		interzas [5].gameObject.SetActive (false);
		interzas [6].gameObject.SetActive (false);
		interzas [7].gameObject.SetActive (false);
		interzas [8].gameObject.SetActive (false);
		interzas [9].gameObject.SetActive (false);
	}

	//Contiene los mensajes de instrucciones y de exito
	void PanelInteractivo (){ // Cambiar a verbo
		switch (PanelActivado) {
		case ActivaPanelInteractivo.Siguiente:
			interzas [0].gameObject.SetActive (false);
			interzas [1].gameObject.SetActive (true);
			PanelActivado = ActivaPanelInteractivo.Inicio;
			break;
		case ActivaPanelInteractivo.Inicio:
			interzas [1].gameObject.SetActive (false);
			interzas [10].gameObject.SetActive (false);
			PanelActivado = ActivaPanelInteractivo.Juegue;
			PanelDedosActivo = _secuencia [0];
			PanelDedos (_mano);
			break;
		case ActivaPanelInteractivo.ExitoParcial:
			DesactivaIngredientes ();
			mensajeFelicitacion.text = "¡Lo estas haciendo genial, sigue asi!\n\n\n" + _limite  + "  de  " + _numeroDeRepeticiones; 
			interzas [10].gameObject.SetActive (true);
			break;
		case ActivaPanelInteractivo.Exito:
			if (AudiodeExito != null) {
				AudiodeExito ();
			}
			DesactivaIngredientes ();
			interzas [11].gameObject.SetActive (true);
			break;
		}
	}

	/*Se hace porque asi lo quise ... Porque al tener varias repeticiones entra al caso ActivaPanelInteractivo.Inicio en la funcion "PanelInteractivo"
	Y para que la musica del juego no se reinicie creamos la funcion*/
	void ActivaMusicaDeJuego(){
		if (AudioDeJuego != null && PanelActivado == ActivaPanelInteractivo.Inicio) {
			AudioDeJuego ();
		}
	}


	void Reinicio(){ 
		if (_limite < _numeroDeRepeticiones) {
			_pan = true;
			_jamon = true;
			_queso = true;
			_jitomate = true;
			_count = 0;
			_limite += 1;
			PanelActivado = ActivaPanelInteractivo.Juegue;
			_destruir = GameObject.FindGameObjectsWithTag ("Estatico");
			for (int i = 0; i <= _destruir.Length - 1; i++) {
				Destroy (_destruir [i]);
			}
		}
	} 

	public void BotonRegresar(){
		interzas [12].gameObject.SetActive (true);
	}

	public void BotonSi(){
		SceneManager.LoadScene (1);
	}

	public void BotonNo(){
		interzas [12].gameObject.SetActive (false);
	}

	void Exito(){
		SceneManager.LoadScene (1);
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

	// Update is called once per frame
	void Update () {
		if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) && 
			(PanelActivado == ActivaPanelInteractivo.Bienvenido || PanelActivado == ActivaPanelInteractivo.Siguiente || PanelActivado == ActivaPanelInteractivo.Inicio || 
				PanelActivado == ActivaPanelInteractivo.ExitoParcial)){
			if (PanelActivado == ActivaPanelInteractivo.Bienvenido) {
				PanelActivado = ActivaPanelInteractivo.Siguiente;
				GeneraSecuenciaAleatoria ();
				PanelInteractivo ();
			} else {
				ActivaMusicaDeJuego ();
				PanelInteractivo ();
				DecideSecuencia ();
			}	

			if (PanelActivado == ActivaPanelInteractivo.ExitoParcial) {
				PanelActivado = ActivaPanelInteractivo.Inicio;
				PanelInteractivo ();
				Reinicio ();
				DecideSecuencia ();
			}

		}

		if ((Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) 
			|| Input.GetKeyDown(KeyCode.LeftArrow)) && PanelActivado == ActivaPanelInteractivo.Exito) {
			Exito ();
		}

		if (Input.GetKeyDown (KeyCode.UpArrow) && _pan == false && PanelActivado == ActivaPanelInteractivo.Juegue) {
			SpawnPan ();
		} else if (Input.GetKeyDown (KeyCode.RightArrow) && _jamon == false) {
			SpawnJamon ();
		} else if (Input.GetKeyDown (KeyCode.DownArrow) && _queso == false) {
			SpawnQueso ();		
		} else if (Input.GetKeyDown (KeyCode.LeftArrow) && _jitomate == false) {
			SpawnJitomate ();
		}

		if (Input.GetKeyUp (KeyCode.UpArrow) && _ingredienteClon != null && _pan == false) {
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
		} 
	}
}

