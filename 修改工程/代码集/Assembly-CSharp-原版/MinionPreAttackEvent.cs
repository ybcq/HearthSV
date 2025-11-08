using System;

public class MinionPreAttackEvent
{
	public void SwitchTargetTo(Character other)
	{
		if (this.Status != PreStatus.Cancelled)
		{
			this.Target = other;
			this.Status = PreStatus.TargetSwitched;
		}
	}

	public void Cancel()
	{
		this.Status = PreStatus.Cancelled;
	}

	public Minion Minion;

	public Character Target;

	public int DamageAmount;

	public PreStatus Status;
}
