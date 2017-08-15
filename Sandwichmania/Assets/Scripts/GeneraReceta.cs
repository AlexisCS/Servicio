using UnityEngine;
using System.Collections;

public class GeneraReceta : MonoBehaviour {

	public int numeroGenerador;
	public GameObject[] obj;
	private GameObject pan;
	private string newtag = "nuevo";
	// Use this for initialization
	void Start () {
		NotificationCenter.DefaultCenter().AddObserver(this, "MuestraIngre");
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void MuestraIngre(){
		if (Input.GetKeyDown (KeyCode.UpArrow) && numeroGenerador == 1 ) {
			pan = (GameObject) Instantiate (obj [0], transform.position, Quaternion.identity);
			pan.gameObject.tag = newtag;

		}
	}
}
