using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[ExecuteInEditMode]
	[AddComponentMenu("Image Effects/Color Adjustments/Grayscale")]
	public class Grayscale : ImageEffectBase
	{
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.material.SetTexture("_RampTex", this.textureRamp);
			base.material.SetFloat("_RampOffset", this.rampOffset);
			base.material.SetFloat("_Amount", this.amount);
			Graphics.Blit(source, destination, base.material);
		}

		public Texture textureRamp;

		[Range(-1f, 1f)]
		public float rampOffset;

		[Range(0f, 1f)]
		public float amount;
	}
}
