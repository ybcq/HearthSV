using System;

public class HeroPreDamageEvent
{
	public void Cancel()
	{
		this.Status = PreStatus.Cancelled;
	}

	public Hero Hero;

	public Character Attacker;

	public int DamageAmount;

	public PreStatus Status;
}
