using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using System.Text;
using System.Linq;

enum UltimaPartidaJugada {Ninguna, UltimaPartidaNivel1, UltimaPartidaNivel2, UltimaPartidaNivel3}
//[RequireComponent(typeof(AudioSource))]
public class ResultadosClinica : MonoBehaviour {
//	[SerializeField]
//	[Tooltip("Probando rangos")]
//	[Range(-1.0f,1.0f)]
//	private float velocidad;

	public Text nombrePaciente;
	public Text noPartidasJugadasNivel1;
	public Text noPartidasJugadasNivel2;
	public Text noPartidasJugadasNivel3;
	public Text ultimaPartida;

	private static InfoPartida dataForRead;
	public static InfoPartida DataForRead {
		get { return dataForRead; }
	}

	private int _noPartidasJugadasNivel1;
	private int _noPartidasJugadasNivel2;
	private int _noPartidasJugadasNivel3;
	private string _fechaPartidaNivel1, _fechaPartidaNivel2, _fechaPartidaNivel3;
	private string _nombrePaciente;
	private StringBuilder _datosUltimaPartida;

	UltimaPartidaJugada DecideUltimaPartida;


	void Awake(){
		_datosUltimaPartida = new StringBuilder ();
	}

	// Use this for initialization
	void Start () {
//		#if UNITY_EDITOR
//		if (workSpace == null) {
//			Debug.LogWarning ("Falta que");
//			Debug.Break ();
//		}
//		#endif

		dataForRead = new InfoPartida ();
		dataForRead = GameSaveLoad.Load (GameSaveLoad._FileLocation +"\\"+GameSaveLoad._PacienteName+"_DataSandwich.xml");
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
		MuestraDatosUltimaPartida ();
	}
		
