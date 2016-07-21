using UnityEngine;
using System.Collections;

public class MouseAction : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Debug.Log("Pressed left click.");

        if (Input.GetMouseButtonDown(1))
            Debug.Log("Pressed right click.");

        if (Input.GetMouseButtonDown(2))
            Debug.Log("Pressed middle click.");

    }
}