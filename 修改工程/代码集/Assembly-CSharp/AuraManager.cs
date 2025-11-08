using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AuraManager : MonoBehaviour
{
	private AuraManager()
	{
	}

	public static AuraManager Instance
	{
		get
		{
			return AuraManager._instance;
		}
	}

	private void Awake()
	{
		AuraManager._instance = this;
	}

	public void AddMinionAura(Aura<Minion> aura)
	{
		if (!this.MinionAuras.Contains(aura))
		{
			this.MinionAuras.Add(aura);
		}
		GameManager.Instance.GameUpdate();
	}

	public void RemoveMinionAura(Aura<Minion> aura)
	{
		if (this.MinionAuras.Contains(aura))
		{
			this.MinionAuras.Remove(aura);
			foreach (Minion obj in GameManager.Instance.GetAllMinions())
			{
				aura.Remove(obj);
			}
		}
		GameManager.Instance.GameUpdate();
	}

	public void AddCardAura(Aura<BaseCard> aura)
	{
		if (!this.CardAuras.Contains(aura))
		{
			this.CardAuras.Add(aura);
		}
		GameManager.Instance.GameUpdate();
	}

	public void RemoveCardAura(Aura<BaseCard> aura)
	{
		if (this.CardAuras.Contains(aura))
		{
			this.CardAuras.Remove(aura);
			foreach (BaseCard obj in GameManager.Instance.GetAllCards())
			{
				aura.Remove(obj);
			}
		}
		GameManager.Instance.GameUpdate();
	}

	public void AddHeroPowerAura(Aura<BaseHeroPower> aura)
	{
		if (!this.HeroPowerAuras.Contains(aura))
		{
			this.HeroPowerAuras.Add(aura);
		}
		GameManager.Instance.GameUpdate();
	}

	public void RemoveHeroPowerAura(Aura<BaseHeroPower> aura)
	{
		if (this.HeroPowerAuras.Contains(aura))
		{
			this.HeroPowerAuras.Remove(aura);
			aura.Remove(GameManager.Instance.SelfPlayer.Hero.HeroPower);
			aura.Remove(GameManager.Instance.EnemyPlayer.Hero.HeroPower);
		}
		GameManager.Instance.GameUpdate();
	}

	public void AddHeroAura(Aura<Hero> aura)
	{
		if (!this.HeroAuras.Contains(aura))
		{
			this.HeroAuras.Add(aura);
		}
		GameManager.Instance.GameUpdate();
	}

	public void RemoveHeroAura(Aura<Hero> aura)
	{
		if (this.HeroAuras.Contains(aura))
		{
			this.HeroAuras.Remove(aura);
			aura.Remove(GameManager.Instance.SelfPlayer.Hero);
			aura.Remove(GameManager.Instance.EnemyPlayer.Hero);
		}
		GameManager.Instance.GameUpdate();
	}

	public void UpdateAuras()
	{
		this.UpdateMinionAuras();
		this.UpdateCardAuras();
		this.UpdateHeroPowerAuras();
		this.UpdateHeroAuras();
	}

	private void UpdateMinionAuras()
	{
		foreach (Aura<Minion> aura in this.MinionAuras.ToList<Aura<Minion>>())
		{
			if (aura.ExistCondition())
			{
				foreach (Minion minion in GameManager.Instance.GetAllMinions())
				{
					if (aura.ApplyCondition(minion))
					{
						aura.Apply(minion);
					}
					else
					{
						aura.Remove(minion);
					}
				}
			}
			else
			{
				this.MinionAuras.Remove(aura);
				foreach (Minion obj in GameManager.Instance.GetAllMinions())
				{
					aura.Remove(obj);
				}
			}
		}
	}

	private void UpdateCardAuras()
	{
		foreach (Aura<BaseCard> aura in this.CardAuras.ToList<Aura<BaseCard>>())
		{
			if (aura.ExistCondition())
			{
				foreach (BaseCard baseCard in GameManager.Instance.GetAllHandCards())
				{
					if (aura.ApplyCondition(baseCard))
					{
						aura.Apply(baseCard);
					}
					else
					{
						aura.Remove(baseCard);
					}
				}
			}
			else
			{
				this.CardAuras.Remove(aura);
				foreach (BaseCard obj in GameManager.Instance.GetAllHandCards())
				{
					aura.Remove(obj);
				}
			}
		}
	}

	private void UpdateHeroPowerAuras()
	{
		foreach (Aura<BaseHeroPower> aura in this.HeroPowerAuras.ToList<Aura<BaseHeroPower>>())
		{
			if (aura.ExistCondition())
			{
				if (aura.ApplyCondition(GameManager.Instance.SelfPlayer.Hero.HeroPower))
				{
					aura.Apply(GameManager.Instance.SelfPlayer.Hero.HeroPower);
				}
				else
				{
					aura.Remove(GameManager.Instance.SelfPlayer.Hero.HeroPower);
				}
				if (aura.ApplyCondition(GameManager.Instance.EnemyPlayer.Hero.HeroPower))
				{
					aura.Apply(GameManager.Instance.EnemyPlayer.Hero.HeroPower);
				}
				else
				{
					aura.Remove(GameManager.Instance.EnemyPlayer.Hero.HeroPower);
				}
			}
			else
			{
				this.HeroPowerAuras.Remove(aura);
				aura.Remove(GameManager.Instance.SelfPlayer.Hero.HeroPower);
				aura.Remove(GameManager.Instance.EnemyPlayer.Hero.HeroPower);
			}
		}
	}

	private void UpdateHeroAuras()
	{
		foreach (Aura<Hero> aura in this.HeroAuras.ToList<Aura<Hero>>())
		{
			if (aura.ExistCondition())
			{
				if (aura.ApplyCondition(GameManager.Instance.SelfPlayer.Hero))
				{
					aura.Apply(GameManager.Instance.SelfPlayer.Hero);
				}
				else
				{
					aura.Remove(GameManager.Instance.SelfPlayer.Hero);
				}
				if (aura.ApplyCondition(GameManager.Instance.EnemyPlayer.Hero))
				{
					aura.Apply(GameManager.Instance.EnemyPlayer.Hero);
				}
				else
				{
					aura.Remove(GameManager.Instance.EnemyPlayer.Hero);
				}
			}
			else
			{
				this.HeroAuras.Remove(aura);
				aura.Remove(GameManager.Instance.SelfPlayer.Hero);
				aura.Remove(GameManager.Instance.EnemyPlayer.Hero);
			}
		}
	}

	private static AuraManager _instance;

	public List<Aura<Minion>> MinionAuras = new List<Aura<Minion>>();

	public List<Aura<BaseCard>> CardAuras = new List<Aura<BaseCard>>();

	public List<Aura<BaseHeroPower>> HeroPowerAuras = new List<Aura<BaseHeroPower>>();

	public List<Aura<Hero>> HeroAuras = new List<Aura<Hero>>();
}
