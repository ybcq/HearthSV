using System;
using System.Collections;
using System.Linq;

public class IllFatedSquire : MinionCard
{
	public IllFatedSquire()
	{
		this.Name = "地狱魔牛勇士";
		this.Description = "Current turn player gains a random weapon.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Demon;
		this.BaseCost = 5;
		this.BaseAttack = 4;
		this.BaseHealth = 4;
		this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		WeaponCard card = RNG.RandomItemFrom<WeaponCard>((from m in CardManager.Instance.AllCards.OfType<WeaponCard>()
		where m.Class == HeroClass.DeathKnight
		select m).ToList<WeaponCard>());
		yield return this.turnPlayer.AddCardToHand(card);
		yield break;
	}

	public IEnumerator OnTurnStart(TurnEvent evt)
	{
		this.turnPlayer = evt.Player;
		yield break;
	}

	public Player turnPlayer;
}
