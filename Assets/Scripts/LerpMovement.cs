using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMovement : MonoBehaviour
{
    public PlayerController playerController;
    public Rigidbody playerRB;
    public Vector3 startPos;
    public Vector3 endPos;
    public Vector3 endPosOffset = new Vector3(0, -1.5f, 0);
    public float lerpPct = 0;
    
    public float lerpDuration = 1f;
    public float elapsedTime = 0f;
    public bool lerpActive = false;

    public Vector3 pipePosition; 

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerRB = GetComponent<Rigidbody>();

    }

    void Update()
    {
        pipePosition = playerController.pipePosition;

        PlayerLerp();

    }

    public void PlayerLerp()
    {
        if(lerpActive == true)
        {
            playerRB.useGravity = false;

            elapsedTime += Time.deltaTime;
            lerpPct = elapsedTime / lerpDuration;

            transform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0, 1, lerpPct));

            if(lerpPct >= 1)
            {
                lerpActive = false;
                playerRB.useGravity = true;
                elapsedTime = 0;
            }
        }
    }

}
