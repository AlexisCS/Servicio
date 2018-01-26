using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AjustaCamara : MonoBehaviour {

	public Camera mainCamera;

	void OnEnable(){
		SceneManager.sceneLoaded += OnLoadScene;
	}

	void OnDisable(){
		SceneManager.sceneLoaded -= OnLoadScene;
	}

	void OnLoadScene(Scene scene, LoadSceneMode mode){
		if (scene.buildIndex == 3) {
			mainCamera.orthographicSize = 5.5f;
		} else
			mainCamera.orthographicSize = 5.0f;
	}

}
