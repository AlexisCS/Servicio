using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Collections;

public class AdminNivel2 : MonoBehaviour {

	public delegate void Audio ();
	public static event Audio AudioDeJuego;
	public static event Audio AudiodeExito;

	public GameObject[] ingredientes;
	public GameObject[] interfaz;
	public Text mensajeFelicitacion;
	public AudioClip AudioColision;

	public int numeroDeRepeticiones;

	private GameObject _ingredienteClon; 
	private GameObject[] _destruir;
	private List<ActivaPanelDedos> _secuencia;
	private AudioSource _audioSource;
	private int _contadorCapa, _count, _limite, _mano;
	private bool _pan, _jamon, _queso, _jitomate;

	enum ActivaPanelInteractivo {Bienvenido, Siguiente, Inicio, Juegue, ExitoParcial, SegundoInicio, Exito}
	ActivaPanelInteractivo PanelActivado;

	enum ActivaPanelDedos {SinSeleccion, Indice, Medio, Anular, Meñique}
	ActivaPanelDedos PanelDedosActivo;

	void Awake(){
		Admin_level0.datos = new InfoPartida ();
		_secuencia =  new List<ActivaPanelDedos> {ActivaPanelDedos.Indice, ActivaPanelDedos.Medio, ActivaPanelDedos.Anular, ActivaPanelDedos.Meñique};
		PanelActivado = ActivaPanelInteractivo.Bienvenido;
		PanelDedosActivo = ActivaPanelDedos.SinSeleccion;
		interfaz [0].gameObject.SetActive (true);
		interfaz [13].gameObject.SetActive (true);
		_audioSource = GetComponent <AudioSource> ();
		_mano = Admin_level0.datos.mano;
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

	void DecideSecuencia(){
		if(_count == _secuencia.Count && _limite == numeroDeRepeticiones){ 
			PanelActivado = ActivaPanelInteractivo.Exito;
			PanelInteractivo ();
			return;
		}

		if(_count == _secuencia.Count && _limite < numeroDeRepeticiones){
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
			Debug.Log (_count+"y"+_secuencia.Count);
			_count += 1;
		}

	}

	void AgregaPan(){
		_audioSource.PlayOneShot (AudioColision, 1.0f);
		_ingredienteClon = null;
		DecideSecuencia ();
	}

	void AgregaJamon(){
		_audioSource.PlayOneShot (AudioColision, 1.0f);
		_ingredienteClon = null;
		DecideSecuencia ();
	}

	void AgregaQueso(){
		_audioSource.PlayOneShot (AudioColision, 1.0f);
		_ingredienteClon = null;
		DecideSecuencia ();
	}

	void AgregaJitomate(){
		_audioSource.PlayOneShot (AudioColision, 1.0f);
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
		case 1:
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
		
	//Contiene los mensajes de instrucciones y de exito
	void PanelInteractivo (){ // Cambiar a verbo
		switch (PanelActivado) {
		case ActivaPanelInteractivo.Siguiente:
			interfaz [0].gameObject.SetActive (false);
			interfaz [1].gameObject.SetActive (true);
			PanelActivado = ActivaPanelInteractivo.Inicio;
			break;
		case ActivaPanelInteractivo.Inicio:
			interfaz [1].gameObject.SetActive (false);
			interfaz [10].gameObject.SetActive (false);
			PanelActivado = ActivaPanelInteractivo.Juegue;
			PanelDedosActivo = _secuencia [0];
			PanelDedos (_mano);
			break;
		case ActivaPanelInteractivo.ExitoParcial:
			DesactivaIngredientes ();
			mensajeFelicitacion.text = "¡Lo estas haciendo genial, sigue asi!\n\n\n" + _limite  + "  de  " + numeroDeRepeticiones; 
			interfaz [10].gameObject.SetActive (true);
			break;
		case ActivaPanelInteractivo.Exito:
			if (AudiodeExito != null) {
				AudiodeExito ();
			}
			DesactivaIngredientes ();
			interfaz [11].gameObject.SetActive (true);
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
		if (_limite < numeroDeRepeticiones) {
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
		interfaz [12].gameObject.SetActive (true);
	}

	public void BotonSi(){
		SceneManager.LoadScene (1);
	}

	public void BotonNo(){
		interfaz [12].gameObject.SetActive (false);
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
