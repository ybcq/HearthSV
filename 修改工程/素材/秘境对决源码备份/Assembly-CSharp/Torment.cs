using System;
using System.Collections;

public class Torment : SpellCard
{
	public Torment()
	{
		this.Name = "崇高的教义";
		this.Description = "Decrease 1 own amulet by 2 rounds and count down, and draw 1 card.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.HasWeapon();
	}

	public override IEnumerator Cast(Character target)
	{
		this.Player.Weapon.CurrentDurability -= 2;
		if (this.Player.Weapon.CurrentDurability <= 0)
		{
			yield return this.Player.DestroyWeapon();
		}
		yield return this.Player.Draw(1, null);
		yield break;
	}
}
