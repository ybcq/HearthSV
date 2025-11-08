using System;
using System.Collections;

public class LichKing : MinionCard
{
	public LichKing()
	{
		this.Name = "巫妖王";
		this.Description = "Battlecry: Replace your hero with the Lich King.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Epic;
		this.MinionType = MinionType.General;
		this.BaseCost = 10;
		this.BaseAttack = 5;
		this.BaseHealth = 15;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character character)
	{
		yield return this.Player.ReplaceHero(this.Minion, new TheLichKing());
		yield return this.Player.EquipWeapon(new Frostmourne(), null);
		yield return this.Player.ReplaceHeroPower(new Dominate(this.Player.Hero));
		yield break;
	}
}
