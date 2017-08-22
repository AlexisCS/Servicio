using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerarIngredientes : MonoBehaviour {
	//public GameObject Ingrediente;
	public Text CantidadText;

	private int Cantidad;
	//public Rigidbody2D rb2D;
	// Use this for initialization

	void OnEnable(){
		//suscribiendonos al evento
		DetectaColision.OnPanApilado += ActualizaCantidadPan;
		//DetectaColision.OnJitomateApilado += ActualizaCantidad;
	}

	void OnDisable(){
		//nos desuscribimos unicamente cuando este script se desactiva (al cambiar de escena, cerrar la app, etc)
		DetectaColision.OnPanApilado -= ActualizaCantidad;
	}

	void Start () {
		Cantidad = 0;
		//ActualizaCantidad ();
		//rb2D = GetComponent<Rigidbody2D> ();
		//count = 0;
	}
		
	void ActualizaCantidadPan (){
		CantidadText.text = "Pan:" + Cantidad;
		Debug.Log ("Evento Activado");
	}

	void ActualizaCantidadJamon (){
	
	}

	void ActualizaCantidadQueso (){

	}


		
	// Update is called once per frame
	void Update () {
		
	}
}
