using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectaColision : MonoBehaviour {

	//Crear eventos:
	public delegate void IngredienteAction(); 
	public static event IngredienteAction OnPanApilado;
	public static event IngredienteAction OnQuesoApilado;
	public static event IngredienteAction OnJitomateApilado;
	public static event IngredienteAction OnJamonApilado;

	private string TagActual;
	private string TagFinal;
	private int count = 0;

	void Start (){
		TagActual = "Pan";
		TagFinal = "Estatico";
	}
		
	void OnCollisionEnter2D (Collision2D other){
		Debug.Log ("Colisione");
		if (this.gameObject.tag == TagActual) {
			Debug.Log ("Entre al if");
			Debug.Log (this.gameObject.tag);
			this.gameObject.tag = TagFinal;
			//virificando que existan suscriptores
			if (OnPanApilado != null) {
				OnPanApilado ();
			}
		} 
		Debug.Log ("Fuera del if");
		Debug.Log (this.gameObject.tag);
		//other.gameObject.tag = TagFinal;
	}

}
