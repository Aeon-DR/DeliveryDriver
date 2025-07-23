using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float _turnSpeed = 250f;
    [SerializeField] private float _moveSpeed = 18f;

    private void Start()
    {
        
    }

    private void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * _turnSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * _moveSpeed * Time.deltaTime; 
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, moveAmount, 0);
    }
}
