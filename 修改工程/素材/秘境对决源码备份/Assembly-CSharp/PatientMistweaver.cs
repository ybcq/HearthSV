using System;
using System.Collections;

public class PatientMistweaver : MinionCard
{
	public PatientMistweaver()
	{
		this.Name = "迷雾织者";
		this.Description = "Meditate: Gain 1 Mana Crystal this turn only.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 3;
		this.BaseAttack = 4;
		this.BaseHealth = 3;
		this.Mechanics.Meditate.Add(new Func<Player, IEnumerator>(this.Meditate));
		base.InitializeMinion();
	}

	public IEnumerator Meditate(Player player)
	{
		this.Player.AddTurnMana(1);
		yield break;
	}
}
