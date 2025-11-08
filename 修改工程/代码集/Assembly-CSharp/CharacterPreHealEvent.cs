using System;

public class CharacterPreHealEvent
{
	public CharacterPreHealEvent(MinionPreHealEvent evt)
	{
		this.Character = evt.Minion;
		this.HealAmount = evt.HealAmount;
		this.Status = evt.Status;
	}

	public CharacterPreHealEvent(HeroPreHealEvent evt)
	{
		this.Character = evt.Hero;
		this.HealAmount = evt.HealAmount;
		this.Status = evt.Status;
	}

	public void Cancel()
	{
		this.Status = PreStatus.Cancelled;
	}

	public Character Character;

	public int HealAmount;

	public PreStatus Status;
}
