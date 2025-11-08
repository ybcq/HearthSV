using System;
using System.Collections;

public abstract class CharacterController : BaseController
{
	public abstract void AnimateDelevitate();

	public abstract IEnumerator DelevitateAnimation();

	public abstract void AnimateLevitateWait();

	public abstract IEnumerator LevitateWaitAnimation();

	public abstract void AnimateAttack(Character target, int enemyDamage, int selfDamage);

	public abstract IEnumerator AttackAnimation(Character target, int enemyDamage, int selfDamage);

	public abstract void AnimateDestroy();

	public abstract IEnumerator DestroyAnimation();

	public abstract Character GetCharacter();
}
