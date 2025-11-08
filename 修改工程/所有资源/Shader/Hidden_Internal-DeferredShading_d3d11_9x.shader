//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Hidden/Internal-DeferredShading" {
Properties {
_LightTexture0 ("", any) = "" { }
_LightTextureB0 ("", 2D) = "" { }
_ShadowMapTexture ("", any) = "" { }
_SrcBlend ("", Float) = 1
_DstBlend ("", Float) = 1
}
SubShader {
 Pass {
  Tags { "SHADOWSUPPORT" = "true" }
  ZClip Off
  ZWrite Off
  GpuProgramID 26031
Program "vp" {
}
Program "fp" {
}
}
 Pass {
  ZClip Off
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 103316
Program "vp" {
}
Program "fp" {
}
}
}
}