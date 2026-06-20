using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Player SFX")]
    public AudioClip hurt;
    public AudioClip shoot;
    public AudioClip jump;
    public AudioClip walk;

    [Header("Music")]
    public AudioClip bgMusic;
    public AudioClip titleMusic;
    public AudioClip endMusic;

    [Header("Enemy SFX")]
    public AudioClip e_hurt;
    public AudioClip e_shoot;
    //public AudioClip bgMusic;
    //public AudioClip bgMusic;
    //public AudioClip bgMusic;

    private static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //musicSource.clip = bgMusic;
        //musicSource.Play();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Title":
                PlayMusic(titleMusic, true);
                break;

            case "Game":
                PlayMusic(bgMusic, true);
                break;

            case "GameOver":
                PlayMusic(endMusic, false);
                break;

            default:
                PlayMusic(bgMusic, true);
                break;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    public void PlayMusic(AudioClip clip, bool loop)
    {
        if (clip == null) return;

        if (musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }








}
