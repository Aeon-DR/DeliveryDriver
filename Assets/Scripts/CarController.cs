using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float _turnSpeed = 250f;
    [SerializeField] private float _moveSpeed = 18f;
    [SerializeField] private float _boostSpeed = 1f;

    private void OnEnable()
    {
        GameManager.OnGameOver += FreezeMovement;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= FreezeMovement;
    }

    private void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal");
        float moveAmount = Input.GetAxis("Vertical");

        transform.Translate(Vector3.up * Time.deltaTime * _moveSpeed * moveAmount);
        transform.Rotate(Vector3.back, Time.deltaTime * _turnSpeed * steerAmount * moveAmount);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(Vector3.up * Time.deltaTime * (_moveSpeed + _boostSpeed) * moveAmount);
        }
    }

    private void FreezeMovement()
    {
        _turnSpeed = 0;
        _moveSpeed = 0;
        _boostSpeed = 0;

        // Freeze the car's movement by adjusting its rb constraints instead once proper car physics are implemented
        // var rb = GetComponent<Rigidbody2D>();
        // rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
