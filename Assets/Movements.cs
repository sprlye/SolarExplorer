﻿using UnityEngine;
using System.Collections;

public class Movements : MonoBehaviour {
	
	float G = 6.67384f * Mathf.Pow(10, -11);
	
	float moveSpeed = 50.0f;
	float turnSpeed = 250.0f;
	float mass = 1000.0f;
	float range = 100.0f;//Range where the space elements are used for the computation of forces
	public Vector3 velocity = new Vector3 (0,0,0);
	public Vector3 acceleration = new Vector3 (0,0,0);
	float maxRange = 1000f;
	public GameObject[] planets;
	public GameObject[] stars;
	public GameObject[] blackholes;
	GameObject gaze;
	float planetsVolumicMass = 60000000.0f;
	float starsVolumicMass = 100000000.0f;
	float blackholesVolumicMass = 1000000000.0f;
	
	bool gameOver = false;
	string message = "";
	
	Vector3 moveDirection, rotateDirection;
	
	//Not necessary but for easy of use (some of the rotate vectors very unclear/ hard to remember)
	public  Vector3 FORWARD = Vector3.forward;
	public  Vector3 BACKWARD = -Vector3.forward;
	public  Vector3 LEFT = Vector3.left;
	public  Vector3 RIGHT = Vector3.right;
	public  Vector3 ROTATE_LEFT = -Vector3.up;
	public  Vector3 ROTATE_RIGHT = Vector3.up;
	public  Vector3 ROTATE_UP = Vector3.left;
	public  Vector3 ROTATE_DOWN = -Vector3.left;
	
	float getDistance(GameObject obj){//Returns the distance between this and obj
		return Mathf.Sqrt(Mathf.Pow( (gaze.transform.position.x -  obj.transform.position.x), 2) + Mathf.Pow( (gaze.transform.position.y -  obj.transform.position.y), 2) + Mathf.Pow( (gaze.transform.position.z -  obj.transform.position.z), 2));
	}
	
	Vector3 getDirection(GameObject obj){//Returns the vector from obj to this
		return gaze.transform.position - obj.transform.position;
		//return new Vector3(- (this.transform.position.x -  obj.transform.position.x), - (this.transform.position.y -  obj.transform.position.y), - (this.transform.position.z -  obj.transform.position.z));
	}
	
	
	void OnGUI () {
		if(gameOver){
			// Make a background box
			GUI.Box(new Rect(Screen.width/4.0f,Screen.height/4.0f,Screen.width/2.0f,Screen.height/2.0f), message);
			
			// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
			if(GUI.Button(new Rect((Screen.width/2.0f) - 20f,(Screen.height/2.0f) - 10f,60f,25f), "Restart")) {
				Application.LoadLevel(Application.loadedLevel);
			}
		}
		
		GUI.Box(new Rect(Screen.width/4.0f,Screen.height/4.0f,Screen.width/2.0f,Screen.height/2.0f), this.transform.position.x.ToString());
		
	}
	
	// Use this for initialization
	void Start () {
		planets = GameObject.FindGameObjectsWithTag("Planet");
		stars = GameObject.FindGameObjectsWithTag("Star");
		blackholes = GameObject.FindGameObjectsWithTag("Blackhole");
		
		moveDirection = new Vector3 (0,0,0);
		rotateDirection = new Vector3 (0,0,0);
		
		GameObject[] gazes = GameObject.FindGameObjectsWithTag("LunaGaze");
		gaze = gazes[0];
	}
	
	public void moveInDirection(Vector3 direction){
		this.moveDirection += direction;
		//Debug.Log (this.moveDirection.x);
	}
	
	public void rotateInDirection(Vector3 direction){
		this.rotateDirection += direction;
		//Debug.Log (this.moveDirection.x);
	}
	
	void resetMoveDirection(){
		//		moveDirection.x = 0;
		//		moveDirection.y = 0;
		//		moveDirection.z = 0;
		moveDirection.Set(0,0,0);
	}
	
