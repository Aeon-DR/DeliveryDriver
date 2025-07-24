using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float _turnSpeed = 250f;
    [SerializeField] private float _moveSpeed = 18f;
    [SerializeField] private float _boostSpeed = 1f;

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
}
