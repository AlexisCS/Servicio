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
	public static Nivel2 datosRutina;
	public static string nombreRutinaAJugar;

	private static bool asistidoPorTerapeuta;
	public static bool AsistidoPorTerapeuta{
		get {return asistidoPorTerapeuta;}
	}

	private static bool rutinaAsignada;
	public static bool RutinaAsignada{
		get {return rutinaAsignada;}
	}

	public Text warning;
	public InputField id;
	public InputField password;
	public GameObject panelAsistidoPorTerapeuta;

	private bool _idNumerico;
	private string _routineData;

	private string _postURL1= "http://132.248.16.11/unity/validaUsuario.php"; //direccion que recibe el Id del paciente y devuelve su contraseña (fecha de nacimiento)
	private string _postURL2 =  "http://132.248.16.11/unity/index.php"; 	//direccion que recibe el Id del paciente y devuelve su nombre y apellidos
	private string _postURL3 = "http://132.248.16.11/unity/validaDoctor.php";  //me dice si existe o no existe el terapeuta
	private string _postURL6 = "http://132.248.16.11/citan/authGame/";  //valida la contraseña del terapeuta
	private string _urlRevisaSiTieneRutina="http://132.248.16.11/unity/rutinaAsignada.php"; //este php nos devuelve el nombre de la rutina que el paciente tenga asignada, en caso de no tener devuelve "Ninguna"

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
		datosRutina = new Nivel2 ();
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
			nombreRutinaAJugar = postName.text.ToString ();
			//Debug.Log (nombreRutinaAJugar);
			string[] rutina = postName.text.ToString ().Split ('_'); //el formato del nombre de la rutina debe ser IdDoc_NombreRutinaRutina.xml
			AdminNivel2.NumeroDeRepeticiones = int.Parse (rutina [3].ToString ());
			StartCoroutine (DownloadRoutine (rutina [0], rutina [0] + "_" + rutina [1] + "_" + rutina [2], rutina [1]));
		} else
			Debug.Log ("No hay rutina");
			rutinaAsignada = false;
			SceneManager.LoadScene (1);
	}

//	void ShowPatientOptionsRoutine(){
//		veryAsignarRutina_button.gameObject.SetActive (false);
//		PanelMensajeNoRutina.SetActive (false);
//		tutorial_button.gameObject.SetActive (true);
//		personaje_button.gameObject.SetActive (true);
//		asignaNivel_button.gameObject.SetActive (false);
//		if (AsistidoPorTerapeuta) {
//			jugar_button.gameObject.SetActive (false);
//			cargarRutina_button.gameObject.SetActive (true);
//			crearRutina_button.gameObject.SetActive (true);
//			seleccionarNivel_button.gameObject.SetActive (true);
//		} else {
//			jugar_button.gameObject.SetActive (true);
//			cargarRutina_button.gameObject.SetActive (false);
//			crearRutina_button.gameObject.SetActive (false);
//			seleccionarNivel_button.gameObject.SetActive (false);
//		}
//	}

//	void ShowPatientOpctionsNoRoutine(){
		
//	}

	IEnumerator DownloadRoutine(string doc_id, string name, string nameRutina){
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
			//ShowPatientOptionsRoutine ();
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

	void Asistido(){
	}

	public void NoAsistido(){
		asistidoPorTerapeuta = true;
		panelAsistidoPorTerapeuta.gameObject.SetActive (false);
		StartCoroutine (GetRutinasName (id.text.ToString ()));
	}


}
