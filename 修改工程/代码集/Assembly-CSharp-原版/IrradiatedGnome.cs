using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class IrradiatedGnome : MinionCard
{
	public IrradiatedGnome()
	{
		this.Name = "Irradiated Gnome";
		this.Description = "Whenever this minion takes damage, deal 1 damage to a random enemy.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.General;
		this.BaseCost = 2;
		this.BaseAttack = 1;
		this.BaseHealth = 4;
		this.DamagedSubscription = this.Mechanics.OnDamaged.Add(new Func<MinionDamagedEvent, IEnumerator>(this.OnDamaged));
		base.InitializeMinion();
	}

	public IEnumerator OnDamaged(MinionDamagedEvent evt)
	{
		List<Character> availableCharacters = (from x in this.Player.Enemy.GetAllCharacters()
		where x.IsAlive()
		select x).ToList<Character>();
		if (availableCharacters.Count > 0)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			Character randomTarget = RNG.RandomItemFrom<Character>(availableCharacters);
			yield return randomTarget.Damage(null, 1);
			yield return randomTarget.CheckDeath();
		}
		yield break;
	}

	public IDisposable DamagedSubscription;
}
