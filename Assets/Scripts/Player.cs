using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Serialized Attributes
    [Header("Player Attribute")]
    [SerializeField] float _moveSpeed;
    [SerializeField] float _smoothMoveSpeed;
    [SerializeField] bool _isAuto;
    #endregion

    #region Private Attributes
    Rigidbody _playerRigidbody;
    PlayerController _playerController;
    bool _canMove;

    // Automatic Path
    [Header("Automatic")]
    [SerializeField] PathSO _automaticPathConfig;
    IEnumerator<Transform> _automaticPathPointer;

    #endregion

    #region Unity Methods
    void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerController = GetComponent<PlayerController>();
    }

    void Start()
    {
        _automaticPathPointer = _automaticPathConfig.GetWaypoints();
        _automaticPathPointer.MoveNext();
    }
    void FixedUpdate()
    {
        if (!_canMove)
        {
            return;
        }

        if (_isAuto)
        {
            MoveAutomatically();
        }
        else
        {
            MoveManually();
        }
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

    void MoveManually()
    {
        // Move According to the physics
        Vector2 moveDir = _playerController.MoveAmount.normalized;
        _playerRigidbody.velocity = new Vector3(
            moveDir.x * _moveSpeed * Time.fixedDeltaTime,
            0,
            moveDir.y * _moveSpeed * Time.fixedDeltaTime
        );

        // Update the Blue Vector
        transform.forward =
            moveDir == Vector2.zero ?
            transform.forward :
            _playerRigidbody.velocity;
    }

    void MoveAutomatically()
    {
        // Move According to the physics
        transform.position = Vector3.MoveTowards(
            transform.position,
            _automaticPathPointer.Current.position,
            _smoothMoveSpeed * Time.deltaTime
        );

        // Move to the next Checkpoint
        if (transform.position == _automaticPathPointer.Current.position)
        {
            if (!_automaticPathPointer.MoveNext())
            {
                _automaticPathPointer = _automaticPathConfig.GetWaypoints();
                _automaticPathPointer.MoveNext();
            }
        }

        // Update the Blue Vector
        transform.forward = _automaticPathPointer.Current.position - transform.position;
    }

    #endregion
}