	void resetRotation(){
		//		rotateDirection.x = 0;
		//		rotateDirection.y = 0;
		//		rotateDirection.z = 0;
		
		rotateDirection.Set(0,0,0);
	}
	
	// Update is called once per frame
	void Update () {
	
		
		
		if(!gameOver){
		
		
			planets = GameObject.FindGameObjectsWithTag("Planet");
			stars = GameObject.FindGameObjectsWithTag("Star");
			blackholes = GameObject.FindGameObjectsWithTag("Blackhole");
			
			//Check if collision with an object
			for(int i = 0; i < this.planets.Length ; i++){
				//if(this.getDistance(this.planets[i]) <= ( this.planets[i].transform.localScale.x +  this.planets[i].transform.localScale.y   + this.planets[i].transform.localScale.z ) / 3.0f){
				SphereCollider curCollider = (this.planets[i].collider as SphereCollider);
				if(this.getDistance(this.planets[i]) <= 1 + curCollider.radius * ( this.planets[i].transform.lossyScale.x +  this.planets[i].transform.lossyScale.y   + this.planets[i].transform.lossyScale.z ) / 3.0f){
					gameOver = true;
					message = "You hit a planet!";
				}
				
			}
			for(int i = 0; i < this.stars.Length ; i++){
				SphereCollider curCollider = (this.stars[i].collider as SphereCollider);
				if(this.getDistance(this.stars[i]) <= 1 + curCollider.radius * ( this.stars[i].transform.lossyScale.x +  this.stars[i].transform.lossyScale.y   + this.stars[i].transform.lossyScale.z ) / 3.0f){
					gameOver = true;
					message = "You hit a star!";
				}
				
			}
			for(int i = 0; i < this.blackholes.Length ; i++){
				SphereCollider curCollider = (this.blackholes[i].collider as SphereCollider);
				if(this.getDistance(this.blackholes[i]) <= 1 + curCollider.radius * ( this.blackholes[i].transform.lossyScale.x +  this.blackholes[i].transform.lossyScale.y   + this.blackholes[i].transform.lossyScale.z ) / 3.0f){
					gameOver = true;
					message = "You got absorbed by a blackhole!";
				}
				
			}
			
			
			
			//Update according to the user controls:
			transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
			transform.Rotate(rotateDirection * turnSpeed * Time.deltaTime);
			
			//Update according to the environment laws:
			Vector3 force = new Vector3(0,0,0);
			force += transform.TransformPoint(moveDirection * moveSpeed * Time.deltaTime);
			
			this.planets[1].renderer.material.color = Color.cyan;
			
			//float planetMass = this.planetsVolumicMass * (4.0f/3.0f) * Mathf.PI * Mathf.Pow(this.planets[1].transform.lossyScale.magnitude , 3) ;
			//force += - G * this.mass * planetMass * this.getDirection(this.planets[1])/Mathf.Max(0.001f, Mathf.Pow(this.getDistance(this.planets[1]), 3));
			
			//Use Newton law to compute forces
			for(int i = 0; i < this.planets.Length ; i++){
				//Check if the planet is in the range
				if(this.getDistance(this.planets[i]) <= this.range){
					float planetMass = this.planetsVolumicMass * (4.0f/3.0f) * Mathf.PI * Mathf.Pow(this.planets[i].transform.lossyScale.magnitude , 3) ;
					force += - G * this.mass * planetMass * this.getDirection(this.planets[i])/Mathf.Max(0.001f, Mathf.Pow(this.getDistance(this.planets[i]), 3));
				}
			}
			for(int i = 0; i < this.stars.Length ; i++){
				if(this.getDistance(this.stars[i]) <= this.range){
					float starMass = this.starsVolumicMass * (4.0f/3.0f) * Mathf.PI * Mathf.Pow(this.stars[i].transform.lossyScale.magnitude , 3) ;
					//force += - G * this.mass * starMass * this.getDirection(this.stars[i])/Mathf.Max(0.001f, Mathf.Pow(this.getDistance(this.stars[i]), 3));
				}
			}
			for(int i = 0; i < this.blackholes.Length ; i++){
				if(this.getDistance(this.blackholes[i]) <= this.range){
					float blackholeMass = this.blackholesVolumicMass * (4.0f/3.0f) * Mathf.PI * Mathf.Pow(this.blackholes[i].transform.lossyScale.magnitude , 3) ;
					//force += - G * this.mass * blackholeMass * this.getDirection(this.blackholes[i])/Mathf.Max(0.001f, Mathf.Pow(this.getDistance(this.blackholes[i]), 3));
				}
			}
			
			//Find the closest element
			/*float distance = range;
		GameObject closestObject = null;

		for(int i = 0; i < this.planets.Length ; i++){
			//Check if the planet is in the range
			if(this.getDistance(this.planets[i]) < distance){
				closestObject = this.planets[i];
			}
		}
		for(int i = 0; i < this.stars.Length ; i++){
			if(this.getDistance(this.stars[i]) <= distance){
				closestObject = this.stars[i];
			}
		}
		for(int i = 0; i < this.blackholes.Length ; i++){
			if(this.getDistance(this.blackholes[i]) <= distance){
				closestObject = this.blackholes[i];
			}
		}

		if (closestObject != null) {
						float objectMass = this.planetsVolumicMass * (4.0f / 3.0f) * Mathf.PI * Mathf.Pow (closestObject.transform.lossyScale.magnitude, 3);
						force += - G * this.mass * objectMass * this.getDirection (closestObject) / Mathf.Max (0.001f, Mathf.Pow (this.getDistance (closestObject), 3));
		} else {
			acceleration = new Vector3(0,0,0);
			velocity = new Vector3(0,0,0);
				}*/
			
			
			//force = -force;
			if (transform.position.magnitude <= maxRange) {
				transform.Translate(Time.deltaTime * velocity + Time.deltaTime * Time.deltaTime * 0.5f * acceleration, Space.World);
				
				//Update Velocity and acceleration
				
				acceleration = (1 / this.mass) * force;
				
				velocity = velocity + Time.deltaTime * 0.5f * (acceleration + (1 / this.mass) * force);
			}
			else {
				
				acceleration = new Vector3(0,0,0);
				
				velocity = new Vector3(0,0,0);
				gameOver = true;
				message = "You reached the end of the universe!";
			}
			
			if(Input.GetKey(KeyCode.C))//For testing purpose only: to be removed
			{
				for(int i = 0; i < this.planets.Length ; i++){
					this.planets[i].renderer.material.color = Color.cyan;
				}
				for(int i = 0; i < this.stars.Length ; i++){
					this.stars[i].renderer.material.color = Color.red;
				}
				for(int i = 0; i < this.blackholes.Length ; i++){
					this.blackholes[i].renderer.material.color = Color.yellow;
				}
				
			}
			if(Input.GetKey(KeyCode.Delete))//For testing purpose only: to be removed
			{
				for(int i = 0; i < this.planets.Length ; i++){
					Destroy(this.planets[i]);
				}
				for(int i = 0; i < this.stars.Length ; i++){
					Destroy(this.stars[i]);
				}
				for(int i = 0; i < this.blackholes.Length ; i++){
					Destroy(this.blackholes[i]);
				}
				
			}
			
			
			//		if(Input.GetKey(KeyCode.N)){
			//			transform.FindChild("Gaze").Rotate(-Vector3.up, 100 * Time.deltaTime);
			//		}
			//		if(Input.GetKey(KeyCode.M))
			//		{
			//			transform.FindChild("Gaze").Rotate(Vector3.up, 100 * Time.deltaTime);
			//		}
			
			resetMoveDirection ();
			resetRotation ();
			
		}
	}
}
