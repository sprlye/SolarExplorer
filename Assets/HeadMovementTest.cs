using UnityEngine;
using System.Collections;

public class HeadMovementTest : MonoBehaviour {

	float turnSpeed = 100.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.N))
		{	
			transform.Rotate(-Vector3.up, turnSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.M))
		{
			transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
		}
	}
}
