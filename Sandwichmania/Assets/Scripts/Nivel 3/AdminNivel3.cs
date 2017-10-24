using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Collections;

public enum Teclado {SinPresionar, PresionaPan, PresionaJamon, PresionaQueso, PresionaJitomate, PresionaCualquiera}
public enum ActivaPanelDedos {SinSeleccion, Indice, Medio, Anular, Meñique}

public class AdminNivel3 : MonoBehaviour {

	public delegate void Audio ();
	public static event Audio Colision;
	public static event Audio AudioDeJuegoNivel3;
	public static event Audio AudiodeExitoNivel3;

	public GameObject[] interfaz;
	public GameObject[] ingredientes;
	public Text noRepeticionesExito1, noRepeticionesExito2, noRepeticionesExito3;
	public Button ayudaBoton;

	private static List <List<ActivaPanelDedos>> guardaResultados;
	public static List <List<ActivaPanelDedos>> GuardaResultados{
		get{ 
			return guardaResultados;
		}
	}

	private static List <ActivaPanelDedos> ingredientesAleatorios;
	public static List <ActivaPanelDedos> IngredientesAleatorios{
		get {
			return ingredientesAleatorios;
		}
	}

	public void BotonRegresar(){
		interfaz [6].gameObject.SetActive (true);
	}

	public void BotonSi(){
		SceneManager.LoadScene (1);
	}

	public void BotonNo(){
		interfaz [6].gameObject.SetActive (false);
	}

	public void BotonEntendido(){
		interfaz [7].gameObject.SetActive (false);
		interfaz [8].gameObject.SetActive (false);
	}

	public void ActivaPanelAyuda(){
		switch (_mano) {
		case Mano.Izquierda:
			interfaz [8].gameObject.SetActive (true);
			break;
		case Mano.Derecha:
			interfaz [7].gameObject.SetActive (true);
			break;
		}
	}
		
	private GameObject _ingredienteClon; 
	private GameObject[] _destruir;
	private int _numeroDeIngredientes, _numeroDeRepeticiones, _contadorCapa, _contadorIngredientesDeUsuario, _limite, _errores;
	Mano _mano;
	private float _tiempoDePausaEntreIngredientes;
	private List <Transform> _posicionDeIngredientesClon; 
	private List <ActivaPanelDedos>[] _guardaErrores;
	private List <ActivaPanelDedos> _ingredientesDeUsuario;
	private Dictionary<int, List<ActivaPanelDedos>> _informacionPartida;


	enum ActivaPanelInteractivo {SinPanel, Bienvenido, Inicio, Juegue, ExitoParcial, Reinicio, Exito}
	ActivaPanelInteractivo PanelActivado;

