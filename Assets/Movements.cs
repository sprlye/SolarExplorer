using UnityEngine;
using System.Collections;

public class Movements : MonoBehaviour {
	//TODO: enable the user to watch in another direction than where the one he is going
	
	float G = 6.67384f * Mathf.Pow(10, -11);
	
	float moveSpeed = 2.0f;
	float turnSpeed = 250.0f;
	float mass = 100.0f;
	float range = 5.0f;//Range where the space elements are used for the computation of forces
	Vector3 velocity = new Vector3 (0,0,0);
	Vector3 acceleration = new Vector3 (0,0,0);
	public GameObject[] planets;
	public GameObject[] stars;
	public GameObject[] blackholes;
	float planetsVolumicMass = 6000000.0f;
	float starsVolumicMass = 10000000.0f;
	float blackholesVolumicMass = 10000000.0f;
	
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
		return Mathf.Sqrt(Mathf.Pow( (this.transform.position.x -  obj.transform.position.x), 2) + Mathf.Pow( (this.transform.position.y -  obj.transform.position.y), 2) + Mathf.Pow( (this.transform.position.z -  obj.transform.position.z), 2));
	}
	
	Vector3 getDirection(GameObject obj){//Returns the vector from obj to this
		return this.transform.position - obj.transform.position;
		//return new Vector3(- (this.transform.position.x -  obj.transform.position.x), - (this.transform.position.y -  obj.transform.position.y), - (this.transform.position.z -  obj.transform.position.z));
	}
	
	// Use this for initialization
	void Start () {
		planets = GameObject.FindGameObjectsWithTag("Planet");
		stars = GameObject.FindGameObjectsWithTag("Star");
		blackholes = GameObject.FindGameObjectsWithTag("Blackhole");
		
		moveDirection = new Vector3 (0,0,0);
		rotateDirection = new Vector3 (0,0,0);
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
		//Update according to the user controls:
		transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
		transform.Rotate(rotateDirection * turnSpeed * Time.deltaTime);
		
		//Update according to the environment laws:
		Vector3 force = new Vector3(0,0,0);

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
			if(this.getDistance(this.planets[i]) <= this.range){
				float starMass = this.starsVolumicMass * (4.0f/3.0f) * Mathf.PI * Mathf.Pow(this.stars[i].transform.lossyScale.magnitude , 3) ;
				force += - G * this.mass * starMass * this.getDirection(this.stars[i])/Mathf.Max(0.001f, Mathf.Pow(this.getDistance(this.planets[i]), 3));
			}
		}
		for(int i = 0; i < this.blackholes.Length ; i++){
			if(this.getDistance(this.planets[i]) <= this.range){
				float blackholeMass = this.blackholesVolumicMass * (4.0f/3.0f) * Mathf.PI * Mathf.Pow(this.blackholes[i].transform.lossyScale.magnitude , 3) ;
				force += - G * this.mass * blackholeMass * this.getDirection(this.blackholes[i])/Mathf.Max(0.001f, Mathf.Pow(this.getDistance(this.planets[i]), 3));
			}
		}

		//force = -force;
		transform.Translate(Time.deltaTime * velocity + Time.deltaTime * Time.deltaTime * 0.5f * acceleration, Space.World);
		//transform.Rotate();TODO add torque here
		
		//Update Velocity and acceleration
		velocity = velocity + Time.deltaTime * 0.5f * (acceleration + (1 / this.mass) * force);
		acceleration = (1 / this.mass) * force;

		
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
