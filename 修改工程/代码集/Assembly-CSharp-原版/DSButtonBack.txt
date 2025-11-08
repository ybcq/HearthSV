using System;
using UnityEngine;

public class DSButtonBack : MonoBehaviour
{
	public void OnClick()
	{
		MenuManager.Instance.Exit();
	}
}
