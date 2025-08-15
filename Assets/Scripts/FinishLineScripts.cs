using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineScripts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.instance.LevelComplete)
        {
            Debug.Log("Won");
            collision.transform.parent.GetComponent<BikeMovementScript>().stopBike();
            Finished();
        }
        
    }

    void Finished()
    {
        GameManager.instance.FinishedActions();
    }
}
