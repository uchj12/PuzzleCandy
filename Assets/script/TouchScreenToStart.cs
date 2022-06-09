using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScreenToStart : MonoBehaviour
{
    public GameObject EventSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            EventSystem.GetComponent<SceneChange>().scenechange();
        }
    }
}
