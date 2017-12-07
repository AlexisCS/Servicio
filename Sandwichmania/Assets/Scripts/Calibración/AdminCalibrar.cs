using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AdminCalibrar : MonoBehaviour {
	public void Jugar(){
		if (AdminMenu.datosNivel1.nivel == 1) {
			SceneManager.LoadScene (3);
		} 

		if (Admin_level0.datosNivel2.nivel == 2 && AdminMenu.RutinaSeleccionada == Rutina.ConRutina) {
			SceneManager.LoadScene (4);
		} 

		if (Admin_level0.datosNivel2.nivel == 2 && AdminMenu.RutinaSeleccionada == Rutina.SinRutina) {
			SceneManager.LoadScene (5);
		}

		if (AdminMenu.datosNivel3.nivel == 3) {
			SceneManager.LoadScene (6);
		}
	}
}
