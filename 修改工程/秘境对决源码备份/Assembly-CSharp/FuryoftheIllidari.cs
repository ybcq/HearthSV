using System;
using System.Collections;
using System.Collections.Generic;

public class FuryoftheIllidari : SpellCard
{
	public FuryoftheIllidari()
	{
		this.Name = "灼热风暴";
		this.Description = "Deal 4 damages to all enemies.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 7;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		foreach (Minion minion in GameManager.Instance.GetAllMinions())
		{
			if (minion.Card.MinionType != MinionType.Totem)
			{
				InterfaceManager.Instance.SpawnDamageSplatOn(minion.Controller, 4 + this.Player.GetSpellPower());
				yield return minion.Damage(null, 4 + this.Player.GetSpellPower());
				yield return minion.CheckDeath();
			}
			minion = null;
		}
		List<Minion>.Enumerator enumerator = default(List<Minion>.Enumerator);
		yield break;
		yield break;
	}
}
