using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    [Header("Player Sounds")]
    [SerializeField] private AudioClip playerShootClip;
    [SerializeField] private AudioClip playerDamageClip;
    [SerializeField] private AudioClip playerDeathClip;

    [Header("Enemy Sounds")]
    [SerializeField] private AudioClip enemyShootClip;
    [SerializeField] private AudioClip enemyDamageClip;
    [SerializeField] private AudioClip enemyDeathClip;

    [Header("ASteroid Sounds")]
    [SerializeField] private AudioClip asteroidDestroyClip;
    

    [Header("Power Up Sounds")]
    [SerializeField] private AudioClip powerUpPickupClip;

    [Header("Other Sounds")]
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private AudioClip buttonClickClip;

    [Header("Music")]
    [SerializeField] private AudioClip backgroundMusicClip;
    [SerializeField] private AudioClip gameMenuMusicClip;
    [Header("Volume")]
    [Range(0f, 1f)]
    [SerializeField] private float sfxVolume = 1f;

    [Range(0f, 1f)]
    [SerializeField] private float musicVolume = 0.5f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    

    private void PlaySfx(AudioClip clip)
    {
        if (clip == null || sfxSource == null)
            return;

        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    private void PlayMusic(AudioClip clip)
    {
        if (clip == null || musicSource == null)
            return;

        musicSource.clip = clip;
        musicSource.volume = musicVolume;
        musicSource.loop = true;
        musicSource.Play();
    }

    private void StopMusicClip()
    {
        if (musicSource == null)
            return;

        musicSource.Stop();
    }


    public static void PlayPlayerShoot()
    {
        if (instance == null)
            return;

        instance.PlaySfx(instance.playerShootClip);
    }

    public static void PlayPlayerDamage()
    {
        if (instance == null)
            return;

        instance.PlaySfx(instance.playerDamageClip);
    }

    public static void PlayPlayerDeath()
    {
        if (instance == null)
            return;

        instance.PlaySfx(instance.playerDeathClip);
    }

    public static void PlayEnemyShoot()
    {
        if (instance == null)
            return;

        instance.PlaySfx(instance.enemyShootClip);
    }

    public static void PlayEnemyDamage()
    {
        if (instance == null)
            return;

        instance.PlaySfx(instance.enemyDamageClip);
    }

    public static void PlayEnemyDeath()
    {
        if (instance == null)
            return;

        instance.PlaySfx(instance.enemyDeathClip);
    }
    public static void PlayAsteroidDestruction()
    {
        if (instance == null)
            return;

        instance.PlaySfx(instance.asteroidDestroyClip);
    }

    public static void PlayPowerUpPickup()
    {
        if (instance == null)
            return;

        instance.PlaySfx(instance.powerUpPickupClip);
    }

    public static void PlayExplosion()
    {
        if (instance == null)
            return;

        instance.PlaySfx(instance.explosionClip);
    }

    public static void PlayButtonClick()
    {
        if (instance == null)
            return;

        instance.PlaySfx(instance.buttonClickClip);
    }

    public static void PlayBackgroundMusic()
    {
        if (instance == null)
            return;

        instance.PlayMusic(instance.backgroundMusicClip);
    }
    public static void PlayMenuMusic()
    {
        if (instance == null)
            return;

        instance.PlayMusic(instance.gameMenuMusicClip);

    }

    public static void StopMusic()
    {
        if (instance == null)
            return;

        instance.StopMusicClip();
    }
}