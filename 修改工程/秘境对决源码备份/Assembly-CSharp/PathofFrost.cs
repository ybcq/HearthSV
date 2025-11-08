using System;
using System.Collections;
using System.Linq;

public class PathofFrost : SpellCard
{
	public PathofFrost()
	{
		this.Name = "冰霜之路";
		this.Description = "Draw a card for each Frozen character.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllCharacters().Any((Character c) => c.IsFrozen);
	}

	public override IEnumerator Cast(Character target)
	{
		foreach (Character character in GameManager.Instance.GetAllCharacters())
		{
			if (character.IsFrozen)
			{
				yield return this.Player.Draw(null);
			}
		}
		yield break;
	}
}
