using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;

public class AdminResultadosNivel2 : MonoBehaviour {

	public Text numeroDeRepeticiones;
	public Text numeroDeIngredientes;
	public Text promedio;
	public Text tiempos;

	private float _promedioEnSegundos;
	private StringBuilder _tiemposText;


	void Awake (){
		_tiemposText = new StringBuilder ();
		PromedioTiempos ();
		EntregaResultados ();
		RedondeoTiempos ();
		Admin_level0.datos.HistorialPartidasNivel2.Add (Admin_level0.datosNivel2);
		GameSaveLoad.Save (Admin_level0.datos);
	}

	void Start () {
		listadoDeTiempos ();
		numeroDeRepeticiones.text = "No. Repeticiones: " + Admin_level0.datosNivel2.numeroDeRepeticiones;
		numeroDeIngredientes.text = "No. Ingredientes por repetición: " + Admin_level0.datosNivel2.Rutina.Count;
		//numeroDeRepeticiones.text = "No. Repeticiones: " + AdminMenu.datosNivel2.numeroDeRepeticiones;
		//numeroDeIngredientes.text = "No. Ingredientes por repetición: " + AdminMenu.datosNivel2.Rutina.Count;
	}

	public void SiguienteBoton(){
		SceneManager.LoadScene (1);
		Admin_level0.datosNivel2.tiempos.Clear ();
		//AdminMenu.datosNivel2.tiempos.Clear ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void RedondeoTiempos(){
		for (int i = 0; i <= Admin_level0.datosNivel2.tiempos.Count - 1; i++) {
			Admin_level0.datosNivel2.tiempos[i] = DelimitaSegundos (Admin_level0.datosNivel2.tiempos[i]);
		}
	}

	void PromedioTiempos(){
		List<float> listaTemp = Admin_level0.datosNivel2.tiempos;
		float sumaDeTiempos = 0;

		if (listaTemp.Count == 1) {
			_promedioEnSegundos = listaTemp [0];
			return;
		}

		for (int i = 0; i <= listaTemp.Count - 1; i++) {
			sumaDeTiempos += listaTemp [i];
		}
		_promedioEnSegundos = sumaDeTiempos / listaTemp.Count;
		Admin_level0.datosNivel2.tiempoPromedio = DelimitaSegundos (_promedioEnSegundos);
	}

	void listadoDeTiempos(){
		_tiemposText.Append ("\nTiempos de cada repetición:\n[Repetición,Tiempo]\n");
		for (int i = 0; i <= Admin_level0.datosNivel2.tiempos.Count - 1; i++) {
			_tiemposText.Append ("[ ");
			_tiemposText.Append (i+1);
			_tiemposText.Append (" , ");
			if (Admin_level0.datosNivel2.tiempos[i] <= 60f) {
				_tiemposText.Append (DelimitaSegundos (Admin_level0.datosNivel2.tiempos[i]));
				_tiemposText.Append (" segundos");
			} else {
				_tiemposText.Append (ConvierteSegundosAMinutos (Admin_level0.datosNivel2.tiempos[i]));
				_tiemposText.Append (" minutos");
			}
			_tiemposText.Append (" ]\n");
		}
		tiempos.text = _tiemposText.ToString ();
	}

	void EntregaResultados(){
		if (_promedioEnSegundos <= 60f) { 
			promedio.text = "Tiempo promedio: " + DelimitaSegundos (_promedioEnSegundos) + " segundos";
		} else {
			promedio.text = "Tiempo promedio: " + ConvierteSegundosAMinutos (_promedioEnSegundos) + " minutos";
		}
	}

	float DelimitaSegundos(float segundos){
		float segundosConUnDecimal;
		segundosConUnDecimal = Mathf.Round (segundos * 10f) / 10f;
		return segundosConUnDecimal;
	}

	float ConvierteSegundosAMinutos(float segundos){
		float minutos = 0;
		minutos = Mathf.Round ((segundos / 60f) * 10f) / 10f;
		return minutos;
	}
}
