using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using System.Text;
using System.Linq;

public class GraficaResultadosNivel1 : MonoBehaviour {

	public Toggle graficaIndiceToggle;
	public Toggle graficaMedioToggle;
	public Toggle graficaAnularToggle;
	public Toggle graficaMeñiqueToggle;

	public Image workSpace; //aqui es donde se dibujara la grafica, es el area en blanco
	public UILineTextureRenderer puntosDePartidas; //el line renderer que dibuja la grafica
	public UILineTextureRenderer ejeY;
	public UILineTextureRenderer graficaIndice; //el line renderer que dibuja la grafica
	public UILineTextureRenderer graficaMedio; //el line renderer que dibuja la grafica
	public UILineTextureRenderer graficaAnular; //el line renderer que dibuja la grafica
	public UILineTextureRenderer graficaMeñique; //el line renderer que dibuja la grafica
	public Button graphic_button; //este boton se instanciara sobre la grafica por cada partida, al darle click se mostraran mas detalles de la partida
	public Text numerosEjeY;
	[Tooltip("Colocar aqui el prefab de la ventana de información")]
	public GameObject data_window; //La ventana donde se mostraran los detalles especificos de cada partida

	private InfoPartida dataForReadNivel1;
	private int _noPartidasJugadasNivel1;
	private int _delimitaPartidas;
	private float _menorDeLaEscala;
	private float _mayorDeLaEscala;
	private float _escalarGrafica;
	private List<Button> botonesxNivel;
	private List<GameObject> cuadrosDeInfo;
	private StringBuilder _datosEnVentanas;


	public void RegresarBoton(){
		RemoveInfoWindows ();
		RemoveListeners (botonesxNivel);
		SceneManager.LoadScene ("Resultados Clinica");
	}

	public void CancelarBoton(){
		RemoveInfoWindows ();
		RemoveListeners (botonesxNivel);
		SceneManager.LoadScene ("Interfaz de Clinica");
	
	}

	public void MuestraOcultaGraficaIndice(){
		if (graficaIndiceToggle.isOn)
			graficaIndice.gameObject.SetActive (true);
		else
			graficaIndice.gameObject.SetActive (false);
	}

	public void MuestraOcultaGraficaMedio(){
		if (graficaMedioToggle.isOn)
			graficaMedio.gameObject.SetActive (true);
		else
			graficaMedio.gameObject.SetActive (false);
	}

	public void MuestraOcultaGraficaAnular(){
		if (graficaAnularToggle.isOn)
			graficaAnular.gameObject.SetActive (true);
		else
			graficaAnular.gameObject.SetActive (false);
	}

	public void MuestraOcultaGraficaMeñique(){
		if (graficaMeñiqueToggle.isOn)
			graficaMeñique.gameObject.SetActive (true);
		else
			graficaMeñique.gameObject.SetActive (false);
	}

	void Awake(){
		_datosEnVentanas = new StringBuilder ();
		botonesxNivel = new List<Button> ();
		//lista que almacena las ventanas que contienen la informacion de cada partida cuando se presiona algun boton de la lista botonesxNivel 
		cuadrosDeInfo = new List<GameObject> ();
	}


	// Use this for initialization
	void Start () {
		dataForReadNivel1 = ResultadosClinica.DataForRead;
		_noPartidasJugadasNivel1 = dataForReadNivel1.HistorialPartidasNivel1.Count;
		if (_noPartidasJugadasNivel1 < 15)
			_delimitaPartidas = 0;
		if (_noPartidasJugadasNivel1 > 15)
			_delimitaPartidas = _noPartidasJugadasNivel1 - 15;
		EscalaEjeY ();
		DibujaTextoEjeY ();
		DibujaBotonesEjeX ();
		DibujaGraficaDedoIndice ();
		DibujaGraficaDedoMedio ();
		DibujaGraficaDedoAnular ();
		DibujaGraficaDedoMeñique ();
	}

	void EscalaEjeY(){
		List<float> tiempos = new List<float> ();
		for (int i = _delimitaPartidas; i <= _noPartidasJugadasNivel1 - 1; i++) {
			tiempos.Add (dataForReadNivel1.HistorialPartidasNivel1 [i].tiempoDedoIndice);
			tiempos.Add (dataForReadNivel1.HistorialPartidasNivel1[i].tiempoDedoMedio);
			tiempos.Add (dataForReadNivel1.HistorialPartidasNivel1[i].tiempoDedoAnular);
			tiempos.Add (dataForReadNivel1.HistorialPartidasNivel1[i].tiempoDedoMeñique);
		}
		tiempos.Sort ();
		_menorDeLaEscala = tiempos [0];
		_mayorDeLaEscala = tiempos [tiempos.Count - 1];
	}

