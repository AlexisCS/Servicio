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


public class InterfazClinica : MonoBehaviour {

	public RectTransform[] enviaAlFinalPanel;
	public GameObject salirPanel, cerrarSesionPanel;
	public GameObject opcionesPanel;
	public GameObject descripcionDeRutinaPanel, ingresaRutinaPanel, mensajeContinuar;
	public GameObject rutinaVaciaPanel, guardarRutinaPanel, guardadoExitosoPanel, falloGuardarPanel;
	public GameObject pantallaDeCargaPanel;
	public GameObject asignaRutinaPanel;
	public GameObject confirmarAsignarRutinaPanel, asignacionExitosaPanel, falloAsignacionPanel;
	public InputField nombreDeRutina, descripcionDeRutina;
	public InputField muestraRutina;
	public InputField numeroDeRepeticiones;
	public Text advertenciaCreaRutina;
	public Text descripcionRutina;
	public Text advertenciaSeleccion;
	//public DropDownList rutinas;
	public DropDownListClinica rutinas;

	public static RutinaData myRoutineData;
	private static bool entreInterfazClinica;
	public static bool EntreInterfazClinica{
		get { return entreInterfazClinica; }
	}

	FlechasTeclado ElementoDeRutina;

	private string _urlAsignaRutina="http://132.248.16.11/unity/actualizaRutinaConSets.php"; //este script hace un update a la tabla medical_histories en el campo nombre rutina
	private string _urlRevisaSiTieneRutina="http://132.248.16.11/unity/rutinaAsignada.php"; //este php nos devuelve el nombre de la rutina que el paciente tenga asignada, en caso de no tener devuelve "Ninguna"
	private string pathStoreAllInfo=GameSaveLoad._FileLocation;
	private List<ActivaPanelDedos> _rutina;
	private StringBuilder _rutinaText;
	private bool _rutinaSeleccionada = false;
	private string _nombreDeRutinaEnviada;
	private string _nombreRutinaSeleccionada;
	private string _data, _routineData;

	void Awake(){	
		opcionesPanel.gameObject.SetActive (true);
		rutinas.Initialize ();
		_rutina = new List<ActivaPanelDedos> ();
		_rutinaText = new StringBuilder ();
		ObtieneRutinas ();
		entreInterfazClinica = false;
	}

	// Use this for initialization
	void Start () {
		

	}

	public void EnviaAlFinal(){
		enviaAlFinalPanel [0].SetAsLastSibling ();
		enviaAlFinalPanel [1].SetAsLastSibling ();
		enviaAlFinalPanel [2].SetAsLastSibling ();
	}

	public void SalirBoton (){
		salirPanel.gameObject.SetActive (true);
	}

	public void DeseaSalirBoton (){
		Application.Quit ();
	}

	public void NoDeseaSalirBoton (){
		salirPanel.gameObject.SetActive (false);
	}

	public void CerraSesionBoton(){
		cerrarSesionPanel.gameObject.SetActive (true);
	}

	public void DeseaCerrarSesion(){
		SceneManager.LoadScene (0);
	}

	public void NoDeseaCerrarSesion(){
		cerrarSesionPanel.gameObject.SetActive (false);
	}

	public void CrearRutinaBoton(){
		nombreDeRutina.text = "";
		descripcionDeRutina.text = "";
		advertenciaCreaRutina.text = "";
		opcionesPanel.gameObject.SetActive (false);
		descripcionDeRutinaPanel.gameObject.SetActive (true);
	}

	public void ContinuarRutina(){
		if (nombreDeRutina.text.Length.Equals (0)) {
			advertenciaCreaRutina.text = "Por favor, ingrese el nombre de la rutina";
			return;
		} 
		mensajeContinuar.gameObject.SetActive (true);
	}

	public void SiDeseaContinuarBoton(){
		_nombreDeRutinaEnviada = nombreDeRutina.text.ToString ();
		_nombreDeRutinaEnviada = _nombreDeRutinaEnviada.Replace (" ", ".");
		advertenciaCreaRutina.text = "";
		descripcionDeRutinaPanel.gameObject.SetActive (false);
		mensajeContinuar.gameObject.SetActive (false);
		ingresaRutinaPanel.gameObject.SetActive (true);
		ElementoDeRutina = FlechasTeclado.Cualquiera;
	}

	public void NoDeseaContinuarBoton(){
		advertenciaCreaRutina.text = "";
		mensajeContinuar.gameObject.SetActive (false);
	}

	public void GuardarRutinaBoton(){
		if (muestraRutina.text.Length.Equals (0) || _rutina.Count == 0) {
			rutinaVaciaPanel.gameObject.SetActive (true);
			ElementoDeRutina = FlechasTeclado.Ninguna;
		} else {
			guardarRutinaPanel.gameObject.SetActive (true);
			ElementoDeRutina = FlechasTeclado.Ninguna;
		}
	}

	public void SiDeseaGuardarRutina(){
		guardarRutinaPanel.gameObject.SetActive (false);
		pantallaDeCargaPanel.gameObject.SetActive (true);
		Save ();
		StartCoroutine (IfNewUploadXMLToServer ());
	}

