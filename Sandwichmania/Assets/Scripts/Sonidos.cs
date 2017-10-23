using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Sonidos : MonoBehaviour {
	public static bool reproduceSonido = true;
	public static Sonidos manejador;
	public AudioClip[] sonidos;
	private AudioSource _audioDeEscena;
	private AudioSource _colision;


	public static void EnciedeOApAGA(bool v){
		GameObject Cam = GameObject.Find ("Main Camera");
		if (Cam != null)
			Debug.Log ("encontre main camera");
		AudioSource temp = Cam.GetComponent <AudioSource> ();
		if (v) { //encendido
			Debug.Log ("enntre estatic");
			if (!temp.isPlaying)
				temp.Play ();
		} else
			temp.Pause();
	}

	void OnEnable(){
		SceneManager.sceneLoaded += OnLoadScene;
		//--------------------------------------------------------//
		AdminNivel1.Colision += Colision;
		AdminNivel1.MusicaAmbiente += InicioMusicaAmbiente;
		AdminNivel1.AudioExito += InicioAudioExito;
		//--------------------------------------------------------//
		AdminNivel2.Colision += Colision;
		AdminNivel2.AudioDeJuego += InicioMusicaAmbiente;
		AdminNivel2.AudiodeExito += InicioAudioExito;
		//--------------------------------------------------------//
		AdminNivel2SinRutina.Colision += Colision;
		AdminNivel2SinRutina.AudioDeJuego += InicioMusicaAmbiente;
		AdminNivel2SinRutina.AudiodeExito += InicioAudioExito;
		//--------------------------------------------------------//
		AdminNivel3.Colision += Colision;
		AdminNivel3.AudioDeJuegoNivel3 += InicioMusicaAmbiente;
		AdminNivel3.AudiodeExitoNivel3 += InicioAudioExito;
	}

	void OnDisable(){
		SceneManager.sceneLoaded -= OnLoadScene;
		//--------------------------------------------------------//
		AdminNivel1.Colision -= Colision;
		AdminNivel1.MusicaAmbiente -= InicioMusicaAmbiente;
		AdminNivel1.AudioExito -= InicioAudioExito;
		//--------------------------------------------------------//
		AdminNivel2.Colision -= Colision;
		AdminNivel2.AudioDeJuego -= InicioMusicaAmbiente;
		AdminNivel2.AudiodeExito -= InicioAudioExito;
		//--------------------------------------------------------//
		AdminNivel2SinRutina.Colision -= Colision;
		AdminNivel2SinRutina.AudioDeJuego -= InicioMusicaAmbiente;
		AdminNivel2SinRutina.AudiodeExito -= InicioAudioExito;
		//--------------------------------------------------------//
		AdminNivel3.Colision -= Colision;
		AdminNivel3.AudioDeJuegoNivel3 -= InicioMusicaAmbiente;
		AdminNivel3.AudiodeExitoNivel3 -= InicioAudioExito;
	}

	void OnLoadScene(Scene scene,LoadSceneMode mode){
		if (scene.buildIndex >=3 && scene.buildIndex <=6)
			return;
		_audioDeEscena.clip = sonidos [0];
		_audioDeEscena.loop = true;
//		if(!_audioDeEscena.isPlaying)
//			_audioDeEscena.Play ();
	}

	void Awake(){
		//_temp = ControlDeAudio.silenciar;
		_colision = GetComponent <AudioSource> ();
		_audioDeEscena = this.GetComponent<AudioSource> ();
		_audioDeEscena.clip = sonidos [0];
		//_audioDeEscena.Play ();
		if (manejador == null) {
			DontDestroyOnLoad (this.transform);
			manejador = this;
		}
		else  {
			Destroy (gameObject);
		}
	}

	void InicioMusicaAmbiente(){
		if (reproduceSonido) {
			_audioDeEscena.Stop ();
			_audioDeEscena.clip = sonidos [1];
			_audioDeEscena.Play ();
			_audioDeEscena.loop = true;
		} else {
			_audioDeEscena.clip = sonidos [1];
			_audioDeEscena.Pause ();
		}
	}

	void InicioAudioExito(){
		if (reproduceSonido) {
			_audioDeEscena.Stop ();
			_audioDeEscena.clip = sonidos [2];
			_audioDeEscena.Play ();
			_audioDeEscena.loop = false;
		} else {
			_audioDeEscena.clip = sonidos [1];
			_audioDeEscena.Pause ();
		}
	}

	void Colision(){
		if (reproduceSonido) {
			_colision.PlayOneShot (sonidos [3], 1.0f);
		} else {
			_colision.Pause ();
		}
	}


	void Update(){
		
	}
}


