using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class UniWiiCheck : MonoBehaviour {
	
	[DllImport ("UniWii")]
	private static extern void wiimote_start();
	[DllImport ("UniWii")]
	private static extern void wiimote_stop();
	[DllImport ("UniWii")]
	private static extern int wiimote_count();
	
	private String display;
	
	void OnGUI() {
		int c = wiimote_count();
		if (c>0) {
			display = "";
			for (int i=0; i<=c-1; i++) {
				display += "Wiimote " + i + " found!\n";
			}
		}
		else display = "Connect your wiimote \n mac: activate bluetooth and press 1 & 2 on the wiimote until it appears \n windows see further documentation in wiimote doc";
		GUI.Label( new Rect(10,Screen.height-100, 500, 100), display);
	}
	
	void Start (){
		wiimote_start();
	}
	void OnApplicationQuit() {
		wiimote_stop();}
}
