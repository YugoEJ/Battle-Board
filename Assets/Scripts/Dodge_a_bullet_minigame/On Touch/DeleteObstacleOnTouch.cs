using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObstacleOnTouch : MonoBehaviour
{
    public static int obstacleCounter = 15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "obstacle")
        {
            Destroy(col.gameObject);
            //obstacleCounter--;
            //Debug.Log(obstacleCounter);
        }

        //OnTouchPlayer.isObstacleTouchedPlayer = true;
    }
}
