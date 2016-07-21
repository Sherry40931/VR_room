using UnityEngine;
using System.Collections;

public class ModelPosition : MonoBehaviour {

    Vector3 pos;
    Quaternion rot;

	// Use this for initialization
	void Start () {
        pos = GameObject.FindWithTag("Camera").transform.position;
        rot = GameObject.FindWithTag("Camera").transform.rotation;
        //Debug.Log("ini = " + pos);
    }
	
	// Update is called once per frame
	void Update () {
        pos = GameObject.FindWithTag("Camera").transform.position;
        rot = GameObject.FindWithTag("Camera").transform.rotation;
        pos.y = -0.5f;
        rot.x = 0;
        rot.z = 0;

        gameObject.transform.position = pos;
        gameObject.transform.rotation = rot;
        //Debug.Log("pos = " + pos);
    }
}
