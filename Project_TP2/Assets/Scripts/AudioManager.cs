using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _itemCollectSound;
    [SerializeField] private AudioClip _loseSound;
    [SerializeField] private AudioClip _winSound;

    private AudioSource _musicSource;
    private AudioSource _sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _musicSource = gameObject.AddComponent<AudioSource>();
        _sfxSource = gameObject.AddComponent<AudioSource>();

        _musicSource.loop = true;
        _musicSource.clip = _backgroundMusic;
        _musicSource.Play();
    }

    public void PlayJumpSound()
    {
        _sfxSource.PlayOneShot(_jumpSound);
    }

    public void PlayItemCollectSound()
    {
        _sfxSource.PlayOneShot(_itemCollectSound);
    }

    public void PlayLoseSound()
    {
        _sfxSource.PlayOneShot(_loseSound);
    }

    public void PlayWinSound()
    {
        _sfxSource.PlayOneShot(_winSound);
    }
}
