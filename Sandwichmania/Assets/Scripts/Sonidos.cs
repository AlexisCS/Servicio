using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Sonidos : MonoBehaviour {

	public static Sonidos manejador;
	public AudioClip[] sonidos;
	public AudioSource _audioDeEscena;

	void OnEnable(){
		SceneManager.sceneLoaded += OnLoadScene;
	}

	void OnDisable(){
		SceneManager.sceneLoaded -= OnLoadScene;
	}

	void OnLoadScene(Scene scene,LoadSceneMode mode){
		//_audioDeEscena = GameObject.Find ("MainCamera").GetComponent<AudioSource> ();
//		Debug.Log ("Funciona");
		if (scene.buildIndex == 0 && !_audioDeEscena.isPlaying) {
			_audioDeEscena.clip = sonidos [0];
			_audioDeEscena.Play ();
			_audioDeEscena.loop=true;
		} 
	}

	void Awake(){
		if (manejador == null) {
			DontDestroyOnLoad (this.transform);
			manejador = this;
		}
		else if (manejador != this) {
			Destroy (gameObject);
		}
//		manejadorSonido = this.GetComponent<AudioSource> ();
//		manejadorSonido.clip = sonidos [1];
//		manejadorSonido.Play ();
	}
		

	void Update(){
//		if (condicion) {
//						manejadorSonido.Stop ();
//						manejadorSonido.clip = sonidos [2];
//						manejadorSonido.loop = false;
//						manejadorSonido.Play ();
//		}
	}
}


