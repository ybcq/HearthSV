using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraManager : MonoBehaviour
{
	private CameraManager()
	{
	}

	public static CameraManager Instance
	{
		get
		{
			return CameraManager._instance;
		}
	}

	private void Awake()
	{
		if (CameraManager._instance == null)
		{
			CameraManager._instance = this;
			UnityEngine.Object.DontDestroyOnLoad(this.Main);
			UnityEngine.Object.DontDestroyOnLoad(this.Secondary);
			this.MainGrayscale = this.Main.GetComponent<Grayscale>();
			this.MainBlur = this.Main.GetComponent<BlurOptimized>();
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
			foreach (Camera camera in from c in Camera.allCameras
			where c != CameraManager._instance.Main && c != CameraManager._instance.Secondary
			select c)
			{
				UnityEngine.Object.Destroy(camera.gameObject);
			}
		}
	}

	public void Reposition(Vector3 position, Vector3 rotation, float fov)
	{
		this.Main.transform.localPosition = position;
		this.Main.transform.localEulerAngles = rotation;
		this.Main.fieldOfView = fov;
		this.Secondary.transform.localPosition = position;
		this.Secondary.transform.localEulerAngles = rotation;
		this.Secondary.fieldOfView = fov;
	}

	public void StopGrayFade()
	{
		if (this.GrayCoroutine != null)
		{
			base.StopCoroutine(this.GrayCoroutine);
		}
	}

	public void FadeToGray(float duration)
	{
		this.StopGrayFade();
		this.GrayCoroutine = base.StartCoroutine(this.FadeToGrayAnimation(duration));
	}

	private IEnumerator FadeToGrayAnimation(float duration)
	{
		this.MainGrayscale.amount = 0f;
		this.MainGrayscale.enabled = true;
		float startTime = Time.timeSinceLevelLoad;
		while (Time.timeSinceLevelLoad - startTime < duration)
		{
			float elapsedTime = Time.timeSinceLevelLoad - startTime;
			this.MainGrayscale.amount = elapsedTime / duration;
			yield return null;
		}
		this.MainGrayscale.amount = 1f;
		this.GrayCoroutine = null;
		yield break;
	}

	public void FadeToNormal(float duration)
	{
		this.StopGrayFade();
		this.GrayCoroutine = base.StartCoroutine(this.FadeToNormalAnimation(duration));
	}

	private IEnumerator FadeToNormalAnimation(float duration)
	{
		this.MainGrayscale.enabled = true;
		float startTime = Time.timeSinceLevelLoad;
		float remainingTime = this.MainGrayscale.amount * duration;
		while (Time.timeSinceLevelLoad - startTime < remainingTime)
		{
			float elapsedTime = Time.timeSinceLevelLoad - startTime;
			this.MainGrayscale.amount = remainingTime - elapsedTime / duration;
			yield return null;
		}
		this.MainGrayscale.amount = 0f;
		this.GrayCoroutine = null;
		yield break;
	}

	public void DisableGray()
	{
		this.MainGrayscale.amount = 0f;
		this.MainGrayscale.enabled = false;
	}

	public void StopBlurFade()
	{
		if (this.BlurCoroutine != null)
		{
			base.StopCoroutine(this.BlurCoroutine);
		}
	}

	public void FadeToBlur(float duration)
	{
		this.StopBlurFade();
		this.BlurCoroutine = base.StartCoroutine(this.FadeToBlurAnimation(duration));
	}

	private IEnumerator FadeToBlurAnimation(float duration)
	{
		this.MainBlur.blurSize = 0f;
		this.MainBlur.enabled = true;
		float startTime = Time.timeSinceLevelLoad;
		while (Time.timeSinceLevelLoad - startTime < duration)
		{
			float elapsedTime = Time.timeSinceLevelLoad - startTime;
			this.MainBlur.blurSize = elapsedTime / duration * 2.5f;
			yield return null;
		}
		this.MainBlur.blurSize = 2.5f;
		this.BlurCoroutine = null;
		yield break;
	}

	public void DisableBlur()
	{
		this.MainBlur.blurSize = 0f;
		this.MainBlur.enabled = false;
	}

	private static CameraManager _instance;

	public Camera Main;

	public Camera Secondary;

	private Grayscale MainGrayscale;

	private BlurOptimized MainBlur;

	private Coroutine GrayCoroutine;

	private Coroutine BlurCoroutine;

	private const float MAX_BLUR_SIZE = 2.5f;
}
