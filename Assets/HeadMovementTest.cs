using UnityEngine;
using System.Collections;

public class HeadMovementTest : MonoBehaviour {

	float turnSpeed = 100.0f;
	float moveSpeed = 0.5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.J))
		{	
			transform.Rotate(-Vector3.up, turnSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.K))
		{
			transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
		}
		
		if(Input.GetKey(KeyCode.N))
		{	
			GameObject Luna = GameObject.Find("Luna");
			Movements lunam = (Movements)Luna.GetComponent(typeof(Movements));

			transform.Translate(lunam.LEFT * moveSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.M))
		{	
			GameObject Luna = GameObject.Find("Luna");
			Movements lunam = (Movements)Luna.GetComponent(typeof(Movements));

			transform.Translate(lunam.RIGHT * moveSpeed * Time.deltaTime);
		}
	}
}