	public void NoDeseaGuardarRutina(){
		guardarRutinaPanel.gameObject.SetActive (false);
		ElementoDeRutina = FlechasTeclado.Cualquiera;
	}

	public void AsignaRutinaBoton(){
		opcionesPanel.gameObject.SetActive (false);
		asignaRutinaPanel.gameObject.SetActive (true);
		numeroDeRepeticiones.text = "";
		rutinas.RebuildPanel ();
	}

	public void ConfirmarAsignaRutina(){
		if (!_rutinaSeleccionada) {
			advertenciaSeleccion.text = "Por favor seleccione una rutina para continuar.";
			return;
		}
		if (numeroDeRepeticiones.text.Length.Equals (0)) {
			advertenciaSeleccion.text = "Por favor ingrese el numero de repeticiones para continuar.";
			return;
		}
		confirmarAsignarRutinaPanel.gameObject.SetActive (true);
	}
		
	public void SiConfirmaAsignacion(){
		StartCoroutine (ActualizaRutina ());
		confirmarAsignarRutinaPanel.gameObject.SetActive (false);
		asignacionExitosaPanel.gameObject.SetActive (true);
	}

	public void NoConfirmaAsignacion(){
		confirmarAsignarRutinaPanel.gameObject.SetActive (false);
	}

	public void JugarBoton(){
		StartCoroutine (GetRutinasName (Admin_level0.datos.id.ToString ()));
	}

	public void CancelarBoton(){
		opcionesPanel.gameObject.SetActive (true);
		asignaRutinaPanel.gameObject.SetActive (false);
		descripcionDeRutinaPanel.gameObject.SetActive (false);
		ingresaRutinaPanel.gameObject.SetActive (false);
	}

	public void OkBoton(string panel){
		switch (panel) {
		case "rutinaVacia":
			rutinaVaciaPanel.gameObject.SetActive (false);
			break;
		case "guardadoExitoso":
			muestraRutina.text = "";
			_rutina.Clear ();
			rutinas.Items.Clear ();
			ObtieneRutinas ();
			guardadoExitosoPanel.gameObject.SetActive (false);
			ingresaRutinaPanel.gameObject.SetActive (false);
			opcionesPanel.gameObject.SetActive (true);
			break;
		case "asignacionExitosa":
			//rutinas.ItemsToDisplay.Equals (null);
			numeroDeRepeticiones.text = "";
			advertenciaSeleccion.text = "";
			//descripcionRutina.text = "\nDescripción de la rutina...";
			asignaRutinaPanel.gameObject.SetActive (false);
			asignacionExitosaPanel.gameObject.SetActive (false);
			opcionesPanel.gameObject.SetActive (true);
			break;
		}	
	}

	void ImprimeRutina (){
		_rutinaText.Remove (0,_rutinaText.Length);
		_rutinaText.Append (nombreDeRutina.text);
		_rutinaText.Append (": {");
		for (int i = 0; i <= _rutina.Count - 1; i++) {
			_rutinaText.Append (_rutina[i]);
			_rutinaText.Append (",");
		}
		_rutinaText.Append ("}");
		muestraRutina.text = _rutinaText.ToString ();
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
		FileInfo t= new FileInfo(GameSaveLoad._FileLocation + "\\" +Admin_level0.terapeuta.Id +"_"+_nombreDeRutinaEnviada+"_sandwichRutina.xml");
		//no se sobreescribira el archivo de la rutina
		Debug.Log ("no llega aqui");
		if(!t.Exists) { 
			writer = t.CreateText(); 
			writer.Write(_data); 
			writer.Close(); 
		} 
		Debug.Log("File written."); 
	} 

