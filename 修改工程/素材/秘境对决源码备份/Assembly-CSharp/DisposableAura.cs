using System;

public class DisposableAura<T> : IDisposable
{
	public DisposableAura(Aura<T> aura)
	{
		this.Aura = aura;
	}

	public void Dispose()
	{
		string name = typeof(T).Name;
		if (name != null)
		{
			if (!(name == "Minion"))
			{
				if (!(name == "BaseCard"))
				{
					if (!(name == "BaseHeroPower"))
					{
						if (name == "Hero")
						{
							AuraManager.Instance.RemoveHeroAura(this.Aura as Aura<Hero>);
						}
					}
					else
					{
						AuraManager.Instance.RemoveHeroPowerAura(this.Aura as Aura<BaseHeroPower>);
					}
				}
				else
				{
					AuraManager.Instance.RemoveCardAura(this.Aura as Aura<BaseCard>);
				}
			}
			else
			{
				AuraManager.Instance.RemoveMinionAura(this.Aura as Aura<Minion>);
			}
		}
	}

	public Aura<T> Aura;
}