	void DibujaBotonesEjeX(){
		float escala = 735.0f / _noPartidasJugadasNivel1;
		puntosDePartidas.Points = new Vector2[_noPartidasJugadasNivel1];
		for (int i = _delimitaPartidas; i < puntosDePartidas.Points.Length; i++) {
			float partidasEjeX = escala * i	;
			//Se crea un boton por cada partida jugada
			Button partida_button = Instantiate(graphic_button);
			partida_button.transform.SetParent(workSpace.transform);
			Vector2 button_position = new Vector2(-5.0f+partidasEjeX , -11.3f);
			partida_button.GetComponent<RectTransform>().anchoredPosition = button_position;
			//partida_button.name="partida_"+i;
			partida_button.name = i.ToString();
			partida_button.GetComponentInChildren<Text> ().text = (i + 1).ToString ();
			//importante quitar los listener cuando el GameObject se destruya o deshabilite con DestroyListener
			//A cada boton que se ha creado se le agrega un listener para poder llamar a la funcion MoreInfoPartida tomando como argumento
			//el nombre del boton para saber el indice de la partida y mostrar sus respectiva info.
			partida_button.onClick.AddListener(()=>MoreInfoPartida(int.Parse(partida_button.name),"Grid_Image",button_position));
			botonesxNivel.Add(partida_button);
		}
	}

	void DibujaTextoEjeY(){
		float temporal = _mayorDeLaEscala;
		int numeroDeSegmentos = 7;
		float segmentoEntreTiempos = 220f / numeroDeSegmentos;
		float incrementoEnElTexto=((_mayorDeLaEscala-_menorDeLaEscala)/numeroDeSegmentos);
		ejeY.Points = new Vector2[numeroDeSegmentos];
		for (int i = 0; i <= ejeY.Points.Length; i++) {
			float puntosEjeY=(i*segmentoEntreTiempos)+5f;
			float incremetaSegmentos =  incrementoEnElTexto* i+_menorDeLaEscala;
			Text numerosText = Instantiate (numerosEjeY);
			numerosText.transform.SetParent (workSpace.transform);
			Vector2 textPosition = new Vector2(-35.0f, puntosEjeY);
			numerosText.GetComponent<RectTransform> ().anchoredPosition = textPosition;
			if (incremetaSegmentos > 59) {
				numerosText.text = ConvierteSegundosAMinutos (incremetaSegmentos).ToString () + " m";
			}

			if (incremetaSegmentos <= 59) {
				numerosText.text = Mathf.Round (incremetaSegmentos).ToString () + " s";
			}
		}
	}
		
	void DibujaGraficaDedoIndice(){
		//Para la grafica cada dia sera una unidad en el eje X y en el eje Y cada 10% de exito sera una unidad
		float escalaEjeX = 735.0f / _noPartidasJugadasNivel1;
		graficaIndice.Points = new Vector2[_noPartidasJugadasNivel1];
		for (int i = _delimitaPartidas; i < graficaIndice.Points.Length; i++) {
			float partidasEjeX = escalaEjeX * i;
			float tiempos =dataForReadNivel1.HistorialPartidasNivel1 [i].tiempoDedoIndice;
			float puntosEjeY = (float)(((tiempos - _menorDeLaEscala)*((220.0f)/(_mayorDeLaEscala - _menorDeLaEscala))) + 13.0f);
			graficaIndice.Points [i].Set (partidasEjeX, puntosEjeY);
		}
	}

	void DibujaGraficaDedoMedio(){
		//Para la grafica cada dia sera una unidad en el eje X y en el eje Y cada 10% de exito sera una unidad
		float escalaEjeX = 735.0f / _noPartidasJugadasNivel1;
		graficaMedio.Points = new Vector2[_noPartidasJugadasNivel1];
		for (int i = _delimitaPartidas; i < graficaMedio.Points.Length; i++) {
			float partidasEjeX = escalaEjeX * i;
			float tiempos = dataForReadNivel1.HistorialPartidasNivel1 [i].tiempoDedoMedio;
			float puntosEjeY = (float)(((tiempos - _menorDeLaEscala)*((220.0f)/(_mayorDeLaEscala - _menorDeLaEscala))) + 13.0f);
			graficaMedio.Points [i].Set (partidasEjeX, puntosEjeY);
		}
	}

