using UnityEngine;
using System.Collections;

//NOTE: This file is not ready, do not use it

public class MovementsWithHeadTracking : MonoBehaviour {
	//TODO: enable the user to watch in another direction than where the one he is going
	
	float moveSpeed = 2.0f;
	float turnSpeed = 100.0f;
	float mass = 100.0f;
	public GameObject[] planets;
	public GameObject[] stars;
	public GameObject[] blackholes;

	GameObject luna;
	GameObject lunaGaze;

	// Use this for initialization
	void Start () {
		planets = GameObject.FindGameObjectsWithTag("Planet");
		stars = GameObject.FindGameObjectsWithTag("Star");
		blackholes = GameObject.FindGameObjectsWithTag("Blackhole");
		luna = GameObject.FindGameObjectWithTag ("Luna");
		lunaGaze = GameObject.FindGameObjectWithTag ("LunaGaze");
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

		//Extra Rotation inmplied by head tracking
		//if (Input.GetKey(KeyCode.Z)) {//Attention de tourner autour des bons axe (ceux de Luna Gaze)
		//	transform.Rotate(lunaGaze.Vector3.left, turnSpeed * Time.deltaTime);
		//}
		
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
