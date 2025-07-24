using UnityEngine;

public class PackageDelivery : MonoBehaviour
{
    [SerializeField] private Color32 _originalCarTint = new Color32(255, 255, 255, 255);
    [SerializeField] private Color32 _hasPackageCarTint = new Color32(51, 255, 51, 255);

    private SpriteRenderer _spriteRenderer;
    private bool _hasPackage;
    private float _destroyDelay = 0.5f;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Package") && !_hasPackage)
        {
            Debug.Log("Package picked up!");
            _hasPackage = true;
            _spriteRenderer.color = _hasPackageCarTint;
            Destroy(collision.gameObject, _destroyDelay);
        }

        if (collision.CompareTag("Customer") && _hasPackage)
        {
            Debug.Log("Package delivered!");
            _hasPackage = false;
            _spriteRenderer.color = _originalCarTint;
            Destroy(collision.gameObject, _destroyDelay);
        }
    }
}
