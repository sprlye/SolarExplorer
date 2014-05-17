using UnityEngine;
using System;

public class RayCastDemo : MonoBehaviour {
	
	public Transform target1, target2;
	private string display;
	
	GameObject go;
	bool hittingObject;
	
	
	void Update () {
		if (Input.GetMouseButton(0)) {

			Ray ray = Camera.current.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			display = "mouseposition( " + Input.mousePosition.x + "," + Input.mousePosition.y + "," + Input.mousePosition.z + ")";
			if (!hittingObject && Physics.Raycast(ray, out hit)) {
				if (hit.transform == target1) {
					Debug.Log("Hit target 1");
				} else if (hit.transform == target2) {
					Debug.Log("Hit target 2");
				}
				Debug.Log("Hit something " + hit.transform.gameObject.name);
				
				go = hit.transform.gameObject;
				hittingObject = true;
				
				
			}
			
			
			if(hittingObject){
				//Debug.Log("Moharhar");
				Vector3 temp = Input.mousePosition;
				temp.z = go.transform.position.z - Camera.current.transform.position.z;
				
				go.transform.Translate(Camera.current.ScreenToWorldPoint(temp) - go.transform.position);
			}
		} 
		else {
			Debug.Log("Hit nothing");
			hittingObject = false;
		}

		
		
	}
	
	void OnGUI() {
		GUI.Label( new Rect(10,10, 500, 100), display);
	}
	
}
