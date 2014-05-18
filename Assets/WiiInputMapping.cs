using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;

public class WiiInputMapping : MonoBehaviour {

	LineRenderer line;

	// Basic wii function
	[DllImport ("UniWii")]
	private static extern void wiimote_start();
	[DllImport ("UniWii")]
	private static extern void wiimote_stop();
	[DllImport ("UniWii")]
	private static extern int wiimote_count();
	[DllImport ("UniWii")]	
	private static extern bool wiimote_enableIR( int which );


	//Acceleration
	[DllImport ("UniWii")]
	private static extern byte wiimote_getAccX(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getAccY(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getAccZ(int which);

	//Pointer
	[DllImport ("UniWii")]
	private static extern float wiimote_getIrX(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getIrY(int which);
//	[DllImport ("UniWii")]
//	private static extern float wiimote_isIREnabled(int which);

	//Gyroscope
	[DllImport ("UniWii")]
	private static extern float wiimote_getRoll(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getPitch(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getYaw(int which);

	//Wii buttons
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonA(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonB(int which);


	//Nunchuck functions
	[DllImport ("UniWii")]
	private static extern int wiimote_isExpansionPortEnabled(int which);

	[DllImport ("UniWii")]
	private static extern byte wiimote_getNunchuckStickX(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getNunchuckStickY(int which);
	
	[DllImport ("UniWii")]
	private static extern byte wiimote_getNunchuckAccX(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getNunchuckAccZ(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonNunchuckC(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonNunchuckZ(int which);



	//Main charcter objects required
	public Movements mainCharacter;
	GameObject Luna;
	
	//Testing text
	String display;

	//Constants
	public const int PLAYER1 = 0;

	//Wiimote data
	Vector3 acceleration, gyro;

	//Objects and information required for interaction with an object
	bool wiimoteInteractingWithObject;
	GameObject interactingObject;
	int pointerX, pointerY;
	string cursorText;
	Vector3 screenPosition, offset;


	//Variables needed for the handling of the IR Data
	Queue<float> irXValues = new Queue<float>();
	Queue<float> irYValues = new Queue<float>();
	public const int IRVALUES = 20;


	float getAverageIR(Queue<float> irvalues){
		float average = 0;
		foreach (float value in irvalues) {
			average += value;
		}
		return average / IRVALUES;
	}

	void smoothValues(ref float irX, ref float irY){
		
		//Remove values from queue if it is full
		if(irXValues.Count > IRVALUES){
			irXValues.Dequeue();
			irYValues.Dequeue();
		}

		var smoothing = 1.0;
		//Add values to queue if the difference is not too big (to avoid jitter and have more smooth motions
		if(Math.Abs(irX - getAverageIR(irXValues)) < smoothing) 
			irXValues.Enqueue(irX);
		if(Math.Abs(irY - getAverageIR(irYValues)) < smoothing) 
			irYValues.Enqueue(irY);
		
		//Get average new IR value ( again to ensure smooth motion since raw signal is very noisy)
		irX = getAverageIR(irXValues);
		irY = getAverageIR(irYValues);
		
	}
	
	void Update() {
		
		int c = wiimote_count();
		if (c>0) {
			display = "";
			int x = wiimote_getAccX(PLAYER1);
			int y = wiimote_getAccY(PLAYER1);
			int z = wiimote_getAccZ(PLAYER1);
			float roll = Mathf.Round(wiimote_getRoll(PLAYER1));
			float p = Mathf.Round(wiimote_getPitch(PLAYER1));
			float yaw = Mathf.Round(wiimote_getYaw(PLAYER1));
			float ir_x = wiimote_getIrX(PLAYER1);
			float ir_y = wiimote_getIrY(PLAYER1);
			display += "Wiimote " + PLAYER1 + "\n " +
						"accX: " + x + " accY: " + y + " accZ: " + z + "\n " +
						"roll: " + roll + " pitch: " + p + " yaw: " + yaw + " \n IR X: " + ir_x + " IR Y: " + ir_y + "\n";
			display += "\n Nunchuck:\n "+
				"(x: " + wiimote_getNunchuckStickX(PLAYER1) + " , y:" + wiimote_getNunchuckStickY(PLAYER1) + " )\n " + 
					"(ax: " + wiimote_getNunchuckAccX(PLAYER1) + " , ay:" + wiimote_getNunchuckAccZ(PLAYER1) + " )";
			
			//WIMOTE VALUES
			//ROll: Restposition -50 below how they change / increase
			// - buttons upward: -50
			// - buttons toward screen: 30
			// - buttons downards: -50
			// - buttons towards plays: -105

			//PITCH: Restposition -57
			// buttons upward : - 57
			// buttons to the rigth : - 110
			// buttons down : - 57
			// butons to the left: 33

			// YAW: Restposition: - 165
			// - very complicated, dont get it. values changes when both the otehrs are changed. 

			//Nunchuck X values from 20 - 200, resposition: 120;
			//Nunchuck Y valyes from 27 - 227, restposition: 130;


			//X = left / right
			// should rotate around up vector

			int nunchuckX = wiimote_getNunchuckStickX(PLAYER1);
			int nunchuckY = wiimote_getNunchuckStickY(PLAYER1);

			//float scaleFactor = 200.0f;

			float nunchuckXSpeed = Math.Abs(120f - nunchuckX)/100;
			float nunchuckYSpeed = Math.Abs(130f - nunchuckY)/100;


			if( nunchuckX < 110 && nunchuckX > 10 ){
				mainCharacter.rotateInDirection(mainCharacter.ROTATE_LEFT*nunchuckXSpeed);
			}
			if( nunchuckX > 130 ){
				mainCharacter.rotateInDirection(mainCharacter.ROTATE_RIGHT*nunchuckXSpeed);
			}
			
			if(nunchuckY < 120 && nunchuckY  > 10 ){
				mainCharacter.rotateInDirection(mainCharacter.ROTATE_DOWN*nunchuckYSpeed);
			}
			if(nunchuckY  > 140 ){
				mainCharacter.rotateInDirection(mainCharacter.ROTATE_UP*nunchuckYSpeed);
			}

			if(wiimote_getButtonNunchuckZ(PLAYER1)){
				mainCharacter.moveInDirection(mainCharacter.FORWARD);
			}

			if(wiimote_getButtonNunchuckC(PLAYER1)){
				mainCharacter.moveInDirection(mainCharacter.BACKWARD);
			}

			//-- hämta in IR värden (-1,-1) - (1,1)
			float irX = wiimote_getIrX(PLAYER1);
			float irY = wiimote_getIrY(PLAYER1);
			
			//Debug.Log ("X: " + irX + " , Y: " + irY);

			//Check that IR information is available
			if(!float.IsNaN(irX) && !float.IsNaN(irY) && irX != -100 && irY != -100 ){

				//Get smoothed values (average of last 5, without jumps etc)
				smoothValues(ref irX, ref irY);

				//Rescale values to match screen size and get correct type
				pointerX = Mathf.RoundToInt(Screen.width*(irX + 1)/2);
				pointerY = Mathf.RoundToInt(Screen.height* ( 1 - irY)/2);

				//Check if wiimote A button is pressed (ie user wish to interact with the scene
				if(wiimote_getButtonA(PLAYER1)){

					Debug.Log("Tja A");
					line.enabled = true;

					//If user is not already interacting with object check wether he is hitting an object
					if(!wiimoteInteractingWithObject){
						Debug.Log("Throw a ray!");

						//Create a new ray from the camera and shoot into scene
						Ray ray = Camera.current.ScreenPointToRay(new Vector3(pointerX,pointerY,Camera.current.transform.position.z));
						RaycastHit hit;
						line.SetPosition(0, ray.origin);
						
						//Check if it hits something
						if(Physics.Raycast(ray, out hit)){

							//Check which object it hit and save it
							Debug.Log ("Hit object: " + hit.transform.gameObject.name + "\n");
							line.SetPosition(1, hit.point);
							interactingObject = hit.transform.gameObject;

							//Set interacting value to true
							wiimoteInteractingWithObject = true;
							
							//Save the objects position in screen coordinates
							screenPosition = Camera.main.WorldToScreenPoint(interactingObject.transform.position);
							
							//calculate difference between object position and mouse position
							offset = interactingObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(pointerX, -pointerY, screenPosition.z));

						}
						else{
							Debug.Log ("Didn't hit anything :( ");
							line.SetPosition(1, ray.GetPoint(100));

						}
						cursorText = "x: " + pointerX + "\n y: " + pointerY;

					}
					else{
						// TODO decide what to do with the object that is being interacted with. 

						Debug.Log("Interacting with object: " + interactingObject.name);

						cursorText = "x: " + pointerX + "\n y: " + pointerY;
						//interactingObject.transform.Translate();

						
						Vector3 curScreenPoint = new Vector3(pointerX, -pointerY, screenPosition.z);
						curScreenPoint = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
						
						interactingObject.transform.position = curScreenPoint;

					}
					line.enabled = false;
					
				}
				else{
					//If A button is not pressed there is no interaction and it should be dropped. 
					wiimoteInteractingWithObject = false;
				}
			
			}
			else{
				//display = "IR has to be enabled in order for this application to work";
			}
		
		}
		else display = "Press the '1' and '2' buttons on your Wii Remote.";
	}

	void OnGUI(){
		GUI.Label( new Rect(20,Screen.height-150, 500, 150), display);
		GUI.Box (new Rect (pointerX, pointerY, 50, 50), cursorText);

	}
	
	void Start (){

		//Main player
		Luna = GameObject.Find("Luna");

		//Wiimote values
		acceleration = new Vector3 (0,0,0);
		gyro = new Vector3 (0,0,0);

		//Interaction objects
		wiimoteInteractingWithObject = false;
		cursorText = "";

		//Wiimote start
		wiimote_start();
		int c = wiimote_count();
		if (c > 0) {
			wiimote_enableIR (PLAYER1);
		}

		line = gameObject.GetComponent<LineRenderer>();
		line.enabled = false;
		
		
	}
	void OnApplicationQuit() {
		wiimote_stop();}
}