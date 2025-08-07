using System;
using System.Collections;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public static event Action OnMovementFreeze;
    public static event Action OnMovementUnfreeze;
    public static event Action OnCarAcceleration;

    public static bool IsInPenalty;

    [SerializeField] private float _moveForce = 200f;
    [SerializeField] private float _turnTorque = 35f;
    [SerializeField] private float _boostMultiplier = 1.5f;

    private Rigidbody2D _rigidbody;
    private float _penaltyDuration = 1.5f;

    private void OnEnable()
    {
        GameManager.OnGameOver += FreezeMovement;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= FreezeMovement;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        UnfreezeMovement();
    }

    private void Update()
    {
        // Play engine sound only once when Left Shift is pressed
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            OnCarAcceleration?.Invoke();
        }
    }


    private void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");
        float steerInput = Input.GetAxis("Horizontal");
        float force = _moveForce;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            force *= _boostMultiplier;
        }
            
        _rigidbody.AddForce(transform.up * moveInput * force);

        // Apply torque only when moving
        if (moveInput != 0)
        {
            _rigidbody.AddTorque(-steerInput * _turnTorque * moveInput);
        }
    }

    private void OnCollisionEnter2D()
    {
        StartCoroutine(CollisionPenaltyRoutine(_penaltyDuration));
    }

    private void FreezeMovement()
    {
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        IsInPenalty = true;
    }

    private void UnfreezeMovement()
    {
        _rigidbody.constraints = RigidbodyConstraints2D.None;
        IsInPenalty = false;
    }

    private IEnumerator CollisionPenaltyRoutine(float penaltyDuration)
    {
        FreezeMovement();
        OnMovementFreeze?.Invoke();
        yield return new WaitForSeconds(penaltyDuration);

        if (!GameManager.IsGameOver)
        {
            UnfreezeMovement();
            OnMovementUnfreeze?.Invoke();
        }
    }
}
