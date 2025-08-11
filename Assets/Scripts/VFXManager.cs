using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _leftNitroFlame;
    [SerializeField] private ParticleSystem _rightNitroFlame;
    [SerializeField] private ParticleSystem _smokePrefab;
    [SerializeField] private GameObject _playerCar;

    private void OnEnable()
    {
        CarController.OnCarAccelerationStart += PlayAccelerationVFX;
        CarController.OnCarAccelerationEnd += StopAccelerationVFX;
        CarController.OnMovementFreeze += PlaySmokeVFX;
    }

    private void OnDisable()
    {
        CarController.OnCarAccelerationStart -= PlayAccelerationVFX;
        CarController.OnCarAccelerationEnd -= StopAccelerationVFX;
        CarController.OnMovementFreeze -= PlaySmokeVFX;
    }

    private void PlayAccelerationVFX()
    {
        if (CarController.IsInPenalty || GameManager.IsGameOver)
        {
            return;
        }

        _leftNitroFlame.Play();
        _rightNitroFlame.Play();
    }

    private void StopAccelerationVFX()
    {
        _leftNitroFlame.Stop();
        _rightNitroFlame.Stop();
    }

    private void PlaySmokeVFX()
    {
        ParticleSystem smoke = Instantiate(_smokePrefab, _playerCar.transform.position, Quaternion.identity);
        smoke.Play();
        Destroy(smoke.gameObject, smoke.main.duration + smoke.main.startLifetime.constantMax);
    }
}
