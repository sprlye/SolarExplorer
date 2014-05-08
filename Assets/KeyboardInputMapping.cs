using UnityEngine;
using System.Collections;

public class KeyboardInputMapping : MonoBehaviour {
	
	public Movements mainCharacter;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.Space)) {
			Debug.Log ("pressing space");
			mainCharacter.moveInDirection(mainCharacter.FORWARD);
		}
		
		if (Input.GetKey (KeyCode.LeftAlt)) {
			mainCharacter.moveInDirection(mainCharacter.BACKWARD);
		}
		
		if(Input.GetKey(KeyCode.RightArrow)){
			//mainCharacter.rotateInDirection(Vector3.up);
			mainCharacter.rotateInDirection(mainCharacter.ROTATE_RIGHT);
		}
		
		if(Input.GetKey(KeyCode.LeftArrow))
		{	
			//mainCharacter.rotateInDirection(-Vector3.up);
			mainCharacter.rotateInDirection(mainCharacter.ROTATE_LEFT);
		}
		if(Input.GetKey(KeyCode.UpArrow))
		{
			//mainCharacter.rotateInDirection(Vector3.left);
			mainCharacter.rotateInDirection(mainCharacter.ROTATE_UP);
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			//.rotateInDirection(-Vector3.left);
			mainCharacter.rotateInDirection(mainCharacter.ROTATE_DOWN);
		}
		
	}
}
