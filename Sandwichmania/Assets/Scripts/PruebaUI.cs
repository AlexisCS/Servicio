using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PruebaUI : MonoBehaviour {
	
	public Text accion;
	public Text mensaje;
	public GameObject MenuIni;
	public GameObject[] generador;
	public GameObject[] textAyuda;
	public GameObject[] ingre;
	public GameObject[] dedos;
	public string estadoActual;
	private int enableMusic = 0;
	private string mensajeEnviar;
	public static int manoTratamiento;

	void Start () {
		estadoActual = "ini";
		for (int i = 0; i < generador.Length; i++) {
			generador [i].SetActive (false); 
		}
		for (int i = 0; i < textAyuda.Length; i++) {
			textAyuda [i].SetActive (false); 
		}
		for (int i = 0; i < ingre.Length; i++) {
			ingre [i].SetActive (false); 
		}
		for (int i = 0; i < dedos.Length; i++) {
			dedos [i].SetActive (false); 
		}
		manoTratamiento = Menu_Ini_Btons.mano;
		NotificationCenter.DefaultCenter().AddObserver(this, "iniJamon");
		NotificationCenter.DefaultCenter().AddObserver(this, "iniQueso");
		NotificationCenter.DefaultCenter().AddObserver(this, "iniJitoma");
		NotificationCenter.DefaultCenter().AddObserver(this, "finLevel");
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.RightArrow)
			|| Input.GetKeyDown (KeyCode.LeftArrow)) && estadoActual == "ini") {
			accion.enabled = false;
			mensaje.enabled = false;
			iniPan ();
			for (int i = 0; i < textAyuda.Length; i++) {
				textAyuda [i].SetActive (true); 
			}
			textAyuda [9].gameObject.SetActive (false);
		} else if (Input.GetKeyDown (KeyCode.UpArrow) && estadoActual == "pan") {
			ingre [0].gameObject.SetActive (false);
			dedos [0 + (manoTratamiento * 4)].gameObject.SetActive (false);
			MenuIni.SetActive (false);
			textAyuda [8].gameObject.SetActive (false);

			generador [0].gameObject.SetActive (true);
			if (enableMusic == 0) {
				mensajeEnviar = "sonidoAmbiental";
				NotificationCenter.DefaultCenter ().PostNotification (this, "musicPlay", mensajeEnviar);
				enableMusic = 1;
			}
		} else if (Input.GetKeyDown (KeyCode.DownArrow) && estadoActual == "jamon") {
			ingre [1].gameObject.SetActive (false);
			dedos [1 + (manoTratamiento * 4)].gameObject.SetActive (false);
			MenuIni.SetActive (false);
			textAyuda [8].gameObject.SetActive (false);
			generador [1].gameObject.SetActive (true); 
		} else if (Input.GetKeyDown (KeyCode.LeftArrow) && estadoActual == "queso") {
			ingre [2].gameObject.SetActive (false);
			dedos [2 + (manoTratamiento * 4)].gameObject.SetActive (false);
			MenuIni.SetActive (false);
			textAyuda [8].gameObject.SetActive (false);
			generador [2].gameObject.SetActive (true);
		} else if (Input.GetKeyDown (KeyCode.RightArrow) && estadoActual == "jitomate") {
			ingre [3].gameObject.SetActive (false);
			dedos [3 + (manoTratamiento * 4)].gameObject.SetActive (false);
			MenuIni.SetActive (false);
			textAyuda [8].gameObject.SetActive (false);
			generador [3].gameObject.SetActive (true);
		} else if ((Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.RightArrow)
			|| Input.GetKeyDown (KeyCode.LeftArrow)) && estadoActual == "final") {
				Application.LoadLevel (2);
		}
	}

	void iniPan(){
		ingre [0].gameObject.SetActive (true);
		dedos [0+(manoTratamiento*4)].gameObject.SetActive (true);
		textAyuda [8].gameObject.SetActive (true);
		estadoActual = "pan";
	}
	void iniJamon(){
		generador [0].gameObject.SetActive (false);
		ingre [1].gameObject.SetActive (true);
		dedos [1+(manoTratamiento*4)].gameObject.SetActive (true);
		textAyuda [8].gameObject.SetActive (true);
		MenuIni.SetActive (true);
		estadoActual = "jamon";
	}
	void iniQueso(){
		generador [1].gameObject.SetActive (false);
		ingre [2].gameObject.SetActive (true);
		dedos [2+(manoTratamiento*4)].gameObject.SetActive (true);
		MenuIni.SetActive (true);
		textAyuda [8].gameObject.SetActive (true);
		estadoActual = "queso";	
	}
	void iniJitoma(){
		generador [2].gameObject.SetActive (false);
		ingre [3].gameObject.SetActive (true);
		dedos [3+(manoTratamiento*4)].gameObject.SetActive (true);
		MenuIni.SetActive (true);
		textAyuda [8].gameObject.SetActive (true);
		estadoActual = "jitomate";	
	}
	void finLevel(){
		generador [3].gameObject.SetActive (false);
		textAyuda [9].gameObject.SetActive (true);
		accion.enabled = true;
		mensajeEnviar = "Exito";
		MenuIni.SetActive (true);
		NotificationCenter.DefaultCenter ().PostNotification(this,"musicPlay",mensajeEnviar);
		estadoActual = "final";	
	}
}
