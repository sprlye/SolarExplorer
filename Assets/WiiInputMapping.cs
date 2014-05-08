using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class WiiInputMapping : MonoBehaviour {


	// Basic wii function
	[DllImport ("UniWii")]
	private static extern void wiimote_start();
	[DllImport ("UniWii")]
	private static extern void wiimote_stop();
	[DllImport ("UniWii")]
	private static extern int wiimote_count();


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
	[DllImport ("UniWii")]
	private static extern float wiimote_isIREnabled(int which);

	//Gyroscope
	[DllImport ("UniWii")]
	private static extern float wiimote_getRoll(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getPitch(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getYaw(int which);


	//Nunchuck functions
	[DllImport ("UniWii")]
	private static extern int wiimote_isExpansionPortEnabled();

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



	public Movements mainCharacter;


	//Testing text
	String display;


	public const int PLAYER1 = 0;

	//Userdata
	Vector3 acceleration, gyro;


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
			display += "Wiimote " + PLAYER1 + "\n " +
						"accX: " + x + " accY: " + y + " accZ: " + z + "\n " +
						"roll: " + roll + " pitch: " + p + " yaw: " + yaw; //+ " \n "IR X: " + ir_x + " IR Y: " + ir_y + "\n";
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


			if( nunchuckX < 115 && nunchuckX > 10 ){
				mainCharacter.rotateInDirection(mainCharacter.ROTATE_LEFT*nunchuckXSpeed);
			}
			if( nunchuckX > 125 ){
				mainCharacter.rotateInDirection(mainCharacter.ROTATE_RIGHT*nunchuckXSpeed);
			}
			
			if(nunchuckY < 125 && nunchuckY  > 10 ){
				mainCharacter.rotateInDirection(mainCharacter.ROTATE_DOWN*nunchuckYSpeed);
			}
			if(nunchuckY  > 135 ){
				mainCharacter.rotateInDirection(mainCharacter.ROTATE_UP*nunchuckYSpeed);
			}

			if(wiimote_getButtonNunchuckZ(PLAYER1)){
				mainCharacter.moveInDirection(mainCharacter.FORWARD);
			}

			if(wiimote_getButtonNunchuckC(PLAYER1)){
				mainCharacter.moveInDirection(mainCharacter.BACKWARD);
			}


		
		}
		else display = "Press the '1' and '2' buttons on your Wii Remote.";
	}

	void OnGUI(){
		GUI.Label( new Rect(10,Screen.height-100, 500, 100), display);

	}
	
	void Start (){
		acceleration = new Vector3 (0,0,0);
		gyro = new Vector3 (0,0,0);
		wiimote_start();

	}
	void OnApplicationQuit() {
		wiimote_stop();}
}