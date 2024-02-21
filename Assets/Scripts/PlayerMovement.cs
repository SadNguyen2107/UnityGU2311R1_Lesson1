using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody m_playerRigidbody;

    [SerializeField]
    float m_moveSpeed;
    [SerializeField]
    float m_rotationSpeed;

    void Awake()
    {
        m_playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        m_playerRigidbody.MovePosition(
            transform.position + new Vector3(0, 0, v * Time.deltaTime * m_moveSpeed)
        );

        m_playerRigidbody.MoveRotation(
            transform.rotation * Quaternion.Euler(0, h * Time.deltaTime * m_rotationSpeed, 0)
        );
    }
}
