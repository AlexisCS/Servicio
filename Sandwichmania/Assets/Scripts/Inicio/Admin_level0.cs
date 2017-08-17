using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NUnit.Framework.Internal.Filters;

public class Admin_level0 : MonoBehaviour {
	public InputField id;
	public InputField password;
	public Text message;

	public void Setget (){
		if (!(id.text.ToString().Equals ("alexis")) || !(password.text.ToString ().Equals ("contraseña"))) {
			message.text = "Usuario o contraseña incorrecta";
		} else {
			SceneManager.LoadScene (1);
		}
	} 
		
	//Salir y 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
