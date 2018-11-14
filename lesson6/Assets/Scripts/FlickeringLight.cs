using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour {

    public Light fLight;
    float randomN;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        randomN = Random.value;
        if(randomN <= .1f)
        {
            fLight.enabled = true;
        }
        else
        {
            fLight.enabled = false;
        }
	}
}
