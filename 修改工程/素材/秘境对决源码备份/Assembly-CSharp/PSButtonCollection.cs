using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PSButtonCollection : MonoBehaviour
{
	public void OnClick()
	{
		if (!PlaySelector.Instance.IsLoading)
		{
			MenuManager.Instance.LoadDeckBuilder();
			SceneManager.UnloadScene("PlaySelector");
		}
	}
}
