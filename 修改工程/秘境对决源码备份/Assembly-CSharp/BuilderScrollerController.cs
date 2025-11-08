using System;
using UnityEngine;

public class BuilderScrollerController : MonoBehaviour
{
	private BuilderScrollerController()
	{
	}

	public static BuilderScrollerController Instance
	{
		get
		{
			return BuilderScrollerController._instance;
		}
	}

	private void Awake()
	{
		BuilderScrollerController._instance = this;
	}

	private void Update()
	{
		float scrollerPosition;
		if (this.IsDragging)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			Physics.Raycast(ray, out raycastHit);
			scrollerPosition = raycastHit.point.z;
		}
		else
		{
			scrollerPosition = base.transform.localPosition.y + Input.GetAxis("Mouse ScrollWheel") * 5f;
		}
		this.UpdateScrollerPosition(scrollerPosition);
	}

	private void UpdateScrollerPosition(float scrollerPosition)
	{
		float num = Mathf.Clamp(scrollerPosition, -4f, 5f);
		base.transform.localPosition = new Vector3(7f, num, 0f);
		float num2 = (num - -4f) / 4.5f;
		float y = Mathf.Clamp(8f - num2 * 2f, 4.25f, 8f);
		this.SelectionParent.transform.localPosition = new Vector3(5.25f, y, 0f);
	}

	public void ResetScrollerPosition()
	{
		this.UpdateScrollerPosition(5f);
	}

	private void OnMouseDown()
	{
		this.IsDragging = true;
	}

	private void OnMouseUp()
	{
		this.IsDragging = false;
	}

	private static BuilderScrollerController _instance;

	public GameObject SelectionParent;

	private const float TOP = 5f;

	private const float BOT = -4f;

	private const float BG_TOP = 8f;

	private const float BG_BOT = 4.25f;

	private const float MOUSE_SPEED = 5f;

	private bool IsDragging;
}
