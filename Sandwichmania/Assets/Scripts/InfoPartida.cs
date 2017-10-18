using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InfoPartida  {
	public string id;
	public int mano = 1; //mano derecha 1 y mano izquierda 0
	public int nivel;
	public int rutina; //0 >>> Rutina ... 1 >>> Sin Rutina
	public int numeroDeRepeticiones;
	public int numeroDeIngredientes;
	public int numeroDeRepeticionesNivel3 = 1;
	public int numeroDeIngredientesNivel3 = 5;
	public float tiempoDedoIndice, tiempoDedoMedio, tiempoDedoAnular, tiempoDedoMeñique;
}
