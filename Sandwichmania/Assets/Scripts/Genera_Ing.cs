using UnityEngine;
using System.Collections;

public class Genera_Ing : MonoBehaviour {

	public GameObject[] obj;
	public string name_generador;
	private GameObject pan;
	private GameObject jamon;
	private GameObject queso;
	private GameObject jitomate;
	private string newtag = "nuevo";
	private string noDestrugTag = "noDestructible";
	public TextMesh muestraContador;
	private float countdown;
	private int contador;
	private int numIngre = 10;

	void Start () {
		NotificationCenter.DefaultCenter().AddObserver(this, "IncCant_Ing");
		contador = 0;
	}

	void Update () {
		if (contador == numIngre && name_generador == "pan") {
			NotificationCenter.DefaultCenter ().PostNotification(this,"iniJamon");
		}
		else if (contador == numIngre && name_generador == "jamon") {
			NotificationCenter.DefaultCenter ().PostNotification(this,"iniQueso");
		}
		else if (contador == numIngre && name_generador  == "queso") {
			NotificationCenter.DefaultCenter ().PostNotification(this,"iniJitoma");
		}
		else if (contador == numIngre && name_generador  == "jitomate") {
			NotificationCenter.DefaultCenter ().PostNotification(this,"finLevel");
		}


		if (Input.GetKeyDown (KeyCode.UpArrow) && name_generador == "pan" ) {
			pan = (GameObject) Instantiate (obj [0], transform.position, Quaternion.identity);
			pan.gameObject.tag = newtag;
		}
		else if (Input.GetKeyDown (KeyCode.DownArrow) && name_generador == "jamon") {
			jamon = (GameObject) Instantiate (obj [1], transform.position, Quaternion.identity); 
			jamon.gameObject.tag = newtag;
		}
		else if (Input.GetKeyDown (KeyCode.LeftArrow) && name_generador  == "queso") {
			queso = (GameObject) Instantiate (obj [2], transform.position, Quaternion.identity);
			queso.gameObject.tag = newtag;
		}
		else if (Input.GetKeyDown (KeyCode.RightArrow) && name_generador  == "jitomate") {
			jitomate = (GameObject) Instantiate (obj [3], transform.position, Quaternion.identity); 
			jitomate.gameObject.tag = newtag;
		} 


		if(pan != null){
			if (Input.GetKeyUp (KeyCode.UpArrow) &&  pan.tag ==  newtag) {
				Destroy (pan.gameObject);
				}
		}
		else if (jamon != null) {
			if (Input.GetKeyUp (KeyCode.DownArrow) && jamon.tag == newtag) {
				Destroy (jamon.gameObject);
			}
		}
		else if (queso != null) {
			if (Input.GetKeyUp (KeyCode.LeftArrow) && queso.tag == newtag) {
				Destroy (queso.gameObject);
			}
		}
		else if (jitomate != null) {
			if (Input.GetKeyUp (KeyCode.RightArrow) && jitomate.tag == newtag) {
				Destroy (jitomate.gameObject);
			}
		}

	}

	void OnCollisionEnter2D(Collision2D collision){
		collision.collider.gameObject.tag = noDestrugTag;
		contador = contador + 1;
	}

	void IncCant_Ing(Notification notificacion){
		contador = contador + (int)notificacion.data;
		ActualizarContador ();
	}

	void ActualizarContador(){
		muestraContador.text = contador.ToString ();
	}

}
