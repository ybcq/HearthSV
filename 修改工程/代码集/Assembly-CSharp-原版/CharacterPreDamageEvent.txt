using System;

public class CharacterPreDamageEvent
{
	public CharacterPreDamageEvent(MinionPreDamageEvent evt)
	{
		this.Character = evt.Minion;
		this.Attacker = evt.Attacker;
		this.DamageAmount = evt.DamageAmount;
		this.Status = evt.Status;
	}

	public CharacterPreDamageEvent(HeroPreDamageEvent evt)
	{
		this.Character = evt.Hero;
		this.Attacker = evt.Attacker;
		this.DamageAmount = evt.DamageAmount;
		this.Status = evt.Status;
	}

	public void Cancel()
	{
		this.Status = PreStatus.Cancelled;
	}

	public Character Character;

	public Character Attacker;

	public int DamageAmount;

	public PreStatus Status;
}
