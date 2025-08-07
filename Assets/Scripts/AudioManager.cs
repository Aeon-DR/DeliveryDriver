using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource _sfxSource;

    [SerializeField] private AudioClip _carCrash;
    [SerializeField] private AudioClip _carEngine;
    [SerializeField] private AudioClip _carDoorShut;
    [SerializeField] private AudioClip _carHorn;
    [SerializeField] private AudioClip _tadaFanfare;

    private void OnEnable()
    {
       PackageDelivery.OnPackagePickedUp += PlayPackagePickedSound;
       PackageDelivery.OnPackageDelivered += PlayPackageDeliveredSound;
       CarController.OnMovementFreeze += PlayCarCrashSound;
       CarController.OnCarAcceleration += PlayCarEngineSound;
       GameManager.OnGameOver += PlayFanfareSound;
    }

    private void OnDisable()
    {
        PackageDelivery.OnPackagePickedUp -= PlayPackagePickedSound;
        PackageDelivery.OnPackageDelivered -= PlayPackageDeliveredSound;
        CarController.OnMovementFreeze -= PlayCarCrashSound;
        CarController.OnCarAcceleration -= PlayCarEngineSound;
        GameManager.OnGameOver -= PlayFanfareSound;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void PlayPackagePickedSound()
    {
        _sfxSource.PlayOneShot(_carDoorShut);
    }

    private void PlayPackageDeliveredSound()
    {
        _sfxSource.PlayOneShot(_carHorn);
    }

    private void PlayCarCrashSound()
    {
        _sfxSource.PlayOneShot(_carCrash);
    }

    private void PlayCarEngineSound()
    {
        if (CarController.IsInPenalty || GameManager.IsGameOver)
        {
            return;
        }

        // Stop the engine sound if it's already playing to have only one instance playing at a time
        if (_sfxSource.isPlaying && _sfxSource.clip == _carEngine)
        {
            _sfxSource.Stop();
        }

        _sfxSource.clip = _carEngine;
        _sfxSource.Play();
    }

    private void PlayFanfareSound()
    {
        _sfxSource.PlayOneShot(_tadaFanfare);
    }
}
