using System;
using System.Collections;

public class PillarofFrost : SpellCard
{
	public PillarofFrost()
	{
		this.Name = "Pillar of Frost";
		this.Description = "Give your hero +3 Attack this turn and 4 Armor.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		this.Player.Hero.AddAttackModifier(new Func<int, int>(this.PillarofFrostModifier));
		this.Player.Hero.CurrentArmor += 4;
		this.TurnEndSubscription = EventManager.Instance.TurnEndHandler.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		yield break;
	}

	public int PillarofFrostModifier(int attack)
	{
		return attack + 3;
	}

	public IEnumerator OnTurnEnd(TurnEvent turnEvent)
	{
		this.Player.Hero.RemoveAttackModifier(new Func<int, int>(this.PillarofFrostModifier));
		this.TurnEndSubscription.Dispose();
		yield break;
	}

	public IDisposable TurnEndSubscription;
}
