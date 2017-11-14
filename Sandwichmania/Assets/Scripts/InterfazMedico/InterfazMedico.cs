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
using UnityEngine.UI.Extensions;

public class InterfazMedico : MonoBehaviour {

	private string _postURL1= "http://132.248.16.11/unity/verPacientes.php";
	private string _urlAsignaRutina="http://132.248.16.11/unity/actualizaRutina.php"; //este script hace un update a la tabla medical_histories en el campo nombre rutina

	public GameObject[] interfaz;
	public InputField nombreDeRutina, descripcionDeRutina;
	public InputField muestraRutina;
	public Text advertencia;
	public Text nombreTerapeuta;
	public Text descripcionRutina;
	public DropDownList rutinas;
	public DropDownList pacientes;

	//De este objeto se leeran las caracteristicas y parametros de la rutina en la sig. escena
	public static RutinaData myRoutineData;

	private bool rutinaSeleccionada=false;
	private bool pacienteSeleccionado=false;
	private string idPacienteSeleccionado;
	private string _nombreRutinaSeleccionada;

	private List<ActivaPanelDedos> _rutina = new List<ActivaPanelDedos> ();
	private string _nombresPacientes;
	string _data, _routineData;
	enum FlechasTeclado {Ninguna, Cualquiera, Arriba, Derecha, Abajo, Izquierda}
	FlechasTeclado ElementoRutina;

	void Awake(){
		pacientes.Initialize ();
		rutinas.Initialize ();
		EntregaNombreTerapeuta ();
		_nombresPacientes = "";
		try {
			StartCoroutine (GetPatientsFromDoc(Admin_level0.terapeuta.Id.ToString ()));
		} catch (Exception e) {
			Debug.Log ("Problemas obteniendo pacientes del doc.");
		}
		//primero obtenemos todos los ducumentos que contengan rutinas
		ObtieneRutinas();
	}

	void ObtieneRutinas(){
		DirectoryInfo dir = new DirectoryInfo(GameSaveLoad._FileLocation);
		FileInfo[] info = dir.GetFiles("*_SandwichmaniaRutina.xml");
		foreach (FileInfo f in info)  
		{ 
			Debug.Log(f+"ruta");
			DropDownListItem temporal = new DropDownListItem();
			//atem.ID=f.ToString();   //<=no me esta leyendo esta direccion en el ejecutable
			temporal.Caption=f.ToString().Substring(f.ToString().LastIndexOf("\\")+1);
			temporal.ID=GameSaveLoad._FileLocation+"\\"+temporal.Caption;
			temporal.Caption=temporal.Caption.Substring(0,temporal.Caption.LastIndexOf("_"));
			temporal.Caption=temporal.Caption.Substring(temporal.Caption.IndexOf("_")+1);
			rutinas.Items.Add (temporal);
		}
		rutinas.RebuildPanel ();
	}

	private IEnumerator GetPatientsFromDoc(string id){
		Debug.Log ("Obteniendo pacientes del Doc.");
		string urlString = _postURL1 + "?"+"id_doc=" + WWW.EscapeURL (id);
		WWW postName = new WWW (urlString);
		yield return postName;
		Debug.Log ("Pacientes:"+postName.text.ToString()+"FIN");
		//namesP = postName.text.ToString ();

		string[] patients = postName.text.ToString().Split (';');
		foreach (string p in patients) 
		{ 
			if(p.Length>2){
				Debug.Log(p+"paciente");
				DropDownListItem etemporal=new DropDownListItem();
				etemporal.Caption=p.ToString().Substring(0,p.LastIndexOf("_"));
				//aaqui tengo que poner el id del paceinte
				etemporal.ID=p.ToString().Substring(p.LastIndexOf("_")+1);
				pacientes.Items.Add (etemporal);
			}
		}
		Debug.Log (postName);
		Debug.Log ("termine");
		pacientes.RebuildPanel ();


		Debug.Log ("termine");
	}

