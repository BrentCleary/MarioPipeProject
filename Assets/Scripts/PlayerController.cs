using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LerpMovement lerpScript;

    public Rigidbody playerRB;
    public PipeWarpBehavior pipeWarpBehavior;
    public float horizontalInput;
    public float moveForce = 2;
    public float jumpForce = 10;
    public bool onWarpPipe = false;

    public int currentMaterial = 0;
    public Material[] materialList;
    public Renderer rend;

    public Vector3 pipePosition;
    public Vector3 exitPipePos;

    public GameObject collisionPipe;


    // Start is called before the first frame update
    void Start()
    {
        lerpScript = GetComponent<LerpMovement>();
        playerRB = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        Vector3 endPosOffSetY = lerpScript.endPosOffset;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        if(Input.GetKey(KeyCode.G) && onWarpPipe)
        {
            lerpScript.lerpEnterActive = true;

            // Sets Enter Start/End Points in LerpScript
            lerpScript.enterStartPos = new Vector3(pipePosition.x, transform.position.y, transform.position.z);
            lerpScript.enterEndPos = lerpScript.enterStartPos + lerpScript.endPosOffset;
            
            // Sets Exit Start/End Points in LerpScript
            lerpScript.exitStartPos = exitPipePos;
            lerpScript.exitEndPos = exitPipePos - lerpScript.endPosOffset;
            
            lerpScript.PipeEntryLerp();

        }


        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ChangeColor());
        }

        ResetPlayer();

    }

    void PlayerMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if(horizontalInput != 0)
        {
            transform.Translate(Vector3.right * horizontalInput*  moveForce * Time.deltaTime);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }



    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("PipeCenter"))
        {
            onWarpPipe = true;
            collisionPipe = other.transform.parent.gameObject;
            pipeWarpBehavior = collisionPipe.GetComponent<PipeWarpBehavior>();
            pipeWarpBehavior.Invoke("PipeExitSwitcher", 0);
            pipePosition = collisionPipe.GetComponent<Transform>().position;
            exitPipePos = pipeWarpBehavior.exitPipePosition;

        }
        else
        {
            onWarpPipe = false;
        }
    }

    void ResetPlayer()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            transform.position = new Vector3(0,5,0);
        }
    }

    // 08/26/22 - Needs to cycle. Currently index is out of range at 3
    // Completed
    IEnumerator ChangeColor()
    {
        for(int i = 0; i < materialList.Length + 1; i++)
        {
            if(i == materialList.Length)
            {
                i = 0;
                rend.sharedMaterial = materialList[i];
            }

            rend.sharedMaterial = materialList[i];

            yield return new WaitForSeconds(1.0f);
        }

        // Lerp a transition from the top of the pipe to below the rim
        // Possible character turn like in mario with rotation
        // Call Scene Change and load at the end of the lerp
        // Stop the Coroutine?
    }

    // void ChangeColor()
    // {
    //     if(currentMaterial < materialList.Length)
    //     {
    //         currentMaterial += 1;
    //         rend.sharedMaterial = materialList[currentMaterial];
    //     }
    //     else if(currentMaterial > materialList.Length - 1)
    //     {
    //         currentMaterial = 0;
    //     }
    // }
}