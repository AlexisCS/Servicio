using UnityEngine;
using System.Collections;

public class Muestra_Img : MonoBehaviour {

	public GameObject[] obj;
	public float tiempoMin = 10.0f;
	public float tiempoMax = 12.0f;
	private int teclaPresiona = 10;
	// Use this for initialization
	void Start () {
		Muestra(0);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.UpArrow) && (teclaPresiona==0) ) {
			Muestra (1);
			GetComponent<AudioSource>().Play();
		}
		if (Input.GetKey (KeyCode.UpArrow) && (teclaPresiona==1)) {
			Muestra (2);
			GetComponent<AudioSource>().Play();
		}
		if (Input.GetKey (KeyCode.DownArrow) && (teclaPresiona==2)) {
			Muestra (3);
			GetComponent<AudioSource>().Play();
		}
		if (Input.GetKey (KeyCode.LeftArrow) && (teclaPresiona==3)) {
			Muestra (4);
			GetComponent<AudioSource>().Play();
		}
		if (Input.GetKey (KeyCode.RightArrow) && (teclaPresiona == 4)) {
			Muestra (1);
			GetComponent<AudioSource> ().Play ();
		} 
	}

	void Muestra(int dedo){
		GameObject clone = (GameObject) Instantiate (obj [dedo], transform.position, Quaternion.identity);
		Destroy (clone, 20.0f);
		teclaPresiona = dedo;
		Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, 
		                                              Camera.main.transform.position.z - 0.0001f);
	}
}
