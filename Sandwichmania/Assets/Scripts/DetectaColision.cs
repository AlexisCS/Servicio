using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectaColision : MonoBehaviour {

	//Crear eventos:
	public delegate void IngredienteAction(); 
	public static event IngredienteAction OnPanApilado;
	public static event IngredienteAction OnJamonApilado;
	public static event IngredienteAction OnQuesoApilado;
	public static event IngredienteAction OnJitomateApilado;

	void Start(){
		
	}

	void OnCollisionEnter2D (Collision2D other){
		//Debug.Log ("Colision");
		switch (this.gameObject.tag) {
		case "Pan":
			this.gameObject.tag = "Estatico";
			if (OnPanApilado != null) {
				OnPanApilado ();
			}
			break;
		case "Jamon":
			this.gameObject.tag = "Estatico";
			if (OnJamonApilado != null) {
				OnJamonApilado ();
			}
			break;
		case "Queso":
			this.gameObject.tag = "Estatico";
			if (OnQuesoApilado != null) {
				OnQuesoApilado ();
			}
			break;
		case "Jitomate":
			this.gameObject.tag = "Estatico";
			if (OnJitomateApilado != null) {
				OnJitomateApilado ();
			}
			break;
		}
	}
}
