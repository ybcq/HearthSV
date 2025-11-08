using System;
using System.Collections;

public class AshenWyrm : MinionCard
{
	public AshenWyrm()
	{
		this.Name = "灰烬之龙";
		this.Description = "Frozen enemies take +2 damage.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 5;
		this.BaseAttack = 4;
		this.BaseHealth = 6;
		this.Mechanics.OnCharacterPreDamage.Add(new Func<CharacterPreDamageEvent, IEnumerator>(this.OnCharacterPreDamage));
		base.InitializeMinion();
	}

	public IEnumerator OnCharacterPreDamage(CharacterPreDamageEvent evt)
	{
		if (evt.Character.IsEnemyOf(this.Player.Hero) && evt.Character.IsFrozen)
		{
			evt.DamageAmount += 2;
		}
		yield break;
	}
}
