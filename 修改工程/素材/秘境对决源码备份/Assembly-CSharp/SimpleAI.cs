using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleAI : BaseAI
{
	public SimpleAI()
	{
		Dictionary<string, Func<bool>> dictionary = new Dictionary<string, Func<bool>>();
		dictionary.Add("加里维克斯的幸运币", () => this.Player.Hand.Any((BaseCard c) => c.CurrentCost == this.Player.AvailableMana + 1));
		dictionary.Add("持斧食尸鬼", () => this.Player.HasWeapon());
		dictionary.Add("崇高的教义", () => this.Player.Minions.Count <= 3);
		dictionary.Add("亡者大军", () => this.Player.Minions.Count <= 4);
		dictionary.Add("复仇军领袖", () => !this.Player.HasWeapon());
		dictionary.Add("灭亡之触", () => this.Player.Hand.Any((BaseCard c) => c is MinionCard && c.CurrentCost == this.Player.AvailableMana + 1));
		dictionary.Add("无头骑士", () => this.Player.Hand.Count((BaseCard c) => c is SpellCard && c.CurrentCost > 0) > 2);
		dictionary.Add("亚夏巫觐", () => this.Player.Enemy.Minions.TargeteablesBySpellOf(this.Player).Count > 0);
		dictionary.Add("龙之启示", () => this.Player.Enemy.Minions.Count >= 2);
		dictionary.Add("鼓舞", () => this.Player.Hero.CurrentHealth < this.Player.Enemy.Hero.CurrentHealth);
		dictionary.Add("崭新的命运", () => this.Player.Minions.Count <= 4);
		dictionary.Add("翡翠玉龙", () => this.Player.Hero.CurrentAttack > 0);
		dictionary.Add("达里安·莫格莱恩", () => this.Player.Enemy.Minions.Count >= 2);
		dictionary.Add("泰伦·戈尔", () => this.Player.Hero.CurrentHealth < this.Player.Enemy.Hero.CurrentHealth);
		dictionary.Add("纺火花", () => this.Player.Minions.Count <= 4);
		dictionary.Add("魔法茶", () => this.Player.Hero.CurrentAttack > 0);
		dictionary.Add("纳兹夏尔女士", () => this.Player.Hero.CurrentHealth < this.Player.Enemy.Hero.CurrentHealth);
		dictionary.Add("咏唱：神域守护者", () => this.Player.Minions.Count <= 4);
		dictionary.Add("虎之姿态", () => this.Player.Hero.CurrentAttack > 0);
		dictionary.Add("火土风暴", () => this.Player.Hero.CurrentAttack > 0);
		dictionary.Add("吸血鬼之血", () => this.Player.Hero.CurrentHealth <= 15);
		dictionary.Add("旋转鹤踢", () => this.Player.Enemy.Minions.Count >= 4);
		dictionary.Add("邪恶灾祸", () => this.Player.Hand.Any((BaseCard c) => c is MinionCard && c.Mechanics.Deathrattle != null));
		dictionary.Add("邪恶之力", () => this.Player.Minions.Count >= 1);
		dictionary.Add("邪恶狂热", () => this.Player.Minions.Count >= 1);
		dictionary.Add("虹色光辉", () => this.Player.Minions.TargeteablesBySpellOf(this.Player).Count > 0);
		dictionary.Add("暗黑供奉", () => this.Player.Hero.CurrentHealth <= 15);
		dictionary.Add("咏唱：神圣祈愿", () => this.Player.Hand.Count <= 7);
		dictionary.Add("妖精的恶作剧", () => this.Player.Minions.TargeteablesBySpellOf(this.Player).Any((Minion m) => m.IsDamaged()) && this.Player.Enemy.Minions.Count >= 1);
		dictionary.Add("炼金术的知识", () => this.Player.Enemy.Minions.Count((Minion m) => m.CurrentHealth <= 3) > 3);
		dictionary.Add("炽燃雕像", () => this.Player.Enemy.Minions.Count((Minion m) => m.CurrentHealth <= 2) > 2);
		dictionary.Add("军师的妙计", () => 10 - this.Player.Hand.Count > this.Player.Minions.Count);
		dictionary.Add("不洁重生", () => this.Player.Minions.TargeteablesBySpellOf(this.Player).Any((Minion m) => m.IsDamaged()));
		dictionary.Add("回归根源", () => this.Player.Minions.Count + 2 < this.Player.Enemy.Minions.Count);
		dictionary.Add("破坏神的气息", () => this.Player.Hero.CurrentHealth <= 10);
		dictionary.Add("暗影哀悼", () => !this.Player.HasWeapon());
		dictionary.Add("天灾打击", delegate
		{
			if (this.Player.Minions.Any((Minion m) => m.Card.Description == "砰砰箱"))
			{
				if (this.Player.Minions.Any((Minion m) => m.Card.Description == "食尸鬼"))
				{
					return true;
				}
			}
			return this.Player.Enemy.Hero.CurrentHealth < 10;
		});
		dictionary.Add("姆诺兹多", () => this.Player.Hand.Count <= 9);
		dictionary.Add("周末狂欢", () => this.Player.Enemy.Minions.Count((Minion m) => m.Card.MinionType == MinionType.Undead) < this.Player.Minions.Count((Minion m) => m.Card.MinionType == MinionType.Undead));
		dictionary.Add("灰烬破灭狂徒", () => this.Player.Enemy.HasWeapon());
		dictionary.Add("破法符文", () => this.Player.HasWeapon());
		dictionary.Add("剃刀之符文", () => this.Player.HasWeapon());
		dictionary.Add("巫妖符文", () => this.Player.HasWeapon());
		dictionary.Add("符文熔炉", () => this.Player.HasWeapon());
		dictionary.Add("末日机器人", () => this.Player.Minions.Count + 3 < this.Player.Enemy.Minions.Count);
		dictionary.Add("末日狂欢", () => this.Player.Minions.Count + 3 < this.Player.Enemy.Minions.Count);
		dictionary.Add("萌芽花寄生", () => this.Player.Hand.Count((BaseCard c) => c.CurrentCost >= 7) > 2);
		dictionary.Add("墓生食尸鬼", () => this.Player.DeadMinions.Count >= 4);
		dictionary.Add("冰霜之路", () => GameManager.Instance.GetAllMinions().Count((Minion m) => m.IsFrozen) > 0);
		dictionary.Add("巫妖王", () => this.Player.Hero.CurrentHealth <= 10);
		dictionary.Add("拉佐格尔", () => this.Player.Minions.Count >= 3);
		dictionary.Add("风神", () => this.Player.Minions.Count >= 2);
		dictionary.Add("黑暗精灵·芙蕾", () => this.Player.Hand.Count <= 9);
		dictionary.Add("凯旋的骑士", () => this.Player.Minions.Count <= 5);
		dictionary.Add("疾风怒涛", () => this.Player.Minions.Count >= 5);
		dictionary.Add("暗夜中的兽群", () => this.Player.Minions.Count <= 5);
		dictionary.Add("冰封坚韧", () => this.Player.Hero.CurrentHealth <= 10);
		dictionary.Add("尖叫爆炸", () => this.Player.Enemy.Minions.Count((Minion m) => m.IsFrozen) > 2);
		dictionary.Add("尖叫女妖", () => this.Player.Minions.Count >= 1);
		dictionary.Add("湮灭", () => this.Player.Hero.CurrentHealth >= 10);
		dictionary.Add("亡灵法师", () => this.Player.Hand.Count((BaseCard c) => c is SpellCard && c.CurrentCost > 3) > 2);
		dictionary.Add("狂野的拉佐格尔", () => this.Player.Minions.Count((Minion m) => m.Card.MinionType == MinionType.Dragon) > 2);
		dictionary.Add("灼热风暴", () => this.Player.Minions.Count + 3 < this.Player.Enemy.Minions.Count);
		dictionary.Add("霜之哀伤", () => !this.Player.HasWeapon());
		dictionary.Add("森林的意志", () => this.Player.Hand.Count >= 7);
		dictionary.Add("骷髅法师", () => this.Player.Hand.Count <= 7);
		dictionary.Add("利爪的一击", () => this.Player.Hero.CurrentHealth >= 3);
		dictionary.Add("血之契约", () => this.Player.Hero.CurrentHealth >= 3 && this.Player.Hand.Count <= 7);
		dictionary.Add("龙龟", () => this.Player.Minions.Count >= 1);
		dictionary.Add("死亡祝福", () => this.Player.Minions.Count <= 5);
		dictionary.Add("龙之传令", () => this.Player.Hand.Count <= 8);
		dictionary.Add("死亡祝福", () => this.Player.Minions.Count <= 6);
		dictionary.Add("融合死骑", () => this.Player.Minions.Count((Minion m) => m.Card.Name == "食尸鬼") >= 2);
		dictionary.Add("死亡契约", () => this.Player.Hero.CurrentHealth <= 10);
		dictionary.Add("黑暗模拟", () => this.Player.Hand.Count <= 8);
		dictionary.Add("人偶师的线", delegate
		{
			if (this.Player.Hand.Count > 6)
			{
				return this.Player.Enemy.Minions.Count((Minion m) => m.CurrentHealth <= 1) >= 3;
			}
			return true;
		});
		dictionary.Add("好运波葛", () => this.Player.HasWeapon());
		dictionary.Add("忒弥斯的审判", () => this.Player.Minions.Count + 3 < this.Player.Enemy.Minions.Count);
		dictionary.Add("鬼灵骑兵", () => this.Player.Hero.CurrentHealth > 30);
		dictionary.Add("灰烬使者", () => !this.Player.HasWeapon());
		dictionary.Add("疯狂的刽子手", () => this.Player.Hero.CurrentHealth >= 3);
		dictionary.Add("腐败飓风", delegate
		{
			if (this.Player.Hand.Count > 6)
			{
				return this.Player.Enemy.Minions.Count((Minion m) => m.CurrentHealth <= 3) >= 3;
			}
			return true;
		});
		dictionary.Add("侠盗的仁义", () => this.Player.Hand.Count >= 5);
		dictionary.Add("行船商人", () => this.Player.Hand.Count <= 8);
		dictionary.Add("新星魔术师·萨米", () => this.Player.Hand.Count <= 8);
		dictionary.Add("冬之女王的即兴艺术", () => this.Player.Minions.Count + 2 < this.Player.Enemy.Minions.Count);
		dictionary.Add("风语冥想师", () => this.Player.Hand.Count <= 8);
		dictionary.Add("骨盾", () => this.Player.Hero.CurrentHealth <= 10);
		dictionary.Add("血虫雨", () => this.Player.Hero.CurrentHealth <= 15 && this.Player.Minions.Count <= 5);
		dictionary.Add("血丝", delegate
		{
			if (this.Player.Hero.CurrentHealth <= 5)
			{
				return this.Player.Minions.Count((Minion m) => m.Card.Name == "食尸鬼") >= 2;
			}
			return false;
		});
		dictionary.Add("鲜血打击", () => this.Player.Minions.Count((Minion m) => m.Card.Name == "食尸鬼") >= 3);
		dictionary.Add("高阶牧师", () => this.Player.HasWeapon());
		dictionary.Add("龙人施法者", () => this.Player.Minions.Count <= 4);
		dictionary.Add("启示录之剑", () => !this.Player.HasWeapon());
		dictionary.Add("反魔法护盾", () => this.Player.Minions.Count >= 3);
		dictionary.Add("镜像亡者", () => this.Player.Enemy.DeadMinions.Count >= 1);
		dictionary.Add("反魔法空间", () => this.Player.Enemy.Hand.Count((BaseCard c) => c is SpellCard) > 2);
		dictionary.Add("睿智指挥官", () => this.Player.Minions.Count >= 2);
		dictionary.Add("阿彻鲁斯传送门", () => this.Player.Minions.Count <= 6);
		this.SpecialPlayRules = dictionary;
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
				"战斗姿态",
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
			},
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

	public override IEnumerator Think()
	{
		yield return new WaitForSeconds(1f);
		yield return this.CheckLethal();
		yield return new WaitForSeconds(0.3f);
		yield return base.PlayCardsWith(OrderPriority.First);
		yield return new WaitForSeconds(0.3f);
		yield return base.PlayCardsWith(OrderPriority.BeforeAttacking);
		yield return new WaitForSeconds(0.3f);
		yield return this.MinionAttack();
		yield return new WaitForSeconds(0.3f);
		yield return base.PlayCardsWith(OrderPriority.AfterAttacking);
		yield return new WaitForSeconds(0.3f);
		yield return base.PlayUnorderedCards();
		yield return new WaitForSeconds(0.3f);
		yield return base.PlayCardsWith(OrderPriority.BeforeHeroPower);
		yield return new WaitForSeconds(0.3f);
		yield return this.HeroPower();
		yield return new WaitForSeconds(0.3f);
		yield return base.PlayCardsWith(OrderPriority.AfterHeroPower);
		yield return new WaitForSeconds(0.3f);
		yield return this.HeroAttack();
		yield return new WaitForSeconds(0.3f);
		yield return this.HeroAttack();
		yield return new WaitForSeconds(0.3f);
		yield return this.MinionAttack();
		yield return new WaitForSeconds(0.3f);
		yield return base.PlayCardsWith(OrderPriority.Last);
		yield return new WaitForSeconds(0.3f);
		yield return GameManager.Instance.TurnEnd();
		yield break;
	}

	private IEnumerator CheckLethal()
	{
		if ((from m in this.Player.Enemy.Minions
		where m.HasTaunt && !m.IsStealth && m.IsAlive()
		select m).ToList<Minion>().Count == 0 && !this.Player.Enemy.Hero.IsImmune)
		{
			int num = 0;
			foreach (Minion minion2 in this.Player.Minions)
			{
				if (minion2.CanAttack() && minion2.CanAttackTo(this.Player.Enemy.Hero))
				{
					num += minion2.CurrentAttack;
					if (minion2.HasWindfury)
					{
						num += minion2.CurrentAttack;
					}
				}
			}
			if (this.Player.HasWeapon() && this.Player.Hero.CanAttack() && this.Player.Hero.CanAttackTo(this.Player.Enemy.Hero))
			{
				num += this.Player.Weapon.CurrentAttack;
				if (this.Player.Hero.HasWindfury)
				{
					num += this.Player.Weapon.CurrentAttack;
				}
			}
			if (num >= this.Player.Enemy.Hero.CurrentHealth)
			{
				if (this.Player.HasWeapon())
				{
					yield return this.Player.Hero.Controller.LevitateWaitAnimation();
					yield return this.Player.Hero.Attack(this.Player.Enemy.Hero);
				}
				foreach (Minion minion in this.Player.Minions)
				{
					if (minion.CanAttack() && minion.CanAttackTo(this.Player.Enemy.Hero))
					{
						yield return minion.Attack(this.Player.Enemy.Hero);
						if (minion.HasWindfury)
						{
							yield return minion.Attack(this.Player.Enemy.Hero);
						}
					}
					minion = null;
				}
				List<Minion>.Enumerator enumerator2 = default(List<Minion>.Enumerator);
			}
		}
		yield break;
		yield break;
	}

	private IEnumerator HeroAttack()
	{
		if (this.Player.Hero.CanAttack() && !this.Player.Hero.IsStealth)
		{
			List<Minion> targeteableEnemyMinions = base.GetTargeteableEnemyMinions(this.Player.Hero);
			if (targeteableEnemyMinions.Count > 0)
			{
				Minion target = (from m in targeteableEnemyMinions
				where m.CurrentHealth <= this.Player.Hero.CurrentAttack || m.HasTaunt
				orderby m.CurrentAttack descending
				select m).FirstOrDefault<Minion>();
				if (target != null && target.CurrentAttack < this.Player.Hero.CurrentHealth)
				{
					yield return this.Player.Hero.Controller.LevitateWaitAnimation();
					yield return this.Player.Hero.Attack(target);
				}
				else if (this.Player.Hero.CanAttackTo(this.Player.Enemy.Hero))
				{
					yield return this.Player.Hero.Controller.LevitateWaitAnimation();
					yield return this.Player.Hero.Attack(this.Player.Enemy.Hero);
				}
				target = null;
			}
			else if (this.Player.Hero.CanAttackTo(this.Player.Enemy.Hero))
			{
				yield return this.Player.Hero.Controller.LevitateWaitAnimation();
				yield return this.Player.Hero.Attack(this.Player.Enemy.Hero);
			}
		}
		yield break;
	}

	private IEnumerator MinionAttack()
	{
		using (List<Minion>.Enumerator enumerator = (from m in this.Player.Minions
		where m.CanAttack() && m.IsAlive()
		orderby m.CurrentAttack
		select m).ToList<Minion>().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Minion minion = enumerator.Current;
				List<Minion> targeteableEnemyMinions = base.GetTargeteableEnemyMinions(minion);
				if (targeteableEnemyMinions.Count > 0)
				{
					Minion minion2 = (from m in targeteableEnemyMinions
					where m.CurrentHealth <= minion.CurrentAttack || m.HasTaunt
					orderby m.CurrentAttack descending
					select m).FirstOrDefault<Minion>();
					if (minion2 != null)
					{
						yield return minion.Attack(minion2);
						yield return new WaitForSeconds(0.5f);
					}
					else if (minion.CanAttackTo(this.Player.Enemy.Hero))
					{
						yield return minion.Attack(this.Player.Enemy.Hero);
						yield return new WaitForSeconds(0.5f);
					}
				}
				else if (minion.CanAttackTo(this.Player.Enemy.Hero))
				{
					yield return minion.Attack(this.Player.Enemy.Hero);
					yield return new WaitForSeconds(0.5f);
				}
			}
		}
		List<Minion>.Enumerator enumerator = default(List<Minion>.Enumerator);
		yield break;
		yield break;
	}

	private IEnumerator HeroPower()
	{
		if (this.Player.Hero.HeroPower.IsAvailable())
		{
			string name = this.Player.Hero.HeroPower.Name;
			if (name != null)
			{
				if (!(name == "Raise Ghoul"))
				{
					if (name == "Chi Burst" || name == "Megamorphosis" || name == "Metamorphosis")
					{
						if (this.Player.Minions.Any((Minion m) => m.CurrentHealth < m.MaxHealth))
						{
							yield return this.Player.Hero.HeroPower.Controller.RotateDownAnimation();
							yield return this.Player.UseHeroPower((from m in this.Player.Minions
							orderby m.CurrentHealth
							select m).First<Minion>());
						}
						else if (this.Player.Hero.CurrentHealth < this.Player.Hero.MaxHealth)
						{
							yield return this.Player.Hero.HeroPower.Controller.RotateDownAnimation();
							yield return this.Player.UseHeroPower(this.Player.Hero);
						}
					}
					else if (name == "Dominate" && this.Player.Enemy.Minions.Any<Minion>())
					{
						yield return this.Player.Hero.HeroPower.Controller.RotateDownAnimation();
						yield return this.Player.UseHeroPower((from m in this.Player.Enemy.Minions
						orderby m.CurrentHealth
						select m).First<Minion>());
					}
				}
				else if (this.Player.Minions.Count < 7)
				{
					yield return this.Player.Hero.HeroPower.Controller.RotateDownAnimation();
					yield return this.Player.UseHeroPower(null);
				}
			}
		}
		yield break;
	}

	public Character ImpalingSpineTargetRule(List<Character> characters)
	{
		List<Character> list = (from c in characters
		where c is Minion && c.As<Minion>().Card is HighWarlordNajentus
		select c).ToList<Character>();
		if (list.Count > 0)
		{
			return RNG.RandomItemFrom<Character>(list);
		}
		return RNG.RandomItemFrom<Character>(characters);
	}

	public Character AllWillServeTargetRule(List<Character> characters)
	{
		List<Character> list = (from c in characters
		where c.CurrentHealth <= 2
		select c).ToList<Character>();
		if (list.Count > 0)
		{
			return (from c in list
			where this.Player.Hero.IsEnemyOf(c)
			orderby c.CurrentAttack descending
			select c).FirstOrDefault<Character>();
		}
		return (from c in characters
		where this.Player.Hero.IsEnemyOf(c)
		orderby c.CurrentAttack descending
		select c).FirstOrDefault<Character>();
	}

	public Character BloodStrikeTargetRule(List<Character> characters)
	{
		List<Character> list = (from c in characters
		where c.CurrentHealth <= 4
		select c).ToList<Character>();
		if (list.Count > 0)
		{
			return (from c in list
			orderby c.CurrentAttack descending
			select c).FirstOrDefault<Character>();
		}
		return (from c in characters
		where this.Player.Hero.IsEnemyOf(c)
		orderby c.CurrentAttack descending
		select c).FirstOrDefault<Character>();
	}

	public Character DeathGripTargetRule(List<Character> characters)
	{
		return (from c in characters
		where this.Player.Hero.IsEnemyOf(c)
		orderby c.CurrentAttack + c.CurrentHealth descending
		select c).FirstOrDefault<Character>();
	}

	public Character BrawlingStanceTargetRule(List<Character> characters)
	{
		return (from c in characters
		where this.Player.Hero.IsFriendlyOf(c)
		orderby c.CurrentAttack
		select c).FirstOrDefault<Character>();
	}

	public Character GiftoftheSerpentTargetRule(List<Character> characters)
	{
		return (from c in characters
		where this.Player.Hero.IsEnemyOf(c)
		orderby c.CurrentAttack + c.CurrentHealth descending
		select c).FirstOrDefault<Character>();
	}

	public Character ChiWaveTargetRule(List<Character> characters)
	{
		return (from c in characters
		where this.Player.Hero.IsFriendlyOf(c)
		where c.CurrentHealth < c.MaxHealth
		orderby c.MaxHealth - c.CurrentHealth descending
		select c).FirstOrDefault<Character>();
	}

	public Character TastyBrewTargetRule(List<Character> characters)
	{
		return (from c in characters
		where this.Player.Hero.IsFriendlyOf(c)
		orderby c.CurrentHealth
		select c).FirstOrDefault<Character>();
	}

	public Character SpinningFireBlossomTargetRule(List<Character> characters)
	{
		return (from c in characters
		where this.Player.Hero.IsEnemyOf(c)
		orderby c.CurrentHealth descending
		select c).FirstOrDefault<Character>();
	}

	public bool CorpseExplosionComboCondition()
	{
		return this.Player.AvailableMana >= 5 && this.Player.Hero.HeroPower.IsAvailable() && this.Player.Minions.Count < 7 && this.Player.Enemy.Minions.Count > 2;
	}

	public IEnumerator CorpseExplosionCombo()
	{
		yield return this.HeroPower();
		SpellCard spellCard = this.Player.Hand.First((BaseCard c) => c.Name == "Corpse Explosion") as SpellCard;
		Character ghoul = this.Player.Minions.Last((Minion m) => m.Card.Name == "Ghoul");
		yield return this.Player.PlaySpell(spellCard, ghoul);
		yield return new WaitForSeconds(1.5f);
		List<Minion> list = (from m in this.Player.Enemy.Minions
		where m.CurrentHealth > 2 && ghoul.CanAttackTo(m)
		select m).ToList<Minion>();
		if (list.Count > 0)
		{
			ghoul.Attack((from m in list
			orderby m.CurrentAttack descending
			select m).First<Minion>());
		}
		else
		{
			ghoul.Attack(base.GetRandomEnemyMinion());
		}
		yield break;
	}

	public Character EnemyLessThan5Rule(List<Character> characters)
	{
		List<Character> list = (from c in characters
		where c.CurrentHealth <= 5
		select c).ToList<Character>();
		if (list.Count > 0)
		{
			return (from c in list
			where this.Player.Hero.IsEnemyOf(c)
			orderby c.CurrentAttack descending
			select c).FirstOrDefault<Character>();
		}
		return (from c in characters
		where this.Player.Hero.IsEnemyOf(c)
		orderby c.CurrentAttack descending
		select c).FirstOrDefault<Character>();
	}
}
