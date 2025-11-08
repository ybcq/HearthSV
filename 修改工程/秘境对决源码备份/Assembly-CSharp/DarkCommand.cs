using System;
using System.Collections;
using System.Linq;

public class DarkCommand : SpellCard
{
	public DarkCommand()
	{
		this.Name = "黑暗命令";
		this.Description = "Force an enemy minion to attack one of your minions.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		Minion randomMinion = RNG.RandomItemFrom<Minion>((from m in this.Player.Minions
		where m.IsAlive()
		select m).ToList<Minion>());
		if (randomMinion != null)
		{
			InterfaceManager.Instance.SpawnDamageSplatOn(randomMinion.Controller, target.CurrentAttack);
			yield return randomMinion.Damage(null, target.CurrentAttack);
			yield return randomMinion.CheckDeath();
			InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, randomMinion.CurrentAttack);
			yield return target.Damage(null, randomMinion.CurrentAttack);
			yield return target.CheckDeath();
		}
		yield break;
	}

	public override bool CanCast()
	{
		return this.Player.Minions.Count > 0;
	}
}
