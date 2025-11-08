using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class Util
{
	public static T As<T>(this object self)
	{
		return (T)((object)self);
	}

	public static string GetName(this Character self)
	{
		if (self == null)
		{
			return "Nothing";
		}
		if (self.IsHero())
		{
			return self.As<Hero>().Class.GetEnumName();
		}
		return self.As<Minion>().Card.Name;
	}

	public static string GetEnumName(this Enum enumValue)
	{
		return Enum.GetName(enumValue.GetType(), enumValue);
	}

	public static string GetTypeName(this object typeInstance)
	{
		return typeInstance.GetType().Name;
	}

	public static IEnumerable<Type> FindDerivedTypesOf(Type baseType)
	{
		return from t in Assembly.GetExecutingAssembly().GetTypes()
		where t != baseType && baseType.IsAssignableFrom(t)
		select t;
	}

	public static string GetInverseNumberColor(int current, int basic)
	{
		if (current > basic)
		{
			return "Red";
		}
		if (current == basic)
		{
			return "White";
		}
		return "Green";
	}

	public static string GetCharacterNumberColor(int current, int basic)
	{
		if (current > basic)
		{
			return "Green";
		}
		if (current == basic)
		{
			return "White";
		}
		return "Red";
	}

	public static string GetCharacterNumberColor(int current, int basic, int max)
	{
		if (max > basic)
		{
			if (current == max)
			{
				return "Green";
			}
			return "Red";
		}
		else
		{
			if (max == basic)
			{
				return Util.GetCharacterNumberColor(current, basic);
			}
			if (current == max)
			{
				return "White";
			}
			return "Red";
		}
	}

	public static string GetAttackNumberColor(int current, int basic)
	{
		if (current <= basic)
		{
			return "White";
		}
		return "Green";
	}

	public static bool ContainsCardOfType<T>(this List<BaseCard> list)
	{
		return list.Count((BaseCard x) => x.GetType().BaseType == typeof(T)) > 0;
	}

	public static List<BaseCard> GetCardsOfType<T>(this List<BaseCard> list)
	{
		return (from x in list
		where x.GetType().BaseType == typeof(T)
		select x).ToList<BaseCard>();
	}

	public static List<Character> TargeteablesBySpellOf(this List<Character> list, Player player)
	{
		return (from c in list
		where !c.HasSpellshield && (c.Player == player || !c.IsStealth)
		select c).ToList<Character>();
	}

	public static List<Minion> TargeteablesBySpellOf(this List<Minion> list, Player player)
	{
		return (from c in list
		where !c.HasSpellshield && (c.Player == player || !c.IsStealth)
		select c).ToList<Minion>();
	}

	public static Character GetCharacterAtMouse()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] array = Physics.RaycastAll(ray);
		foreach (RaycastHit raycastHit in array)
		{
			BaseController component = raycastHit.collider.gameObject.GetComponent<BaseController>();
			if (component != null)
			{
				string name = component.GetType().Name;
				if (name != null)
				{
					if (name == "MinionController")
					{
						return component.As<MinionController>().Minion;
					}
					if (name == "HeroController")
					{
						return component.As<HeroController>().Hero;
					}
				}
			}
		}
		return null;
	}

	public static Vector3 GetWorldMousePosition()
	{
		return Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0f, 0f, 1940f));
	}

	public static void Reset(this Transform self)
	{
		self.localPosition = Vector3.zero;
		self.localEulerAngles = Vector3.zero;
		self.localScale = Vector3.one;
	}

	public static void ChangeParent(this Transform self, Transform parent)
	{
		self.parent = parent;
		self.Reset();
	}

	public static void ChangeParentAt(this Transform self, Transform parent, Vector3 position)
	{
		self.parent = parent;
		self.Reset();
		self.localPosition = position;
	}

	public static GameObject Instantiate(GameObject prefab, Transform parent)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab);
		gameObject.transform.ChangeParentAt(parent, prefab.transform.localPosition);
		gameObject.transform.localScale = prefab.transform.localScale;
		return gameObject;
	}

	public static float SquaredLerp(float from, float to, float t)
	{
		return from + (to - from) * (t * t);
	}

	public static Vector3 SquaredLerp(Vector3 from, Vector3 to, float t)
	{
		return from + (to - from) * (t * t);
	}

	public static float CubicLerp(float from, float to, float t)
	{
		return from + (to - from) * (t * t * t);
	}

	public static Vector3 CubicLerp(Vector3 from, Vector3 to, float t)
	{
		return from + (to - from) * (t * t * t);
	}

	public static float InverseCubicLerp(float from, float to, float t)
	{
		return from + (to - from) * (1f + (t - 1f) * (t - 1f) * (t - 1f));
	}

	public static Vector3 InverseCubicLerp(Vector3 from, Vector3 to, float t)
	{
		return from + (to - from) * (1f + (t - 1f) * (t - 1f) * (t - 1f));
	}
}
