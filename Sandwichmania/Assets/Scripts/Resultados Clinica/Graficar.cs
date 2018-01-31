using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using System.Text;

enum UltimaPartidaJugada {Ninguna, UltimaPartidaNivel1, UltimaPartidaNivel2, UltimaPartidaNivel3}
//[RequireComponent(typeof(AudioSource))]
public class Graficar : MonoBehaviour {
//	[SerializeField]
//	[Tooltip("Probando rangos")]
//	[Range(-1.0f,1.0f)]
//	private float velocidad;
	
	[Tooltip(" masdas")]
	private GameObject resultadosPanel;
	public GameObject resultadosNivel1Panel;
	public GameObject resultadosNivel2Panel;
	public GameObject resultadosNivel3Panel;

	public Text nombrePaciente;
	public Text noPartidasJugadasNivel1;
	public Text noPartidasJugadasNivel2;
	public Text noPartidasJugadasNivel3;
	public Text ultimaPartida;

	public Image workSpace; //aqui es donde se dibujara la grafica, es el area en blanco
	public UILineTextureRenderer graficaIndice; //el line renderer que dibuja la grafica
	public UILineTextureRenderer graficaMedio; //el line renderer que dibuja la grafica
	public UILineTextureRenderer graficaAnular; //el line renderer que dibuja la grafica
	public UILineTextureRenderer graficaMeñique; //el line renderer que dibuja la grafica
	public Button graphic_button; //este boton se instanciara sobre la grafica por cada partida, al darle click se mostraran mas detalles de la partida
	[Tooltip("Colocar aqui el prefab de la ventana de información")]
	public GameObject data_window; //La ventana donde se mostraran los detalles especificos de cada partida

	private InfoPartida dataForRead;
	private int _noPartidasJugadasNivel1;
	private int _noPartidasJugadasNivel2;
	private int _noPartidasJugadasNivel3;
	private bool _ultimaPartidaNivel1, _ultimaPartidaNivel2, _ultimaPartidaNivel3;
	private List<Button> botonesxNivel;
	private List<GameObject> cuadrosDeInfo;
	private string _fechaPartidaNivel1, _fechaPartidaNivel2, _fechaPartidaNivel3;
	private StringBuilder _datosUltimaPartida;
	private string _nombrePaciente;

	UltimaPartidaJugada DecideUltimaPartida;

