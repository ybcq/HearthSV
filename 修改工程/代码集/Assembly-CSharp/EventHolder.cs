using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EventHolder<T> : IFireable
{
	public int Count
	{
		get
		{
			return this.Events.Count;
		}
	}

	public DisposableEvent<T> Add(Func<T, IEnumerator> evt)
	{
		this.Events.Add(evt);
		return new DisposableEvent<T>(evt, this);
	}

	public void Remove(Func<T, IEnumerator> evt)
	{
		if (this.Events.Contains(evt))
		{
			this.Events.Remove(evt);
		}
	}

	public IEnumerator Fire(object value)
	{
		foreach (Func<T, IEnumerator> evt in this.Events.ToList<Func<T, IEnumerator>>())
		{
			if (value != null)
			{
				yield return evt((T)((object)value));
			}
			else
			{
				yield return evt(default(T));
			}
		}
		yield break;
	}

	public void DisposeAll()
	{
		this.Events.Clear();
	}

	public List<Func<T, IEnumerator>> Events = new List<Func<T, IEnumerator>>();
}
