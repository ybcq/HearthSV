//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Standard" {
Properties {
_Color ("Color", Color) = (1,1,1,1)
_MainTex ("Albedo", 2D) = "white" { }
_Cutoff ("Alpha Cutoff", Range(0, 1)) = 0.5
_Glossiness ("Smoothness", Range(0, 1)) = 0.5
_GlossMapScale ("Smoothness Scale", Range(0, 1)) = 1
[Enum(Metallic Alpha,0,Albedo Alpha,1)] _SmoothnessTextureChannel ("Smoothness texture channel", Float) = 0
_Metallic ("Metallic", Range(0, 1)) = 0
_MetallicGlossMap ("Metallic", 2D) = "white" { }
[ToggleOff] _SpecularHighlights ("Specular Highlights", Float) = 1
[ToggleOff] _GlossyReflections ("Glossy Reflections", Float) = 1
_BumpScale ("Scale", Float) = 1
_BumpMap ("Normal Map", 2D) = "bump" { }
_Parallax ("Height Scale", Range(0.005, 0.08)) = 0.02
_ParallaxMap ("Height Map", 2D) = "black" { }
_OcclusionStrength ("Strength", Range(0, 1)) = 1
_OcclusionMap ("Occlusion", 2D) = "white" { }
_EmissionColor ("Color", Color) = (0,0,0,1)
_EmissionMap ("Emission", 2D) = "white" { }
_DetailMask ("Detail Mask", 2D) = "white" { }
_DetailAlbedoMap ("Detail Albedo x2", 2D) = "grey" { }
_DetailNormalMapScale ("Scale", Float) = 1
_DetailNormalMap ("Normal Map", 2D) = "bump" { }
[Enum(UV0,0,UV1,1)] _UVSec ("UV Set for secondary textures", Float) = 0
_Mode ("__mode", Float) = 0
_SrcBlend ("__src", Float) = 1
_DstBlend ("__dst", Float) = 0
_ZWrite ("__zw", Float) = 1
}
SubShader {
 LOD 300
 Tags { "PerformanceChecks" = "False" "RenderType" = "Opaque" }
 Pass {
  Name "FORWARD"
  LOD 300
  Tags { "LIGHTMODE" = "ForwardBase" "PerformanceChecks" = "False" "RenderType" = "Opaque" "SHADOWSUPPORT" = "true" }
  ZClip Off
  ZWrite Off
  GpuProgramID 58821
Program "vp" {
}
Program "fp" {
}
}
 Pass {
  Name "FORWARD_DELTA"
  LOD 300
  Tags { "LIGHTMODE" = "ForwardAdd" "PerformanceChecks" = "False" "RenderType" = "Opaque" "SHADOWSUPPORT" = "true" }
  ZClip Off
  ZWrite Off
  GpuProgramID 112610
Program "vp" {
}
Program "fp" {
}
}
 Pass {
  Name "SHADOWCASTER"
  LOD 300
  Tags { "LIGHTMODE" = "SHADOWCASTER" "PerformanceChecks" = "False" "RenderType" = "Opaque" "SHADOWSUPPORT" = "true" }
  ZClip Off
  GpuProgramID 131847
Program "vp" {
}
Program "fp" {
}
}
 Pass {
  Name "DEFERRED"
  LOD 300
  Tags { "LIGHTMODE" = "Deferred" "PerformanceChecks" = "False" "RenderType" = "Opaque" }
  ZClip Off
  GpuProgramID 240401
Program "vp" {
}
Program "fp" {
}
}
 Pass {
  Name "META"
  LOD 300
  Tags { "LIGHTMODE" = "Meta" "PerformanceChecks" = "False" "RenderType" = "Opaque" }
  ZClip Off
  Cull Off
  GpuProgramID 294171
Program "vp" {
SubProgram "d3d11_9x " {
"// shader disassembly not supported on DXBC"
}
}
Program "fp" {
SubProgram "d3d11_9x " {
"// shader disassembly not supported on DXBC"
}
}
}
}
SubShader {
 LOD 150
 Tags { "PerformanceChecks" = "False" "RenderType" = "Opaque" }
 Pass {
  Name "FORWARD"
  LOD 150
  Tags { "LIGHTMODE" = "ForwardBase" "PerformanceChecks" = "False" "RenderType" = "Opaque" "SHADOWSUPPORT" = "true" }
  ZClip Off
  ZWrite Off
  GpuProgramID 354211
Program "vp" {
SubProgram "d3d11_9x " {
Keywords { "DIRECTIONAL" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11_9x " {
Keywords { "DIRECTIONAL" "VERTEXLIGHT_ON" }
"// shader disassembly not supported on DXBC"
}
}
Program "fp" {
SubProgram "d3d11_9x " {
Keywords { "DIRECTIONAL" }
"// shader disassembly not supported on DXBC"
}
}
}
 Pass {
  Name "FORWARD_DELTA"
  LOD 150
  Tags { "LIGHTMODE" = "ForwardAdd" "PerformanceChecks" = "False" "RenderType" = "Opaque" "SHADOWSUPPORT" = "true" }
  ZClip Off
  ZWrite Off
  GpuProgramID 452275
Program "vp" {
SubProgram "d3d11_9x " {
Keywords { "POINT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11_9x " {
Keywords { "DIRECTIONAL" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11_9x " {
Keywords { "SPOT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11_9x " {
Keywords { "POINT_COOKIE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11_9x " {
Keywords { "DIRECTIONAL_COOKIE" }
"// shader disassembly not supported on DXBC"
}
}
Program "fp" {
SubProgram "d3d11_9x " {
Keywords { "POINT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11_9x " {
Keywords { "DIRECTIONAL" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11_9x " {
Keywords { "SPOT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11_9x " {
Keywords { "POINT_COOKIE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11_9x " {
Keywords { "DIRECTIONAL_COOKIE" }
"// shader disassembly not supported on DXBC"
}
}
}
 Pass {
  Name "SHADOWCASTER"
  LOD 150
  Tags { "LIGHTMODE" = "SHADOWCASTER" "PerformanceChecks" = "False" "RenderType" = "Opaque" "SHADOWSUPPORT" = "true" }
  ZClip Off
  GpuProgramID 523244
Program "vp" {
}
Program "fp" {
}
}
 Pass {
  Name "META"
  LOD 150
  Tags { "LIGHTMODE" = "Meta" "PerformanceChecks" = "False" "RenderType" = "Opaque" }
  ZClip Off
  Cull Off
  GpuProgramID 529642
Program "vp" {
SubProgram "d3d11_9x " {
"// shader disassembly not supported on DXBC"
}
}
Program "fp" {
SubProgram "d3d11_9x " {
"// shader disassembly not supported on DXBC"
}
}
}
}
Fallback "VertexLit"
CustomEditor "StandardShaderGUI"
}