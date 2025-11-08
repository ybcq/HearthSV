using System;

[Serializable]
public class SavedCard
{
	public SavedCard(string name, int quantity)
	{
		this.Name = name;
		this.Quantity = quantity;
	}

	public BaseCard ToGameCard()
	{
		Type type = Type.GetType(this.Name);
		if (type != null)
		{
			return (BaseCard)Activator.CreateInstance(type);
		}
		return null;
	}

	public string Name;

	public int Quantity;
}
