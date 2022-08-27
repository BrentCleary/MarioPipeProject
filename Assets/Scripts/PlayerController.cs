using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LerpMovement lerpScript;

    public Rigidbody playerRB;
    public GameObject pipe;
    public float horizontalInput;
    public float moveForce = 2;
    public float jumpForce = 10;
    public bool onWarpPipe = false;

    public int currentMaterial = 0;
    public Material[] materialList;
    public Renderer rend;

    public Vector3 pipePosition;


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

            lerpScript.lerpActive = true;
            lerpScript.startPos = new Vector3(pipePosition.x, transform.position.y, transform.position.z);
            lerpScript.endPos = lerpScript.startPos + lerpScript.endPosOffset;
            lerpScript.PlayerLerp();
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


    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("PipeCenter"))
        {
            onWarpPipe = true;
            pipePosition = other.gameObject.GetComponent<Transform>().position;
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
