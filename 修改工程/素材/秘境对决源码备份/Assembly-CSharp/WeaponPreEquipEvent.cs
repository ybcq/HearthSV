using System;

public class WeaponPreEquipEvent
{
	public void Cancel()
	{
		this.Status = PreStatus.Cancelled;
	}

	public Player Player;

	public WeaponCard Weapon;

	public PreStatus Status;
}