	// Use this for initialization
	void Start () {
		#if UNITY_EDITOR
		if (workSpace == null) {
			Debug.LogWarning ("Falta que");
			Debug.Break ();
		}
		#endif

		dataForRead = new InfoPartida ();
		dataForRead = GameSaveLoad.Load (GameSaveLoad._FileLocation +"\\"+GameSaveLoad._PacienteName+"_DataSandwich.xml");
		_datosUltimaPartida = new StringBuilder ();
		//lista que almacenara los botones creados en la grafica por nivel para despues remover los listeners de cada boton
		botonesxNivel = new List<Button> ();
		//lista que almacena las ventanas que contienen la informacion de cada partida cuando se presiona algun boton de la lista botonesxNivel 
		cuadrosDeInfo = new List<GameObject> ();
		_nombrePaciente = dataForRead.nombre;
		_noPartidasJugadasNivel1 = dataForRead.HistorialPartidasNivel1.Count;
		_noPartidasJugadasNivel2 = dataForRead.HistorialPartidasNivel2.Count;
		_noPartidasJugadasNivel3 = dataForRead.HistorialPartidasNivel3.Count;
		nombrePaciente.text = "Nombre:  " + _nombrePaciente.ToString ();
		noPartidasJugadasNivel1.text = "Partidas jugadas del nivel 1:  " + _noPartidasJugadasNivel1.ToString ();
		noPartidasJugadasNivel2.text = "Partidas jugadas del nivel 2:  " + _noPartidasJugadasNivel2.ToString ();
		noPartidasJugadasNivel3.text = "Partidas jugadas del nivel 3:  " + _noPartidasJugadasNivel3.ToString (); 
		_fechaPartidaNivel1 = dataForRead.HistorialPartidasNivel1 [_noPartidasJugadasNivel1 - 1].fecha.ToString ();
		_fechaPartidaNivel2 = dataForRead.HistorialPartidasNivel2 [_noPartidasJugadasNivel2 - 1].fecha.ToString ();
		_fechaPartidaNivel3 = dataForRead.HistorialPartidasNivel3 [_noPartidasJugadasNivel3 - 1].fecha.ToString ();
		ComparaFechasEntrePartidas (_fechaPartidaNivel1, _fechaPartidaNivel2, _fechaPartidaNivel3);

		if (DecideUltimaPartida == UltimaPartidaJugada.UltimaPartidaNivel1) {
			_datosUltimaPartida.Append ("\t\t\tResultados ultima partida");
			_datosUltimaPartida.Append ("\n\n");
			_datosUltimaPartida.Append ("Fecha:  ");
			_datosUltimaPartida.Append (dataForRead.HistorialPartidasNivel1 [_noPartidasJugadasNivel1 - 1].fecha.ToString ());
			_datosUltimaPartida.Append ("\n");
			_datosUltimaPartida.Append ("Mano seleccionada:  ");
			_datosUltimaPartida.Append (dataForRead.HistorialPartidasNivel1 [_noPartidasJugadasNivel1 - 1].ManoSeleccionada.ToString ());
			_datosUltimaPartida.Append ("\n");
			_datosUltimaPartida.Append ("Tiempo en segundos del dedo indice:  ");
			_datosUltimaPartida.Append (dataForRead.HistorialPartidasNivel1 [_noPartidasJugadasNivel1 - 1].tiempoDedoIndice.ToString ());
			_datosUltimaPartida.Append ("\n");
			_datosUltimaPartida.Append ("Tiempo en segundos del dedo Medio:  ");
			_datosUltimaPartida.Append (dataForRead.HistorialPartidasNivel1 [_noPartidasJugadasNivel1 - 1].tiempoDedoMedio.ToString ());
			_datosUltimaPartida.Append ("\n");
			_datosUltimaPartida.Append ("Tiempo en segundos del dedo Anular:  ");
			_datosUltimaPartida.Append (dataForRead.HistorialPartidasNivel1 [_noPartidasJugadasNivel1 - 1].tiempoDedoAnular.ToString ());
			_datosUltimaPartida.Append ("\n");
			_datosUltimaPartida.Append ("Tiempo en segundos del dedo Meñique:  ");
			_datosUltimaPartida.Append (dataForRead.HistorialPartidasNivel1 [_noPartidasJugadasNivel1 - 1].tiempoDedoMeñique.ToString ());
			ultimaPartida.text = _datosUltimaPartida.ToString ();
		}

		if (DecideUltimaPartida == UltimaPartidaJugada.UltimaPartidaNivel2) {
			_datosUltimaPartida.Append ("\t\t\tResultados ultima partida");
			_datosUltimaPartida.Append ("\n\n");
			_datosUltimaPartida.Append ("Fecha:  ");
			_datosUltimaPartida.Append (dataForRead.HistorialPartidasNivel2[_noPartidasJugadasNivel2 - 1].fecha.ToString ());
			_datosUltimaPartida.Append ("\n");
			_datosUltimaPartida.Append ("Mano seleccionada:  ");
			_datosUltimaPartida.Append (dataForRead.HistorialPartidasNivel2[_noPartidasJugadasNivel2 - 1].ManoSeleccionada.ToString ());
			_datosUltimaPartida.Append ("\n");
			_datosUltimaPartida.Append ("Numero de repeticiones:  ");
			_datosUltimaPartida.Append (dataForRead.HistorialPartidasNivel2[_noPartidasJugadasNivel2 - 1].numeroDeRepeticiones.ToString ());
			_datosUltimaPartida.Append ("\n");
			_datosUltimaPartida.Append ("Numero de ingredientes:  ");
			_datosUltimaPartida.Append (dataForRead.HistorialPartidasNivel2[_noPartidasJugadasNivel2 - 1].numeroDeIngredientes.ToString ());
			_datosUltimaPartida.Append ("\n");
			_datosUltimaPartida.Append ("Tiempo promedio en segundos:  ");
			_datosUltimaPartida.Append (dataForRead.HistorialPartidasNivel2[_noPartidasJugadasNivel2 - 1].tiempoPromedio.ToString ());
			ultimaPartida.text = _datosUltimaPartida.ToString ();
		}

		if (DecideUltimaPartida == UltimaPartidaJugada.UltimaPartidaNivel3) {
			_datosUltimaPartida.Append ("\t\t\tResultados ultima partida");
			_datosUltimaPartida.Append ("\n\n");
			_datosUltimaPartida.Append ("Fecha:  ");
			_datosUltimaPartida.Append (dataForRead.HistorialPartidasNivel3[_noPartidasJugadasNivel3 - 1].fecha.ToString ());
			_datosUltimaPartida.Append ("\n");
			_datosUltimaPartida.Append ("Mano seleccionada:  ");
			_datosUltimaPartida.Append (dataForRead.HistorialPartidasNivel3[_noPartidasJugadasNivel3 - 1].ManoSeleccionada.ToString ());
			_datosUltimaPartida.Append ("\n");
			_datosUltimaPartida.Append ("Numero de repeticiones:  ");
			_datosUltimaPartida.Append (dataForRead.HistorialPartidasNivel3[_noPartidasJugadasNivel3 - 1].numeroDeRepeticiones.ToString ());
			_datosUltimaPartida.Append ("\n");
			_datosUltimaPartida.Append ("Numero de ingredientes:  ");
			_datosUltimaPartida.Append (dataForRead.HistorialPartidasNivel3[_noPartidasJugadasNivel3 - 1].numeroDeIngredientes.ToString ());
			_datosUltimaPartida.Append ("\n");
			_datosUltimaPartida.Append ("Porcentaje de error dedo indice:  ");
			_datosUltimaPartida.Append (dataForRead.HistorialPartidasNivel3[_noPartidasJugadasNivel3 - 1].porcentajDeErrorPan.ToString ());
			_datosUltimaPartida.Append ("\n");
			_datosUltimaPartida.Append ("Porcentaje de error dedo medio:  ");
			_datosUltimaPartida.Append (dataForRead.HistorialPartidasNivel3[_noPartidasJugadasNivel3 - 1].porcentajDeErrorJamon.ToString ());
			_datosUltimaPartida.Append ("\n");
			_datosUltimaPartida.Append ("Porcentaje de error dedo anular:  ");
			_datosUltimaPartida.Append (dataForRead.HistorialPartidasNivel3[_noPartidasJugadasNivel3 - 1].porcentajDeErrorQueso.ToString ());
			_datosUltimaPartida.Append ("\n");
			_datosUltimaPartida.Append ("Porcentaje de error dedo meñique:  ");
			_datosUltimaPartida.Append (dataForRead.HistorialPartidasNivel3[_noPartidasJugadasNivel3 - 1].porcentajDeErrorJitomate.ToString ());
			ultimaPartida.text = _datosUltimaPartida.ToString ();
		}

		DibujaGrafica ();
	}

