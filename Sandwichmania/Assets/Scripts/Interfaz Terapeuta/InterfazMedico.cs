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
using Ionic.Zip;

enum FlechasTeclado {Ninguna, Cualquiera}

public class InterfazMedico : MonoBehaviour {

	private string _postURL1= "http://132.248.16.11/unity/verPacientes.php";
	private string _urlAsignaRutina="http://132.248.16.11/unity/actualizaRutinaConSets.php"; //este script hace un update a la tabla medical_histories en el campo nombre rutina

	public RectTransform[] panel;
	public GameObject[] interfaz;
	public InputField nombreDeRutina, descripcionDeRutina;
	public InputField muestraRutina;
	public InputField numeroDeRepeticiones;
	public Text advertencia;
	public Text advertenciaSeleccion;
	public Text nombreTerapeuta;
	public Text descripcionRutina;
	public DropDownList rutinas;
	public DropDownList pacientes;

	//De este objeto se leeran las caracteristicas y parametros de la rutina en la sig. escena
	public static RutinaData myRoutineData;

	private  int repeticiones;
	private bool _rutinaSeleccionada = false;
	private bool _pacienteSeleccionado = false;
	private string _nombreRutinaSeleccionada;
	private string _idPacienteSeleccionado;
	private string _nombrePacienteSleccionado;
	private string namesP;
	private string _urlZIP = "http://132.248.16.11/unity/createZIP.php?id=";//no crea (si existen) un ZIP de todas las rutinas creadas por el terapeuta 
	private string _urlDeleteZIP="http://132.248.16.11/unity/deleteZIP.php?id="; //borra un archivo ZIP del servidor, se tiene que enviar el id del terapeuta
	private string pathStoreAllInfo=GameSaveLoad._FileLocation;

	private List<ActivaPanelDedos> _rutina = new List<ActivaPanelDedos> ();
	private string _nombresPacientes;
	string _data, _routineData;

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

	void Start(){
		DescomprimeZIP ();
	}


	public void UltimosHijos(){
		panel [0].SetAsLastSibling ();
		panel [1].SetAsLastSibling ();
	}

