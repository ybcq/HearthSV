using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
	private SoundManager()
	{
	}

	public static SoundManager Instance
	{
		get
		{
			return SoundManager._instance;
		}
	}

	private void Awake()
	{
		if (SoundManager._instance == null)
		{
			SoundManager._instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void Play(AudioClip clip)
	{
		this.GlobalAudioSource.PlayOneShot(clip, 0.5f * this.GlobalVolume * this.GeneralVolume);
	}

	public void Play(string clip)
	{
		AudioClip audioClip = ResourcesManager.Sounds[clip];
		if (audioClip != null)
		{
			this.Play(audioClip);
		}
	}

	public void Play(AudioClip clip, float volume)
	{
		this.GlobalAudioSource.PlayOneShot(clip, volume * this.GlobalVolume * this.GeneralVolume);
	}

	public void Play(string clip, float volume)
	{
		AudioClip audioClip = ResourcesManager.Sounds[clip];
		if (audioClip != null)
		{
			this.Play(audioClip, volume);
		}
	}

	public void PlayDelayed(AudioClip clip, float volume, float delay)
	{
		base.StartCoroutine(this.PlayDelayedCoroutine(clip, volume, delay));
	}

	public void PlayDelayed(string clip, float volume, float delay)
	{
		AudioClip audioClip = ResourcesManager.Sounds[clip];
		if (audioClip != null)
		{
			this.PlayDelayed(audioClip, volume, delay);
		}
	}

	private IEnumerator PlayDelayedCoroutine(AudioClip clip, float volume, float delay)
	{
		yield return new WaitForSeconds(delay);
		this.GlobalAudioSource.PlayOneShot(clip, volume * this.GlobalVolume * this.GeneralVolume);
		yield break;
	}

	public AudioSource PlayOnLoop(string clip, float volume)
	{
		AudioClip audioClip = ResourcesManager.Sounds[clip];
		if (audioClip != null)
		{
			return this.PlayOnLoop(audioClip, volume);
		}
		return null;
	}

	public AudioSource PlayOnLoop(AudioClip clip, float volume)
	{
		AnimationCurve curve = AnimationCurve.Linear(0f, 1f, 9999f, 1f);
		AudioSource audioSource = base.gameObject.AddComponent<AudioSource>();
		audioSource.loop = true;
		audioSource.clip = clip;
		audioSource.volume = volume * this.GlobalVolume * this.GeneralVolume;
		audioSource.rolloffMode = AudioRolloffMode.Custom;
		audioSource.SetCustomCurve(AudioSourceCurveType.CustomRolloff, curve);
		audioSource.Play();
		return audioSource;
	}

	public void PlayMinionSound(MinionCard minionCard, string action, float volume)
	{
		string path = string.Concat(new string[]
		{
			"Sounds/",
			minionCard.Class.GetEnumName(),
			"/Minions/",
			minionCard.GetTypeName(),
			"_",
			action
		});
		AudioClip audioClip = Resources.Load<AudioClip>(path);
		if (audioClip != null)
		{
			this.GlobalAudioSource.PlayOneShot(audioClip, volume * this.GlobalVolume * this.GeneralVolume);
		}
	}

	public void PlayHeroSound(Hero hero, string action, float volume)
	{
		string path = "Sounds/" + hero.Class.GetEnumName() + "/Hero/" + action;
		AudioClip audioClip = Resources.Load<AudioClip>(path);
		if (audioClip != null)
		{
			this.GlobalAudioSource.PlayOneShot(audioClip, volume * this.GlobalVolume * this.GeneralVolume);
		}
	}

	public void PlayImpactSound(int damage)
	{
		if (damage >= 0 && damage <= 5)
		{
			this.GlobalAudioSource.PlayOneShot(ResourcesManager.Sounds["Game_Impact_Normal"], 0.2f * this.GlobalVolume * this.GeneralVolume);
		}
		else if (damage >= 6 && damage <= 7)
		{
			this.GlobalAudioSource.PlayOneShot(ResourcesManager.Sounds["Game_Impact_Mid"], 0.25f * this.GlobalVolume * this.GeneralVolume);
		}
		else
		{
			this.GlobalAudioSource.PlayOneShot(ResourcesManager.Sounds["Game_Impact_Large"], 0.3f * this.GlobalVolume * this.GeneralVolume);
		}
	}

	public void PlayDropSound(int value)
	{
		if (value >= 0 && value <= 5)
		{
			this.GlobalAudioSource.PlayOneShot(ResourcesManager.Sounds["Game_Drop_Normal"], 0.2f * this.GlobalVolume * this.GeneralVolume);
		}
		else if (value >= 6 && value <= 7)
		{
			this.GlobalAudioSource.PlayOneShot(ResourcesManager.Sounds["Game_Drop_Mid"], 0.25f * this.GlobalVolume * this.GeneralVolume);
		}
		else
		{
			this.GlobalAudioSource.PlayOneShot(ResourcesManager.Sounds["Game_Drop_Large"], 0.3f * this.GlobalVolume * this.GeneralVolume);
		}
	}

	public void OnGlobalChanged(Slider slider)
	{
		this.GlobalVolume = slider.value;
	}

	public void OnGeneralChanged(Slider slider)
	{
		this.GeneralVolume = slider.value;
	}

	public void OnMusicChanged(Slider slider)
	{
		this.MusicVolume = slider.value;
	}

	private static SoundManager _instance;

	public AudioSource GlobalAudioSource;

	[Range(0f, 1f)]
	public float GlobalVolume = 0.5f;

	[Range(0f, 1f)]
	public float GeneralVolume = 0.5f;

	[Range(0f, 1f)]
	public float MusicVolume = 0.1f;
}
