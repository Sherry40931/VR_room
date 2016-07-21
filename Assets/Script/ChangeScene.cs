using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        /* TRANSFORM SCENE */
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Debug.Log("change to piano");
            SceneManager.LoadSceneAsync("PianoRoom");
        }
        else
        {
            Debug.Log("change to island");
            SceneManager.LoadSceneAsync("SmallIsland");
        }
    }
}
