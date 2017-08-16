using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_Ini_Btons : MonoBehaviour {

	public GameObject[] music;
	public static int mano;
	private AudioSource manejadorSonido;

	void Awake() {
		DontDestroyOnLoad(music[0]);
	}

	// Use this for initialization
	void Start () {
		NotificationCenter.DefaultCenter().AddObserver(this, "altoMusicaInicio");
		manejadorSonido = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void cargaEscena(int valor)
	{
		//Application.LoadLevel (1);
		SceneManager.LoadScene (1);
		mano = valor;
	}

	public void altoMusicaInicio(){
		manejadorSonido.Stop ();
	}
}
//cerrar se
//salir