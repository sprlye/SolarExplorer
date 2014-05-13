using UnityEngine;
using System;

public class RayCastDemo : MonoBehaviour {
	
	public Transform target1, target2;
	private string display;
	
	void Update () {
		if (Input.GetMouseButton(0)) {
			Ray ray = Camera.current.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			display = "mouseposition( " + Input.mousePosition.x + "," + Input.mousePosition.y + "," + Input.mousePosition.z + ")";
			if (Physics.Raycast(ray, out hit)) {
				if (hit.transform == target1) {
					Debug.Log("Hit target 1");
				} else if (hit.transform == target2) {
					Debug.Log("Hit target 2");
				}
				Debug.Log("Hit something " + hit.transform.gameObject.name);
			} else {
				Debug.Log("Hit nothing");
			}
		}
	}
	
	void OnGUI() {
		GUI.Label( new Rect(10,10, 500, 100), display);
	}
	
}
