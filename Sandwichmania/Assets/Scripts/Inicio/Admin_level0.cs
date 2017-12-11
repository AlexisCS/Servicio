using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml; 
using System.Xml.Serialization; 
using System.Text;
using System.IO;
using UnityEngine.UI;
using Ionic.Zip;
//using NUnit.Framework.Internal.Filters;

public class Admin_level0 : MonoBehaviour {

	public static InfoPartida datos;
	public static TerapeutaData terapeuta;
	public static Nivel2 datosNivel2;
	//public static string nombreRutinaAJugar;

	private static string nombreRutinaTemp;
	public static string NombreRutinaTemp{
		get { return nombreRutinaTemp; }
	}

	private static bool asistidoPorTerapeuta;
	public static bool AsistidoPorTerapeuta{
		get {return asistidoPorTerapeuta;}
	}

	private static bool rutinaAsignada;
	public static bool RutinaAsignada{
		set { rutinaAsignada = value; }
		get { return rutinaAsignada; }
	}

	public Text warning;
	public InputField id;
	public InputField password;
	public InputField idTerapeuta, passTerapeuta;
	public GameObject panelAsistidoPorTerapeuta;
	public GameObject panelIngresaTerapeuta;

	private bool _idNumerico;
	private string _routineData;

	private string _postURL1= "http://132.248.16.11/unity/validaUsuario.php"; //direccion que recibe el Id del paciente y devuelve su contraseña (fecha de nacimiento)
	private string _postURL2 =  "http://132.248.16.11/unity/index.php"; 	//direccion que recibe el Id del paciente y devuelve su nombre y apellidos
	private string _postURL3 = "http://132.248.16.11/unity/validaDoctor.php";  //me dice si existe o no existe el terapeuta
	private string _postURL6 = "http://132.248.16.11/citan/authGame/";  //valida la contraseña del terapeuta
	private string _urlRevisaSiTieneRutina="http://132.248.16.11/unity/rutinaAsignada.php"; //este php nos devuelve el nombre de la rutina que el paciente tenga asignada, en caso de no tener devuelve "Ninguna"
	private string _urlZIP = "http://132.248.16.11/unity/createZIP.php?id=";//no crea (si existen) un ZIP de todas las rutinas creadas por el terapeuta 
	private string _urlDeleteZIP="http://132.248.16.11/unity/deleteZIP.php?id="; //borra un archivo ZIP del servidor, se tiene que enviar el id del terapeuta

	private string pathStoreAllInfo=GameSaveLoad._FileLocation;
	private string _userName;

	public void Setget (){
		int i = 0;
		string usuario = id.text.ToString ();
		string contraseña = password.text.ToString ();
		_idNumerico = int.TryParse (usuario, out i);
		if(_idNumerico == true){
			StartCoroutine (VerifyPatient (usuario, contraseña));
		} else {
			StartCoroutine (VerifyDoctor (usuario, contraseña));
		}
	} 

