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
		InvokeRepeating("SpawnPan",1f,2f);
		//CancelInvoke ("SpawnPan");
	}
		
	void ActualizaCantidadPan (){
		Cantidad += 1;
		CantidadPanText.text = "Pan:" + Cantidad;
		Debug.Log ("Evento Activado");
	}

	void ActualizaCantidadJamon (){
		
	
	}

	void ActualizaCantidadQueso (){

	}

	void ActualizaCantidadJitomate (){

	}

	void SpawnPan(){
		Vector3 Posicion = new Vector3 (6.6f, 7.6f, 0);
		Instantiate (Ingredientes [0], Posicion, Quaternion.identity);
	}

//	IEnumerator SpawPan () {
//		while (true) {
//			Vector3 Posicion = new Vector3 (-36.6f, 23f, 0);
//			Instantiate (Ingredientes [0], Posicion, Quaternion.identity);
//		}
//	}
		
	// Update is called once per frame
	void Update () {
		
	}
}

