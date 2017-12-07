using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public enum Mano {Derecha, Izquierda};

public class InfoPartida  {
	public string nombre;
	public int id;
	public List <Nivel1> HistorialPartidasNivel1 = new List<Nivel1> ();
	public List <Nivel2> HistorialPartidasNivel2 = new List<Nivel2> ();
	public List <Nivel3> HistorialPartidasNivel3 = new List<Nivel3>();
}

public class TerapeutaData{
	public string Nombre;
	public int Id;
}
	
public class Nivel1{
	public int nivel;
	public Mano ManoSeleccionada;
	public float tiempoDedoIndice; 
	public float tiempoDedoMedio; 
	public float tiempoDedoAnular;
	public float tiempoDedoMeñique;
}

public class Nivel2{
	public int nivel;
	public string nombreDeRutina;
	//public Rutina RutinaSeleccionada;
	public Mano ManoSeleccionada;
	public List<ActivaPanelDedos> Rutina;
	public List<float> tiempos = new List<float>();
	public float tiempoPromedio;
	public int numeroDeRepeticiones;
	public int numeroDeIngredientes;
}

public class Nivel3{
	public int nivel;
	public Mano ManoSeleccionada;
	public int numeroDeRepeticiones;
	public int numeroDeIngredientes;
	public float porcentajDeErrorPan;
	public float porcentajDeErrorJamon;
	public float porcentajDeErrorQueso;
	public float porcentajDeErrorJitomate;
}