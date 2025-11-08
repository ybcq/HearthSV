using System;
using System.Collections;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
	private EffectsManager()
	{
	}

	public static EffectsManager Instance
	{
		get
		{
			return EffectsManager._instance;
		}
	}

	private void Awake()
	{
		EffectsManager._instance = this;
	}

	public IEnumerator ShowSelfFatigue(int value)
	{
		yield return this.ShowFatigue("Self", value);
		yield break;
	}

	public IEnumerator ShowEnemyFatigue(int value)
	{
		yield return this.ShowFatigue("Enemy", value);
		yield break;
	}

	private IEnumerator ShowFatigue(string trigger, int value)
	{
		GameObject fatigueObject = UnityEngine.Object.Instantiate<GameObject>(this.FatiguePrefab);
		SoundManager.Instance.Play("Game_Draw_Fatigue");
		fatigueObject.GetComponentInChildren<TextMesh>().text = "没有牌了！你受到了" + value + "\n点伤害.";
		fatigueObject.GetComponent<Animator>().SetTrigger(trigger);
		yield return new WaitForSeconds(3.35f);
		SoundManager.Instance.Play("Game_Draw_FatigueStrike");
		yield return new WaitForSeconds(0.65f);
		UnityEngine.Object.Destroy(fatigueObject);
		yield break;
	}

	private static EffectsManager _instance;

	public GameObject MinionDeathrattlePrefab;

	public GameObject MinionDivineShieldPrefab;

	public GameObject MinionEvasionPrefab;

	public GameObject MinionFreezePrefab;

	public GameObject MinionImmunePrefab;

	public GameObject MinionInspirePrefab;

	public GameObject MinionPoisonPrefab;

	public GameObject MinionSilencePrefab;

	public GameObject MinionSpellshieldPrefab;

	public GameObject MinionStealthPrefab;

	public GameObject MinionTriggerPrefab;

	public GameObject MinionTriggerFlashPrefab;

	public GameObject HeroEvasionPrefab;

	public GameObject HeroFreezePrefab;

	public GameObject HeroImmunePrefab;

	public GameObject HeroSpellshieldPrefab;

	public GameObject HeroStealthPrefab;

	public GameObject PresencePrefab;

	public GameObject WeaponDeathrattlePrefab;

	public GameObject WeaponTriggerPrefab;

	public GameObject WeaponTriggerFlashPrefab;

	public GameObject FatiguePrefab;
}
