using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cameraMovements : MonoBehaviour {
		
		private UDPReceive faceAPI;
		
		public float multiplyer = 1;
		

	
	Queue<float> pitchValues = new Queue<float>();
	Queue<float> yawValues = new Queue<float>();
	Queue<float> rollValues = new Queue<float>();

	public const int MAXVALUES = 10;

	GameObject Luna;
	Vector3 cameraPosition;
	
	
	float getAverage(Queue<float> values){
		float average = 0;
		foreach (float value in values) {
			average += value;
		}
		return average / MAXVALUES;
	}


	void smoothValues(ref float pitch, ref float roll, ref float yaw){
		
		//Remove values from queue if it is full
		if(pitchValues.Count > MAXVALUES){
			pitchValues.Dequeue();
			rollValues.Dequeue();
			yawValues.Dequeue();
		}
		
		//Add values to queue if the difference is not too big (to avoid jitter and have more smooth motions
//		if(Math.Abs(irX - getAverage(pitchValues)) < 1.0) 
//			pitchValues.Enqueue(irX);
//		if(Math.Abs(irY - getAverage(irYValues)) < 1.0) 
//			irYValues.Enqueue(irY);

		pitchValues.Enqueue (pitch);
		rollValues.Enqueue (roll);
		yawValues.Enqueue (yaw);

		//Get average new IR value ( again to ensure smooth motion since raw signal is very noisy)
		pitch = getAverage(pitchValues);
		yaw = getAverage(yawValues);
		roll = getAverage(rollValues);
		
	}


		void Start()
		{
			GameObject contoller = GameObject.FindGameObjectWithTag("GameController");
			faceAPI = contoller.GetComponentInChildren<UDPReceive>();

			Luna = GameObject.Find("Luna");
			cameraPosition = transform.position;
		}
		
		void Update()
		{
			//this.transform.eulerAngles = new Vector3(-faceAPI.pitch, 0, 0);

		var pitch = faceAPI.pitch;
		var roll = faceAPI.roll;
		var yaw = faceAPI.yaw;

		smoothValues (ref pitch, ref roll, ref yaw);

		Quaternion xQuaternionP = Quaternion.AngleAxis(pitch * -multiplyer, new Vector3(1, 0, 0));
		Quaternion xQuaternionR = Quaternion.AngleAxis(roll * multiplyer, new Vector3(0, 0, 1));
		Quaternion xQuaternionY = Quaternion.AngleAxis(yaw * -multiplyer, new Vector3(0, 1, 0));
		transform.localRotation = xQuaternionP*xQuaternionR*xQuaternionY;

//		Debug.Log ("Position; " + transform.position.x + " , " + transform.position.y + " , " + transform.position.z);


//		transform.position = cameraPosition;
//		transform.position = Luna.transform.position;
//		transform.localPosition = cameraPosition;
//		
//		transform.localRotation = xQuaternionP;
//		transform.localRotation = xQuaternionY;
//		transform.localRotation = xQuaternionR;

//		transform.Rotate(new Vector3(1, 0, 0), -pitch * multiplyer * Time.deltaTime);
//		transform.Rotate(new Vector3(0, 0, 1), roll * multiplyer * Time.deltaTime);
//		transform.Rotate(new Vector3(0, 1, 0), -yaw * multiplyer * Time.deltaTime);

		}

}
