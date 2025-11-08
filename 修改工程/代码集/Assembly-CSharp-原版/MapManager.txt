using System;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	private MapManager()
	{
	}

	public static MapManager Instance
	{
		get
		{
			return MapManager._instance;
		}
	}

	public GameObject CurrentTableTop
	{
		get
		{
			return this.CurrentMap.transform.Find("STW_TableTop_mesh").gameObject;
		}
	}

	private void Awake()
	{
		MapManager._instance = this;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			if (this._mapNumber == 0)
			{
				this._mapNumber = 6;
			}
			else
			{
				this._mapNumber--;
			}
			this.SwitchMap();
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			if (this._mapNumber == 6)
			{
				this._mapNumber = 0;
			}
			else
			{
				this._mapNumber++;
			}
			this.SwitchMap();
		}
	}

	private void SwitchMap()
	{
		UnityEngine.Object.Destroy(this.CurrentMap);
		this.CurrentMap = UnityEngine.Object.Instantiate<GameObject>(this.Maps[this._mapNumber]);
	}

	private static MapManager _instance;

	public List<GameObject> Maps = new List<GameObject>();

	public GameObject CurrentMap;

	private int _mapNumber;
}
