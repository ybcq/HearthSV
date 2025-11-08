using System;
using System.Collections;
using System.Linq;

public class AspiringStudent : MinionCard
{
	public AspiringStudent()
	{
		this.Name = "有志之徒";
		this.Description = "Your hero has +1 Attack on your turn. Meditatey: Add a Legendary Card to your hand.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.BaseCost = 2;
		this.BaseAttack = 1;
		this.BaseHealth = 4;
		this.HeroAura = new Aura<Hero>(new Action<Hero>(this.ApplyAura), new Action<Hero>(this.RemoveAura), new Func<Hero, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		this.Mechanics.Meditate.Add(new Func<Player, IEnumerator>(this.Meditate));
		base.InitializeMinion();
	}

	public void ApplyAura(Hero hero)
	{
		hero.AddAuraAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
	}

	public void RemoveAura(Hero hero)
	{
		hero.RemoveAuraAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
	}

	public int ApplyAttackModifier(int attack)
	{
		return attack + 1;
	}

	public bool ApplyCondition(Hero hero)
	{
		return hero == this.Minion.Player.Hero && GameManager.Instance.CurrentPlayer == this.Minion.Player;
	}

	public bool ExistCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}

	public IEnumerator Meditate(Player player)
	{
		MinionCard card = RNG.RandomItemFrom<MinionCard>((from m in CardManager.Instance.AllCards.OfType<MinionCard>()
		where m.Rarity == CardRarity.Legendary
		select m).ToList<MinionCard>());
		yield return this.Player.AddCardToHand(card);
		yield break;
	}
}
