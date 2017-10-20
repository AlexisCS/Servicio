using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Sonidos : MonoBehaviour {
	public static bool reproduceSonido = true;
	public static Sonidos manejador;
	public AudioClip[] sonidos;
	private AudioSource _audioDeEscena;
	private bool _temp;


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
		AdminNivel1.MusicaAmbiente += InicioMusicaAmbiente;
		AdminNivel1.AudioExito += InicioAudioExito;
		AdminNivel2.AudioDeJuego += InicioMusicaAmbiente;
		AdminNivel2.AudiodeExito += InicioAudioExito;
		AdminNivel2SinRutina.AudioDeJuego += InicioMusicaAmbiente;
		AdminNivel2SinRutina.AudiodeExito += InicioAudioExito;
		AdminNivel3.AudioDeJuegoNivel3 += InicioMusicaAmbiente;
		AdminNivel3.AudiodeExitoNivel3 += InicioAudioExito;
	}

	void OnDisable(){
		SceneManager.sceneLoaded -= OnLoadScene;
		AdminNivel1.MusicaAmbiente -= InicioMusicaAmbiente;
		AdminNivel1.AudioExito -= InicioAudioExito;
		AdminNivel2.AudioDeJuego -= InicioMusicaAmbiente;
		AdminNivel2.AudiodeExito -= InicioAudioExito;
		AdminNivel2SinRutina.AudioDeJuego -= InicioMusicaAmbiente;
		AdminNivel2SinRutina.AudiodeExito -= InicioAudioExito;
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

	void PausaAudio(){
		if (_temp == true) {
			_audioDeEscena.Pause ();
		}

	}

	void ContinuaReproduccion(){
		if (_temp == false) {
			_audioDeEscena.Play ();
		}
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


