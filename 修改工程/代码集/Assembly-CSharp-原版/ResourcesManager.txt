using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
	private ResourcesManager()
	{
	}

	public static ResourcesManager Instance
	{
		get
		{
			return ResourcesManager._instance;
		}
	}

	private void Awake()
	{
		if (ResourcesManager._instance != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		ResourcesManager._instance = this;
		ResourcesManager.Attributes = new Dictionary<string, Sprite>
		{
			{
				"Attack",
				Resources.Load<Sprite>("Sprites/General/Attack")
			},
			{
				"Health",
				Resources.Load<Sprite>("Sprites/General/Health")
			},
			{
				"Armor",
				Resources.Load<Sprite>("Sprites/General/Armor")
			}
		};
		ResourcesManager.Crystals = new Dictionary<string, Sprite>
		{
			{
				"Available",
				Resources.Load<Sprite>("Sprites/General/AvailableCrystal")
			},
			{
				"Used",
				Resources.Load<Sprite>("Sprites/General/UsedCrystal")
			},
			{
				"Overloaded",
				Resources.Load<Sprite>("Sprites/General/OverloadedCrystal")
			}
		};
		ResourcesManager.Effects = new Dictionary<string, Sprite>
		{
			{
				"DivineShield",
				Resources.Load<Sprite>("Sprites/General/DivineShield")
			},
			{
				"Stealth",
				Resources.Load<Sprite>("Sprites/General/Stealth")
			},
			{
				"Windfury",
				Resources.Load<Sprite>("Sprites/General/Windfury")
			},
			{
				"Enraged",
				Resources.Load<Sprite>("Sprites/General/Enraged")
			},
			{
				"Deathrattle",
				Resources.Load<Sprite>("Sprites/General/Deathrattle")
			},
			{
				"Trigger",
				Resources.Load<Sprite>("Sprites/General/Trigger")
			},
			{
				"Poison",
				Resources.Load<Sprite>("Sprites/General/Poison")
			},
			{
				"Inspire",
				Resources.Load<Sprite>("Sprites/General/Inspire")
			},
			{
				"Meditate",
				Resources.Load<Sprite>("Sprites/General/Meditate")
			}
		};
		ResourcesManager.Tokens = new Dictionary<string, Sprite>
		{
			{
				"Minion_Normal",
				Resources.Load<Sprite>("Sprites/General/Minion_NormalToken")
			},
			{
				"Minion_Legendary",
				Resources.Load<Sprite>("Sprites/General/Minion_LegendaryToken")
			},
			{
				"Minion_Taunt",
				Resources.Load<Sprite>("Sprites/General/Minion_TauntToken")
			},
			{
				"HeroPower_Front",
				Resources.Load<Sprite>("Sprites/General/HeroPower_FrontToken")
			},
			{
				"HeroPower_Back",
				Resources.Load<Sprite>("Sprites/General/HeroPower_BackToken")
			},
			{
				"Weapon_Open",
				Resources.Load<Sprite>("Sprites/General/Weapon_OpenToken")
			},
			{
				"Weapon_Closed",
				Resources.Load<Sprite>("Sprites/General/Weapon_ClosedToken")
			}
		};
		ResourcesManager.Glows = new Dictionary<string, Sprite>
		{
			{
				"Card_LegendaryMinion_Echo",
				Resources.Load<Sprite>("Sprites/Glows/Card_LegendaryMinion_Echo")
			},
			{
				"Card_Minion_Echo",
				Resources.Load<Sprite>("Sprites/Glows/Card_Minion_Echo")
			},
			{
				"Card_Spell_Echo",
				Resources.Load<Sprite>("Sprites/Glows/Card_Spell_Echo")
			},
			{
				"Card_Weapon_Echo",
				Resources.Load<Sprite>("Sprites/Glows/Card_Weapon_Echo")
			},
			{
				"Card_LegendaryMinion_GreenGlow",
				Resources.Load<Sprite>("Sprites/Glows/Card_LegendaryMinion_GreenGlow")
			},
			{
				"Card_Minion_GreenGlow",
				Resources.Load<Sprite>("Sprites/Glows/Card_Minion_GreenGlow")
			},
			{
				"Card_Spell_GreenGlow",
				Resources.Load<Sprite>("Sprites/Glows/Card_Spell_GreenGlow")
			},
			{
				"Card_Weapon_GreenGlow",
				Resources.Load<Sprite>("Sprites/Glows/Card_Weapon_GreenGlow")
			},
			{
				"Card_LegendaryMinion_WhiteGlow",
				Resources.Load<Sprite>("Sprites/Glows/Card_LegendaryMinion_WhiteGlow")
			},
			{
				"Card_Minion_WhiteGlow",
				Resources.Load<Sprite>("Sprites/Glows/Card_Minion_WhiteGlow")
			},
			{
				"Card_Spell_WhiteGlow",
				Resources.Load<Sprite>("Sprites/Glows/Card_Spell_WhiteGlow")
			},
			{
				"Card_Weapon_WhiteGlow",
				Resources.Load<Sprite>("Sprites/Glows/Card_Weapon_WhiteGlow")
			},
			{
				"Card_Normal_GreenGlow",
				Resources.Load<Sprite>("Sprites/Glows/Card_Normal_GreenGlow")
			},
			{
				"Card_Normal_RedGlow",
				Resources.Load<Sprite>("Sprites/Glows/Card_Normal_RedGlow")
			},
			{
				"Hero_Portrait_GreenGlow",
				Resources.Load<Sprite>("Sprites/Glows/Hero_Portrait_GreenGlow")
			},
			{
				"Hero_Portrait_RedGlow",
				Resources.Load<Sprite>("Sprites/Glows/Hero_Portrait_RedGlow")
			},
			{
				"Hero_Portrait_WhiteGlow",
				Resources.Load<Sprite>("Sprites/Glows/Hero_Portrait_WhiteGlow")
			},
			{
				"Hero_Power_GreenGlow",
				Resources.Load<Sprite>("Sprites/Glows/Hero_Power_GreenGlow")
			},
			{
				"Hero_Power_RedGlow",
				Resources.Load<Sprite>("Sprites/Glows/Hero_Power_RedGlow")
			},
			{
				"Hero_Power_WhiteGlow",
				Resources.Load<Sprite>("Sprites/Glows/Hero_Power_WhiteGlow")
			},
			{
				"Minion_Legendary_GreenGlow",
				Resources.Load<Sprite>("Sprites/Glows/Minion_Legendary_GreenGlow")
			},
			{
				"Minion_Legendary_RedGlow",
				Resources.Load<Sprite>("Sprites/Glows/Minion_Legendary_RedGlow")
			},
			{
				"Minion_Legendary_WhiteGlow",
				Resources.Load<Sprite>("Sprites/Glows/Minion_Legendary_WhiteGlow")
			},
			{
				"Minion_LegendaryTaunt_GreenGlow",
				Resources.Load<Sprite>("Sprites/Glows/Minion_LegendaryTaunt_GreenGlow")
			},
			{
				"Minion_LegendaryTaunt_RedGlow",
				Resources.Load<Sprite>("Sprites/Glows/Minion_LegendaryTaunt_RedGlow")
			},
			{
				"Minion_LegendaryTaunt_WhiteGlow",
				Resources.Load<Sprite>("Sprites/Glows/Minion_LegendaryTaunt_WhiteGlow")
			},
			{
				"Minion_Normal_GreenGlow",
				Resources.Load<Sprite>("Sprites/Glows/Minion_Normal_GreenGlow")
			},
			{
				"Minion_Normal_RedGlow",
				Resources.Load<Sprite>("Sprites/Glows/Minion_Normal_RedGlow")
			},
			{
				"Minion_Normal_WhiteGlow",
				Resources.Load<Sprite>("Sprites/Glows/Minion_Normal_WhiteGlow")
			},
			{
				"Minion_NormalTaunt_GreenGlow",
				Resources.Load<Sprite>("Sprites/Glows/Minion_NormalTaunt_GreenGlow")
			},
			{
				"Minion_NormalTaunt_RedGlow",
				Resources.Load<Sprite>("Sprites/Glows/Minion_NormalTaunt_RedGlow")
			},
			{
				"Minion_NormalTaunt_WhiteGlow",
				Resources.Load<Sprite>("Sprites/Glows/Minion_NormalTaunt_WhiteGlow")
			},
			{
				"Weapon_GreenGlow",
				Resources.Load<Sprite>("Sprites/Glows/Weapon_GreenGlow")
			},
			{
				"Weapon_RedGlow",
				Resources.Load<Sprite>("Sprites/Glows/Weapon_RedGlow")
			},
			{
				"Weapon_WhiteGlow",
				Resources.Load<Sprite>("Sprites/Glows/Weapon_WhiteGlow")
			},
			{
				"Banner_BlueGlow",
				Resources.Load<Sprite>("Sprites/DeckBuilder/Banner_BlueGlow")
			},
			{
				"Banner_WhiteGlow",
				Resources.Load<Sprite>("Sprites/DeckBuilder/Banner_WhiteGlow")
			},
			{
				"Meditate_GreenGlow",
				Resources.Load<Sprite>("Sprites/Glows/Meditate_GreenGlow")
			}
		};
		ResourcesManager.Splats = new Dictionary<string, Sprite>
		{
			{
				"Damage",
				Resources.Load<Sprite>("Sprites/General/DamageSplat")
			},
			{
				"Heal",
				Resources.Load<Sprite>("Sprites/General/HealSplat")
			}
		};
		ResourcesManager.Decks = new Dictionary<string, Sprite>
		{
			{
				"Cross",
				Resources.Load<Sprite>("Sprites/DeckBuilder/Cross")
			},
			{
				"Banner_DeathKnight",
				Resources.Load<Sprite>("Sprites/DeckBuilder/Banner_DeathKnight")
			},
			{
				"Banner_Monk",
				Resources.Load<Sprite>("Sprites/DeckBuilder/Banner_Monk")
			},
			{
				"Banner_DemonHunter",
				Resources.Load<Sprite>("Sprites/DeckBuilder/Banner_DemonHunter")
			},
			{
				"BannerShade",
				Resources.Load<Sprite>("Sprites/DeckBuilder/BannerShade")
			},
			{
				"BannerHero_Token",
				Resources.Load<Sprite>("Sprites/DeckBuilder/BannerHero_Token")
			},
			{
				"BannerHero_DeathKnight",
				Resources.Load<Sprite>("Sprites/DeckBuilder/BannerHero_DeathKnight")
			},
			{
				"BannerHero_Monk",
				Resources.Load<Sprite>("Sprites/DeckBuilder/BannerHero_Monk")
			},
			{
				"BannerHero_DemonHunter",
				Resources.Load<Sprite>("Sprites/DeckBuilder/BannerHero_DemonHunter")
			}
		};
		ResourcesManager.Numbers = new Dictionary<string, Sprite[]>
		{
			{
				"White",
				Resources.LoadAll<Sprite>("Sprites/General/NumbersWhite")
			},
			{
				"Green",
				Resources.LoadAll<Sprite>("Sprites/General/NumbersGreen")
			},
			{
				"Red",
				Resources.LoadAll<Sprite>("Sprites/General/NumbersRed")
			}
		};
		ResourcesManager.Shaders = new Dictionary<ShaderMode, Shader>
		{
			{
				ShaderMode.Normal,
				Shader.Find("Unlit/Alpha Shadows")
			},
			{
				ShaderMode.Culled,
				Shader.Find("Unlit/Alpha Shadows (Culled)")
			},
			{
				ShaderMode.Transparent,
				Shader.Find("Unlit/Transparent")
			}
		};
		ResourcesManager.Sounds = new Dictionary<string, AudioClip>
		{
			{
				"Menu_Button_Hover",
				Resources.Load<AudioClip>("Sounds/UI/menu_button_hover")
			},
			{
				"Menu_Button_Click",
				Resources.Load<AudioClip>("Sounds/UI/menu_button_click")
			},
			{
				"Menu_Load",
				Resources.Load<AudioClip>("Sounds/UI/menu_load")
			},
			{
				"Menu_Return",
				Resources.Load<AudioClip>("Sounds/UI/menu_return")
			},
			{
				"Spinner_WindUp",
				Resources.Load<AudioClip>("Sounds/UI/spinner_windup")
			},
			{
				"Spinner_WindDown",
				Resources.Load<AudioClip>("Sounds/UI/spinner_winddown")
			},
			{
				"Spinner_Start",
				Resources.Load<AudioClip>("Sounds/UI/spinner_start")
			},
			{
				"Spinner_Loop",
				Resources.Load<AudioClip>("Sounds/UI/spinner_loop")
			},
			{
				"Spinner_End",
				Resources.Load<AudioClip>("Sounds/UI/spinner_end")
			},
			{
				"GameMenu_Open",
				Resources.Load<AudioClip>("Sounds/UI/gamemenu_open")
			},
			{
				"GameMenu_Close",
				Resources.Load<AudioClip>("Sounds/UI/gamemenu_close")
			},
			{
				"DeckBuilder_Card_Add",
				Resources.Load<AudioClip>("Sounds/DeckBuilder/card_add")
			},
			{
				"DeckBuilder_Card_Remove",
				Resources.Load<AudioClip>("Sounds/DeckBuilder/card_remove")
			},
			{
				"DeckBuilder_Card_Hover",
				Resources.Load<AudioClip>("Sounds/DeckBuilder/card_hover")
			},
			{
				"DeckBuilder_Card_Invalid",
				Resources.Load<AudioClip>("Sounds/DeckBuilder/card_invalid")
			},
			{
				"DeckBuilder_ClassTab_Click",
				Resources.Load<AudioClip>("Sounds/DeckBuilder/classtab_click")
			},
			{
				"DeckBuilder_Page_Back",
				Resources.Load<AudioClip>("Sounds/DeckBuilder/page_back")
			},
			{
				"DeckBuilder_Page_Forward",
				Resources.Load<AudioClip>("Sounds/DeckBuilder/page_forward")
			},
			{
				"DeckBuilder_Scene_MoveUp",
				Resources.Load<AudioClip>("Sounds/DeckBuilder/scene_moveup")
			},
			{
				"DeckBuilder_Scene_MoveDown",
				Resources.Load<AudioClip>("Sounds/DeckBuilder/scene_movedown")
			},
			{
				"Game_YourTurn",
				Resources.Load<AudioClip>("Sounds/Game/alert_yourturn")
			},
			{
				"Game_TurnButton_Start",
				Resources.Load<AudioClip>("Sounds/Game/turnbutton_start")
			},
			{
				"Game_TurnButton_End",
				Resources.Load<AudioClip>("Sounds/Game/turnbutton_end")
			},
			{
				"Game_TurnButton_Up",
				Resources.Load<AudioClip>("Sounds/Game/turnbutton_up")
			},
			{
				"Game_TurnButton_Down",
				Resources.Load<AudioClip>("Sounds/Game/turnbutton_down")
			},
			{
				"Game_Draw_Card",
				Resources.Load<AudioClip>("Sounds/Game/draw_card")
			},
			{
				"Game_Draw_Card_Hand",
				Resources.Load<AudioClip>("Sounds/Game/draw_card_hand")
			},
			{
				"Game_Draw_Fatigue",
				Resources.Load<AudioClip>("Sounds/Game/draw_card_fatigue")
			},
			{
				"Game_Draw_FatigueStrike",
				Resources.Load<AudioClip>("Sounds/Game/draw_card_fatiguestrike")
			},
			{
				"Game_Hero_Attack_Start",
				Resources.Load<AudioClip>("Sounds/Game/hero_attack_start")
			},
			{
				"Game_Hero_Attack_End",
				Resources.Load<AudioClip>("Sounds/Game/hero_attack_end")
			},
			{
				"Game_Hero_Portrait_Crack",
				Resources.Load<AudioClip>("Sounds/Game/hero_portrait_crack")
			},
			{
				"Game_Hero_Portrait_Explode",
				Resources.Load<AudioClip>("Sounds/Game/hero_portrait_explode")
			},
			{
				"Game_HeroPower_Flip_On",
				Resources.Load<AudioClip>("Sounds/Game/heropower_flip_on")
			},
			{
				"Game_HeroPower_Flip_Off",
				Resources.Load<AudioClip>("Sounds/Game/heropower_flip_off")
			},
			{
				"Game_Weapon_Equip",
				Resources.Load<AudioClip>("Sounds/Game/weapon_equip")
			},
			{
				"Game_Weapon_Destroy",
				Resources.Load<AudioClip>("Sounds/Game/weapon_destroy")
			},
			{
				"Game_Weapon_Sheathe",
				Resources.Load<AudioClip>("Sounds/Game/weapon_sheathe")
			},
			{
				"Game_Weapon_Unsheathe",
				Resources.Load<AudioClip>("Sounds/Game/weapon_unsheathe")
			},
			{
				"Game_Secret_Play",
				Resources.Load<AudioClip>("Sounds/Game/secret_play")
			},
			{
				"Game_Secret_Trigger",
				Resources.Load<AudioClip>("Sounds/Game/secret_trigger")
			},
			{
				"Game_Impact_Normal",
				Resources.Load<AudioClip>("Sounds/Game/impact_normal")
			},
			{
				"Game_Impact_Mid",
				Resources.Load<AudioClip>("Sounds/Game/impact_mid")
			},
			{
				"Game_Impact_Large",
				Resources.Load<AudioClip>("Sounds/Game/impact_large")
			},
			{
				"Game_Drop_Normal",
				Resources.Load<AudioClip>("Sounds/Game/drop_normal")
			},
			{
				"Game_Drop_Mid",
				Resources.Load<AudioClip>("Sounds/Game/drop_mid")
			},
			{
				"Game_Drop_Large",
				Resources.Load<AudioClip>("Sounds/Game/drop_large")
			},
			{
				"Game_Minion_Death",
				Resources.Load<AudioClip>("Sounds/Game/minion_death")
			},
			{
				"Game_Victory_Start",
				Resources.Load<AudioClip>("Sounds/Game/victory_screen_start")
			},
			{
				"Game_Victory_Jingle",
				Resources.Load<AudioClip>("Sounds/Game/victory_jingle")
			},
			{
				"Game_Victory_Fireworks_Start",
				Resources.Load<AudioClip>("Sounds/Game/victory_fireworks")
			},
			{
				"Game_Victory_Fireworks_Loop",
				Resources.Load<AudioClip>("Sounds/Game/victory_fireworks_loop")
			},
			{
				"Game_Defeat_Start",
				Resources.Load<AudioClip>("Sounds/Game/defeat_screen_start")
			},
			{
				"Game_Defeat_Thunder_Loop",
				Resources.Load<AudioClip>("Sounds/Game/defeat_thunder_rumble_loop")
			},
			{
				"Game_Mechanic_Silence",
				Resources.Load<AudioClip>("Sounds/Game/mechanic_silence")
			},
			{
				"Game_Mechanic_Freeze",
				Resources.Load<AudioClip>("Sounds/Game/mechanic_freeze")
			},
			{
				"Game_Mechanic_Trigger",
				Resources.Load<AudioClip>("Sounds/Game/mechanic_trigger")
			}
		};
		ResourcesManager.Cursors = new Dictionary<string, Texture2D>
		{
			{
				"Normal",
				Resources.Load<Texture2D>("Sprites/Cursor/normal")
			},
			{
				"Click",
				Resources.Load<Texture2D>("Sprites/Cursor/click")
			},
			{
				"Drag",
				Resources.Load<Texture2D>("Sprites/Cursor/drag")
			},
			{
				"Wait",
				Resources.Load<Texture2D>("Sprites/Cursor/wait")
			},
			{
				"Right",
				Resources.Load<Texture2D>("Sprites/Cursor/right")
			},
			{
				"Left",
				Resources.Load<Texture2D>("Sprites/Cursor/left")
			}
		};
		ResourcesManager.Font = Resources.Load<Font>("Fonts/Belwe-Bold");
		ResourcesManager.FontMaterial = Resources.Load<Material>("Fonts/Belwe-Bold");
		ResourcesManager.FontTexture = Resources.Load<Texture>("Fonts/Belwe-Bold");
	}

	public void WarmAssets()
	{
		if (!this.HasWarmed)
		{
			this.StartedCoroutines = 0;
			this.EndedCoroutines = 0;
			foreach (string path in this.TextureFolders)
			{
				this.StartedCoroutines++;
				base.StartCoroutine(this.WarmTextures(path));
			}
			Shader.WarmupAllShaders();
			this.HasWarmed = true;
		}
	}

	private IEnumerator WarmTextures(string path)
	{
		Texture2D[] effectTextures = Resources.LoadAll<Texture2D>(path);
		foreach (Texture2D texture in effectTextures)
		{
			Graphics.DrawTexture(new Rect(0f, 0f, 128f, 128f), texture);
			yield return null;
		}
		this.WarmedTextures.AddRange(effectTextures);
		MonoBehaviour.print(string.Concat(new object[]
		{
			"Loaded ",
			effectTextures.Length,
			" textures on ",
			path
		}));
		this.EndedCoroutines++;
		yield break;
	}

	public bool IsAssetWarmFinished()
	{
		return this.EndedCoroutines == this.StartedCoroutines;
	}

	private static ResourcesManager _instance;

	public static Dictionary<string, Sprite> Attributes;

	public static Dictionary<string, Sprite> Crystals;

	public static Dictionary<string, Sprite> Effects;

	public static Dictionary<string, Sprite> Tokens;

	public static Dictionary<string, Sprite> Glows;

	public static Dictionary<string, Sprite> Splats;

	public static Dictionary<string, Sprite> Decks;

	public static Dictionary<string, Sprite[]> Numbers;

	public static Dictionary<ShaderMode, Shader> Shaders;

	public static Dictionary<string, AudioClip> Sounds;

	public static Dictionary<string, Texture2D> Cursors;

	public static Font Font;

	public static Material FontMaterial;

	public static Texture FontTexture;

	public bool HasWarmed;

	private int StartedCoroutines;

	private int EndedCoroutines;

	private volatile List<Texture2D> WarmedTextures = new List<Texture2D>();

	private List<string> TextureFolders = new List<string>
	{
		"Effects/HeroFreeze",
		"Effects/HeroSpellshield",
		"Effects/HeroStealth",
		"Effects/MinionDeathrattle",
		"Effects/MinionDivineShield",
		"Effects/MinionFreeze",
		"Effects/MinionInspire",
		"Effects/MinionPoison",
		"Effects/MinionSilence",
		"Effects/MinionSpellshield",
		"Effects/MinionStealth",
		"Effects/MinionTrigger",
		"Effects/WeaponDeathrattle",
		"Effects/WeaponTrigger",
		"Generic"
	};
}
