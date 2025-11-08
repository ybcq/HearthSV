using System;

public class Aura<T>
{
	public Aura(Action<T> apply, Action<T> remove, Func<T, bool> applyCondition, Func<bool> existCondition)
	{
		this.Apply = apply;
		this.Remove = remove;
		this.ApplyCondition = applyCondition;
		this.ExistCondition = existCondition;
	}

	public Action<T> Apply;

	public Action<T> Remove;

	public Func<T, bool> ApplyCondition;

	public Func<bool> ExistCondition;
}
