using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Sonidos : MonoBehaviour {

	public static Sonidos manejador;
	public AudioClip[] sonidos;
	private AudioSource _audioDeEscena;

	void OnEnable(){
		SceneManager.sceneLoaded += OnLoadScene;
		AdminNivel1.MusicaAmbiente += InicioMusicaAmbiente;
		AdminNivel1.AudioExito += InicioAudioExito;
		AdminNivel2.AudioDeJuego += InicioMusicaAmbiente;
		AdminNivel2.AudiodeExito += InicioAudioExito;
	}

	void OnDisable(){
		SceneManager.sceneLoaded -= OnLoadScene;
		AdminNivel1.MusicaAmbiente -= InicioMusicaAmbiente;
		AdminNivel1.AudioExito -= InicioAudioExito;
		AdminNivel2.AudioDeJuego -= InicioMusicaAmbiente;
		AdminNivel2.AudiodeExito -= InicioAudioExito;
	}

	void OnLoadScene(Scene scene,LoadSceneMode mode){
		if ((scene.buildIndex == 0 || scene.buildIndex == 1 )) {
			_audioDeEscena.clip = sonidos [0];
			_audioDeEscena.loop = true;
			if(!_audioDeEscena.isPlaying)
				_audioDeEscena.Play ();
			
		} 
	}

	void Awake(){
		_audioDeEscena = this.GetComponent<AudioSource> ();
		_audioDeEscena.clip = sonidos [0];
		_audioDeEscena.Play ();
		if (manejador == null) {
			DontDestroyOnLoad (this.transform);
			manejador = this;
		}
		else  {
			Destroy (gameObject);
		}
	}

	void InicioMusicaAmbiente(){
		_audioDeEscena.Stop ();
		_audioDeEscena.clip = sonidos[1];
		_audioDeEscena.Play ();
		_audioDeEscena.loop = true;
	}

	void InicioAudioExito(){
		_audioDeEscena.Stop ();
		_audioDeEscena.clip = sonidos[2];
		_audioDeEscena.Play ();
		_audioDeEscena.loop = false;
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


