using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public enum Mano {Derecha, Izquierda};
public enum Rutina {SinRutina, ConRutina}

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
	public Rutina RutinaSeleccionada;
	public Mano ManoSeleccionada;
	public List<ActivaPanelDedos> Rutina;
	public int numeroDeRepeticiones;
	public int numeroDeIngredientes;
}

public class Nivel3{
	public int nivel;
	public Mano ManoSeleccionada;
	public int numeroDeRepeticiones;
	public int numeroDeIngredientes;
}