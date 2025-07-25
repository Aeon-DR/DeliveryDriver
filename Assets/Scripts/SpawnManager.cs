using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _spawnLocations;
    [SerializeField] private GameObject _packagePrefab;
    [SerializeField] private GameObject _customerPrefab;
    private int _lastIndex;

    private void OnEnable()
    {
        PackageDelivery.OnPackagePickedUp += HandlePackagedPickedUp;
        PackageDelivery.OnPackageDelivered += HandlePackageDelivered;
    }

    private void OnDisable()
    {
        PackageDelivery.OnPackagePickedUp -= HandlePackagedPickedUp;
        PackageDelivery.OnPackageDelivered -= HandlePackageDelivered;
    }

    private void Start()
    {
        SpawnRandomly(_packagePrefab);
    }

    private void SpawnRandomly(GameObject prefab)
    {
        while (true)
        {
            int randomIndex = Random.Range(0, _spawnLocations.Count);
            if (randomIndex != _lastIndex) // Don't spawn a new object at the same location as the previous object
            {
                Instantiate(prefab, _spawnLocations[randomIndex].transform.position, transform.rotation);
                _lastIndex = randomIndex;
                break;
            }
        }
    }

    private void HandlePackagedPickedUp()
    {
        SpawnRandomly(_customerPrefab);
    }

    private void HandlePackageDelivered()
    {
        SpawnRandomly(_packagePrefab);
    }
}