	void Awake () {
		datos = new InfoPartida ();
		datosNivel2 = new Nivel2 ();
		_userName = " ";
		asistidoPorTerapeuta = false;
		rutinaAsignada = false;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Tab)) {
			if (!id.isFocused)
				id.Select ();
			else
				password.Select ();
		}
		if(Input.GetKeyDown (KeyCode.Return)){
			Setget ();
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
			rutinaAsignada = true;
			//nombreRutinaAJugar = postName.text.ToString ();
			//Debug.Log ("XXD" + nombreRutinaAJugar);
			string[] rutina = postName.text.ToString ().Split ('_'); //el formato del nombre de la rutina debe ser IdDoc_NombreRutinaRutina.xml
			nombreRutinaTemp = rutina[1].ToString ().Replace (".", " ");
			AdminNivel2.NumeroDeRepeticiones = int.Parse (rutina [3].ToString ()); 
			datosNivel2.nombreDeRutina = nombreRutinaTemp;
			StartCoroutine (DownloadRoutine (rutina [0], rutina [0] + "_" + rutina [1] + "_" + rutina [2]));
		} else {
			CargaJuegoSinPartidaAsignada ();
		}
	}

	void CargaJuegoSinPartidaAsignada(){
		Debug.Log ("No hay rutina");
		rutinaAsignada = false;
		SceneManager.LoadScene (1);
	}

	IEnumerator DownloadRoutine(string doc_id, string name){
		string url = "http://132.248.16.11/unity/RutinasSandwich/"+WWW.EscapeURL(doc_id)+"/"+WWW.EscapeURL (name);
		//WWWForm form = new WWWForm();
		Debug.Log(url);
		WWW ww = new WWW(url);
		yield return ww;
		Debug.Log("Descargando la rutina asignada.");
		if (ww.error == null)
		{	
			string fullPath=pathStoreAllInfo+"\\"+name; 
			File.WriteAllBytes (fullPath, ww.bytes);
			//nombreRutinaAJugar=fullPath; //esta direccion se utilizara en la escena PreJuega
			Debug.Log("Rutinaddescargadaconexito");
			ReadRutina (fullPath);
		}
		else
		{
			Debug.Log("ERROR: " + ww.error +"No hay XML en server");  //Probablemente no existe el archivo 
		}
	}

	public void ReadRutina(string path){
		Debug.Log ("Hola");
		InterfazMedico.myRoutineData = new RutinaData();
		_routineData=LoadRoutineXML(path); 
		if (_routineData.ToString () != "") {
			InterfazMedico.myRoutineData = (RutinaData) DeserializeObject (_routineData);
			AdminNivel2._secuencia = InterfazMedico.myRoutineData.Rutina;
			//AdminNivel2._secuencia = InterfazMedico.myRoutineData.Rutina;
			SceneManager.LoadScene (1);
		//if(warning)
		//		warning.gameObject.SetActive(false);
		//} else {
		//	warning.gameObject.SetActive(true);
		//	warning.text="Existe un error con el archivo.\n Por favor seleccione otra rutina.";
		//} 
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
		return _data;
	} 

	private IEnumerator VerifyPatient(string id, string password){
		//Buscando el ID en el sistema CITAN
		string urlString = _postURL2 + "?"+"id=" + WWW.EscapeURL (id);
		Admin_level0.datos.id = int.Parse (WWW.EscapeURL (id));
		GameSaveLoad._PacienteName =  Admin_level0.datos.id.ToString ();
		WWW postName = new WWW (urlString);
		yield return postName;
		//recibimos el nombre asociado al id
		Debug.Log ("Nombre asociado al ID:"+postName.text.ToString ().ToUpper()+"ok");
		_userName=postName.text.ToString ().ToUpper();
		if (_userName.Length>=3 && !_userName.Equals("INEXISTENTE")) {
			Admin_level0.datos.nombre = _userName;
			StartCoroutine(GetPassword(id, password));	//el ID existe pero comprobaremos la contraseña
		}
		else if(_userName.Length<1){
			warning.gameObject.SetActive(true);
			warning.text="El sistema CITAN no responde.\n Se recomienda iniciar sesión sin conexión.";
			Debug.LogWarning("No hay conexión con el servidor");
		}
		else {
			warning.gameObject.SetActive(true);
			warning.text="El ID ingresado no existe.\n Por favor verifique e intente nuevamente.";
			Debug.LogWarning("EL ID INGRESADO NO EXISTE");
		}
	}

	private IEnumerator GetPassword(string id,string InputPassword)
	{
		Debug.Log ("comprobando contraseña");
		string urlString = _postURL1 + "?id=" + WWW.EscapeURL (id);		
		Debug.Log ("Sending: "+urlString);
		WWW postName = new WWW (urlString);
		yield return postName;
		string realPassword = postName.text.ToString ();  //contraseña asociada al ID en CITAN
		Debug.Log ("Real password"+realPassword);
		if (InputPassword.Equals (realPassword)) {
			panelAsistidoPorTerapeuta.gameObject.SetActive (true);
			//StartCoroutine (GetRutinasName (id));
		} else {
			warning.gameObject.SetActive(true);
			warning.text="Contraseña incorrecta.\n Por favor verifique e intente nuevamente.";
		}
	}

	//corutina para que los doctores inicien sesion
	private IEnumerator VerifyDoctor(string userName,string password){
		string docName;
		string urlString = _postURL3 + "?"+"user_unity=" + WWW.EscapeURL (userName); //primero revisamos si existe el terapeuta
		Debug.Log ("Enviando"+urlString);
		WWW postName = new WWW (urlString);
		yield return postName;
		Debug.Log ("Doctor"+postName.text+"ok");
		if (postName.text.Length>4 && !postName.text.Equals("Inexistente")) {
			//_userName = postName.text.ToString ().Substring(0,postName.text.LastIndexOf("-")).ToUpper();
			docName = postName.text.ToString ().ToUpper();
			urlString = _postURL6+ WWW.EscapeURL (userName)+"/"+WWW.EscapeURL(password); //ahora revisaremos si la contraseña es correcta
			WWW postName2 = new WWW (urlString);
			yield return postName2;
			//string pass = postName.text.ToString ().Substring (postName.text.LastIndexOf ("-") + 1);
			//pass = pass.Substring (0, pass.Length-1);
			//if(password.Equals(pass)){  //Todo correcto, procesa los datos
			if(postName2.text.Equals("true")){
				terapeuta = new TerapeutaData();
				terapeuta.Nombre=docName.Substring(0,docName.LastIndexOf(" "));
				terapeuta.Id=int.Parse(docName.Substring(docName.LastIndexOf(" ")));
				//GameSaveLoad._PacienteNameWithSpaces=docName;
				//GameSaveLoad._PacienteName=docName.Replace(" ","");
				warning.gameObject.SetActive(false);
				Debug.LogWarning("TODO CORRECTO SIR!!");
				if (asistidoPorTerapeuta) {
					DescomprimeZIP ();
					yield return new WaitForSeconds (0.5f);
					SceneManager.LoadScene (11);
					yield break;
				}
				SceneManager.LoadScene (10);
				//bienvenido.text=terapeuta.Nombre;
				//opcionesCanvas.SetActive(true);
				//inicioCanvas.SetActive(false);
				//cerrarSesion_button.gameObject.SetActive(true);
				//crearRutina_button.interactable=true;
				//crearRutina_button.gameObject.SetActive(true);
				//seleccionarNivel_button.gameObject.SetActive(false);
				//cargarRutina_button.gameObject.SetActive(false);
				//veryAsignarRutina_button.gameObject.SetActive(true);
				//asignaNivel_button.gameObject.SetActive (true);
				//jugar_button.gameObject.SetActive(false);
				//tutorial_button.gameObject.SetActive(false);
				//personaje_button.gameObject.SetActive(false);
				//IsMedico=true;
				//inicioSesion=true;
				//Online=true;
				//DescomprimeZIP(); //descargamos del servidor las rutinas que haya creado el terapeuta
			}
			else{
				warning.gameObject.SetActive(true);
				warning.text="Contraseña incorrecta.\n Por favor verifique e intente nuevamente.";
			}
		} else {
			warning.gameObject.SetActive(true);
			warning.text="El usuario no existe.\n Por favor verifique e intente nuevamente.";
		}
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
				File.Delete(fullPath); //borramos el ZIP
				//borramos el ZIP del servidor
				url = _urlDeleteZIP+doc_id;
				WWW postName2 = new WWW (url);
				yield return postName2;

			}
		}
	}

	public void DescomprimeZIP(){
		StartCoroutine (DownloadDocRoutines(Admin_level0.terapeuta.Id));
		//string zipfilePath = "C:/Users/Yoás/Desktop/YoisExample.zip";
		//string exportLocation = "C:/Users/Yoás/Desktop/";
		//ZipUtil.Unzip ( zipfilePath, exportLocation);

	}


	public void Asistido(){
		asistidoPorTerapeuta = true;
		panelAsistidoPorTerapeuta.gameObject.SetActive (false);
		panelIngresaTerapeuta.gameObject.SetActive (true);
	}
		

	public void NoAsistido(){
		asistidoPorTerapeuta = false;
		panelAsistidoPorTerapeuta.gameObject.SetActive (false);
		StartCoroutine (GetRutinasName (id.text.ToString ()));
	}

	public void IngresaTerapeuta(){
		StartCoroutine (VerifyDoctor (idTerapeuta.text.ToString (), passTerapeuta.text.ToString ()));
	}
		
}
