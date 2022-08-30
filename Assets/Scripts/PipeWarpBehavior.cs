using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeWarpBehavior : MonoBehaviour
{
    public GameObject[] pipePair;
    public GameObject player;

    public bool warpActive = false;

    public int pipeIndex;

    public PlayerController playerControllerScript;
    public LerpMovement lerpScript;

    public Vector3 exitPipePosition;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerControllerScript = player.GetComponent<PlayerController>();
        lerpScript = player.GetComponent<LerpMovement>();

    }

    // Update is called once per frame
    void Update()
    {

        // WarpBetweenPipes();

        exitPipePosition = pipePair[pipeIndex].GetComponent<Transform>().position;


    }

    public void PipeExitSwitcher()
    {
        if(playerControllerScript.collisionPipe == pipePair[0])
        {
            pipeIndex = 1;
        }

        if(playerControllerScript.collisionPipe == pipePair[1])
        {
            pipeIndex = 0;
        }

    }

    // Warp is set active at end of PipeEntryLerp    
    public void WarpBetweenPipes()
    {
        if(warpActive)
        {
            lerpScript.lerpExitActive = true;
            lerpScript.PipeExitLerp();
            warpActive = false;

        }
    }

    // Lerp from exitPipePosition
    // Lerp to inverse of the LerpOffset

}

    // Get index of entry pipe
    // Identify exitStartPos on collision
    // Place Mario below exit pipe
    // Lerp his position out of the pipe

    // Reverse the entry/exit, so it works both ways
    // Transition scene to a new scene on entry, and exit from a pipe in new scene

    // Setup a statement that switches between indexs of pipes


