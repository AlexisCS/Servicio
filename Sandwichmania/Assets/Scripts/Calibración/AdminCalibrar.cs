using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AdminCalibrar : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Jugar(){
		if (Admin_level0.datos.nivel == 1 && (Admin_level0.datos.mano == 0 || Admin_level0.datos.mano == 1)) {
			SceneManager.LoadScene (3);
		} else if (Admin_level0.datos.nivel == 2 && (Admin_level0.datos.mano == 0 || Admin_level0.datos.mano == 1)) {
			SceneManager.LoadScene (4);
		} else if (Admin_level0.datos.nivel == 3 && (Admin_level0.datos.mano == 0 || Admin_level0.datos.mano == 1)) {
			SceneManager.LoadScene (5);
		}
	}
}
