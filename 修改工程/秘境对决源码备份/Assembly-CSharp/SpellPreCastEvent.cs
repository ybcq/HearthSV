using System;

public class SpellPreCastEvent
{
	public void SwitchTargetTo(Character other)
	{
		if (this.Target != null && this.Status != PreStatus.Cancelled)
		{
			this.Target = other;
			this.Status = PreStatus.TargetSwitched;
		}
	}

	public void Cancel()
	{
		this.Status = PreStatus.Cancelled;
	}

	public Player Player;

	public SpellCard Spell;

	public Character Target;

	public PreStatus Status;
}
