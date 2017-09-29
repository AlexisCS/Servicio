using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NUnit.Framework.Internal.Filters;

public class Admin_level0 : MonoBehaviour {

	public static InfoPartida datos;

	public InputField id;
	public InputField password;
	public Text message;

	public void Setget (){
		if (!(id.text.ToString().Equals ("a")) || !(password.text.ToString ().Equals ("123"))) {
			message.text = "Usuario o contraseña incorrecta";
		} else {
			Admin_level0.datos.id = id.text;
			SceneManager.LoadScene (1);
		}
	} 

	void Awake () {
		datos = new InfoPartida ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
