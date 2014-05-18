using UnityEngine;
using System.Collections;

public class armMovementMouse : MonoBehaviour {

	Quaternion targetrotation;
	Plane targetPlane;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//Create target plane
		targetPlane = new Plane (-this.transform.forward, 
		                               new Vector3(transform.position.x, transform.position.y, transform.position.z) + this.transform.forward*3);

		float dist = 10.0f;

		//Cast a ray from  mouse 
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		//Check if it hits the plane		
		if (targetPlane.Raycast (ray, out dist)) {

			//Get targetpoint as a point on the plane
			var targetpoint = ray.GetPoint (dist);	
			//Set appropriate rotation
			targetrotation = Quaternion.LookRotation (targetpoint - transform.position, transform.up );
			//Rotate arm
			transform.rotation = targetrotation;

		} 
		else {
			//Should not(!) be possible
			Debug.Log("Not hitting the plane");
		}
	}

}