	void MuestraDatosUltimaPartida(){
		if (DecideUltimaPartida == UltimaPartidaJugada.UltimaPartidaNivel1) {
			_datosUltimaPartida.AppendLine ("\t\t\tResultados ultima partida");
			_datosUltimaPartida.AppendLine ("Fecha:  " + dataForRead.HistorialPartidasNivel1 [_noPartidasJugadasNivel1 - 1].fecha.ToString ());
			_datosUltimaPartida.AppendLine ("Nivel:  " + dataForRead.HistorialPartidasNivel1 [_noPartidasJugadasNivel1 - 1].nivel.ToString ());
			_datosUltimaPartida.AppendLine ("Mano seleccionada:  " + dataForRead.HistorialPartidasNivel1 [_noPartidasJugadasNivel1 - 1].ManoSeleccionada.ToString ());
			_datosUltimaPartida.AppendLine ("Tiempo que le tomó apilar cada uno de los ingredientes:  ");
			_datosUltimaPartida.AppendLine ("- Indice: " + dataForRead.HistorialPartidasNivel1 [_noPartidasJugadasNivel1 - 1].tiempoDedoIndice.ToString () + " s");
			_datosUltimaPartida.AppendLine ("- Medio: " + dataForRead.HistorialPartidasNivel1 [_noPartidasJugadasNivel1 - 1].tiempoDedoMedio.ToString () + " s");
			_datosUltimaPartida.AppendLine ("- Anular: " + dataForRead.HistorialPartidasNivel1 [_noPartidasJugadasNivel1 - 1].tiempoDedoAnular.ToString () + " s");
			_datosUltimaPartida.AppendLine ("- Meñique: " + dataForRead.HistorialPartidasNivel1 [_noPartidasJugadasNivel1 - 1].tiempoDedoMeñique.ToString () + " s");
			ultimaPartida.text = _datosUltimaPartida.ToString ();
			return;
		}

		if (DecideUltimaPartida == UltimaPartidaJugada.UltimaPartidaNivel2) {
			_datosUltimaPartida.AppendLine ("\t\t\tResultados ultima partida");
			_datosUltimaPartida.AppendLine ("Fecha:  " + dataForRead.HistorialPartidasNivel2[_noPartidasJugadasNivel2 - 1].fecha.ToString ());
			_datosUltimaPartida.AppendLine ("Nivel:  " + dataForRead.HistorialPartidasNivel2 [_noPartidasJugadasNivel2 - 1].nivel.ToString ());
			_datosUltimaPartida.AppendLine ("Mano seleccionada:  " + dataForRead.HistorialPartidasNivel2[_noPartidasJugadasNivel2 - 1].ManoSeleccionada.ToString ());
			_datosUltimaPartida.AppendLine ("Numero de repeticiones:  " + dataForRead.HistorialPartidasNivel2[_noPartidasJugadasNivel2 - 1].numeroDeRepeticiones.ToString ());
			_datosUltimaPartida.AppendLine ("Numero de ingredientes:  " + dataForRead.HistorialPartidasNivel2[_noPartidasJugadasNivel2 - 1].numeroDeIngredientes.ToString ());
			_datosUltimaPartida.AppendLine ("Tiempo promedio:  " + dataForRead.HistorialPartidasNivel2[_noPartidasJugadasNivel2 - 1].tiempoPromedio.ToString () + " s");
			ultimaPartida.text = _datosUltimaPartida.ToString ();
			return;
		}

		if (DecideUltimaPartida == UltimaPartidaJugada.UltimaPartidaNivel3) {
			_datosUltimaPartida.AppendLine ("\t\t\tResultados ultima partida");
			_datosUltimaPartida.AppendLine ("Fecha:  " + dataForRead.HistorialPartidasNivel3[_noPartidasJugadasNivel3 - 1].fecha.ToString ());
			_datosUltimaPartida.AppendLine ("Nivel:  " + dataForRead.HistorialPartidasNivel3 [_noPartidasJugadasNivel3 - 1].nivel.ToString ());
			_datosUltimaPartida.AppendLine ("Mano seleccionada:  " + dataForRead.HistorialPartidasNivel3[_noPartidasJugadasNivel3 - 1].ManoSeleccionada.ToString ());
			_datosUltimaPartida.AppendLine ("Numero de repeticiones:  " + dataForRead.HistorialPartidasNivel3[_noPartidasJugadasNivel3 - 1].numeroDeRepeticiones.ToString ());
			_datosUltimaPartida.AppendLine ("Numero de ingredientes:  " + dataForRead.HistorialPartidasNivel3[_noPartidasJugadasNivel3 - 1].numeroDeIngredientes.ToString ());
			_datosUltimaPartida.AppendLine ("Porcentaje de error dedo indice:  " + dataForRead.HistorialPartidasNivel3[_noPartidasJugadasNivel3 - 1].porcentajDeErrorPan.ToString ());
			_datosUltimaPartida.AppendLine ("Porcentaje de error dedo medio:  " + dataForRead.HistorialPartidasNivel3[_noPartidasJugadasNivel3 - 1].porcentajDeErrorJamon.ToString ());
			_datosUltimaPartida.AppendLine ("Porcentaje de error dedo anular:  " + dataForRead.HistorialPartidasNivel3[_noPartidasJugadasNivel3 - 1].porcentajDeErrorQueso.ToString ());
			_datosUltimaPartida.AppendLine ("Porcentaje de error dedo meñique:  " + dataForRead.HistorialPartidasNivel3[_noPartidasJugadasNivel3 - 1].porcentajDeErrorJitomate.ToString ());
			_datosUltimaPartida.AppendLine ("(-1: El ingrediente no se generó)");
			ultimaPartida.text = _datosUltimaPartida.ToString ();
			return;
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
			DecideUltimaPartida = UltimaPartidaJugada.UltimaPartidaNivel3;
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
		
	public void  VerResultadosNivel1Boton(){
		SceneManager.LoadScene ("Resultados Nivel 1 (Clinica)");
	}

//	public void  VerResultadosNivel2Boton(){
//		resultadosPanel.gameObject.SetActive (false);
//		resultadosNivel2Panel.gameObject.SetActive (true);
//	}
//
//	public void  VerResultadosNivel3Boton(){
//		resultadosPanel.gameObject.SetActive (false);
//		resultadosNivel3Panel.gameObject.SetActive (true);
}