	void DibujaGrafica(){
		/*si el numero de partidas jugadas es mayor a 21 entocnes se tendra que aumentar el tamaño
		*tento del workspace como de Grid asi como su posicion, por cada 2 partidas el tamaño de 
		*workspace aumentara en +55 width y -172.5 PosX   
		*y el de grid en  +55 width y +27.5 PosX
		 */

		//		_noPartidasJugadas = 55;
		//nombrePaciente2.text = nombrePaciente.text;
//		int aAumentar=1;
//		if (_noPartidasJugadas > 21) {
//			aAumentar=(int)((_noPartidasJugadas-21)/2f);
//			float tempW=workSpace.GetComponent<RectTransform> ().rect.width;
//			float tempH=workSpace.GetComponent<RectTransform> ().rect.height;
//			tempW += 55f * aAumentar;
//			//aumentando el ancho del WorkSpace
//			workSpace.GetComponent<RectTransform> ().sizeDelta=new Vector2(tempW, tempH);
//			float tempX=workSpace.GetComponent<RectTransform> ().anchoredPosition.x;
//			tempX -= 172.5f * aAumentar;
//			//Se tiene que mover la posX del WorkSpace porque al aumentar su ancho se desajusta
//			workSpace.GetComponent<RectTransform> ().anchoredPosition=new Vector2(tempX, -7.9f);
//			float tempW2=grid.GetComponent<RectTransform> ().rect.width;
//			float tempH2=grid.GetComponent<RectTransform> ().rect.height;
//			tempW2 += 55f * aAumentar;
//			//aumentando el ancho del grid en proporcion al ancho que se aumento el WorkSpace
//			grid.GetComponent<RectTransform> ().sizeDelta=new Vector2(tempW2, tempH2);
//			float tempX2=grid.GetComponent<RectTransform> ().anchoredPosition.x;
//			tempX2 += 27.5f * aAumentar;
//			//de igual forma que con el Workspace, se tiene que cambiar la posX del grid para que no se desajuste
//			grid.GetComponent<RectTransform> ().anchoredPosition=new Vector2(tempX2, 158.4315f);
//
//		}
//
		//Para la grafica cada dia sera una unidad en el eje X y en el eje Y cada 10% de exito sera una unidad

		//int fecha_inicial = CalculaDiasTranscurridos (dataForRead.HistorialPartidas [0].Date);
		int fecha_inicial = CalculaDiasTranscurridos (dataForRead.HistorialPartidasNivel1[0].fecha);

		graficaIndice.Points = new Vector2[_noPartidasJugadasNivel1];
		for (int i=0; i<graficaIndice.Points.Length; i++) {
			float partida_x = CalculaDiasTranscurridos (dataForRead.HistorialPartidasNivel1 [i].fecha)%fecha_inicial;
			float tiempo_y = dataForRead.HistorialPartidasNivel1 [i].tiempoDedoIndice;
			Debug.Log("Partida"+ partida_x);
			Debug.Log("Tiempo"+ tiempo_y);

			//graphic.Points[i].Set(27.5f*i,27.5f*i);
			//graphic.Points[i].Set(27.5f*time_x,27.5f*exito_y);
			if(_noPartidasJugadasNivel1>1)
				graficaIndice.Points[i].Set(30f*partida_x,9f*tiempo_y);
			else{
				graficaIndice.gameObject.SetActive(false);
			}
			//Se crea un boton por cada partida jugada
			Button partida_button=Instantiate(graphic_button);
			partida_button.transform.SetParent(workSpace.transform);
			Vector2 button_position=new Vector2(20f*partida_x, 9f*tiempo_y);
			partida_button.GetComponent<RectTransform>().anchoredPosition=button_position;
			//partida_button.name="partida_"+i;
			partida_button.name=i.ToString();
			//importante quitar los listener cuando el GameObject se destruya o deshabilite con DestroyListener
			//A cada boton que se ha creado se le agrega un listener para poder llamar a la funcion MoreInfoPartida tomando como argumento
			//el nombre del boton para saber el indice de la partida y mostrar sus respectiva info.
			partida_button.onClick.AddListener(()=>MoreInfoPartida(int.Parse(partida_button.name),"Grafica_Panel",button_position));
			botonesxNivel.Add(partida_button);
		}
	}

