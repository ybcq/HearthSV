using System;
using System.Collections;

public class VampiricBlood : SpellCard
{
	public VampiricBlood()
	{
		this.Name = "吸血鬼之血";
		this.Description = "Give your hero +8 Health.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		this.Player.Hero.CurrentHealth += 8;
		InterfaceManager.Instance.SpawnHealSplatOn(this.Player.Hero.Controller, 8);
		this.Player.Hero.AddHealthModifier(new Func<int, int>(this.VampiricBloodModifier));
		yield break;
	}

	public int VampiricBloodModifier(int health)
	{
		return health + 8;
	}
}
