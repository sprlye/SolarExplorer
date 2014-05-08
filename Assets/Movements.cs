using UnityEngine;
using System.Collections;

public class Movements : MonoBehaviour {
	//TODO: enable the user to watch in another direction than where the one he is going
	
	float moveSpeed = 2.0f;
	float turnSpeed = 250.0f;
	float mass = 100.0f;
	public GameObject[] planets;
	public GameObject[] stars;
	public GameObject[] blackholes;

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

	// Use this for initialization
	void Start () {
		planets = GameObject.FindGameObjectsWithTag("Planet");
		stars = GameObject.FindGameObjectsWithTag("Star");
		blackholes = GameObject.FindGameObjectsWithTag("Blackhole");

		moveDirection = new Vector3 (0,0,0);
		rotateDirection = new Vector3 (0,0,0);
	}
	
	public void moveInDirection(Vector3 direction ){
		this.moveDirection += direction;
		//Debug.Log (this.moveDirection.x);
	}
	
	public void rotateInDirection(Vector3 direction ){
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


		transform.Translate (moveDirection*moveSpeed*Time.deltaTime);
		transform.Rotate (rotateDirection*turnSpeed * Time.deltaTime);
		
//		if (Input.GetKey (KeyCode.Space)) {
//			Debug.Log("PRESSED SPACE");
//			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
//		}
//		
//		if (Input.GetKey (KeyCode.LeftAlt)) {
//			Debug.Log("Press alt");
//			transform.Translate(- Vector3.forward * moveSpeed * Time.deltaTime);
//		}
//		
//		if(Input.GetKey(KeyCode.RightArrow))
//		{	//Debug.Log(transform.position.x);
//			transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
//		}
//		if(Input.GetKey(KeyCode.LeftArrow))
//		{
//			transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
//		}
//		if(Input.GetKey(KeyCode.UpArrow))
//		{
//			transform.Rotate(Vector3.left, turnSpeed * Time.deltaTime);
//		}
//		if(Input.GetKey(KeyCode.DownArrow))
//		{
//			transform.Rotate(Vector3.left, -turnSpeed * Time.deltaTime);
//		}

		/*
		//Compute the force applied on the character by the environment
		Vector3 force = Vector3 (0.0f, 0.0f, 0.0f);
		float G = 6.67384;//TODO: Mettre la bonne constante gravitationnelle et vérifier les formules
		//Planets
		float planetsDensity = 4.0f;
		for(int i = 0; i < this.planets.Length ; i++){
			float planetRadius = (this.planets[i].transform.localScale[0] + this.planets[i].transform.localScale[1] + this.planets[i].transform.localScale[2])/3.0;
			float planetMass = Mathf.PI * Mathf.Pow(planetRadius, 3) * planetsDensity;
			Vector3 userPlanet = (transform.position - this.planets[i].transform.position);
			force -= planetMass * this.mass * G * userPlanet/(Mathf.Pow(userPlanet.magnitude, 3));
		}
		//TODO: Use only the planets that are close enough from the user

		//Stars

		//Blackholes

		//Compute the torque applied on the character by the environment
		//No torque for now

		//Apply the force
		transform.Translate(force);//TODO: INTEGRATE THE NEWTON LAW!! Don't put the force directly (store the speed to do so?)
			*/

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
