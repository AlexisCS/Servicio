using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BasuraEstatica {

	private static bool v1=true;

	public static void ActivaODesactivaV(){
		v1 = !v1;
		if (v1) {
			Debug.Log ("encendido");
		} else {
			Debug.Log ("apagado");
		}
	}


}
