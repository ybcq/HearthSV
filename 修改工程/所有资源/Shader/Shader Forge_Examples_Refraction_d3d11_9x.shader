//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Shader Forge/Examples/Refraction" {
Properties {
_Opacity ("Opacity", Range(0, 1)) = 1
_Color ("Color", Color) = (0,0,0,1)
_Metallic ("Metallic", Float) = 0.5
_Gloss ("Gloss", Float) = 0.5
_Cutoff ("Alpha cutoff", Range(0, 1)) = 0.5
}
SubShader {
 Tags { "QUEUE" = "Transparent" "RenderType" = "Transparent" }
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE" = "ForwardBase" "QUEUE" = "Transparent" "RenderType" = "Transparent" "SHADOWSUPPORT" = "true" }
  ZClip Off
  ZWrite Off
  Cull Off
  GpuProgramID 33641
Program "vp" {
}
Program "fp" {
}
}
 Pass {
  Name "FORWARD_DELTA"
  Tags { "LIGHTMODE" = "ForwardAdd" "QUEUE" = "Transparent" "RenderType" = "Transparent" "SHADOWSUPPORT" = "true" }
  ZClip Off
  ZWrite Off
  Cull Off
  GpuProgramID 104596
Program "vp" {
}
Program "fp" {
}
}
}
Fallback "Diffuse"
CustomEditor "ShaderForgeMaterialInspector"
}