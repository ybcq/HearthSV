using System;
using System.Collections;

public class Soulseeker : WeaponCard
{
	public Soulseeker()
	{
		this.Name = "咏唱：神圣祈愿";
		this.Description = "Count 3. Deathrattle: Draw 2 cards";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.BaseCost = 1;
		this.BaseAttack = 0;
		this.BaseDurability = 3;
		this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeWeapon();
	}

	public IEnumerator OnTurnStart(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			int currentDurability = this.Weapon.CurrentDurability;
			this.Weapon.CurrentDurability = currentDurability - 1;
			yield return this.Weapon.CurrentDurability;
		}
		yield break;
	}

	public IEnumerator Deathrattle(Minion evt)
	{
		yield return this.Player.Draw(2, null);
		yield break;
	}
}
