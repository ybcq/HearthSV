using System;
using System.Collections;
using System.Linq;

public class TastyBrew : SpellCard
{
	public TastyBrew()
	{
		this.Name = "美汁源";
		this.Description = "Give a minion +1 Attack and Evasive. Add a Random Elves Card to your hand";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player).Any<Minion>();
	}

	public override IEnumerator Cast(Character target)
	{
		target.AddAttackModifier(new Func<int, int>(this.TastyBrewModifier));
		target.HasWindfury = true;
		MinionCard card = RNG.RandomItemFrom<MinionCard>((from m in CardManager.Instance.AllCards.OfType<MinionCard>()
		where m.Class == HeroClass.DemonHunter
		select m).ToList<MinionCard>());
		yield return this.Player.AddCardToHand(card);
		yield break;
	}

	public int TastyBrewModifier(int value)
	{
		return value + 1;
	}
}
