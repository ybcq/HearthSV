//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Hidden/Internal-ScreenSpaceShadows" {
Properties {
_ShadowMapTexture ("", any) = "" { }
}
SubShader {
 Tags { "ShadowmapFilter" = "HardShadow" }
 Pass {
  Tags { "ShadowmapFilter" = "HardShadow" }
  ZClip Off
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 27348
Program "vp" {
SubProgram "d3d9 " {
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_SPLIT_SPHERES" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_SINGLE_CASCADE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_SPLIT_SPHERES" "SHADOWS_SINGLE_CASCADE" }
"// shader disassembly not supported on DXBC"
}
}
Program "fp" {
SubProgram "d3d9 " {
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_SPLIT_SPHERES" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_SINGLE_CASCADE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_SPLIT_SPHERES" "SHADOWS_SINGLE_CASCADE" }
"// shader disassembly not supported on DXBC"
}
}
}
}
SubShader {
 Tags { "ShadowmapFilter" = "HardShadow_FORCE_INV_PROJECTION_IN_PS" }
 Pass {
  Tags { "ShadowmapFilter" = "HardShadow_FORCE_INV_PROJECTION_IN_PS" }
  ZClip Off
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 127620
Program "vp" {
SubProgram "d3d9 " {
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_SPLIT_SPHERES" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_SINGLE_CASCADE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_SPLIT_SPHERES" "SHADOWS_SINGLE_CASCADE" }
"// shader disassembly not supported on DXBC"
}
}
Program "fp" {
SubProgram "d3d9 " {
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_SPLIT_SPHERES" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_SINGLE_CASCADE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_SPLIT_SPHERES" "SHADOWS_SINGLE_CASCADE" }
"// shader disassembly not supported on DXBC"
}
}
}
}
SubShader {
 Tags { "ShadowmapFilter" = "PCF_5x5" }
 Pass {
  Tags { "ShadowmapFilter" = "PCF_5x5" }
  ZClip Off
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 164474
Program "vp" {
SubProgram "d3d9 " {
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_SPLIT_SPHERES" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_SINGLE_CASCADE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_SPLIT_SPHERES" "SHADOWS_SINGLE_CASCADE" }
"// shader disassembly not supported on DXBC"
}
}
Program "fp" {
SubProgram "d3d9 " {
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_SPLIT_SPHERES" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_SINGLE_CASCADE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_SPLIT_SPHERES" "SHADOWS_SINGLE_CASCADE" }
"// shader disassembly not supported on DXBC"
}
}
}
}
SubShader {
 Tags { "ShadowmapFilter" = "PCF_5x5_FORCE_INV_PROJECTION_IN_PS" }
 Pass {
  Tags { "ShadowmapFilter" = "PCF_5x5_FORCE_INV_PROJECTION_IN_PS" }
  ZClip Off
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 234566
Program "vp" {
SubProgram "d3d9 " {
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_SPLIT_SPHERES" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_SINGLE_CASCADE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_SPLIT_SPHERES" "SHADOWS_SINGLE_CASCADE" }
"// shader disassembly not supported on DXBC"
}
}
Program "fp" {
SubProgram "d3d9 " {
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_SPLIT_SPHERES" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_SINGLE_CASCADE" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_SPLIT_SPHERES" "SHADOWS_SINGLE_CASCADE" }
"// shader disassembly not supported on DXBC"
}
}
}
}
}