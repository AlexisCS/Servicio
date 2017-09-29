using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdminResultadosNivel1 : MonoBehaviour {

	public Text apiladoPan, apiladoJamon, apiladoQueso, apiladoJitomate;

	private float _convercionTemp;

	public void BotonSiguiente(){
		SceneManager.LoadScene (1);
	}

	void EntregaResultado(){
		if (Admin_level0.datos.tiempoDedoIndice <= 60f) {
			_convercionTemp = Mathf.Round (Admin_level0.datos.tiempoDedoIndice * 10f) / 10f;
			apiladoPan.text = "Pan\n-Indice-\n\n" + _convercionTemp + "\nsegundos";
		} else {
			_convercionTemp = ConvierteSegundosAMinutos (Admin_level0.datos.tiempoDedoIndice);
			apiladoPan.text = "Pan\n-Indice-\n\n" + _convercionTemp + "\nminutos";
		}

		if (Admin_level0.datos.tiempoDedoMedio <= 60f) {
			_convercionTemp = Mathf.Round (Admin_level0.datos.tiempoDedoMedio * 10f) / 10f;
			apiladoJamon.text = "Jamon\n-Medio-\n\n" + _convercionTemp + "\nsegundos";
		} else {
			_convercionTemp = ConvierteSegundosAMinutos (Admin_level0.datos.tiempoDedoMedio);
			apiladoJamon.text = "Jamon\n-Medio-\n\n" + _convercionTemp + "\nminutos";
		}

		if (Admin_level0.datos.tiempoDedoAnular <= 60f) {
			_convercionTemp = Mathf.Round (Admin_level0.datos.tiempoDedoAnular * 10f) / 10f;
			apiladoQueso.text = "Queso\n-Anular-\n\n" + _convercionTemp + "\nsegundos";
		} else {
			_convercionTemp = ConvierteSegundosAMinutos (Admin_level0.datos.tiempoDedoAnular);
			apiladoQueso.text = "Queso\n-Anular-\n\n" + _convercionTemp + "\nminutos";
		}

		if (Admin_level0.datos.tiempoDedoMeñique <= 60f) {
			_convercionTemp = Mathf.Round (Admin_level0.datos.tiempoDedoMeñique * 10f) / 10f;
			apiladoJitomate.text = "Jitomate\n-Meñique-\n\n" + _convercionTemp + "\nsegundos";
		} else {
			_convercionTemp = ConvierteSegundosAMinutos (Admin_level0.datos.tiempoDedoMeñique);
			apiladoJitomate.text = "Jitomate\n-Meñique-\n\n" + _convercionTemp + "\nminutos";
		}
	}
		
	float ConvierteSegundosAMinutos(float segundos){
		float minutos = 0;
		minutos = Mathf.Round ((segundos / 60f) * 10f) / 10f;
		return minutos;
	}
		
	void Awake(){
		EntregaResultado ();
	}
		
}
