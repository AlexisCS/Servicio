using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminNivel3 : MonoBehaviour {

	public GameObject[] interfaz;
	public GameObject[] ingredientes;

	public int _numeroDeIngredientes;
	public float tiempoDePausaEntreIngredientes=0.1f;

	private GameObject _ingredienteClon;
	private int _contadorCapa;
	private List <ActivaPanelDedos> _ingredientesDeUsuario, _ingredientesAleatorios;

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
		GeneraSecuenciaAleatoria ();
		StartCoroutine (GeneraSandwich ());
		_contadorCapa = 1;
	}

	void Start(){
		//GeneraSandwich ();
	}

	void AgregaPan(){
		_ingredientesDeUsuario.Add (ActivaPanelDedos.Indice);
	}

	void AgregaJamon(){
		_ingredientesDeUsuario.Add (ActivaPanelDedos.Medio);
	}

	void AgregaQueso(){
		_ingredientesDeUsuario.Add (ActivaPanelDedos.Anular);
	}

	void AgregaJitomate(){
		_ingredientesDeUsuario.Add (ActivaPanelDedos.Meñique);
	}
		
	void ImprimeSecuencia(){
		for (int i = 0; i <= _ingredientesDeUsuario.Count - 1; i++) {
			Debug.Log (_ingredientesDeUsuario[i]);
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
		ActualizaCapa ();
	}

	void SpawnJamonAleatorio(){
		Vector3 PosicionJamonAleatorio = new Vector3 (-4.5f, 7.9f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (ingredientes [5], PosicionJamonAleatorio, Quaternion.identity);
		ActualizaCapa ();
	}

	void SpawnQuesoAleatorio(){
		Vector3 PosicionQuesoAleatorio = new Vector3 (-4.5f, 7.9f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (ingredientes [6], PosicionQuesoAleatorio, Quaternion.identity);
		ActualizaCapa ();
	}

	void SpawnJitomateAleatorio(){
		Vector3 PosicionJitomateAleatorio = new Vector3 (-4.5f, 7.9f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (ingredientes [7], PosicionJitomateAleatorio, Quaternion.identity);
		ActualizaCapa ();
	}

	void GeneraSecuenciaAleatoria(){
		int ingredienteAnterior = 0; 
		int numeroAleatorio = 0; 
		_ingredientesAleatorios.Add (SeleccionaIngredienteAleatorio (0));
		for (int i = 3; i <= _numeroDeIngredientes; i++) {
			if (i == 3 || i == _numeroDeIngredientes) {
				numeroAleatorio = Random.Range (1, 3);
				if (numeroAleatorio == ingredienteAnterior) {
					numeroAleatorio = (numeroAleatorio == ingredienteAnterior) ? (numeroAleatorio + 1) : numeroAleatorio;
				}
			} else {
				numeroAleatorio = Random.Range (0, 3);
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
			break;
		case 1:			
			return ActivaPanelDedos.Medio;
			break;
		case 2:			
			return ActivaPanelDedos.Anular;
			break;
		case 3:
			return ActivaPanelDedos.Meñique;
			break;
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

	void imprimeSecuenciaAleatoria(){
		for (int i = 0; i <= _ingredientesAleatorios.Count - 1; i++) {
			Debug.Log (_ingredientesAleatorios[i]);
		}
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			ImprimeSecuencia ();
		}

		if (Input.GetKeyDown (KeyCode.S)) {
			imprimeSecuenciaAleatoria ();
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
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