	IEnumerator IfNewUploadXMLToServer(){
		yield return new WaitForSeconds (5f); 
		string filePath = GameSaveLoad._FileLocation + "\\" +Admin_level0.terapeuta.Id +"_"+_nombreDeRutinaEnviada+"_sandwichRutina.xml";
		// Create a Web Form
		WWWForm form = new WWWForm();
		if (File.Exists (filePath)) {
			StreamReader r = File.OpenText (filePath); 
			string _info = r.ReadToEnd (); 
			r.Close (); 
			Debug.Log ("File Read");
			byte[] levelData =Encoding.UTF8.GetBytes(_info);
			string fileName = Admin_level0.terapeuta.Id +"_"+_nombreDeRutinaEnviada+"_sandwichRutina.xml";
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
		pantallaDeCargaPanel.gameObject.SetActive (false);
		guardadoExitosoPanel.gameObject.SetActive (true);
	}

	void ObtieneRutinas(){
		DirectoryInfo dir = new DirectoryInfo(GameSaveLoad._FileLocation);
		FileInfo[] info = dir.GetFiles("*_sandwichRutina.xml");
		foreach (FileInfo f in info)  
		{ 
			Debug.Log(f+"ruta");
			DropDownListItem temporal = new DropDownListItem();
			temporal.Caption=f.ToString().Substring(f.ToString().LastIndexOf("\\")+1);
			temporal.ID=GameSaveLoad._FileLocation+"\\"+temporal.Caption;
			temporal.Caption=temporal.Caption.Substring(0,temporal.Caption.LastIndexOf("_"));
			temporal.Caption=temporal.Caption.Substring(temporal.Caption.IndexOf("_")+1);
			rutinas.Items.Add (temporal);
		}
		rutinas.RebuildPanel ();
	}

	public void ReadRutina(string path){
		myRoutineData = new RutinaData();
		_routineData=LoadRoutineXML(path);
		if (_routineData.ToString () != "") {
			myRoutineData = (RutinaData)DeserializeObject (_routineData);
			//Admin_level0.datosNivel2.Rutina = myRoutineData.Rutina;
			AdminNivel2._secuencia = myRoutineData.Rutina;
			descripcionRutina.text="\n"+myRoutineData.DescripcionDeRutina.ToString ()+"\n";
			descripcionRutina.text += "[ ";
			for (int i = 0; i <= myRoutineData.Rutina.Count - 1; i++) {
				descripcionRutina.text += myRoutineData.Rutina [i];
				if (i < myRoutineData.Rutina.Count - 1) {
					descripcionRutina.text += ", ";
				}
			}
			descripcionRutina.text += " ]";
			_rutinaSeleccionada=true;
			_nombreRutinaSeleccionada=Admin_level0.terapeuta.Id+"_"+myRoutineData.NombreDeRutina+"_sandwichRutina.xml";
		}
	}

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

	IEnumerator ActualizaRutina(){
		if (_rutinaSeleccionada) {
			string urlString = _urlAsignaRutina + "?id=" + WWW.EscapeURL (Admin_level0.datos.id.ToString ())+"&rutina="+ WWW.EscapeURL (_nombreRutinaSeleccionada) +"&num_repeticiones="+ WWW.EscapeURL (numeroDeRepeticiones.text);
			Debug.Log("Estoy mandando"+urlString);
			WWW postName = new WWW (urlString);
			yield return postName;
			Debug.Log("La actualizacion retorno"+postName.text.ToString());
		}
	}

	private IEnumerator GetRutinasName(string id)
	{
		string urlString = _urlRevisaSiTieneRutina + "?"+"id=" + WWW.EscapeURL (id);
		WWW postName = new WWW (urlString);
		yield return postName;
		Debug.Log ("Nombre Rutina:"+postName.text.ToString()+" XD.");
		//if (!postName.text.ToString ().Contains ("Ninguna") && postName.text.ToString ().Length > 2){ 
		if (postName.text.ToString ().Length > 2) {
			Debug.Log ("Hay rutina");
			Admin_level0.RutinaAsignada = true;
			//Admin_level0.nombreRutinaAJugar = postName.text.ToString ();
			string[] rutina = postName.text.ToString ().Split ('_'); //el formato del nombre de la rutina debe ser IdDoc_NombreRutinaRutina.xml
			string nombreRutinaTemp = rutina[1].ToString ().Replace ("."," ");
			Admin_level0.datosNivel2.nombreDeRutina = nombreRutinaTemp;
			AdminNivel2.NumeroDeRepeticiones = int.Parse (rutina [3].ToString ());
			string fullPath=pathStoreAllInfo+"\\"+rutina [0].ToString () + "_" + rutina [1].ToString () + "_" + rutina [2].ToString (); 
			Debug.Log (fullPath);
			ReadRutinaToPlay (fullPath);
		}
	}

	public void ReadRutinaToPlay(string path){
		myRoutineData = new RutinaData();
		_routineData=LoadRoutineXML(path); 
		if (_routineData.ToString () != "") {
			myRoutineData = (RutinaData) DeserializeObject (_routineData);
			AdminNivel2._secuencia = myRoutineData.Rutina;
			entreInterfazClinica = true;
			SceneManager.LoadScene (1); 
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow) && ElementoDeRutina == FlechasTeclado.Cualquiera) {
			_rutina.Add (ActivaPanelDedos.Indice);
			ImprimeRutina ();
		}

		if (Input.GetKeyDown (KeyCode.RightArrow) &&ElementoDeRutina == FlechasTeclado.Cualquiera) {
			_rutina.Add (ActivaPanelDedos.Medio);
			ImprimeRutina ();
		}

		if (Input.GetKeyDown (KeyCode.DownArrow) && ElementoDeRutina == FlechasTeclado.Cualquiera) {
			_rutina.Add (ActivaPanelDedos.Anular);
			ImprimeRutina ();
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow) &&  ElementoDeRutina == FlechasTeclado.Cualquiera) {
			_rutina.Add (ActivaPanelDedos.Meñique);
			ImprimeRutina ();
		}

		if ((Input.GetKeyDown (KeyCode.Delete) || Input.GetKeyDown (KeyCode.Backspace)) && ElementoDeRutina == FlechasTeclado.Cualquiera) {
			_rutina.RemoveAt (_rutina.Count - 1);
			ImprimeRutina ();
		}
	}
}