	public void MoreInfoPartida(int index, string nameOfParentPanel, Vector2 button_position){
		//Se instancia la pequeña ventana de datos y se agrega a la lista de ventanas
		//Se crea una ventana cada vez que se presiona el boton corresopndiente a la partida jugada
		if (data_window) {
			GameObject window = (GameObject)Instantiate (data_window, transform.position + new Vector3 (button_position.x - 150f, button_position.y - 200f, 0f), transform.rotation);
			window.transform.SetParent (GameObject.Find (nameOfParentPanel).transform);
			window.transform.localScale = new Vector3 (1f, 1f, 1f);
			cuadrosDeInfo.Add (window);
			//Si se necesitara ahondar mas niveles de jerarquía sería Find("NameOfGameObject/NameOfGameObject/.../NameOfTarget")
			//window.transform.Find ("Fecha").GetComponent<Text> ().text = dataForRead.HistorialPartidas [index].Date;
			window.transform.Find ("Fecha").GetComponent<Text> ().text = dataForRead.HistorialPartidasNivel1[index].fecha;
			window.transform.Find ("Nivel").GetComponent<Text> ().text += dataForRead.HistorialPartidasNivel1 [index].nivel.ToString () + System.Math.Round ((button_position.y / 27.5f) * 10f, 2).ToString ();
			window.transform.Find ("Mano Seleccionada").GetComponent<Text> ().text += dataForRead.HistorialPartidasNivel1 [index].ManoSeleccionada.ToString ();
			window.transform.Find ("Tiempo Dedo Indice").GetComponent<Text> ().text += dataForRead.HistorialPartidasNivel1 [index].tiempoDedoIndice.ToString () + "segundos"; //nosets
		}
	}