	void ObtieneRutinas(){
		DirectoryInfo dir = new DirectoryInfo(GameSaveLoad._FileLocation);
		FileInfo[] info = dir.GetFiles("*_sandwichRutina.xml");
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

//	public void AsignarRutina(){
////		if (!_pacienteSeleccionado) {
////			//warning.gameObject.SetActive (true);
////			//warning.text = "Por favor seleccione un paciente para continuar.";
////			advertenciaSeleccion.text = "Por favor seleccione un paciente para continuar.";
////			return;
////		}
////		if (!_rutinaSeleccionada) {
////			//warning.gameObject.SetActive (true);
////			//warning.text = "Por favor seleccione una rutina para continuar.";
////			advertenciaSeleccion.text = "Por favor seleccione una rutina para continuar.";
////			return;
////		}
////		
//	}

	IEnumerator IfNewUploadXMLToServer(){
		yield return new WaitForSeconds (5f); 
		string filePath = GameSaveLoad._FileLocation + "\\" +Admin_level0.terapeuta.Id +"_"+nombreDeRutina.text+"_sandwichRutina.xml";
		// Create a Web Form
		WWWForm form = new WWWForm();
		if (File.Exists (filePath)) {
			StreamReader r = File.OpenText (filePath); 
			string _info = r.ReadToEnd (); 
			r.Close (); 
			Debug.Log ("File Read");
			byte[] levelData =Encoding.UTF8.GetBytes(_info);
			string fileName = Admin_level0.terapeuta.Id +"_"+nombreDeRutina.text+"_sandwichRutina.xml";
			form.AddField("file","file");
			form.AddBinaryData ( "file", levelData, fileName,"text/xml");
			Debug.Log("sin error");
			Debug.Log (fileName);
			WWW w = new WWW("http://132.248.16.11/unity/uploadXML.php",form);
			print("www created");
			yield return w;
			yield return new WaitForSeconds (5f);
			Debug.Log("enviado");
			Debug.Log("Me responde"+w.text.ToString());
		} else {
			Debug.Log("File Doesnt exist");
		}
		yield return new WaitForSeconds (5f);
		interfaz [8].gameObject.SetActive (true);
		interfaz [13].gameObject.SetActive (false);
//		panel_espereUnMomento.SetActive (false);
//		panel_final.SetActive (true);
	}

	IEnumerator ActualizaRutina(){
		if (_pacienteSeleccionado && _rutinaSeleccionada) {
			string urlString = _urlAsignaRutina + "?id=" + WWW.EscapeURL (_idPacienteSeleccionado)+"&rutina="+ WWW.EscapeURL (_nombreRutinaSeleccionada) +"&num_repeticiones="+ WWW.EscapeURL (numeroDeRepeticiones.text);
			Debug.Log("Estoy mandando"+urlString);
			WWW postName = new WWW (urlString);
			yield return postName;
			Debug.Log("La actualizacion retorno"+postName.text.ToString());
		}
	}


	public void GetPatients(){
		ActualizaDropList (namesP);

	}

	private void ActualizaDropList(string postName){

		string[] patients = postName.Split (';');
		foreach (string p in patients)
		{
			if(p.Length>2){
				Debug.Log(p+"paciente");
				DropDownListItem etem=new DropDownListItem();
				etem.Caption=p.ToString().Substring(0,p.LastIndexOf("_"));
				//aaqui tengo que poner el id del paceinte
				etem.ID=p.ToString().Substring(p.LastIndexOf("_")+1);
				pacientes.Items.Add(etem);
			}
		}
		Debug.Log (postName);
		Debug.Log ("termine");
		pacientes.RebuildPanel ();

	}
	//esta función se manda llamar desde el script DropDownList.cs al momento de seleccionar un paciente de la lista desplegable
	public void RegisterPatientId(String patientId, String patientName){
		_pacienteSeleccionado = true;
		_idPacienteSeleccionado = patientId;
		_nombrePacienteSleccionado = patientName;
	}

	public void ReadRutina(string path){
		myRoutineData = new RutinaData();
		_routineData=LoadRoutineXML(path);
		if (_routineData.ToString () != "") {
			myRoutineData = (RutinaData)DeserializeObject (_routineData);
			descripcionRutina.text="\n"+myRoutineData.DescripcionDeRutina.ToString ()+"\n";
			for (int i = 0; i <= myRoutineData.Rutina.Count - 1; i++) {
				descripcionRutina.text += myRoutineData.Rutina [i] +", ";
			}
			_rutinaSeleccionada=true;
			_nombreRutinaSeleccionada=Admin_level0.terapeuta.Id+"_"+myRoutineData.NombreDeRutina+"_sandwichRutina.xml";
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
			return;
		} 
		string temp = nombreDeRutina.text.ToString ();
		temp = temp.Replace (" ", ".");
		nombreDeRutina.text = temp;
		advertencia.text = "";
		interfaz [2].gameObject.SetActive (true);
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
		if (!_pacienteSeleccionado) {
			advertenciaSeleccion.text = "Por favor seleccione un paciente para continuar.";
			return;
		}
		if (!_rutinaSeleccionada) {
			advertenciaSeleccion.text = "Por favor seleccione una rutina para continuar.";
			return;
		}
		if (numeroDeRepeticiones.text.Length.Equals (0)) {
			advertenciaSeleccion.text = "Por favor ingrese el numero de repeticiones para continuar.";
			return;
		}
		interfaz [7].gameObject.SetActive (true);
	}

	public void BotonSiContinuarRutina(){
		interfaz [1].gameObject.SetActive (false);
		interfaz [2].gameObject.SetActive (false);
		interfaz [3].gameObject.SetActive (true);
		ElementoRutina = FlechasTeclado.Cualquiera;
	}

	public void BotonNoContinuarRutina(){
		string temp = nombreDeRutina.text.ToString ();
		temp = temp.Replace (".", " ");
		nombreDeRutina.text = temp;
		interfaz [2].gameObject.SetActive (false);
	}

	public void BotonSiGuardaRutina(){
		interfaz [13].gameObject.SetActive (true);
		interfaz [6].gameObject.SetActive (false);
		Save ();
		StartCoroutine (IfNewUploadXMLToServer ());
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
		case 3:
			interfaz [11].gameObject.SetActive (false);
			break;
		case 4:
			interfaz [12].gameObject.SetActive (false);
			break;
		}
	}

