using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject nebula;

	public Transform player;

	public Gradient nebulaColors;
	public int chunkSize;
	public Vector3 currentChunk;

	public int nebulaCount = 100;
	//public int interval = 60;
	
	//private int frameCount;
    // Start is called before the first frame update
    void Start() {
	    currentChunk = Vector3.zero;
	    GenerateNewEnvironmentStuff();
    }

    // Update is called once per frame
    void Update() {
	    if (player.transform.position.x > chunkSize * currentChunk.x) {
		    currentChunk.x += 1;
		    GenerateNewEnvironmentStuff();
	    }
	    if (player.transform.position.y > chunkSize * currentChunk.y) {
		    currentChunk.y += 1;
		    GenerateNewEnvironmentStuff();
	    }
	    if (player.transform.position.y > chunkSize * currentChunk.z) {
		    currentChunk.z += 1;
		    GenerateNewEnvironmentStuff();
	    }

	    /*if (frameCount >= interval) {
		    GameObject nebula2 = Instantiate(nebula, new Vector3(Random.Range(-1000, 1000), Random.Range(-1000, 1000), Random.Range(-1000, 1000)), new Quaternion());
		    nebula2.GetComponent<ParticleSystem>().startColor = Random.ColorHSV();
	    }*/
    }

    private void GenerateNewEnvironmentStuff() {
	    for (int i = 0; i < nebulaCount; i++) {
		    GameObject nebula2 = Instantiate(nebula, player.transform.position + new Vector3(Random.Range(-1000, 1000), Random.Range(-1000, 1000), Random.Range(-1000, 1000)), new Quaternion());
		    nebula2.GetComponent<ParticleSystem>().startColor = nebulaColors.Evaluate(Random.value);
	    }
    }
}
