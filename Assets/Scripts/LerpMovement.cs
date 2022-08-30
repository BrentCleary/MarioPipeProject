using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMovement : MonoBehaviour
{
    public PlayerController playerController;
    public PipeWarpBehavior pipeWarpBehavior;

    public Rigidbody playerRB;
    public GameObject player;

    public Vector3 enterStartPos;
    public Vector3 enterEndPos;
    public Vector3 exitStartPos;
    public Vector3 exitEndPos;
    public Vector3 endPosOffset = new Vector3(0, -2.0f, 0);

    public float lerpPct = 0;
    public float lerpDuration = 1f;
    public float elapsedTime = 0f;
    public bool lerpEnterActive = false;
    public bool lerpExitActive = false;

    public Vector3 pipePosition; 

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

    }

    void Update()
    {
        // Gets the position of the active pipe
        pipePosition = playerController.pipePosition;
        
        // Gets the PipeWarpBehavior script on active pipe
        pipeWarpBehavior = playerController.pipeWarpBehavior;

        PipeEntryLerp();
        PipeExitLerp();

    }

    public void PipeEntryLerp()
    {
        if(lerpEnterActive == true)
        {
            playerRB.useGravity = false;

            elapsedTime += Time.deltaTime;
            lerpPct = elapsedTime / lerpDuration;

            transform.position = Vector3.Lerp(enterStartPos, enterEndPos, Mathf.SmoothStep(0, 1, lerpPct));

            if(lerpPct >= 1)
            {
                lerpEnterActive = false;
                playerRB.useGravity = true;
                elapsedTime = 0;
                pipeWarpBehavior.warpActive = true;
                pipeWarpBehavior.WarpBetweenPipes();
            }
        }
    }

    public void PipeExitLerp()
    {
        if(lerpExitActive == true)
        {
            player.transform.position = playerController.exitPipePos;

            playerRB.useGravity = false;

            elapsedTime += Time.deltaTime;
            lerpPct = elapsedTime / lerpDuration;

            transform.position = Vector3.Lerp(exitStartPos, exitEndPos, Mathf.SmoothStep(0, 1, lerpPct));

            if(lerpPct >= 1)
            {
                lerpExitActive = false;
                playerRB.useGravity = true;
                elapsedTime = 0;

            }
        }
    }
}