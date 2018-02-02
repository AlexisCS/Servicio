using UnityEngine;
using System.Collections;

public class WindowBehaviour : MonoBehaviour {
	float offsetX;
	float offsetY;

	// Use this for initialization
	void Start () {
	
	}

	public void BeginDrag(){
		offsetX = transform.position.x - Input.mousePosition.x;
		offsetY = transform.position.y - Input.mousePosition.y;
	}

	public void OnDrag(){
		transform.position = new Vector3 (offsetX+Input.mousePosition.x, offsetY+Input.mousePosition.y);
	}

	public void CloseWindow(){
		Destroy (this.gameObject);
	}



	// Update is called once per frame
	void Update () {
	
	}
}
