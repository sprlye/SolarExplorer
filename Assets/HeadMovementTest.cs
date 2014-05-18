using UnityEngine;
using System.Collections;

public class HeadMovementTest : MonoBehaviour {

	float turnSpeed = 1.0f;
	float moveSpeed = 0.5f;

	public float multiplyer = 100.0f;
	public float yOffset = 16;
	private UDPReceive faceAPI;

	Vector3 headposition, lunaDimensions;
	GameObject  Luna, LunaBody;
	public Movements mainCharacter;

	// Use this for initialization
	void Start () {
		GameObject contoller = GameObject.FindGameObjectWithTag("GameController");
		faceAPI = contoller.GetComponentInChildren<UDPReceive>();


		Luna = GameObject.Find ("Luna");
		
//		 LunaBody = GameObject.Find ("LunaBody");
//		headposition = this.transform.position - LunaBody.transform.position;


	}
	
	// Update is called once per frame
//	void Update () {
//		if(Input.GetKey(KeyCode.J))
//		{	
//			transform.Rotate(-Vector3.up, turnSpeed * Time.deltaTime);
//		}
//		if(Input.GetKey(KeyCode.K))
//		{
//			transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
//		}
//		
//		if(Input.GetKey(KeyCode.N))
//		{	
//			GameObject Luna = GameObject.Find("Luna");
//			Movements lunam = (Movements)Luna.GetComponent(typeof(Movements));
//
//			transform.Translate(lunam.LEFT * moveSpeed * Time.deltaTime);
//		}
//		if(Input.GetKey(KeyCode.M))
//		{	
//			GameObject Luna = GameObject.Find("Luna");
//			Movements lunam = (Movements)Luna.GetComponent(typeof(Movements));
//
//			transform.Translate(lunam.RIGHT * moveSpeed * Time.deltaTime);
//		}
//	}

	// Update is called once per frame
	void Update () {
		Vector3 pos = new Vector3(faceAPI.xPos * multiplyer, faceAPI.yPos * multiplyer, (faceAPI.zPos * -multiplyer) + yOffset);
//
		var x = faceAPI.xPos * multiplyer;
		var y = faceAPI.yPos * multiplyer;
		var z = (faceAPI.zPos * -multiplyer) + 6;
//
//		Vector3 pos = new Vector3(x,y,0);
//		pos.Normalize();
//
//
//		this.transform.position = Luna.transform.position + headposition + pos*0.5f;
		
		//			Movements lunam = (Movements)Luna.GetComponent(typeof(Movements));
		//
		//			transform.Translate(lunam.RIGHT * moveSpeed * Time.deltaTime);

//		Luna.transform.position = pos * 0.2f;
//		this.transform.position = LunaBody.transform.position + headposition;
//
//		pos = x * transform.right + y * transform.up + z * transform.forward;
		this.transform.localPosition = pos;
	}
}
