using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminNivel3 : MonoBehaviour {

	public GameObject[] interfaz;
	public GameObject[] ingredientes;

	private GameObject _ingredienteClon, _ingredienteClonAleatorio;
	private int _contadorCapa;
	private List <ActivaPanelDedos> _ingredientesDeUsuario;

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
		GeneraSandwichAleatorio ();
		_ingredientesDeUsuario = new List<ActivaPanelDedos> ();
		_contadorCapa = 0;
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

	void GeneraSandwichAleatorio(){
		Vector3 PosicionJitomate = new Vector3 (-4.5f, 7.9f, 0.0f);
		_ingredienteClon = (GameObject) Instantiate (ingredientes [3], PosicionJitomate, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			ImprimeSecuencia ();
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
