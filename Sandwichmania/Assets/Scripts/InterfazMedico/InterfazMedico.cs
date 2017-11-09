using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Collections;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Xml; 
using System.Xml.Serialization;  
using System.Text;

public class InterfazMedico : MonoBehaviour {

	public GameObject[] interfaz;
	public InputField nombreDeRutina, descripcionDeRutina;
	public InputField muestraRutina;
	public Text advertencia;

	private List<ActivaPanelDedos> _rutina = new List<ActivaPanelDedos> ();
	string _data;
	int count = 0;
	enum FlechasTeclado {Ninguna, Cualquiera, Arriba, Derecha, Abajo, Izquierda}
	FlechasTeclado ElementoRutina;

	public void SeleccionaCategoria (int categoria){
		switch (categoria) {
		case 0: // Crear Rutina
			nombreDeRutina.text = "";
			descripcionDeRutina.text = "";
			advertencia.text = "";
			interfaz [0].gameObject.SetActive (false);
			interfaz [1].gameObject.SetActive (true);
			break;
		case 1: // Asignar Rutina
			interfaz [0].gameObject.SetActive (false);
			interfaz[4].gameObject.SetActive (true);
			break;
		case 2: // Ver Resultados
			//SceneManager.LoadScene (); // Escena Resultados
			break;
		}
	}

	public void CancelarBoton (int seleccion){
		switch (seleccion) {
		case 0:
			interfaz [0].gameObject.SetActive (true);
			interfaz [1].gameObject.SetActive (false);
			break;
		case 1:
			interfaz [0].gameObject.SetActive (true);
			interfaz [3].gameObject.SetActive (false);
			break;
		case 2:
			interfaz [0].gameObject.SetActive (true);
			interfaz[4].gameObject.SetActive (false);
			break;
		}
	}

	public void ContinuarRutina(){
		if (nombreDeRutina.text.Length.Equals (0)) {
			advertencia.text = "Por favor, ingrese el nombre de la rutina";
		} else {
			advertencia.text = "";
			interfaz [2].gameObject.SetActive (true);
		}
	}

	public void GuardarRutinaBoton(){
		if (muestraRutina.text.Length.Equals (0) || _rutina.Count == 0) {
			interfaz [5].gameObject.SetActive (true);
			ElementoRutina = FlechasTeclado.Ninguna;
		} else {
			interfaz [6].gameObject.SetActive (true);
			ElementoRutina = FlechasTeclado.Ninguna;
		}
	}

	public void AsignaRutinaBoton(){
		interfaz [7].gameObject.SetActive (true);
	}

	public void BotonSiContinuarRutina(){
		interfaz [1].gameObject.SetActive (false);
		interfaz [2].gameObject.SetActive (false);
		interfaz [3].gameObject.SetActive (true);
		ElementoRutina = FlechasTeclado.Cualquiera;
	}

	public void BotonNoContinuarRutina(){
		interfaz [2].gameObject.SetActive (false);
	}

	public void BotonSiGuardaRutina(){
		interfaz [8].gameObject.SetActive (true);
		interfaz [6].gameObject.SetActive (false);
	}

	public void BotonNoGuardaRutina(){
		interfaz [6].gameObject.SetActive (false);
		ElementoRutina = FlechasTeclado.Cualquiera;
	}

	public void BotonOk(int seccion){
		switch (seccion) {
		case 0:
			interfaz [5].gameObject.SetActive (false);
			ElementoRutina = FlechasTeclado.Cualquiera;
			break;
		case 1:
			Save ();
			count += 1;
			interfaz [0].gameObject.SetActive (true);
			interfaz [3].gameObject.SetActive (false);
			interfaz [8].gameObject.SetActive (false);
			muestraRutina.text = "";
			_rutina.Clear ();
			break;
		case 2:
			interfaz [0].gameObject.SetActive (true);
			interfaz [4].gameObject.SetActive (false);
			interfaz [9].gameObject.SetActive (false);
			break;
		}
	}

	public void BotonSiAsignaRutina(){
		interfaz [9].gameObject.SetActive (true);
	}

	public void BotonNoAsignaRutina(){
		interfaz [7].gameObject.SetActive (false);
	}

	// Use this for initialization
	void Start () {
	}

	void ImprimeRutina(){
		muestraRutina.text = nombreDeRutina.text+": {";
		for (int i = 0; i <= _rutina.Count - 1; i++) {
			muestraRutina.text+= _rutina[i] + ",";
		}
		muestraRutina.text+= "}";
	}


	void Save(){ 
		RutinaData myData = new RutinaData();
		myData.NombreDeRutina = nombreDeRutina.text.ToString ();
		myData.DescripcionDeRutina = descripcionDeRutina.text.ToString ();
		myData.NumeroDeRepeticiones = 3;
		myData.Rutina = _rutina;
		//		if (RutinaOnlyForKinectV2) {
		//			myData.dispo = RutinaData.Dispositivo.KinectV2;
		//		}
		//		else
		//			myData.dispo = RutinaData.Dispositivo.Any;
		_data = SerializeObject(myData); 
		// This is the final resulting XML from the serialization process 
		CreateXML(); 
		return;
	} 

	string SerializeObject(object pObject) 
	{ 
		string XmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		XmlSerializer xs = new XmlSerializer(typeof(RutinaData)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString; 
	} 

	static	string UTF8ByteArrayToString(byte[] characters) 
	{      
		UTF8Encoding encoding = new UTF8Encoding(); 
		string constructedString = encoding.GetString(characters); 
		return (constructedString); 
	} 

	// Finally our save and load methods for the file itself 
	void CreateXML() 
	{ 
		StreamWriter writer; 
		FileInfo t = new FileInfo(GameSaveLoad._FileLocation+"\\"+"Terapeuta"+count+"_"+"ID"+"_PMRutina.xml"); 
		//no se sobreescribira el archivo de la rutina
		if(!t.Exists) 
		{ 
			writer = t.CreateText(); 
			writer.Write(_data); 
			writer.Close(); 
		} 
		Debug.Log("File written."); 
	} 

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow) && ElementoRutina == FlechasTeclado.Cualquiera) {
			_rutina.Add ( ActivaPanelDedos.Indice);
			ImprimeRutina ();
		}

		if (Input.GetKeyDown (KeyCode.RightArrow) &&ElementoRutina == FlechasTeclado.Cualquiera) {
			_rutina.Add ( ActivaPanelDedos.Medio);
			ImprimeRutina ();
		}

		if (Input.GetKeyDown (KeyCode.DownArrow) && ElementoRutina == FlechasTeclado.Cualquiera) {
			_rutina.Add ( ActivaPanelDedos.Anular);
			ImprimeRutina ();
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow) &&  ElementoRutina == FlechasTeclado.Cualquiera) {
			_rutina.Add ( ActivaPanelDedos.Meñique);
			ImprimeRutina ();
		}

		if ((Input.GetKeyDown (KeyCode.Delete) || Input.GetKeyDown (KeyCode.Backspace)) && ElementoRutina == FlechasTeclado.Cualquiera) {
			_rutina.RemoveAt (_rutina.Count - 1);
			ImprimeRutina ();
		}
	}
}

public class RutinaData{
	public string NombreDeRutina;
	public string DescripcionDeRutina;
	public int NumeroDeRepeticiones;
	public List <ActivaPanelDedos> Rutina;
}