	ActivaPanelDedos PanelDedosActivo;

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
		_mano = AdminMenu.datosNivel3.ManoSeleccionada;
		_numeroDeIngredientes = AdminMenu.datosNivel3.numeroDeIngredientes;
		_numeroDeRepeticiones = AdminMenu.datosNivel3.numeroDeRepeticiones;
		ayudaBoton.gameObject.SetActive (false);
		interfaz [0].gameObject.SetActive (true);
		_ingredientesDeUsuario = new List<ActivaPanelDedos> ();
		ingredientesAleatorios = new List<ActivaPanelDedos> ();
		_posicionDeIngredientesClon = new List<Transform> ();
		_guardaErrores = new List<ActivaPanelDedos>[_numeroDeRepeticiones];
		for (int i = 0; i <= _numeroDeRepeticiones - 1; i++) {
			_guardaErrores[i] = new List<ActivaPanelDedos>();
		}
		guardaResultados = new List<List<ActivaPanelDedos>> ();
		ActivaTecla = Teclado.SinPresionar;
		PanelActivado = ActivaPanelInteractivo.SinPanel;
		_tiempoDePausaEntreIngredientes = 0.5f;
		_contadorCapa = 1;
		_errores = 0;
		_contadorIngredientesDeUsuario = 0;
		_limite = 0;

	}

	void AgregaPan(){
		Colision ();
		_ingredienteClon = null;
		_contadorIngredientesDeUsuario += 1;
		_ingredientesDeUsuario.Add (ActivaPanelDedos.Indice);
		if (_contadorIngredientesDeUsuario == _numeroDeIngredientes) {
			ActivaTecla = Teclado.SinPresionar;
			ComparaIngredientes ();
			PanelActivado = ActivaPanelInteractivo.ExitoParcial;
			ActivaPanel ();
		}
		ActivaTecla = Teclado.PresionaCualquiera;

	}

	void AgregaJamon(){
		Colision ();
		_ingredienteClon = null;
		_contadorIngredientesDeUsuario += 1;
		_ingredientesDeUsuario.Add (ActivaPanelDedos.Medio);
		if (_contadorIngredientesDeUsuario == _numeroDeIngredientes) {
			ActivaTecla = Teclado.SinPresionar;
			ComparaIngredientes ();
			PanelActivado = ActivaPanelInteractivo.ExitoParcial;
			ActivaPanel ();
		}
		ActivaTecla = Teclado.PresionaCualquiera;
	}

	void AgregaQueso(){
		Colision ();
		_ingredienteClon = null;
		_contadorIngredientesDeUsuario += 1;
		_ingredientesDeUsuario.Add (ActivaPanelDedos.Anular);
		if (_contadorIngredientesDeUsuario == _numeroDeIngredientes) {
			ActivaTecla = Teclado.SinPresionar;
			ComparaIngredientes ();
			PanelActivado = ActivaPanelInteractivo.ExitoParcial;
			ActivaPanel ();
		}
		ActivaTecla = Teclado.PresionaCualquiera;
	}

	void AgregaJitomate(){
		Colision ();
		_ingredienteClon = null;
		_contadorIngredientesDeUsuario += 1;
		_ingredientesDeUsuario.Add (ActivaPanelDedos.Meñique);
		if (_contadorIngredientesDeUsuario == _numeroDeIngredientes) {
			ActivaTecla = Teclado.SinPresionar;
			ComparaIngredientes ();
			PanelActivado = ActivaPanelInteractivo.ExitoParcial;
			ActivaPanel ();
		}
		ActivaTecla = Teclado.PresionaCualquiera;
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
		ingredientesAleatorios.Add (SeleccionaIngredienteAleatorio (0));
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
			ingredientesAleatorios.Add (SeleccionaIngredienteAleatorio (numeroAleatorio));
			ingredienteAnterior = numeroAleatorio;

		}
		ingredientesAleatorios.Add (SeleccionaIngredienteAleatorio (0));
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
		for (int i = 0; i <= ingredientesAleatorios.Count - 1; i++) {
			switch (ingredientesAleatorios [i]) {
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
			yield return new WaitForSeconds (_tiempoDePausaEntreIngredientes);
		}
		yield return new WaitForSeconds (1.5f);
		ActivaTecla = Teclado.PresionaCualquiera;
	}
		

	void ComparaIngredientes(){
		for (int i = 0; i <= ingredientesAleatorios.Count - 1; i++) {
			if (ingredientesAleatorios [i] == _ingredientesDeUsuario [i]) {
			} else {
				_guardaErrores [_limite].Add(ingredientesAleatorios[i]);
				_errores += 1;
			}
		}
		InformacionDePartida (_guardaErrores[_limite]);
		_limite++;
	}

	void InformacionDePartida(List<ActivaPanelDedos> errores){
		guardaResultados.Add (errores);
//		for (int i = 0; i < guardaResultados.Count; i++) {
//			Debug.Log ("Nivel: "+i+"\n");
//			List <ActivaPanelDedos> temp = guardaResultados [i];
//			for (int j = 0; j < temp.Count; j++) {
//				Debug.Log (temp[j]);
//			}
//			Debug.Log ("-----------------------------------------------");
//		}
	}
		
