using System;
using UnityEngine;

public class PSButtonBack : MonoBehaviour
{
	public void OnClick()
	{
		if (!PlaySelector.Instance.IsLoading)
		{
			MenuManager.Instance.Exit();
		}
	}
}
