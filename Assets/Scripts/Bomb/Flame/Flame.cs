using UnityEngine;
using System.Collections;

public class Flame : MonoBehaviour {


	// Use this for initialization
	void Start () {
	    Invoke("FlameVanish", 1.5f);
	}

    void FlameVanish()
    {
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
