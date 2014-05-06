using UnityEngine;
using System.Collections;

public class Movements : MonoBehaviour {
	//TODO: enable the user to watch in another direction than where the one he is going
	
	float moveSpeed = 2.0f;
	float turnSpeed = 100.0f;
	float mass = 100.0f;
	public GameObject[] planets;
	public GameObject[] stars;
	public GameObject[] blackholes;
	
	// Use this for initialization
	void Start () {
		planets = GameObject.FindGameObjectsWithTag("Planet");
		stars = GameObject.FindGameObjectsWithTag("Star");
		blackholes = GameObject.FindGameObjectsWithTag("Blackhole");
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.Space)) {
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		}
		
		if (Input.GetKey (KeyCode.LeftAlt)) {
			transform.Translate(- Vector3.forward * moveSpeed * Time.deltaTime);
		}
		
		if(Input.GetKey(KeyCode.RightArrow))
		{	//Debug.Log(transform.position.x);
			transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.UpArrow))
		{
			transform.Rotate(Vector3.left, turnSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			transform.Rotate(Vector3.left, -turnSpeed * Time.deltaTime);
		}

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
	}
}