	public void BotonSiAsignaRutina(){
		interfaz [9].gameObject.SetActive (true);
		StartCoroutine (ActualizaRutina());
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
		myData.Rutina = _rutina;
		_data = SerializeObject(myData); 
		CreateXML(); 
		return;
	} 

	string SerializeObject(object pObject){ 
		string XmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		XmlSerializer xs = new XmlSerializer(typeof(RutinaData)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString; 
	} 

	static	string UTF8ByteArrayToString(byte[] characters){      
		UTF8Encoding encoding = new UTF8Encoding(); 
		string constructedString = encoding.GetString(characters); 
		return (constructedString); 
	} 

	// Finally our save and load methods for the file itself 
	void CreateXML() { 
		StreamWriter writer; 
		//FileInfo t = new FileInfo(GameSaveLoad._FileLocation+"\\"+"Terapeuta"+count+"_"+"ID"+"_PMRutina.xml"); 
		FileInfo t= new FileInfo(GameSaveLoad._FileLocation + "\\" +Admin_level0.terapeuta.Id +"_"+nombreDeRutina.text.ToString ()+"_sandwichRutina.xml");
		//no se sobreescribira el archivo de la rutina
		Debug.Log ("no llega aqui");
		if(!t.Exists) { 
			writer = t.CreateText(); 
			writer.Write(_data); 
			writer.Close(); 
		} 
		Debug.Log("File written."); 
	} 

	void EntregaNombreTerapeuta(){
		nombreTerapeuta.text = Admin_level0.terapeuta.Nombre.ToString ();
	}

	public void DescomprimeZIP(){
		StartCoroutine (DownloadDocRoutines(Admin_level0.terapeuta.Id));
		//string zipfilePath = "C:/Users/Yoás/Desktop/YoisExample.zip";
		//string exportLocation = "C:/Users/Yoás/Desktop/";
		//ZipUtil.Unzip ( zipfilePath, exportLocation);

	}

	IEnumerator DownloadDocRoutines(int doc_id){
		string url = _urlZIP+ doc_id+"&juego='sandwich'";
		Debug.Log ("DEntro");
		WWW postName = new WWW (url);
		yield return postName;		//creamos el ZIP de las rutinas que ha creado el terapeuta se llama id_doc.zip ej. 13.zip
		//yield return new WaitForSeconds (5f);
		Debug.Log (postName.text.ToString() + "Hola");
		if (postName.text.ToString ().Contains ("ZIPcreated")) { //si existen rutinas creadas por el terapeuta descargamos el zip de esas rutinas
			url = "http://132.248.16.11/unity/"+doc_id+".zip";
			//WWWForm form = new WWWForm();
			WWW ww = new WWW(url);
			yield return ww; //aqui ya descargamos el zip
			if (ww.error == null)
			{	
				string fullPath = pathStoreAllInfo+"\\"+doc_id+".zip"; //ruta donde guardamos el ZIP que descargamos
				File.WriteAllBytes (fullPath, ww.bytes);
				string exportLocation = pathStoreAllInfo+"\\"; 	//ruta donde extraemos el contenido del ZIP ej. "C:/Users/Yoás/Desktop//"	
				ZipUtil.Unzip ( fullPath, exportLocation);
				//				using(ZipFile zip=ZipFile.Read(fullPath)){
				//					//extrayendo
				//					foreach(ZipEntry z in zip){
				//						z.Extract(exportLocation,ExtractExistingFileAction.OverwriteSilently);
				//					}
				//				}

				//yield return new WaitForSeconds(2f);
				File.Delete(fullPath); //borramos el ZIP
				//borramos el ZIP del servidor
				url = _urlDeleteZIP+doc_id;
				WWW postName2 = new WWW (url);
				yield return postName2;
			}
		}
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
	public List <ActivaPanelDedos> Rutina;
}