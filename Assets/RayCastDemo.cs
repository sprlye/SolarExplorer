using UnityEngine;
using System;

public class RayCastDemo : MonoBehaviour {
	
	public Transform target1, target2;
	private string display;
	
	GameObject interactingObject;
	bool hittingObject;
	Vector3 screenPosition, currentScreenPosition, offset;
	LineRenderer line;
	
	
	void Update () {

		//Get input
		if (Input.GetMouseButton(0)) {

			line.enabled = true;

//			Debug.Log ("Mouseposition: (" + Input.mousePosition.x + " , " + Input.mousePosition.y + ")");
			//Check for hit
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			line.SetPosition(0, this.transform.position);

			
			if (!hittingObject && Physics.Raycast(ray, out hit)) {
//				if (hit.transform == target1) {
//					Debug.Log("Hit target 1");
//				} else if (hit.transform == target2) {
//					Debug.Log("Hit target 2");
//				}
				Debug.Log("Hit something " + hit.transform.gameObject.name);

				//Save that we are hitting something
				interactingObject = hit.transform.gameObject;
				line.SetPosition(1, hit.point);

				hittingObject = true;

				//Save the objects position in screen coordinates
				screenPosition = Camera.main.WorldToScreenPoint(interactingObject.transform.position);

				//calculate difference between object position and mouse position
				offset = interactingObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z));
				
			}
			
			if(hittingObject){

				Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);
				curScreenPoint = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

				interactingObject.transform.position = curScreenPoint;
				line.SetPosition(1, curScreenPoint);
			}
			else{
				line.SetPosition(1, this.transform.position);
			}



		} 
		else {
			Debug.Log("Hit nothing");
			hittingObject = false;
			line.enabled = false;

		}

		
		
	}
	
	void OnGUI() {
		GUI.Label( new Rect(10,10, 500, 100), display);
	}

	void Start()
	{
		line = gameObject.GetComponent<LineRenderer>();
		line.enabled = false;
	}

	
}
