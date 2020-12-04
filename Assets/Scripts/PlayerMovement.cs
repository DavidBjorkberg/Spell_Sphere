using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    private void Update()
    {
        Move();
    }
    public void Move()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");
        if (horizontalMove != 0 || verticalMove != 0)
        {

            Vector3 moveDir = Vector3.zero;
            moveDir += transform.forward * verticalMove * Time.deltaTime;
            moveDir += transform.right * horizontalMove * Time.deltaTime;
            moveDir.Normalize();
            transform.position += moveDir * movementSpeed * Time.deltaTime;
        }
    }
}
