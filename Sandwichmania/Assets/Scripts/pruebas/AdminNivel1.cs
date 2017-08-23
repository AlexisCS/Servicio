using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminNivel1 : MonoBehaviour {

	public Text CantidadText;

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
	}
		
	void ActualizaCantidadPan (){
		Cantidad += 1;
		CantidadText.text = "Pan:" + Cantidad;
		Debug.Log ("Evento Activado");
	}

	void ActualizaCantidadJamon (){
	
	}

	void ActualizaCantidadQueso (){

	}

	void ActualizaCantidadJitomate (){

	}
		
	// Update is called once per frame
	void Update () {
		
	}
}
