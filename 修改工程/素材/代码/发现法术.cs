亡语 Skeleton Commander 亡灵 1
战吼 Acherus Deathcharger 野兽 2
溅射 Bony Construct 亡灵 2
光环 Deathwhisper Necrolyte 亡灵 2
战吼 Howling Banshee 亡灵 3
激励 Nerubian Sycophant 亡灵 3
光环 Party Crashers 这张卡可以隐藏，作为中立卡 3
结束 Scourge Necromancer 亡灵 3
光环 Ambushing Geist 亡灵 4
战吼 Dark Rider of Acherus 4
亡语 Hungry Wyrmling 龙 4
亡语 Invincible 野兽 4
光环 Ashen Wyrm 龙 5
光环 Ebon Blade Vindicator 5
光环 Flesh Colossus 亡灵 6
光环 Lord Marrowgar 亡灵 6
战吼 Exhumed Lich 亡灵 7
战吼/亡语 Teron Gorefiend 7 神器 
魔法庇护 战吼 Darion Mograine 8 神器
战吼 Lich King 10 神器

this.Collectible = false; //隐藏卡牌

BloodTap 普通 亡灵大厅 <b>发现</b>一张亡灵。他们的法力值消耗都为3点。 0
尖叫女妖 HowlingBanshee
纳鲁比 NerubianSycophant
天灾死灵法师 ScourgeNecromancer
		this.Player.DiscoverCard(new HowlingBanshee(), new NerubianSycophant(), new ScourgeNecromancer());
		yield break;

DeathsAdvance 稀有 埋骨之地 <b>发现</b>一张骑士，一张龙和一张野兽。他们的法力值消耗都为4点。 1
黑暗骑士 DarkRiderofAcherus
饥饿之龙 HungryWyrmling
无敌者 Invincible
		this.Player.DiscoverCard(new DarkRiderofAcherus(), new HungryWyrmling(), new Invincible());
		yield break;

BloodStrike 紫卡 黑暗城堡 <b>发现</b>一个死亡骑士。他们都是传说随从。 2
马洛加勋爵 LordMarrowgar
泰伦 TeronGorefiend
达里安 DarionMograine
    this.Player.DiscoverCard(new LordMarrowgar(), new TeronGorefiend(), new DarionMograine());
		yield break;

ChainsofAcherus 橙卡 阿彻鲁斯之链 <b>发现</b>一个恐怖的灾祸。 3
邪恶灾祸 UnholyPresence
霜冻灾祸 FrostPresence
血液灾祸 BloodPresence
  	this.Player.DiscoverCard(new BloodPresence(), new FrostPresence(), new UnholyPresence());
		yield break;

//旧版发现
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BloodTap : SpellCard
{
	public BloodTap()
	{
		this.Name = "亡者召唤";
		this.Description = "Discover an undead.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		List<BaseCard> cards = (from c in CardManager.Instance.AllCards.OfType<MinionCard>()
		where c.MinionType == MinionType.Undead
		where c.Class == HeroClass.Neutral || c.Class == this.Player.Hero.Class
		select c).Cast<BaseCard>().ToList<BaseCard>();
		this.Player.DiscoverCard(cards);
		yield break;
	}
}

	public override IEnumerator Cast(Character target)
	{
		target.AddAttackModifier(new Func<int, int>(this.TastyBrewModifier));
		target.IsEvasive = true;
		List<BaseCard> cards = (from c in CardManager.Instance.AllCards
		where c.Class == HeroClass.DemonHunter
		select c).Cast<BaseCard>().ToList<BaseCard>();
		this.Player.DiscoverCard(cards);
		yield break;
	}
}