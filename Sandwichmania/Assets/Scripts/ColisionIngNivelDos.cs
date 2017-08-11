using UnityEngine;
using System.Collections;

public class ColisionIngNivelDos : MonoBehaviour {
	private string noDestrugTag = "noDestructible";
	private string nuevoTag = "nuevo";
	public string ingrediente;
	private int ejec = 0;
	
	void Start () {
		
	}
	
	void Update () {
		
	}
	
	void OnCollisionEnter2D(Collision2D collision){
		//cuando colisionan por primera vez
		if(ejec == 0 && this.gameObject.tag == nuevoTag && ejec == 0){
			//print("he colisionado con algo");
			NotificationCenter.DefaultCenter ().PostNotification(this,"CambiaIngrediente",ingrediente);
			GetComponent<AudioSource>().Play();
		}
		
		ejec ++;
		this.gameObject.tag = noDestrugTag;
	}
	
}