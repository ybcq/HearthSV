using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionQueue : MonoBehaviour
{
	private ActionQueue()
	{
	}

	public static ActionQueue Instance
	{
		get
		{
			return ActionQueue._instance;
		}
	}

	public static void Add(Func<IEnumerator> action)
	{
		ActionQueue._instance.Queue.Add(action);
	}

	public static void AddVoid(Action action)
	{
		ActionQueue._instance.Queue.Add(delegate
		{
			action();
			return null;
		});
	}

	public static void StartParallel(Func<IEnumerator> action)
	{
		ActionQueue._instance.StartCoroutine(action());
	}

	private void Awake()
	{
		if (ActionQueue._instance != null)
		{
			ActionQueue._instance.StopAllCoroutines();
		}
		ActionQueue._instance = this;
		this.QueueCoroutine = base.StartCoroutine(this.QueueLoop());
	}

	private IEnumerator QueueLoop()
	{
		for (;;)
		{
			if (this.Queue.Count > 0)
			{
				Func<IEnumerator> firstAction = this.Queue[0];
				yield return firstAction();
				this.Queue.Remove(firstAction);
			}
			if (this.Queue.Count == 0)
			{
				yield return new WaitForSeconds(0.1f);
			}
		}
		yield break;
	}

	public static void StopAll()
	{
		ActionQueue._instance.StopAllCoroutines();
		ActionQueue._instance.Queue.Clear();
	}

	private static ActionQueue _instance;

	private Coroutine QueueCoroutine;

	private volatile List<Func<IEnumerator>> Queue = new List<Func<IEnumerator>>();
}
