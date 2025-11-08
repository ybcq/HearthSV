using System;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
	private CursorManager()
	{
	}

	public static CursorManager Instance
	{
		get
		{
			return CursorManager._instance;
		}
	}

	private void Start()
	{
		if (CursorManager._instance != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		CursorManager._instance = this;
		this.SetCursor(CursorStatus.Normal);
	}

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			if (this.Status == CursorStatus.Normal)
			{
				this.SetCursor(CursorStatus.Click);
			}
		}
		else if (this.Status == CursorStatus.Click)
		{
			this.SetCursor(CursorStatus.Normal);
		}
	}

	public void SetCursor(CursorStatus status)
	{
		Cursor.SetCursor(ResourcesManager.Cursors[status.GetEnumName()], Vector2.zero, CursorMode.Auto);
		this.Status = status;
	}

	private static CursorManager _instance;

	private CursorStatus Status;
}
