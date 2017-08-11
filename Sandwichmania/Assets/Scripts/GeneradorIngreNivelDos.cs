using UnityEngine;
using System.Collections;

public class GeneradorIngreNivelDos : MonoBehaviour {

	public GameObject[] obj;
	public string name_generador;
	private GameObject pan;
	private GameObject jamon;
	private GameObject queso;
	private GameObject jitomate;
	private string newtag = "nuevo";
	public TextMesh muestraContador;
	private float countdown;
	private string mensajeEnviar;
	private string ingrediente;
	
	void Start () {
		NotificationCenter.DefaultCenter().AddObserver(this, "CambiaIngGenerar");
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow) && ingrediente=="pan") {
			pan = (GameObject)Instantiate (obj [0], transform.position, Quaternion.identity);
			pan.gameObject.tag = newtag;
		} 
		if (Input.GetKeyDown (KeyCode.DownArrow) && ingrediente=="jamon") {
			jamon = (GameObject)Instantiate (obj [1], transform.position, Quaternion.identity);
			jamon.gameObject.tag = newtag;
		} 
		if (Input.GetKeyDown (KeyCode.LeftArrow) && ingrediente=="queso") {
			queso = (GameObject)Instantiate (obj [2], transform.position, Quaternion.identity);
			queso.gameObject.tag = newtag;
		} 
		if (Input.GetKeyDown (KeyCode.RightArrow) && ingrediente=="jitomate") {
			jitomate = (GameObject)Instantiate (obj [3], new Vector3(transform.position.x-0.5f,transform.position.y,transform.position.z), Quaternion.identity);
			jitomate.gameObject.tag = newtag;
		}
		
		if(pan != null){
			if (Input.GetKeyUp (KeyCode.UpArrow) &&  pan.tag ==  newtag) {
				Destroy (pan.gameObject);
			}
		}
		if (jamon != null) {
			if (Input.GetKeyUp (KeyCode.DownArrow) && jamon.tag == newtag) {
				Destroy (jamon.gameObject);
			}
		}
		if (queso != null) {
			if (Input.GetKeyUp (KeyCode.LeftArrow) && queso.tag == newtag) {
				Destroy (queso.gameObject);
			}
		}
		if (jitomate != null) {
			if (Input.GetKeyUp (KeyCode.RightArrow) && jitomate.tag == newtag) {
				Destroy (jitomate.gameObject);
			}
		}
		
	}

	public void CambiaIngGenerar(Notification notification){
		ingrediente = notification.data.ToString();

	}


}
