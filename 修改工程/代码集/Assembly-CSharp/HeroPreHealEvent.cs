using System;

public class HeroPreHealEvent
{
	public void Cancel()
	{
		this.Status = PreStatus.Cancelled;
	}

	public Hero Hero;

	public int HealAmount;

	public PreStatus Status;
}
