using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class SimpleAI : BaseAI
{
	public SimpleAI()
	{
		this.SpecialPlayRules = new Dictionary<string, Func<bool>>
		{
			{
				"加里维克斯的幸运币",
				() => this.Player.Hand.Any((BaseCard c) => c.CurrentCost == this.Player.AvailableMana + 1)
			},
			{
				"持斧食尸鬼",
				() => this.Player.HasWeapon()
			},
			{
				"崇高的教义",
				() => (this.Player.HasWeapon() && this.Player.Hand.Count <= 9)
			},
			{
				"亡者大军",
				() => this.Player.Minions.Count <= 3
			},
			{
				"复仇军领袖",
				() => this.Player.Minions.Any((Minion m) => m.HasTaunt)
			},
			{
				"灭亡之触",
				() => this.Player.Enemy.Hero.CurrentHealth < 10 
			},
			{
				"无头骑士",
				() => this.Player.Enemy.Hand.Count <= 8
			},
			{
				"亚夏巫觐",
				() => this.Player.Enemy.Hand.Count <= 9
			},
			{
				"龙之启示",
				() => this.Player.TurnMana <= 8 || this.Player.Hand.Count <= 9 
			},
			{
				"鼓舞",
				() => this.Player.Hand.Any((BaseCard c) => c is MinionCard && c.CurrentCost == this.Player.AvailableMana + 1)
			},
			{
				"崭新的命运",
				() => this.Player.Deck.Count((BaseCard c) => c is MinionCard && c.Description.Contains("Necromancer")) > 5
			},
			{
				"翡翠玉龙",
				() => this.Player.Hero.CurrentHealth < this.Player.Enemy.Hero.CurrentHealth
			},
			{
				"达里安·莫格莱恩",
				() => !this.Player.HasWeapon()
			},
			{
				"泰伦·戈尔",
				() => this.Player.Enemy.Minions.Count >= 2
			},
			{
				"纺火花",
				() => this.Player.Enemy.Minions.TargeteablesBySpellOf(this.Player).Count > 0
			},
			{
				"魔法茶",
				() => this.Player.Hand.Count((BaseCard c) => c is SpellCard && c.CurrentCost > 0) > 2
			},
			{
				"纳兹夏尔女士",
				() => this.Player.Minions.Count((Minion m) => m.IsDamaged()) > 2
			},
			{
				"咏唱：神域守护者",
				() => this.Player.Enemy.Minions.Count >= 2
			},
			{
				"虎之姿态",
				() => this.Player.Hero.CurrentAttack > 0
			},
			{
				"火土风暴",
				() => this.Player.Minions.Count <= 4
			},
			{
				"吸血鬼之血",
				() => this.Player.Hero.CurrentHealth <= 15
			},
						{
				"旋转鹤踢",
				() => this.Player.Enemy.Minions.Count >= 4
			},
			{
				"邪恶灾祸",
				() => this.Player.Hand.Any((BaseCard c) => c is MinionCard && c.Mechanics.Deathrattle != null)
			},
			{
				"邪恶之力",
				() => this.Player.Minions.Count >= 1
				//() => this.Player.Hand.Any((BaseCard c) => c is MinionCard && c.CurrentCost == this.Player.AvailableMana + 1)
			},
			{
				"邪恶狂热",
				() => this.Player.Minions.Count >= 1
				//() => this.Player.Hand.Count((BaseCard c) => c is SpellCard && c.CurrentCost > 0) > 2
			},
			{
				"虹色光辉",
				() => this.Player.Minions.TargeteablesBySpellOf(this.Player).Count > 0
			},
			{
				"暗黑供奉",
				() => this.Player.Hero.CurrentHealth <= 15
			},
			{
				"咏唱：神圣祈愿",
				() => this.Player.Hand.Count <= 7
			},
			{
				"妖精的恶作剧",
				() => this.Player.Minions.TargeteablesBySpellOf(this.Player).Any((Minion m) => m.IsDamaged()) && this.Player.Enemy.Minions.Count >= 1
			},
			{
				"炼金术的知识",
				() => this.Player.Enemy.Minions.Count((Minion m) => m.CurrentHealth <= 3) > 3
			},
			{
				"炽燃雕像",
				() => this.Player.Enemy.Minions.Count((Minion m) => m.CurrentHealth <= 2) > 2
			},
			{
				"军师的妙计",
				() => 10 - this.Player.Hand.Count > this.Player.Minions.Count
			},
			{
				"不洁重生",
				() => this.Player.Minions.TargeteablesBySpellOf(this.Player).Any((Minion m) => m.IsDamaged())
			},
			{
				"回归根源",
				() => this.Player.Minions.Count + 2 < this.Player.Enemy.Minions.Count
			},
			{
				"破坏神的气息",
				() => this.Player.Hero.CurrentHealth <= 10
			},
			{
				"暗影哀悼",
				() => !this.Player.HasWeapon()
			},
			{
				"天灾打击",
				() => this.Player.Minions.Any((Minion m) => m.Card.Name == "砰砰箱") && this.Player.Minions.Any((Minion m) => m.Card.Name == "食尸鬼") || this.Player.Enemy.Hero.CurrentHealth < 10
			},
			{
				"姆诺兹多",
				() => this.Player.Hand.Count <= 8
			},
			{
				"周末狂欢",
				() => this.Player.Enemy.Minions.Count((Minion m) => m.Card.MinionType == MinionType.Undead) < this.Player.Minions.Count((Minion m) => m.Card.MinionType == MinionType.Undead)
			},
			{
				"灰烬破灭狂徒",
				() => this.Player.Enemy.HasWeapon()
			},
			{
				"破法符文",
				() => this.Player.HasWeapon()
			},
			{
				"剃刀之符文",
				() => this.Player.HasWeapon()
			},
			{
				"巫妖符文",
				() => this.Player.HasWeapon()
			},
			{
				"符文熔炉",
				() => this.Player.HasWeapon()
			},
			{
				"末日机器人",
				() => this.Player.Minions.Count + 3 < this.Player.Enemy.Minions.Count
			},
			{
				"末日狂欢",
				() => this.Player.Minions.Count + 3 < this.Player.Enemy.Minions.Count
			},
			{
				"萌芽花寄生",
				() => this.Player.Hand.Count((BaseCard c) => c.CurrentCost >= 7) >= 2
			},
			{
				"墓生食尸鬼",
				() => this.Player.DeadMinions.Count >= 4
			},
			{
				"冰霜之路",
				() => GameManager.Instance.GetAllMinions().Count((Minion m) => m.IsFrozen) > 0
			},
			{
				"巫妖王",
				() => this.Player.Hero.CurrentHealth <= 10
			},
			{
				"拉佐格尔",
				() => this.Player.Minions.Count >= 3
			},
			{
				"风神",
				() => this.Player.Minions.Count >= 2
			},
			{
				"黑暗精灵·芙蕾",
				() => this.Player.Hand.Count <= 8
			},
			{
				"凯旋的骑士",
				() => this.Player.Minions.Count <= 5
			},
			{
				"疾风怒涛",
				() => this.Player.Minions.Count >= 5
			},
			{
				"暗夜中的兽群",
				() => this.Player.Minions.Count <= 5
			},
			{
				"冰封坚韧",
				() => this.Player.Hero.CurrentHealth <= 10
			},
			{
				"尖叫爆炸",
				() => this.Player.Enemy.Minions.Count((Minion m) => m.IsFrozen) > 2
			},
			{
				"尖叫女妖",
				() => this.Player.Minions.Count >= 1
			},
			{
				"湮灭",
				() => this.Player.Hero.CurrentHealth >= 10
			},
			{
				"亡灵法师",
				() => this.Player.Hand.Count((BaseCard c) => c is SpellCard && c.CurrentCost > 3) > 2
			},
			{
				"狂野的拉佐格尔",
				() => this.Player.Minions.Count((Minion m) => m.Card.MinionType == MinionType.Dragon) > 2
			},
			{
				"灼热风暴",
				() => this.Player.Minions.Count + 3 < this.Player.Enemy.Minions.Count
			},
			{
				"霜之哀伤",
				() => !this.Player.HasWeapon()
			},
			{
				"森林的意志",
				() => this.Player.Hand.Count >= 7
			},
			{
				"骷髅法师",
				() => this.Player.Hand.Count <= 7
			},
			{
				"利爪的一击",
				() => this.Player.Hero.CurrentHealth >= 3
			},
			{
				"血之契约",
				() => this.Player.Hero.CurrentHealth >= 3 && this.Player.Hand.Count <= 7
			},
			{
				"龙龟",
				() => this.Player.Minions.Count >= 1
			},
			{
				"死亡祝福",
				() => this.Player.Minions.Count <= 5
			},
			{
				"龙之传令",
				() => this.Player.Hand.Count <= 8
			},
			{
				"死亡祝福",
				() => this.Player.Minions.Count <= 6
			},
			{
				"融合死骑",
				() => this.Player.Minions.Count((Minion m) => m.Card.Name == "食尸鬼") >= 2
			},
			{
				"死亡契约",
				() => this.Player.Hero.CurrentHealth <= 10
			},
			{
				"黑暗模拟",
				() => this.Player.Hand.Count <= 8
			},
			{
				"人偶师的线",
				() => this.Player.Hand.Count <= 6 || this.Player.Enemy.Minions.Count((Minion m) => m.CurrentHealth <= 1 ) >= 3
			},
			{
				"好运波葛",
				() => this.Player.HasWeapon()
			},
			{
				"忒弥斯的审判",
				() => this.Player.Minions.Count + 3 < this.Player.Enemy.Minions.Count
			},
			{
				"鬼灵骑兵",
				() => this.Player.Hero.CurrentHealth > 30
			},
			{
				"灰烬使者",
				() => !this.Player.HasWeapon()
			},
			{
				"疯狂的刽子手",
				() => this.Player.Hero.CurrentHealth >= 3
			},
			{
				"腐败飓风",
				() => this.Player.Hand.Count <= 6 || this.Player.Enemy.Minions.Count((Minion m) => m.CurrentHealth <= 3 ) >= 3
			},
			{
				"侠盗的仁义",
				() => this.Player.Hand.Count >= 5
			},
			{
				"行船商人",
				() => this.Player.Hand.Count <= 8
			},
			{
				"新星魔术师·萨米",
				() => this.Player.Hand.Count <= 8
			},
			{
				"冬之女王的即兴艺术",
				() => this.Player.Minions.Count + 2 < this.Player.Enemy.Minions.Count
			},
			{
				"风语冥想师",
				() => this.Player.Hand.Count <= 8
			},
			{
				"骨盾",
				() => this.Player.Hero.CurrentHealth <= 10
			},
			{
				"血虫雨",
				() => this.Player.Hero.CurrentHealth <= 15 && this.Player.Minions.Count <= 5
			},
			{
				"血丝",
				() => this.Player.Hero.CurrentHealth <= 5 && this.Player.Minions.Count((Minion m) => m.Card.Name == "食尸鬼") >= 2
			},
			{
				"鲜血打击",
				() => this.Player.Minions.Count((Minion m) => m.Card.Name == "食尸鬼") >= 3
			},
			{
				"高阶牧师",
				() => this.Player.HasWeapon()
			},
			{
				"龙人施法者",
				() => this.Player.Minions.Count <= 4
			},
			{
				"启示录之剑",
				() => !this.Player.HasWeapon()
			},
			{
				"反魔法护盾",
				() => this.Player.Minions.Count >= 3
			},
			{
				"镜像亡者",
				() => this.Player.Enemy.DeadMinions.Count >= 1
			},
			{
				"反魔法空间",
				() => this.Player.Enemy.Hand.Count((BaseCard c) => c is SpellCard) > 2
			},
			{
				"睿智指挥官",
				() => this.Player.Minions.Count >= 2
			},
			{
				"阿彻鲁斯传送门",
				() => this.Player.Minions.Count <= 6
			},
		};
		this.SpecialTargetRules = new Dictionary<string, Func<List<Character>, Character>>
		{
			{
				"不灭的怨念",
				new Func<List<Character>, Character>(this.AllWillServeTargetRule)
			},
			{
				"众生臣服",
				new Func<List<Character>, Character>(this.AllWillServeTargetRule)
			},
			{
				"利爪的一击",
				new Func<List<Character>, Character>(this.AllWillServeTargetRule)
			},
			{
				"森林的反扑",
				new Func<List<Character>, Character>(this.AllWillServeTargetRule)
			},
			{
				"魔导飞弹",
				new Func<List<Character>, Character>(this.AllWillServeTargetRule)
			},
			{
				"恶魔冲击波",
				new Func<List<Character>, Character>(this.AllWillServeTargetRule)
			},
			{
				"炽热吐息",
				new Func<List<Character>, Character>(this.AllWillServeTargetRule)
			},
			{
				"漆黑法典",
				new Func<List<Character>, Character>(this.AllWillServeTargetRule)
			},
			{
				"初级诅咒",
				new Func<List<Character>, Character>(this.AllWillServeTargetRule)
			},
			{
				"召唤石像鬼",
				new Func<List<Character>, Character>(this.AllWillServeTargetRule)
			},
			{
				"鲜血的吻唇",
				new Func<List<Character>, Character>(this.AllWillServeTargetRule)
			},
			{
				"神圣打击",
				new Func<List<Character>, Character>(this.AllWillServeTargetRule)
			},
			{
				"灵魂狩猎",
				new Func<List<Character>, Character>(this.AllWillServeTargetRule)
			},
			{
				"寒冰之链",
				new Func<List<Character>, Character>(this.DeathGripTargetRule)
			},
			{
				"寒冰吐息",
				new Func<List<Character>, Character>(this.DeathGripTargetRule)
			},
			{
				"破邪圣光",
				new Func<List<Character>, Character>(this.DeathGripTargetRule)
			},
			{
				"冰原噬魂怪",
				new Func<List<Character>, Character>(this.DeathGripTargetRule)
			},
			{
				"血红净化",
				new Func<List<Character>, Character>(this.DeathGripTargetRule)
			},
			{
				"狂野追击",
				new Func<List<Character>, Character>(this.DeathGripTargetRule)
			},
			{
				"脉轮守护者",
				new Func<List<Character>, Character>(this.DeathGripTargetRule)
			},
			{
				"黑暗命令",
				new Func<List<Character>, Character>(this.DeathGripTargetRule)
			},
			{
				"凋零埋葬",
				new Func<List<Character>, Character>(this.DeathGripTargetRule)
			},
			{
				"女妖",
				new Func<List<Character>, Character>(this.DeathGripTargetRule)
			},

			{
				"战斗姿态",
				new Func<List<Character>, Character>(this.BrawlingStanceTargetRule)
			},
			{
				"邪恶之力",
				new Func<List<Character>, Character>(this.BrawlingStanceTargetRule)
			},
			{
				"奇波",
				new Func<List<Character>, Character>(this.ChiWaveTargetRule)
			},
			{
				"掠夺",
				new Func<List<Character>, Character>(this.ChiWaveTargetRule)
			},
			{
				"被发现的巫妖",
				new Func<List<Character>, Character>(this.ChiWaveTargetRule)
			},
			{
				"龙化秘术",
				new Func<List<Character>, Character>(this.ChiWaveTargetRule)
			},
			{
				"暗影哀悼",
				new Func<List<Character>, Character>(this.ChiWaveTargetRule)
			},
			{
				"虹色光辉",
				new Func<List<Character>, Character>(this.ChiWaveTargetRule)
			},
			{
				"暗黑供奉",
				new Func<List<Character>, Character>(this.ChiWaveTargetRule)
			},
			{
				"死亡契约",
				new Func<List<Character>, Character>(this.ChiWaveTargetRule)
			},
			{
				"不洁重生",
				new Func<List<Character>, Character>(this.ChiWaveTargetRule)
			},
			{
				"妖精的恶作剧",
				new Func<List<Character>, Character>(this.ChiWaveTargetRule)
			},
			{
				"伟大的女修士",
				new Func<List<Character>, Character>(this.ChiWaveTargetRule)
			},
			{
				"纺火花",
				new Func<List<Character>, Character>(this.SpinningFireBlossomTargetRule)
			},
			{
				"致命诅咒",
				new Func<List<Character>, Character>(this.SpinningFireBlossomTargetRule)
			},
			{
				"精灵的诅咒",
				new Func<List<Character>, Character>(this.SpinningFireBlossomTargetRule)
			},
			{
				"暗裔破袭者",
				new Func<List<Character>, Character>(this.SpinningFireBlossomTargetRule)
			},
			{
				"白银的箭击",
				new Func<List<Character>, Character>(this.SpinningFireBlossomTargetRule)
			},
			{
				"美汁源",
				new Func<List<Character>, Character>(this.TastyBrewTargetRule)
			},
			{
				"阿彻鲁斯传送门",
				new Func<List<Character>, Character>(this.TastyBrewTargetRule)
			},
			{
				"血液沸腾",
				new Func<List<Character>, Character>(this.TastyBrewTargetRule)
			},
			{
				"血腥瘟疫",
				new Func<List<Character>, Character>(this.TastyBrewTargetRule)
			},
			{
				"寒冷拥抱",
				new Func<List<Character>, Character>(this.TastyBrewTargetRule)
			},
			{
				"死亡捷径",
				new Func<List<Character>, Character>(this.TastyBrewTargetRule)
			},
			{
				"守护之力",
				new Func<List<Character>, Character>(this.TastyBrewTargetRule)
			},
			{
				"利刃附魔",
				new Func<List<Character>, Character>(this.TastyBrewTargetRule)
			},
			{
				"尖叫女妖",
				new Func<List<Character>, Character>(this.TastyBrewTargetRule)
			},
			{
				"冰霜热",
				new Func<List<Character>, Character>(this.TastyBrewTargetRule)
			},
			{
				"邪恶狂热",
				new Func<List<Character>, Character>(this.TastyBrewTargetRule)
			},
			{
				"巫妖的诱惑",
				new Func<List<Character>, Character>(this.TastyBrewTargetRule)
			},
			{
				"突然宣判",
				new Func<List<Character>, Character>(this.TastyBrewTargetRule)
			},
			{
				"深渊巨兽",
				new Func<List<Character>, Character>(this.EnemyLessThan5Rule)
			},
			{
				"中级诅咒",
				new Func<List<Character>, Character>(this.EnemyLessThan5Rule)
			},
			{
				"破坏神的气息",
				new Func<List<Character>, Character>(this.EnemyLessThan5Rule)
			},
			{
				"天灾打击",
				new Func<List<Character>, Character>(this.EnemyLessThan5Rule)
			},
			{
				"符文打击",
				new Func<List<Character>, Character>(this.EnemyLessThan5Rule)
			},
			{
				"坏死",
				new Func<List<Character>, Character>(this.EnemyLessThan5Rule)
			},
			{
				"凋零缠绕",
				new Func<List<Character>, Character>(this.EnemyLessThan5Rule)
			},
			{
				"恐惧龙兽",
				new Func<List<Character>, Character>(this.EnemyLessThan5Rule)
			},
			{
				"疾风怒涛",
				new Func<List<Character>, Character>(this.EnemyLessThan5Rule)
			},
			{
				"暗夜中的兽群",
				new Func<List<Character>, Character>(this.EnemyLessThan5Rule)
			},
			{
				"湮灭",
				new Func<List<Character>, Character>(this.EnemyLessThan5Rule)
			},
			{
				"冰霜打击",
				new Func<List<Character>, Character>(this.EnemyLessThan5Rule)
			},
			{
				"龙之怒",
				new Func<List<Character>, Character>(this.EnemyLessThan5Rule)
			},
			{
				"沸腾之血",
				new Func<List<Character>, Character>(this.EnemyLessThan5Rule)
			}
			{
				"侠盗的仁义",
				new Func<List<Character>, Character>(this.EnemyLessThan5Rule)
			}
		};
		this.SpecialComboConditions = new Dictionary<string, Func<bool>>
		{
			{
				"尸爆",
				new Func<bool>(this.CorpseExplosionComboCondition)
			}
		};
		this.SpecialCombos = new Dictionary<string, Func<IEnumerator>>
		{
			{
				"尸爆",
				new Func<IEnumerator>(this.CorpseExplosionCombo)
			}
		};
		this.SpecialPositionRules = new Dictionary<string, PositionPriority>
		{
			{
				"泰伦·戈尔",
				PositionPriority.Middle
			},
			{
				"被冰封的勇士",
				PositionPriority.Middle
			}
		};
		this.SpecialOrderRules = new Dictionary<string, OrderPriority>
		{
			{
				"灼热风暴",
				OrderPriority.First
			},
			{
				"血之灾祸",
				OrderPriority.First
			},
			{
				"忒弥斯的审判",
				OrderPriority.First
			},
			{
				"黑暗模拟",
				OrderPriority.First
			},
			{
				"血之契约",
				OrderPriority.First
			},
			{
				"霜冻灾祸",
				OrderPriority.First
			},
			{
				"森林的意志",
				OrderPriority.First
			},
			{
				"冰霜之路",
				OrderPriority.First
			},
			{
				"死亡之握",
				OrderPriority.First
			},	
			{
				"赤焰南瓜",
				OrderPriority.First
			},			
			{
				"末日机器人",
				OrderPriority.First
			},
			{
				"暗黑供奉",
				OrderPriority.First
			},
			
			{
				"致命诅咒",
				OrderPriority.First
			},
			{
				"妖精的恶作剧",
				OrderPriority.First
			},
			{
				"邪恶灾祸",
				OrderPriority.First
			},
			{
				"窒息",
				OrderPriority.First
			},
			{
				"加里维克斯的幸运币",
				OrderPriority.First
			},
			{
				"龙之启示",
				OrderPriority.First
			},
			{
				"尸爆",
				OrderPriority.First
			},
			{
				"凋零埋葬",
				OrderPriority.First
			},
			{
				"死亡凋零",
				OrderPriority.First
			},
			{
				"吸血鬼之血",
				OrderPriority.First
			},
			{
				"鼓舞",
				OrderPriority.First
			},
			{
				"暗黑供奉",
				OrderPriority.First
			},
			{
				"魔法茶",
				OrderPriority.First
			},
			{
				"深渊巨兽",
				OrderPriority.First
			},
			{
				"憎恶",
				OrderPriority.BeforeAttacking
			},
			{
				"伏击者",
				OrderPriority.BeforeAttacking
			},
			{
				"血腥瘟疫",
				OrderPriority.BeforeAttacking
			},
			{
				"反魔法护盾",
				OrderPriority.BeforeAttacking
			},
			{
				"血液沸腾",
				OrderPriority.BeforeAttacking
			},
			{
				"人偶师的线",
				OrderPriority.BeforeAttacking
			},
			{
				"侠盗的仁义",
				OrderPriority.BeforeAttacking
			},
			{
				"死亡捷径",
				OrderPriority.BeforeAttacking
			},
			{
				"恐惧龙兽",
				OrderPriority.BeforeAttacking
			},
			{
				"守护之力",
				OrderPriority.BeforeAttacking
			},
			{
				"火熊猫",
				OrderPriority.BeforeAttacking
			},
			{
				"腐败飓风",
				OrderPriority.BeforeAttacking
			},
			{
				"凋零缠绕",
				OrderPriority.BeforeAttacking
			},
			{
				"掠夺",
				OrderPriority.BeforeAttacking
			},
			{
				"阿彻鲁斯传送门",
				OrderPriority.BeforeAttacking
			},
			{
				"利刃附魔",
				OrderPriority.BeforeAttacking
			},
			{
				"炽热吐息",
				OrderPriority.BeforeAttacking
			},
			{
				"恶魔冲击波",
				OrderPriority.BeforeAttacking
			},
			{
				"腐肉巨像",
				OrderPriority.BeforeAttacking
			},
			{
				"乌木之刃辩护者",
				OrderPriority.BeforeAttacking
			},
			{
				"魔导飞弹",
				OrderPriority.BeforeAttacking
			},
			{
				"冰霜打击",
				OrderPriority.BeforeAttacking
			},
			{
				"精灵驱逐者",
				OrderPriority.BeforeAttacking
			},
			{
				"疾风怒涛",
				OrderPriority.BeforeAttacking
			},
			{
				"冰霜热",
				OrderPriority.BeforeAttacking
			},
			{
				"鲜血的吻唇",
				OrderPriority.BeforeAttacking
			},

			{
				"灵魂狩猎",
				OrderPriority.BeforeAttacking
			},
			{
				"破坏神的气息",
				OrderPriority.BeforeAttacking
			},
			{
				"龙之怒",
				OrderPriority.BeforeAttacking
			},
			{
				"绞肉车",
				OrderPriority.BeforeAttacking
			},
			{
				"利爪的一击",
				OrderPriority.BeforeAttacking
			},
			{
				"湮灭",
				OrderPriority.BeforeAttacking
			},
			{
				"女妖",
				OrderPriority.BeforeAttacking
			},

			{
				"僵尸咏唱家",
				OrderPriority.BeforeAttacking
			},
			{
				"神圣打击",
				OrderPriority.BeforeAttacking
			},	
			{
				"菲利克斯·掠日者",
				OrderPriority.BeforeAttacking
			},
			{
				"符文打击",
				OrderPriority.BeforeAttacking
			},			
			{
				"天灾打击",
				OrderPriority.BeforeAttacking
			},
			{
				"破邪圣光",
				OrderPriority.BeforeAttacking
			},
			{
				"邪恶之力",
				OrderPriority.BeforeAttacking
			},
			{
				"突然宣判",
				OrderPriority.BeforeAttacking
			},
			{
				"暗夜中的兽群",
				OrderPriority.BeforeAttacking
			},
			{
				"帕奇维克",
				OrderPriority.BeforeAttacking
			},
			{
				"精灵的诅咒",
				OrderPriority.BeforeAttacking
			},
			{
				"睿智指挥官",
				OrderPriority.BeforeAttacking
			},
			{
				"众生臣服",
				OrderPriority.BeforeAttacking
			},
			{
				"狂野追击",
				OrderPriority.BeforeAttacking
			},
			{
				"瘟疫使者诺斯",
				OrderPriority.BeforeAttacking
			},
			{
				"炼金术的知识",
				OrderPriority.BeforeAttacking
			},
			{
				"邪恶狂热",
				OrderPriority.BeforeAttacking
			},
			{
				"持斧食尸鬼",
				OrderPriority.BeforeAttacking
			},
			{
				"崇高的教义",
				OrderPriority.BeforeAttacking
			},
			{
				"戳",
				OrderPriority.BeforeAttacking
			},
			{
				"漆黑法典",
				OrderPriority.BeforeAttacking
			},
			{
				"战斗姿态",
				OrderPriority.BeforeAttacking
			},
			{
				"有志之徒",
				OrderPriority.BeforeAttacking
			},
			{
				"亡灵法师",
				OrderPriority.BeforeAttacking
			},
			{
				"伊莉雅，冬拥龙语姬",
				OrderPriority.BeforeAttacking
			},
			{
				"不灭的怨念",
				OrderPriority.BeforeAttacking
			},
			{
				"美汁源",
				OrderPriority.BeforeAttacking
			},
			{
				"丽丽的水龙",
				OrderPriority.BeforeAttacking
			},
			{
				"旋转鹤踢",
				OrderPriority.BeforeAttacking
			},
			{
				"脉轮守护者",
				OrderPriority.BeforeAttacking
			},
			{
				"白虎雪恩",
				OrderPriority.BeforeAttacking
			},
			{
				"血红净化",
				OrderPriority.BeforeAttacking
			},
			{
				"怨念的魔女",
				OrderPriority.BeforeAttacking
			},
			{
				"白银的箭击",
				OrderPriority.BeforeAttacking
			},
			{
				"末日摆锤",
				OrderPriority.BeforeAttacking
			},
			{
				"黑暗命令",
				OrderPriority.BeforeAttacking
			},
			{
				"迷魂之刃",
				OrderPriority.AfterAttacking
			},
			{
				"邪恶之刃",
				OrderPriority.AfterAttacking
			},
			{
				"坏死",
				OrderPriority.AfterAttacking
			},
			{
				"达里安·莫格莱恩",
				OrderPriority.AfterAttacking
			},
			{
				"泰伦·戈尔",
				OrderPriority.AfterAttacking
			},
			{
				"回归根源",
				OrderPriority.AfterAttacking
			},
			{
				"塔兰竹",
				OrderPriority.AfterAttacking
			},
			{
				"萌芽花寄生",
				OrderPriority.AfterAttacking
			},
			{
				"嗜血伏击者",
				OrderPriority.BeforeHeroPower
			},
			{
				"骨盾",
				OrderPriority.BeforeHeroPower
			},
			{
				"冬之女王的即兴艺术",
				OrderPriority.BeforeHeroPower
			},
			{
				"纳鲁比·西科芬",
				OrderPriority.BeforeHeroPower
			},
			{
				"火土风暴",
				OrderPriority.BeforeHeroPower
			},
			{
				"灰烬使者",
				OrderPriority.AfterHeroPower
			},
			{
				"暗影哀悼",
				OrderPriority.AfterHeroPower
			},			
			{
				"破法符文",
				OrderPriority.AfterHeroPower
			},
			{
				"剃刀之符文",
				OrderPriority.AfterHeroPower
			},
			{
				"巫妖符文",
				OrderPriority.AfterHeroPower
			},
			{
				"巨变者·周卓",
				OrderPriority.AfterHeroPower
			},
			{
				"好运波葛",
				OrderPriority.AfterHeroPower
			},
			{
				"血丝",
				OrderPriority.AfterHeroPower
			},
			{
				"反魔法空间",
				OrderPriority.Last
			},
			{
				"纳兹夏尔女士",
				OrderPriority.Last
			},
			{
				"融合死骑",
				OrderPriority.Last
			},
			{
				"崭新的命运",
				OrderPriority.Last
			},
			{
				"不洁重生",
				OrderPriority.Last
			},
			{
				"亡者大军",
				OrderPriority.Last
			},
			{
				"重生骨墙",
				OrderPriority.Last
			},
			{
				"周末狂欢",
				OrderPriority.Last
			},
			{
				"冰封坚韧",
				OrderPriority.Last
			},
			{
				"死亡祝福",
				OrderPriority.Last
			},
			{
				"寒冷拥抱",
				OrderPriority.Last
			},
			{
				"龙化秘术",
				OrderPriority.Last
			},
			{
				"寒冰之链",
				OrderPriority.Last
			},
			{
				"寒冰吐息",
				OrderPriority.Last
			},
			{
				"恶瘴冥灵",
				OrderPriority.Last
			}

		};
	}
}
