using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using NUnit.Framework.Internal.Filters;

public class Admin_level0 : MonoBehaviour {

	public static InfoPartida datos;
	public static TerapeutaData terapeuta;

//	public GameObject PanelAutenticaMedico;
	public Text warning;
	public InputField id;
	public InputField password;

	private bool _idNumerico;

	private string _postURL1= "http://132.248.16.11/unity/validaUsuario.php"; //direccion que recibe el Id del paciente y devuelve su contraseña (fecha de nacimiento)
	private string _postURL2 =  "http://132.248.16.11/unity/index.php"; 	//direccion que recibe el Id del paciente y devuelve su nombre y apellidos
	private string _postURL3 = "http://132.248.16.11/unity/validaDoctor.php";  //me dice si existe o no existe el terapeuta
	private string _postURL6 = "http://132.248.16.11/citan/authGame/";  //valida la contraseña del terapeuta
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
		_userName = " ";
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
			SceneManager.LoadScene (1);
			//PanelAutenticaMedico.SetActive (true);	
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

}
