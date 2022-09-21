using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementHandler : MonoBehaviour
{
    CharacterController characterController;
    Animator animator;
    [SerializeField] Camera playerCam;

    bool playerGrounded;

    Vector3 playerVelocity;
    public float playerSpeed;
    public float gravity;

    [SerializeField]float inputX;
    [SerializeField] float currentX;
    float inputY;

    [SerializeField] float timetorock;

    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        playerGrounded = characterController.isGrounded;

        if (playerGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        else
        {
            playerVelocity.y -= gravity * Time.deltaTime;
            characterController.Move(playerVelocity * Time.deltaTime);
        }
        MovePlayer(inputX, inputY);
        LookatMouse();
    }

    void MovePlayer(float inputx, float inputy)
    {
        
        animator.SetBool("isMoving", Mathf.Abs(inputx + inputy) > 0 ? true : false);
        Vector3 move = new Vector3(inputx, 0, inputy);
        characterController.Move(move * Time.deltaTime * playerSpeed);
        animator.SetFloat("moveX", Mathf.Lerp(animator.GetFloat("moveX"), inputx, 0.1f));
        animator.SetFloat("moveY", inputy);

    }

    void LookatMouse()
    {
        Ray mousePos = playerCam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float raylength;
        if(groundPlane.Raycast(mousePos, out raylength))
        {
            Vector3 pointLook = mousePos.GetPoint(raylength);
            Debug.DrawLine(mousePos.origin, pointLook, Color.blue);
            transform.LookAt(new Vector3(pointLook.x, transform.position.y, pointLook.z));
        }
       
    }
}
