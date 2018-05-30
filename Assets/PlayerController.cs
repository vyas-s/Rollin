using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
//git test
public class PlayerController : MonoBehaviour {
	private Rigidbody rb;
	public GameObject prefab;
	public Text countText, gameoverText;
	private GameObject lastPipe;
	private int count;
	float width, length;
	List<GameObject> pipes;
	Vector3 posCenter, posLeft, posRight ;

	// Use this for initialization
	void Start () {
		count = 0;
		countText.text = "Score: " + count.ToString();
		gameoverText.text = "";
		rb = GetComponent<Rigidbody>();
		pipes = new List<GameObject>();

		posCenter = new Vector3(0.0f,0.5f,0.0f);
		posLeft = new Vector3(0.0f,0.5f,2.6f);
		posRight = new Vector3(0.0f,0.5f,-2.6f);

		pipes.Add((GameObject)Instantiate(prefab,posCenter + new Vector3(2.5f,0,0),Quaternion.identity));
		lastPipe = pipes.Last();
		lastPipe.transform.localScale = new Vector3(4.5f,4.5f,4.5f);
		lastPipe.AddComponent(typeof(MeshCollider));
		lastPipe.transform.Rotate(0,90,0);

		Collider mycollider = lastPipe.GetComponent<Collider>();
		width =mycollider.bounds.size.z;
		length = mycollider.bounds.size.x;
		//Debug.Log(mycollider.bounds.size.x);
		for(int i = 0; i < 10; i++){
			if(Random.value > 0.5){
				pipes.Add((GameObject)Instantiate(prefab,posLeft + new Vector3(lastPipe.transform.position.x+length,0,0),Quaternion.identity));
				lastPipe = pipes.Last();
				lastPipe.transform.localScale = new Vector3(4.5f,4.5f,4.5f);
				lastPipe.AddComponent(typeof(MeshCollider));
				lastPipe.transform.Rotate(0,90,0);
			}
			else{
				pipes.Add((GameObject)Instantiate(prefab,posRight + new Vector3(lastPipe.transform.position.x+length,0,0),Quaternion.identity));
				lastPipe = pipes.Last();
				lastPipe.transform.localScale = new Vector3(4.5f,4.5f,4.5f);
				lastPipe.AddComponent(typeof(MeshCollider));
				lastPipe.transform.Rotate(0,90,0);
			}

		}
		
		//lastPipe.transform.Translate(new Vector3(0,0,width * 1.4f),Space.World);

	}
	void Update(){

		if( ((pipes[1].transform.position.x-length/2)<=(transform.position.x + 0.5)) && (pipes[1].transform.position.z != 0)){
			Debug.Log("game over");
			gameoverText.text = "Game Over! Your Score is: " + count;
			Destroy(GetComponent<Rigidbody>());
			foreach(GameObject pipe in pipes)
			{
				Destroy(pipe);
			}
		}

		if( (pipes[0].transform.position.x+length/2) < (transform.position.x - 0.5)){
			Destroy(pipes[0]);
			pipes.RemoveAt(0);
			count = count+1;
			countText.text = "Score: " + count.ToString();
			if(Random.value > 0.5){
				pipes.Add((GameObject)Instantiate(prefab,posLeft + new Vector3(lastPipe.transform.position.x+length,0,0),Quaternion.identity));
				lastPipe = pipes.Last();
				lastPipe.transform.localScale = new Vector3(4.5f,4.5f,4.5f);
				lastPipe.AddComponent(typeof(MeshCollider));
				lastPipe.transform.Rotate(0,90,0);
			}
			else{
				pipes.Add((GameObject)Instantiate(prefab,posRight + new Vector3(lastPipe.transform.position.x+length,0,0),Quaternion.identity));
				lastPipe = pipes.Last();
				lastPipe.transform.localScale = new Vector3(4.5f,4.5f,4.5f);
				lastPipe.AddComponent(typeof(MeshCollider));
				lastPipe.transform.Rotate(0,90,0);
			}
		}

		if(Input.GetKeyDown(KeyCode.LeftArrow) && (pipes[1].transform.position.z <= 0) ){
				pipes[1].transform.Translate(new Vector3(0,0,2.6f),Space.World);
		}
		if(Input.GetKeyDown(KeyCode.RightArrow) && (pipes[1].transform.position.z >= 0) ){
				pipes[1].transform.Translate(new Vector3(0,0,-2.6f),Space.World);			
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		Vector3 movement = new Vector3(5.0f, 0.0f, 0.0f);
		rb.AddForce(movement);
	}
}
