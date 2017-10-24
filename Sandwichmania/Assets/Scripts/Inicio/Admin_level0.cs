using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using NUnit.Framework.Internal.Filters;

public class Admin_level0 : MonoBehaviour {

	public static InfoPartida datos;

//	public GameObject PanelAutenticaMedico;
	public Text warning;
	public InputField id;
	public InputField password;

	private string _postURL1= "http://132.248.16.11/unity/validaUsuario.php"; //direccion que recibe el Id del paciente y devuelve su contraseña (fecha de nacimiento)
	private string _postURL2 =  "http://132.248.16.11/unity/index.php"; 	//direccion que recibe el Id del paciente y devuelve su nombre y apellidos
	private string _userName;

	public void Setget (){
		string usuario = id.text.ToString ();
		string contraseña = password.text.ToString ();
		StartCoroutine (VerifyPatient (usuario, contraseña));
	} 

	void Awake () {
		datos = new InfoPartida ();
		_userName = " ";
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

}
