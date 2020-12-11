using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce;
    private float fallMultiplier = 2;
    private float lowJumpMultiplier = 1.5f;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Jump();
    }
    private void FixedUpdate()
    {
        Move();
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * lowJumpMultiplier * Time.deltaTime;
        }
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
            if (!HitWall(moveDir))
            {
                transform.position += moveDir * movementSpeed * Time.deltaTime;
            }
        }
    }
    void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = Vector3.up * jumpForce;
        }
    }
    bool IsGrounded()
    {
        return Physics.SphereCast(transform.position, 0.5f, Vector3.down, out RaycastHit hit, 2, 1 << 9);
    }
    bool HitWall(Vector3 walkDir)
    {
        return Physics.Raycast(transform.position, walkDir, out RaycastHit hit, 1, 1 << 9);
    }
}
