using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        private const string Mixer_Music = "MusicVol";
        private const string Mixer_SFX = "SFXVol";

        private const float Min_Volume_Threshold = 0.001f;
        private const float Mute_Db = -80f;
        private const float Db_Multiplier = 20f;

        public static AudioManager Instance;

        [Header("Settings")] [SerializeField] private AudioMixer mainMixer;
        [SerializeField] private AudioLibrary audioLibrary;

        [Header("Sources")] [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            SetMusicVolume(GameDataManager.MusicVolume);
            SetSFXVolume(GameDataManager.SoundVolume);

            PlayMusic(SoundType.BackgroundMusic);
        }

        public void PlaySound(SoundType type)
        {
            if (GameDataManager.SoundVolume <= Min_Volume_Threshold) return;

            AudioClip clip = audioLibrary.GetClip(type, out float vol);
            if (clip != null)
                sfxSource.PlayOneShot(clip, vol);
        }

        public void PlayMusic(SoundType type)
        {
            AudioClip clip = audioLibrary.GetClip(type, out float vol);
            if (clip != null && musicSource.clip != clip)
            {
                musicSource.clip = clip;
                musicSource.volume = vol;
                musicSource.Play();
            }
        }
        
        public void PlayLoopingSound(SoundType type)
        {
            if (GameDataManager.SoundVolume <= Min_Volume_Threshold) return;

            AudioClip clip = audioLibrary.GetClip(type, out float vol);
            if (sfxSource.isPlaying && sfxSource.clip == clip) return;

            if (clip != null)
            {
                sfxSource.clip = clip;
                sfxSource.loop = true;
                sfxSource.volume = vol;
                sfxSource.Play();
            }
        }
        
        public void StopLoopingSound(SoundType type)
        {
            AudioClip clipToStop = audioLibrary.GetClip(type, out float _);
            if (sfxSource.isPlaying && sfxSource.clip == clipToStop)
            {
                sfxSource.Stop();
                sfxSource.clip = null;
                sfxSource.loop = false;
            }
        }

        public void SetMusicVolume(float sliderValue)
        {
            GameDataManager.MusicVolume = sliderValue;
            SetVolumeToMixer(Mixer_Music, sliderValue);
        }

        public void SetSFXVolume(float sliderValue)
        {
            GameDataManager.SoundVolume = sliderValue;
            SetVolumeToMixer(Mixer_SFX, sliderValue);
        }
        
        private void SetVolumeToMixer(string parameterName, float sliderValue)
        {
            float db;
            if (sliderValue <= Min_Volume_Threshold)
                db = Mute_Db;
            else
                db = Mathf.Log10(sliderValue) * Db_Multiplier;

            mainMixer.SetFloat(parameterName, db);
        }
    }
}