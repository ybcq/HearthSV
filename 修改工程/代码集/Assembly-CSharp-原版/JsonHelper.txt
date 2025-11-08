using System;
using UnityEngine;

public static class JsonHelper
{
	public static T[] FromJson<T>(string json)
	{
		JsonHelper.Wrapper<T> wrapper = JsonUtility.FromJson<JsonHelper.Wrapper<T>>(json);
		return wrapper.Items;
	}

	public static string ToJson<T>(T[] array)
	{
		return JsonUtility.ToJson(new JsonHelper.Wrapper<T>
		{
			Items = array
		});
	}

	[Serializable]
	private class Wrapper<T>
	{
		public T[] Items;
	}
}
