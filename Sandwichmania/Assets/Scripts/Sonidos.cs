using UnityEngine;
using System.Collections;

public class Sonidos : MonoBehaviour {

	public AudioClip[] sonidos;
	private AudioSource manejadorSonido;
	private string mensajeRecibido;

	// Use this for initialization
	void Start () {
		NotificationCenter.DefaultCenter().AddObserver(this, "musicPlay");
		manejadorSonido = GetComponent<AudioSource> ();
	}
	// Update is called once per frame
	void Update () {
	
	}

	public void musicPlay(Notification notificacion){
		mensajeRecibido = notificacion.data.ToString();
		print (mensajeRecibido);
		if(mensajeRecibido=="inicio"){
			manejadorSonido.Stop ();
			manejadorSonido.clip = sonidos [0];
			manejadorSonido.loop = true;
			manejadorSonido.Play ();
		}
		if(mensajeRecibido=="sonidoAmbiental"){
			if(Application.loadedLevel!=2){
				NotificationCenter.DefaultCenter ().PostNotification(this,"altoMusicaInicio");
			}
			manejadorSonido.Stop ();
			manejadorSonido.clip = sonidos [1];
			manejadorSonido.loop = true;
			manejadorSonido.Play ();
		}
		if(mensajeRecibido=="Exito"){
			manejadorSonido.Stop ();
			manejadorSonido.clip = sonidos [2];
			manejadorSonido.loop = false;
			manejadorSonido.Play ();
		}
	}
}
