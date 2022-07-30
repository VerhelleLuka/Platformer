using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 10f;
    public PlayerInputActions playerInput;

    private InputAction m_Move;
    private float m_MoveDirection = 0;

    private bool m_IsGrounded;
    private bool m_CanJump;

    private void Awake()
    {
        playerInput = new PlayerInputActions();
    }
    private void OnEnable()
    {
        m_Move = playerInput.Player.Move;
        m_Move.Enable();

        playerInput.Player.Jump += OnJump;
    }
    private void OnDisable()
    {
        m_Move.Disable();  
    }
    // Update is called once per frame
    void Update()
    {
        m_MoveDirection = m_Move.ReadValue<Vector2>().x;
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(m_MoveDirection * moveSpeed * Time.deltaTime, 0);

        m_IsGrounded = false;

        RaycastHit hit;

        // Check grounded
        if (Physics.Raycast(rb.position, Vector3.down, out hit, 0.1f))
        {
            if (hit.distance < 0.1f)
                m_IsGrounded = true;
        }
    }
}
