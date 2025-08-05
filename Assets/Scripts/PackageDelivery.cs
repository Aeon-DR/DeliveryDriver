using System;
using UnityEngine;

public class PackageDelivery : MonoBehaviour
{
    public static event Action OnPackagePickedUp;
    public static event Action OnPackageDelivered;

    private GameObject _package;
    private bool _hasPackage;
    private float _destroyDelay = 0.5f;

    private void Update()
    {
        // Keep the package on top of the player's car once picked up
        if (_package != null)
        {   
            _package.transform.SetPositionAndRotation(transform.position, transform.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Package") && !_hasPackage)
        {
            _hasPackage = true;
            _package = collision.gameObject;
            OnPackagePickedUp?.Invoke();
        }

        if (collision.CompareTag("Customer") && _hasPackage)
        {
            _hasPackage = false;
            OnPackageDelivered?.Invoke();
            Destroy(collision.gameObject, _destroyDelay);
            Destroy(_package, _destroyDelay);
        }
    }
}
