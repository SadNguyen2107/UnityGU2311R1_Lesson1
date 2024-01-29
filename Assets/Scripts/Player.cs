using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Serialized Attributes
    [SerializeField] float _moveSpeed;
    #endregion

    #region Private Attributes
    Rigidbody _playerRigidbody;
    PlayerController _playerController;
    bool _canMove;
    #endregion

    #region Unity Methods
    void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerController = GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        if (!_canMove)
        {
            return;
        }

        _playerRigidbody.velocity = new Vector3(
            _playerController.MoveAmount.x * _moveSpeed * Time.fixedDeltaTime,
            0,
            _playerController.MoveAmount.y * _moveSpeed * Time.fixedDeltaTime
        );
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _canMove = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _canMove = false;
        }
    }

    #endregion
}