	void DibujaGraficaDedoAnular(){
		//Para la grafica cada dia sera una unidad en el eje X y en el eje Y cada 10% de exito sera una unidad
		float escalaEjeX = 735.0f / _noPartidasJugadasNivel1;
		graficaAnular.Points = new Vector2[_noPartidasJugadasNivel1];
		for (int i = _delimitaPartidas; i < graficaAnular.Points.Length; i++) {
			float partidasEjeX = escalaEjeX * i;
			float tiempos = dataForReadNivel1.HistorialPartidasNivel1 [i].tiempoDedoAnular;
			float puntosEjeY = (float)(((tiempos - _menorDeLaEscala)*((220.0f)/(_mayorDeLaEscala - _menorDeLaEscala))) + 13.0f);
			graficaAnular.Points [i].Set (partidasEjeX, puntosEjeY);
		}
	}

	void DibujaGraficaDedoMeñique(){
		//Para la grafica cada dia sera una unidad en el eje X y en el eje Y cada 10% de exito sera una unidad
		float escalaEjeX = 735.0f / _noPartidasJugadasNivel1;
		graficaMeñique.Points = new Vector2[_noPartidasJugadasNivel1];
		for (int i = _delimitaPartidas; i < graficaMeñique.Points.Length; i++) {
			float partidasEjeX = escalaEjeX * i;
			float tiempos = dataForReadNivel1.HistorialPartidasNivel1 [i].tiempoDedoMeñique;
			for (int j = 0; j <= ejeY.Points.Length; j++) {
				float puntosEjeY = (float)(((tiempos - _menorDeLaEscala) * ((220.0f) / (_mayorDeLaEscala - _menorDeLaEscala))) + 13.0f);
				graficaMeñique.Points [i].Set (partidasEjeX, puntosEjeY);
			}
		}
	}

	public void MoreInfoPartida(int index, string nameOfParentPanel, Vector2 button_position){
		//Se instancia la pequeña ventana de datos y se agrega a la lista de ventanas
		//Se crea una ventana cada vez que se presiona el boton corresopndiente a la partida jugada
		if (data_window) {

			GameObject window = (GameObject)Instantiate (data_window, new Vector3 ( Random.Range (-200,200) + 900.0f, Random.Range (-200,200) + 600.0f, 0f), transform.rotation);
			window.transform.SetParent (GameObject.Find (nameOfParentPanel).transform);
			//window.transform.localScale = new Vector3 (1f, 1f, 1f);
			cuadrosDeInfo.Add (window);
			//Si se necesitara ahondar mas niveles de jerarquía sería Find("NameOfGameObject/NameOfGameObject/.../NameOfTarget")
			//window.transform.Find ("Fecha").GetComponent<Text> ().text = dataForRead.HistorialPartidas [index].Date;
			_datosEnVentanas.AppendLine ("Fecha:");
			_datosEnVentanas.AppendLine (dataForReadNivel1.HistorialPartidasNivel1[index].fecha.ToString ());
			_datosEnVentanas.AppendLine ("Partda: " + (index + 1).ToString ());
			_datosEnVentanas.AppendLine ("Mano: " + dataForReadNivel1.HistorialPartidasNivel1 [index].ManoSeleccionada.ToString ());
			_datosEnVentanas.AppendLine ("Tiempo que le tomó \napilar cada uno de \nlos ingredientes:");
			_datosEnVentanas.AppendLine ("- Indice: " + dataForReadNivel1.HistorialPartidasNivel1[index].tiempoDedoIndice.ToString () + "s.");
			_datosEnVentanas.AppendLine ("- Medio: " + dataForReadNivel1.HistorialPartidasNivel1[index].tiempoDedoMedio.ToString () + "s.");
			_datosEnVentanas.AppendLine ("- Anular: " + dataForReadNivel1.HistorialPartidasNivel1[index].tiempoDedoAnular.ToString () + "s.");
			_datosEnVentanas.AppendLine ("- Meñique: " + dataForReadNivel1.HistorialPartidasNivel1[index].tiempoDedoMeñique.ToString () + "s.");
			window.transform.FindChild ("Info_Text").GetComponent<Text> ().text = _datosEnVentanas.ToString ();
			_datosEnVentanas.Remove (0,_datosEnVentanas.Length);
		}
	}

	float ConvierteSegundosAMinutos(float segundos){
		float minutos = 0;
		minutos = Mathf.Round ((segundos / 60f) * 10f) / 10f;
		return minutos;
	}

	void RemoveInfoWindows(){
		foreach (GameObject cuadro in cuadrosDeInfo)
			Destroy (cuadro);
		cuadrosDeInfo.Clear ();
	}

	void RemoveListeners(List<Button> botonesDePartidas){
		if (botonesDePartidas.Count != 0) {
			foreach(Button p in botonesDePartidas){
				p.onClick.RemoveAllListeners();
				Destroy(p.gameObject);
			} 
		}
		botonesDePartidas.Clear ();
	}

}
