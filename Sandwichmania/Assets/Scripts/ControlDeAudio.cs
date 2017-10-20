using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlDeAudio : MonoBehaviour {

	public Image apagado, encendido;
	public Button controlVolumen;
	//public static bool silenciar = false;

//	public void PausarAudio(){
//		silenciar = !silenciar;
//		if (silenciar) {
//			apagado.gameObject.SetActive (true);
//			encendido.gameObject.SetActive (false);
//			silenciar = true;
//		} else {
//			apagado.gameObject.SetActive (false);
//			encendido.gameObject.SetActive (true);
//			silenciar = false;
//		}
////		Debug.Log (silenciar);
//	}
		
	// Use this for initialization
	void Awake () {
		if (Sonidos.reproduceSonido) {
			apagado.gameObject.SetActive (false);
			encendido.gameObject.SetActive (true);
		} else {
			apagado.gameObject.SetActive (true);
			encendido.gameObject.SetActive (false);

		}
		Debug.Log ("boton envia"+Sonidos.reproduceSonido+"en el awake");
		Sonidos.EnciedeOApAGA (Sonidos.reproduceSonido);
	}


	public void PausarAudio(){
		Sonidos.reproduceSonido = !Sonidos.reproduceSonido;
		if (Sonidos.reproduceSonido) {
			apagado.gameObject.SetActive (false);
			encendido.gameObject.SetActive (true);
		} else {
			apagado.gameObject.SetActive (true);
			encendido.gameObject.SetActive (false);

		}
		Sonidos.EnciedeOApAGA (Sonidos.reproduceSonido);
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