	//esta función se manda llamar desde el script DropDownList.cs al momento de seleccionar un paciente de la lista desplegable
	public void RegisterPatientId(String patientId, String patientName){
		//warning.gameObject.SetActive (false);
		///pacienteSeleccionado = true;
		//idPacienteSeleccionado = patientId;
		//nombrePacienteSeleccionado = patientName;
		//nombrePacienteSeleccionadoFile = patientName.ToUpper().Replace(" ",string.Empty)+"Data.xml";
		//Debug.Log ("NOmbre del archvo que debo buscar"+nombrePacienteSeleccionado);
		//Debug.Log (idPacienteSeleccionado);
	}

	public void ReadRutina(string path){
		myRoutineData = new RutinaData();
		_routineData=LoadRoutineXML(path);
		if (_routineData.ToString () != "") {
			myRoutineData = (RutinaData)DeserializeObject (_routineData);
			//Debug.Log(myRoutineData.Descripcion.ToString());
			//if(descripcionRutina)
				descripcionRutina.text="\n"+myRoutineData.DescripcionDeRutina.ToString ()+"\n";
//			if(nivelesRutina_text)
//				nivelesRutina_text.text="No. de sets que contiene la rutina: "+myRoutineData.CantidadDeSets.ToString();
//			if(tiempoDescansoRutina_text)
//				tiempoDescansoRutina_text.text="Tiempo de descanso entre sets: "+myRoutineData.TiempoDeDescansoEntreSets.ToString()+"s.";
			//			if(!Application.loadedLevelName.Equals("PreVisualizaRutina")){
			//				if(myRoutineData.dispo.Equals(RutinaData.Dispositivo.KinectV2))
			//					onlyKinectV2Panel.SetActive(true);
			//				else
			//					onlyKinectV2Panel.SetActive(false);
			//			}
			rutinaSeleccionada=true;
			_nombreRutinaSeleccionada=Admin_level0.terapeuta.Id+"_"+myRoutineData.NombreDeRutina+"_SandwichmaniaRutina.xml";
//			if(warning)
//				warning.gameObject.SetActive(false);
//		} else {
//			rutinaSeleccionada=false;
//			warning.gameObject.SetActive(true);
//			warning.text="Existe un error con el archivo.\n Por favor seleccione otra rutina.";
		}
	}

	// Here we deserialize it back into its original form
	public object DeserializeObject(string pXmlizedString)
	{
		XmlSerializer xs = new XmlSerializer(typeof(RutinaData));
		MemoryStream memoryStream = new MemoryStream(GameSaveLoad.StringToUTF8ByteArray(pXmlizedString));
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
		return xs.Deserialize(memoryStream);
	}

	static string LoadRoutineXML(string routinePath)
	{
		StreamReader r = File.OpenText(routinePath);
		string _info = r.ReadToEnd();
		r.Close();
		string _data=_info;
		Debug.Log("Routine Read");
		return _data;
	}

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
			interfaz [0].gameObject.SetActive (true);
			interfaz [3].gameObject.SetActive (false);
			interfaz [8].gameObject.SetActive (false);
			muestraRutina.text = "";
			_rutina.Clear ();
			rutinas.Items.Clear ();
			ObtieneRutinas ();
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

	public void CerrarSesion(){
		interfaz [10].gameObject.SetActive (true);
	}

	public void BotonSiCerraSesion(){
		SceneManager.LoadScene (0);
	}

	public void BotonNoCerraSesion(){
		interfaz [10].gameObject.SetActive (false);
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
		//FileInfo t = new FileInfo(GameSaveLoad._FileLocation+"\\"+"Terapeuta"+count+"_"+"ID"+"_PMRutina.xml"); 
		FileInfo t= new FileInfo(GameSaveLoad._FileLocation + "\\" +Admin_level0.terapeuta.Id +"_"+nombreDeRutina.text+"_SandwichmaniaRutina.xml");
		//no se sobreescribira el archivo de la rutina
		if(!t.Exists) 
		{ 
			writer = t.CreateText(); 
			writer.Write(_data); 
			writer.Close(); 
		} 
		Debug.Log("File written."); 
	} 

	void EntregaNombreTerapeuta(){
//		nombreTerapeuta.text = Admin_level0.terapeuta.Nombre.ToString ();
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