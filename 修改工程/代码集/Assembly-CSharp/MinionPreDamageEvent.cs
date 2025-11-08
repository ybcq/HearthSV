using System;

public class MinionPreDamageEvent
{
	public void Cancel()
	{
		this.Status = PreStatus.Cancelled;
	}

	public Minion Minion;

	public Character Attacker;

	public int DamageAmount;

	public PreStatus Status;
}
