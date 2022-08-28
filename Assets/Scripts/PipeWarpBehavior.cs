using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeWarpBehavior : MonoBehaviour
{
    public GameObject[] pipePair;
    public GameObject player;
    public bool warpActive = false;
    public LerpMovement lerpScript;

    public Vector3 exitPipePosition;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        lerpScript = player.GetComponent<LerpMovement>();
        exitPipePosition = pipePair[1].GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        // WarpBetweenPipes();
    }

    // Warp is set active at end of PipeEntryLerp    
    public void WarpBetweenPipes()
    {
        if(warpActive)
        {
            player.transform.position = exitPipePosition;
            lerpScript.lerpExitActive = true;

        }
    }

    // Lerp from exitPipePosition
    // Lerp to inverse of the LerpOffset

}

    // Identify exit pipe after lerp
    // Place Mario below exit pipe
    // Lerp his position out of the pipe

    // Reverse the entry/exit, so it works both ways
    // Transition scene to a new scene on entry, and exit from a pipe in new scene

    // Setup a statement that switches between indexs of pipes



