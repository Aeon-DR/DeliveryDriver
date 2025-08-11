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
       CarController.OnCarAccelerationStart += PlayCarEngineSound;
       GameManager.OnGameOver += PlayFanfareSound;
    }

    private void OnDisable()
    {
        PackageDelivery.OnPackagePickedUp -= PlayPackagePickedSound;
        PackageDelivery.OnPackageDelivered -= PlayPackageDeliveredSound;
        CarController.OnMovementFreeze -= PlayCarCrashSound;
        CarController.OnCarAccelerationStart -= PlayCarEngineSound;
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

        _sfxSource.PlayOneShot(_carEngine, 0.4f);
    }

    private void PlayFanfareSound()
    {
        _sfxSource.PlayOneShot(_tadaFanfare);
    }
}
