using System;
using System.Collections;
using UnityEngine; //动画引擎库

public class BladespireShaman : MinionCard
{
	public BladespireShaman()
	{
		this.Name = "愤怒编织者";
		this.Description = "After you play a Demon, deal 1 damage to your hero and gain +2/+2.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 1;
		this.Mechanics.OnMinionSummoned.Add(new Func<MinionSummonedEvent, IEnumerator>(this.OnMinionSummoned));
		base.InitializeMinion();
	}
	//触发函数：
	public IEnumerator OnMinionSummoned(MinionSummonedEvent evt)
	{
		if(evt.Minion.Card.MinionType == MinionType.Demon && evt.Player == this.Player){//此处添加代码
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.25f);
			this.Minion.AddAttackModifier(new Func<int, int>(this.BladespireShamanModifier));//攻击调整
			this.Minion.AddHealthModifier(new Func<int, int>(this.BladespireShamanModifier));//血量调整
			this.CurrentHealth = this.CurrentHealth + 2;//当前血量调整，重要，千万别忘了
			yield return this.Player.Hero.Damage(null, 2);
			yield return this.Player.Hero.CheckDeath();
		}
	}
	public int BladespireShamanModifier(int value)
	{
		return value + 2;
	}

	
}
