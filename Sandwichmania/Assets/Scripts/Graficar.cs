using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class Graficar : MonoBehaviour {
	public Text nombrePaciente;
	public Text noPartidasJugadasNivel1;
	public Text noPartidasJugadasNivel2;
	public Text noPartidasJugadasNivel3;
	public Text ultimaPartida;

//	public Image workSpace; //aqui es donde se dibujara la grafica, es el area en blanco
//	public UILineTextureRenderer graficaIndice; //el line renderer que dibuja la grafica
//	public UILineTextureRenderer graficaMedio; //el line renderer que dibuja la grafica
//	public UILineTextureRenderer graficaAnular; //el line renderer que dibuja la grafica
//	public UILineTextureRenderer graficaMeñique; //el line renderer que dibuja la grafica
//	public Button graphic_button; //este boton se instanciara sobre la grafica por cada partida, al darle click se mostraran mas detalles de la partida
//	public GameObject data_window; //La ventana donde se mostraran los detalles especificos de cada partida

	private InfoPartida dataForRead;
	private int _noPartidasJugadasNivel1;
	private int _noPartidasJugadasNivel2;
	private int _noPartidasJugadasNivel3;
	private List<Button> botonesxNivel;
	private List<GameObject> cuadrosDeInfo;
	private string _nombrePaciente;
	// Use this for initialization
	void Start () {
		dataForRead = new InfoPartida ();
		dataForRead = GameSaveLoad.Load (GameSaveLoad._FileLocation +"\\"+GameSaveLoad._PacienteName+"_DataSandwich.xml");
		//lista que almacenara los botones creados en la grafica por nivel para despues remover los listeners de cada boton
		botonesxNivel = new List<Button> ();
		//lista que almacena las ventanas que contienen la informacion de cada partida cuando se presiona algun boton de la lista botonesxNivel 
		cuadrosDeInfo = new List<GameObject> ();
		_nombrePaciente = dataForRead.nombre;
		_noPartidasJugadasNivel1 = dataForRead.HistorialPartidasNivel1.Count;
		_noPartidasJugadasNivel2 = dataForRead.HistorialPartidasNivel2.Count;
		_noPartidasJugadasNivel3 = dataForRead.HistorialPartidasNivel1.Count;
		nombrePaciente.text = "Nombre: " + _nombrePaciente.ToString ();
		noPartidasJugadasNivel1.text = "Partidas jugadas del nivel 1: " + _noPartidasJugadasNivel1.ToString ();
		noPartidasJugadasNivel2.text = "Partidas jugadas del nivel 2: " + _noPartidasJugadasNivel2.ToString ();
		noPartidasJugadasNivel3.text = "Partidas jugadas del nivel 3: " + _noPartidasJugadasNivel3.ToString ();

	}

	// Update is called once per frame
	void Update () {
		
	}
}
