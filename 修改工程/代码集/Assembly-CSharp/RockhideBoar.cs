using System;
using System.Collections;

public class RockhideBoar : MinionCard
{
	public RockhideBoar()
	{
		this.Name = "派对捣蛋鬼";
		this.Description = "Taunt. You Can't Controll it";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Beast;
		this.BaseCost = 4;
		this.BaseAttack = 3;
		this.BaseHealth = 5;
		this.CantAttack = true;
		this.HasTaunt = true;
		this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		base.InitializeMinion();
	}

	public IEnumerator OnTurnStart(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			Character randomTarget = RNG.RandomItemFrom<Character>(this.Player.Enemy.GetAllCharacters());
			InterfaceManager.Instance.SpawnDamageSplatOn(randomTarget.Controller, this.Minion.CurrentAttack);
			yield return randomTarget.Damage(null, this.Minion.CurrentAttack);
			yield return randomTarget.CheckDeath();
			randomTarget = null;
			randomTarget = null;
		}
		yield break;
	}
}
