using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DeathsAdvance : SpellCard
{
	public DeathsAdvance()
	{
		this.Name = "融合死骑";
		this.Description = "Kill all your Ghouls. Sunmon a DeathKnight.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		int destroyedFragments = 0;
		foreach (Minion minion in (from m in this.Player.Minions
		where m.Card is ChargeTurnGhoul
		select m).ToList<Minion>())
		{
			int num = destroyedFragments;
			destroyedFragments = num + 1;
			yield return minion.Destroy();
		}
		List<Minion>.Enumerator enumerator = default(List<Minion>.Enumerator);
		List<Character> allCharacters = this.Player.GetAllCharacters();
		foreach (Character character in allCharacters)
		{
			yield return character.CheckDeath();
		}
		List<Character>.Enumerator enumerator2 = default(List<Character>.Enumerator);
		DarkRiderofAcherus minionCard = new DarkRiderofAcherus
		{
			BaseCost = destroyedFragments,
			BaseAttack = destroyedFragments,
			BaseHealth = destroyedFragments,
			CurrentHealth = destroyedFragments
		};
		yield return this.Player.SummonMinion(minionCard);
		yield break;
		yield break;
	}

	public override bool CanCast()
	{
		return this.Player.Minions.Any((Minion m) => m.Card is ChargeTurnGhoul) && this.Player.Minions.Count < 7;
	}
}
