using UnityEngine;

public class PackageDelivery : MonoBehaviour
{
    private bool _hasPackage;
    private float _destroyDelay = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Package") && !_hasPackage)
        {
            Debug.Log("Package picked up!");
            _hasPackage = true;
            Destroy(collision.gameObject, _destroyDelay);
        }

        if (collision.CompareTag("Customer") && _hasPackage)
        {
            Debug.Log("Package delivered!");
            _hasPackage = false;
            Destroy(collision.gameObject, _destroyDelay);
        }
    }
}
