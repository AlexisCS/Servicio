using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdminPanel : MonoBehaviour {
	public GameObject[] panels;
	// Use this for initialization
	public void Nivel (){
		DestroyObject (panels[0]);
		panels [1].SetActive (true);
	}
		
	public void Return (){
		
	
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
