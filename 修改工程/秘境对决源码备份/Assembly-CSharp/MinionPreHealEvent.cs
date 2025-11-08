using System;

public class MinionPreHealEvent
{
	public void Cancel()
	{
		this.Status = PreStatus.Cancelled;
	}

	public Minion Minion;

	public int HealAmount;

	public PreStatus Status;
}
