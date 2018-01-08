using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

public class AdminResultadosNivel1 : MonoBehaviour {

	public Text apiladoPan, apiladoJamon, apiladoQueso, apiladoJitomate;
	public List <Nivel1> temp = new List<Nivel1> ();

	private float _convercionTemp;

	public void BotonSiguiente(){
		SceneManager.LoadScene (1);
	}

	void EntregaResultado(){
		if (AdminMenu.datosNivel1.tiempoDedoIndice <= 60f) {
			_convercionTemp = Mathf.Round (AdminMenu.datosNivel1.tiempoDedoIndice * 10f) / 10f;
			apiladoPan.text = "Pan\n-Indice-\n\n" + _convercionTemp + "\nsegundos";
		} else {
			_convercionTemp = ConvierteSegundosAMinutos (AdminMenu.datosNivel1.tiempoDedoIndice);
			apiladoPan.text = "Pan\n-Indice-\n\n" + _convercionTemp + "\nminutos";
		}

		if (AdminMenu.datosNivel1.tiempoDedoMedio <= 60f) {
			_convercionTemp = Mathf.Round (AdminMenu.datosNivel1.tiempoDedoMedio * 10f) / 10f;
			apiladoJamon.text = "Jamon\n-Medio-\n\n" + _convercionTemp + "\nsegundos";
		} else {
			_convercionTemp = ConvierteSegundosAMinutos (AdminMenu.datosNivel1.tiempoDedoMedio);
			apiladoJamon.text = "Jamon\n-Medio-\n\n" + _convercionTemp + "\nminutos";
		}

		if (AdminMenu.datosNivel1.tiempoDedoAnular <= 60f) {
			_convercionTemp = Mathf.Round (AdminMenu.datosNivel1.tiempoDedoAnular * 10f) / 10f;
			apiladoQueso.text = "Queso\n-Anular-\n\n" + _convercionTemp + "\nsegundos";
		} else {
			_convercionTemp = ConvierteSegundosAMinutos (AdminMenu.datosNivel1.tiempoDedoAnular);
			apiladoQueso.text = "Queso\n-Anular-\n\n" + _convercionTemp + "\nminutos";
		}

		if (AdminMenu.datosNivel1.tiempoDedoMeñique <= 60f) {
			_convercionTemp = Mathf.Round (AdminMenu.datosNivel1.tiempoDedoMeñique * 10f) / 10f;
			apiladoJitomate.text = "Jitomate\n-Meñique-\n\n" + _convercionTemp + "\nsegundos";
		} else {
			_convercionTemp = ConvierteSegundosAMinutos (AdminMenu.datosNivel1.tiempoDedoMeñique);
			apiladoJitomate.text = "Jitomate\n-Meñique-\n\n" + _convercionTemp + "\nminutos";
		}
	}
		
	float ConvierteSegundosAMinutos(float segundos){
		float minutos = 0;
		minutos = Mathf.Round ((segundos / 60f) * 10f) / 10f;
		return minutos;
	}

	void RedondeaSegundos(){
		AdminMenu.datosNivel1.tiempoDedoIndice = DelimitaSegundos (AdminMenu.datosNivel1.tiempoDedoIndice); 
		AdminMenu.datosNivel1.tiempoDedoMedio = DelimitaSegundos (AdminMenu.datosNivel1.tiempoDedoMedio);
		AdminMenu.datosNivel1.tiempoDedoAnular = DelimitaSegundos (AdminMenu.datosNivel1.tiempoDedoAnular);
		AdminMenu.datosNivel1.tiempoDedoMeñique = DelimitaSegundos (AdminMenu.datosNivel1.tiempoDedoMeñique);
	}

	float DelimitaSegundos(float segundos){
		float segundosConUnDecimal;
		segundosConUnDecimal = Mathf.Round (segundos * 10f) / 10f;
		return segundosConUnDecimal;
	}


	void Awake(){
		EntregaResultado ();
		RedondeaSegundos ();
		Admin_level0.datos.HistorialPartidasNivel1.Add (AdminMenu.datosNivel1);
		GameSaveLoad.Save (Admin_level0.datos);
		StartCoroutine (GameSaveLoad.IfNewUploadXMLToServer ());
	}
		
}
