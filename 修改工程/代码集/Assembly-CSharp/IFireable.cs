using System;
using System.Collections;

public interface IFireable
{
	int Count { get; }

	IEnumerator Fire(object value);

	void DisposeAll();
}
