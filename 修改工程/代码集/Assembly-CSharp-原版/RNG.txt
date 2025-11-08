using System;
using System.Collections.Generic;
using UnityEngine;

public static class RNG
{
	public static bool RandomBool()
	{
		return UnityEngine.Random.Range(0, 2) == 0;
	}

	public static T RandomChoice<T>(T first, T second)
	{
		return (!RNG.RandomBool()) ? second : first;
	}

	public static int RandomPositive(int max)
	{
		return UnityEngine.Random.Range(0, max + 1);
	}

	public static int RandomInteger(int min, int max)
	{
		return UnityEngine.Random.Range(min, max + 1);
	}

	public static T RandomItemFrom<T>(List<T> characters)
	{
		if (characters.Count > 0)
		{
			return characters[RNG.RandomInteger(0, characters.Count - 1)];
		}
		return default(T);
	}

	public static void Shuffle<T>(this List<T> list)
	{
		int i = list.Count;
		while (i > 1)
		{
			i--;
			int index = RNG.RandomPositive(i);
			T value = list[index];
			list[index] = list[i];
			list[i] = value;
		}
	}
}
