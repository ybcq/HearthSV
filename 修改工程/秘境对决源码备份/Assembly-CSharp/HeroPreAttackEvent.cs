using System;

public class HeroPreAttackEvent
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

	public Hero Hero;

	public Character Target;

	public PreStatus Status;
}
