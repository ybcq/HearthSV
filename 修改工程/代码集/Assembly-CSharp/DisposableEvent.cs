using System;
using System.Collections;

public class DisposableEvent<T> : IDisposable
{
	public DisposableEvent(Func<T, IEnumerator> evt, EventHolder<T> holder)
	{
		this.Event = evt;
		this.Holder = holder;
	}

	public void Dispose()
	{
		this.Holder.Remove(this.Event);
	}

	public Func<T, IEnumerator> Event;

	public EventHolder<T> Holder;
}
