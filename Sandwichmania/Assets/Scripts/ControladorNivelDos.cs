using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControladorNivelDos : MonoBehaviour {
	
	public Text[] textos;
	public GameObject MenuIni;
	public GameObject generador;
	public GameObject[] ingre;
	public GameObject[] dedos;
	public string estadoActual;
	//private int enableMusic = 0;
	private string mensajeEnviar;
	private int manoTratamiento;
	private string ingrediente;
	private int contador=-1;
	
	void Start () {
		estadoActual = "ini";
		generador.SetActive (false); 
		
		for (int i = 0; i < textos.Length; i++) {
			if(i==1 || i==3 || i==4)
			{
				textos [i].gameObject.SetActive (false); 
			}
		}
		for (int i = 0; i < ingre.Length; i++) {
			ingre [i].SetActive (false); 
		}
		for (int i = 0; i < dedos.Length; i++) {
			dedos [i].SetActive (false); 
		}
		manoTratamiento = Admin_level0.datos.mano;
		mensajeEnviar = "inicio";
		NotificationCenter.DefaultCenter().AddObserver(this, "CambiaIngrediente");
		NotificationCenter.DefaultCenter().PostNotification(this,"musicPlay",mensajeEnviar);
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.RightArrow)
		     || Input.GetKeyDown (KeyCode.LeftArrow)) && estadoActual == "ini") {
			estadoActual = "instrucciones";
			for (int i = 0; i < textos.Length; i++) {
				if(i==1 || i==3)
				{
					textos [i].gameObject.SetActive (true); 
				}else{
					textos [i].gameObject.SetActive (false); 
				}
			}
		}
		else if ((Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.RightArrow)
		          || Input.GetKeyDown (KeyCode.LeftArrow)) && estadoActual == "instrucciones") {
			MenuIni.SetActive(false);
			generador.SetActive (true);
			textos [1].gameObject.SetActive (false);
			textos [3].gameObject.SetActive (false);
			iniPan();
			mensajeEnviar = "sonidoAmbiental";
			NotificationCenter.DefaultCenter ().PostNotification(this,"musicPlay",mensajeEnviar);
		} 
		else if ((Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.RightArrow)
		            || Input.GetKeyDown (KeyCode.LeftArrow)) && estadoActual == "final") {
			Application.LoadLevel (2);
		}

		if (ingrediente == "Pan" && contador == 0) {
			iniJamon();
		}
		if (ingrediente == "Jamon" && contador == 1) {
			iniQueso();
		}
		if (ingrediente == "Queso" && contador == 2) {
			iniJitoma();
		}
		if (ingrediente == "Jitomate" && contador == 3) {
			iniQueso();
		}
		if (ingrediente == "Queso" && contador == 4) {
			iniJamon();
		}
		if (ingrediente == "Jamon" && contador ==5) {
			iniPan();
		}
		if (ingrediente == "Pan" && contador == 6 && estadoActual == "pan") {
			finLevel();
		}
	}
	
	void iniPan(){
		contador ++;
		if(contador==0){
			ingre [0].gameObject.SetActive (true);
			dedos [0+(manoTratamiento*4)].gameObject.SetActive (true);
		} else if(contador==6){
			ingre [1].gameObject.SetActive (false);
			dedos [1+(manoTratamiento*4)].gameObject.SetActive (false);
			ingre [0].gameObject.SetActive (true);
			dedos [0+(manoTratamiento*4)].gameObject.SetActive (true);
		}
		estadoActual = "pan";
		NotificationCenter.DefaultCenter ().PostNotification(this,"CambiaIngGenerar",estadoActual);
	}
	void iniJamon(){
		contador ++;
		if(contador==1){
			ingre [0].gameObject.SetActive (false);
			dedos [0+(manoTratamiento*4)].gameObject.SetActive (false);
			ingre [1].gameObject.SetActive (true);
			dedos [1+(manoTratamiento*4)].gameObject.SetActive (true);
		} else if(contador==5){
			ingre [2].gameObject.SetActive (false);
			dedos [2+(manoTratamiento*4)].gameObject.SetActive (false);
			ingre [1].gameObject.SetActive (true);
			dedos [1+(manoTratamiento*4)].gameObject.SetActive (true);
		}
		estadoActual = "jamon";
		NotificationCenter.DefaultCenter ().PostNotification(this,"CambiaIngGenerar",estadoActual);
	}
	void iniQueso(){
		contador ++;
		if(contador==2){
			ingre [1].gameObject.SetActive (false);
			dedos [1+(manoTratamiento*4)].gameObject.SetActive (false);
			ingre [2].gameObject.SetActive (true);
			dedos [2+(manoTratamiento*4)].gameObject.SetActive (true);
		} else if(contador==4){
			ingre [3].gameObject.SetActive (false);
			dedos [3+(manoTratamiento*4)].gameObject.SetActive (false);
			ingre [2].gameObject.SetActive (true);
			dedos [2+(manoTratamiento*4)].gameObject.SetActive (true);
		}
		estadoActual = "queso";
		NotificationCenter.DefaultCenter ().PostNotification(this,"CambiaIngGenerar",estadoActual);
	}
	void iniJitoma(){
		contador ++;
		if(contador==3){
			ingre [2].gameObject.SetActive (false);
			dedos [2+(manoTratamiento*4)].gameObject.SetActive (false);
			ingre [3].gameObject.SetActive (true);
			dedos [3+(manoTratamiento*4)].gameObject.SetActive (true);
		}
		estadoActual = "jitomate";
		NotificationCenter.DefaultCenter ().PostNotification(this,"CambiaIngGenerar",estadoActual);
	}

	void finLevel(){
		generador.SetActive (false);
		MenuIni.SetActive (true);
		textos [1].gameObject.SetActive (true);
		textos [4].gameObject.SetActive (true);
		mensajeEnviar = "Exito";
		NotificationCenter.DefaultCenter ().PostNotification(this,"musicPlay",mensajeEnviar);
		estadoActual = "final";	
	}


	public void CambiaIngrediente(Notification notificacion){
		ingrediente = notificacion.data.ToString();
	}
}