//	IEnumerator AnimacionSalto(){
//		yield return new WaitForSeconds (3.0f);
//		Vector3 temp;
//		for ( int i = 0; i <= _ingredientesAleatorios.Count - 1; i++) {
//			temp = _posicionDeIngredientesClon [i].transform.position;
//			temp.y += (0.12f)*i;
//			_posicionDeIngredientesClon [i].transform.position = temp;
//			_posicionDeIngredientesClon [i].GetComponent <Rigidbody2D> ().bodyType = RigidbodyType2D.Kinematic;
//		}
//
//		yield return new WaitForSeconds (3.0f);
//		for ( int i = 0; i <= _ingredientesAleatorios.Count - 1; i++) {			
//			_posicionDeIngredientesClon [i].GetComponent <Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
//		}
//		StartCoroutine (AnimacionSalto ());
//	}

	void Reinicio(){
		if (_limite == _numeroDeRepeticiones) {
			ActivaTecla = Teclado.SinPresionar;
			PanelActivado = ActivaPanelInteractivo.Exito;
			ActivaPanel ();
			return;
		}

		if (_limite < _numeroDeRepeticiones) {
			_ingredientesDeUsuario.Clear ();
			_errores = 0;
			_contadorIngredientesDeUsuario = 0;
			_destruir = GameObject.FindGameObjectsWithTag ("Estatico");
			for (int i = 0; i <= _destruir.Length - 1; i++) {
				Destroy (_destruir [i]);
			}
		}
	}

	void ActivaPanel(){
		switch (PanelActivado) {
		case ActivaPanelInteractivo.Bienvenido:
			interfaz [0].gameObject.SetActive (false);
			interfaz [1].gameObject.SetActive (true);
			PanelActivado = ActivaPanelInteractivo.Inicio;
			break;
		case ActivaPanelInteractivo.Juegue:
			if (AudioDeJuegoNivel3 != null) {
				AudioDeJuegoNivel3 ();
			}
			ayudaBoton.gameObject.SetActive (true);
			interfaz [1].gameObject.SetActive (false);
			GeneraSecuenciaAleatoria ();
			StartCoroutine (GeneraSandwich ());
			break;
		case ActivaPanelInteractivo.ExitoParcial:
			int temp;
			temp = _numeroDeIngredientes / 2;
			if (_errores == 0) {
				interfaz [2].gameObject.SetActive (true);
				noRepeticionesExito1.text = _limite + " de " +_numeroDeRepeticiones;
			} else if (_errores <= temp) {
				interfaz [3].gameObject.SetActive (true);
				noRepeticionesExito2.text = _limite + " de " +_numeroDeRepeticiones;
			} else if (_errores > temp) {
				interfaz [4].gameObject.SetActive (true);
				noRepeticionesExito3.text = _limite + " de " +_numeroDeRepeticiones;
			}
			break;
		case ActivaPanelInteractivo.Reinicio:
			interfaz [2].gameObject.SetActive (false);
			interfaz [3].gameObject.SetActive (false);
			interfaz [4].gameObject.SetActive (false);
			break;
		case ActivaPanelInteractivo.Exito:
			if (AudiodeExitoNivel3 != null) {
				AudiodeExitoNivel3 ();
			}
			interfaz [5].gameObject.SetActive (true);
			break;
		}
		
	}

	void Exito(){
		SceneManager.LoadScene (9);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.LeftArrow)
			&& (PanelActivado == ActivaPanelInteractivo.SinPanel || PanelActivado == ActivaPanelInteractivo.Inicio || PanelActivado == ActivaPanelInteractivo.ExitoParcial || PanelActivado == ActivaPanelInteractivo.Exito)) {
			if (PanelActivado == ActivaPanelInteractivo.SinPanel) {
				PanelActivado = ActivaPanelInteractivo.Bienvenido;
				ActivaPanel ();
				return;
			}

			if (PanelActivado == ActivaPanelInteractivo.Inicio) {
				PanelActivado = ActivaPanelInteractivo.Juegue;
				ActivaPanel ();
				return;
			}

			if (PanelActivado == ActivaPanelInteractivo.ExitoParcial) {
				PanelActivado = ActivaPanelInteractivo.Reinicio;
				ActivaPanel ();
				Reinicio ();
				return;
			}
		}

		if ((Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) 
			|| Input.GetKeyDown(KeyCode.LeftArrow)) && PanelActivado == ActivaPanelInteractivo.Exito) {
			Exito ();
			return;
		}
			
		if (Input.GetKeyDown (KeyCode.UpArrow) && (ActivaTecla == Teclado.PresionaPan || ActivaTecla == Teclado.PresionaCualquiera)){
			ActivaTecla = Teclado.PresionaPan;
			SpawnPan ();
		} else if (Input.GetKeyDown (KeyCode.RightArrow) && (ActivaTecla == Teclado.PresionaJamon || ActivaTecla == Teclado.PresionaCualquiera)){
			ActivaTecla = Teclado.PresionaJamon;
			SpawnJamon ();
		} else if (Input.GetKeyDown (KeyCode.DownArrow) && (ActivaTecla == Teclado.PresionaQueso || ActivaTecla == Teclado.PresionaCualquiera)){
			ActivaTecla = Teclado.PresionaQueso;
			SpawnQueso ();		
		} else if (Input.GetKeyDown (KeyCode.LeftArrow) && (ActivaTecla == Teclado.PresionaJitomate || ActivaTecla == Teclado.PresionaCualquiera)){
			ActivaTecla = Teclado.PresionaJitomate;
			SpawnJitomate ();
		}

		if (Input.GetKeyUp (KeyCode.UpArrow) && _ingredienteClon != null && (ActivaTecla == Teclado.PresionaPan || ActivaTecla == Teclado.PresionaCualquiera) ) {
			ActivaTecla = Teclado.PresionaCualquiera;
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
		} else if (Input.GetKeyUp (KeyCode.RightArrow) && _ingredienteClon != null && (ActivaTecla == Teclado.PresionaJamon || ActivaTecla == Teclado.PresionaCualquiera)) {
			ActivaTecla = Teclado.PresionaCualquiera;
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
		} else if (Input.GetKeyUp (KeyCode.DownArrow) && _ingredienteClon != null && (ActivaTecla == Teclado.PresionaQueso || ActivaTecla == Teclado.PresionaCualquiera)) {
			ActivaTecla = Teclado.PresionaCualquiera;
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
		} else if (Input.GetKeyUp (KeyCode.LeftArrow) && _ingredienteClon != null && (ActivaTecla == Teclado.PresionaJitomate || ActivaTecla == Teclado.PresionaCualquiera)) {
			ActivaTecla = Teclado.PresionaCualquiera;
			Destroy(_ingredienteClon);
			_ingredienteClon = null;
		} 
	}
}