	//Devuelve true si la partida mas reciente es la Personal, false si la partida mas reciente es la que se jugo por Nivel
	UltimaPartidaJugada ComparaFechasEntrePartidas(string fechaPartidaNivel1, string fechaPartidaNivel2, string fechaPartidaNivel3){
		float diasNivel1=0;
		float diasNivel2=0;
		float diasNivel3=0;

		if (!fechaPartidaNivel1.Equals ("")) {
			diasNivel1 += (float)CalculaDiasTranscurridos (fechaPartidaNivel1.Substring (0, 10));
			diasNivel1 += CalculaTiempoHHMMSS(fechaPartidaNivel1.Substring (11,8));
		}
		if (!fechaPartidaNivel2.Equals ("")) {
			diasNivel2 += (float)CalculaDiasTranscurridos (fechaPartidaNivel2.Substring (0, 10));
			diasNivel2 += CalculaTiempoHHMMSS(fechaPartidaNivel2.Substring (11, 8));
		}
		if (!fechaPartidaNivel3.Equals ("")) {
			diasNivel3 += (float)CalculaDiasTranscurridos (fechaPartidaNivel3.Substring (0, 10));
			diasNivel3 += CalculaTiempoHHMMSS(fechaPartidaNivel3.Substring (11, 8));
		}

		Debug.Log ("dias nivel 1 "+diasNivel1+", " + "dias nivel 2 "+diasNivel2 + "y dias del nivel 3 " + diasNivel3);

		if (diasNivel1 > diasNivel2 && diasNivel1 > diasNivel3) {
			DecideUltimaPartida = UltimaPartidaJugada.UltimaPartidaNivel1;
			return DecideUltimaPartida;
		}

		if (diasNivel2 > diasNivel1 && diasNivel2 > diasNivel3) {
			DecideUltimaPartida = UltimaPartidaJugada.UltimaPartidaNivel2;
			return DecideUltimaPartida;
		}
		if (diasNivel3 > diasNivel1 && diasNivel3 > diasNivel2) {
			DecideUltimaPartida = UltimaPartidaJugada.UltimaPartidaNivel2;
			return DecideUltimaPartida;
		}

		return UltimaPartidaJugada.Ninguna;
	}

	float CalculaTiempoHHMMSS(string date){
		int time = 0;
		Debug.Log(date+"LALALA");
		if(date.Length>1){
			Debug.Log(date.Substring(0,2));
			Debug.Log(date.Substring(3,2));
			Debug.Log(date.Substring(6,2));
			time+=int.Parse(date.Substring(0,2))*3600;
			time+=int.Parse(date.Substring(3,2))*60;
			time+=int.Parse(date.Substring(6,2));
		}
		return time/86400f;
	}

	//Esta funcion devuelve cuantos dias han pasado en el año hasta el dia que se encuentra en el argumento date  yyyy-MM-dd  dd/MM/yyyy
	int CalculaDiasTranscurridos(string date){

		//		int dia=int.Parse(date.Substring(0,2));
		//		int año=int.Parse(date.Substring(date.LastIndexOf("/")+3,2)); 
		//		int dias_transcurridos = dia + DiasMes(date.Substring(date.IndexOf("/")+1,2)) + (año* 365);

		int año=int.Parse(date.Substring(2,2));
		int dia=int.Parse(date.Substring(date.LastIndexOf("-")+1,2)); 
		int dias_transcurridos = dia + DiasMes(date.Substring(date.IndexOf("-")+1,2)) + (año* 365);
		return dias_transcurridos;

	}

	//Esta funcion devuelve cuantos dias han pasado en el año hasta el mes indicado en el argumento mes
	int DiasMes(string mes){
		int enero = 31;
		int febrero = enero + 29;
		int marzo = febrero + 31;
		int abril = marzo + 30;
		int mayo = abril + 31;
		int junio = mayo + 30;
		int julio = junio + 31;
		int agosto = julio + 31;
		int septiembre = agosto + 30;
		int octubre = septiembre + 31;
		int noviembre = octubre + 30;
		int diciembre = noviembre + 31;

		switch(mes){
		case "01":
			return enero;
		case "02":
			return febrero;
		case "03":
			return marzo;
		case "04":
			return abril;
		case "05":
			return mayo;
		case "06":
			return junio;
		case "07":
			return julio;
		case "08":
			return agosto;
		case "09":
			return septiembre;
		case "10":
			return octubre;
		case "11":
			return noviembre;
		case "12":
			return diciembre;
		default:
			return 0; 
		}

	}

	public void CancelarBoton(){
		SceneManager.LoadScene ("Interfaz De Clinica");
	}

	public void RegresarBoton(){
		resultadosPanel.gameObject.SetActive (true);
		resultadosNivel1Panel.gameObject.SetActive (false);
		resultadosNivel2Panel.gameObject.SetActive (false);
		resultadosNivel3Panel.gameObject.SetActive (false);
	}

	public void  VerResultadosNivel1Boton(){
		resultadosPanel.gameObject.SetActive (false);
		resultadosNivel1Panel.gameObject.SetActive (true);
	}

	public void  VerResultadosNivel2Boton(){
		resultadosPanel.gameObject.SetActive (false);
		resultadosNivel2Panel.gameObject.SetActive (true);
	}

	public void  VerResultadosNivel3Boton(){
		resultadosPanel.gameObject.SetActive (false);
		resultadosNivel3Panel.gameObject.SetActive (true);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
