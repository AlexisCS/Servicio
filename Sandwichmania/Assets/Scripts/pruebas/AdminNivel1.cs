using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminNivel1 : MonoBehaviour {

	public GameObject[] Ingredientes;
	public Text CantidadPanText;
	public Text CantidadJamonText;
	public Text CantidadQuesoText;
	public Text CantidadJitomateText;

	private int Cantidad;
	private GameObject _ingredienteClon;

	void OnEnable(){
		//suscribiendonos al evento
		DetectaColision.OnPanApilado += ActualizaCantidadPan;
		DetectaColision.OnJamonApilado += ActualizaCantidadJamon;
		DetectaColision.OnQuesoApilado += ActualizaCantidadQueso;
		DetectaColision.OnJitomateApilado += ActualizaCantidadJitomate;
	}

	void OnDisable(){
		//nos desuscribimos unicamente cuando este script se desactiva (al cambiar de escena, cerrar la app, etc)
		DetectaColision.OnPanApilado -= ActualizaCantidadPan;
		DetectaColision.OnJamonApilado -= ActualizaCantidadJamon;
		DetectaColision.OnQuesoApilado -= ActualizaCantidadQueso;
		DetectaColision.OnJitomateApilado -= ActualizaCantidadJitomate;
	}

	void Start () {
		Cantidad = 0;
		//StartCoroutine (SpawPan ());
		//InvokeRepeating("SpawnPan",1f,2f);
		//CancelInvoke ("SpawnPan");
	}
		
	void ActualizaCantidadPan (){
		_ingredienteClon = null;
		Cantidad += 1;
		CantidadPanText.text = "Pan:" + Cantidad;
		Debug.Log ("Evento Activado");
	}

	void ActualizaCantidadJamon (){
		Cantidad += 1;
		CantidadJamonText.text = "Jamon:" + Cantidad;
	}

	void ActualizaCantidadQueso (){

	}

	void ActualizaCantidadJitomate (){

	}

	void SpawnPan(){
		Vector3 PosicionPan = new Vector3 (6.6f, 7.6f, 0.0f);
		_ingredienteClon=(GameObject) Instantiate (Ingredientes [0], PosicionPan, Quaternion.identity);
	}

	void SpawnJamon(){
		Vector3 PosicionJamon = new Vector3 (2.2f, 7.7f, 0.0f);
		Instantiate (Ingredientes [1], PosicionJamon, Quaternion.identity);
	}

	void SpawnQueso(){
		Vector3 PosicionQueso = new Vector3 (-2.0f, 7.9f, 0.0f);
		Instantiate (Ingredientes [2], PosicionQueso, Quaternion.identity);
	}

	void SpawnJitomate(){
		Vector3 PosicionJitomate = new Vector3 (-7.2f, 7.9f, 0.0f);
		Instantiate (Ingredientes [3], PosicionJitomate, Quaternion.identity);
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
			
		if (Input.GetKeyUp (KeyCode.UpArrow) && _ingredienteClon != null){
		//if (_ingredienteClon != null) {
			//if (Input.GetKeyUp (KeyCode.UpArrow) && Ingredientes [0].tag == "Pan") {
				Destroy(_ingredienteClon);
				_ingredienteClon = null;
				//DestroyImmediate (Ingredientes [0].gameObject);	
				Debug.Log ("Debo de destruir");
		}
			
	}
}

