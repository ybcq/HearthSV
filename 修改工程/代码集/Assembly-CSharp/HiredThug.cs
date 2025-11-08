using System;
using System.Collections;

public class HiredThug : MinionCard
{
	public HiredThug()
	{
		this.Name = "溅射焰团";
		this.Description = "Deathrattle: Deal 1 damage to a random enemy.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.BaseCost = 1;
		this.BaseAttack = 2;
		this.BaseHealth = 1;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
		Character randomTarget = RNG.RandomItemFrom<Character>(this.Player.Enemy.GetAllCharacters());
		yield return randomTarget.Damage(null, 1);
		yield return randomTarget.CheckDeath();
		yield break;
	}
}
