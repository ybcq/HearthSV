//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Legacy Shaders/VertexLit" {
Properties {
_Color ("Main Color", Color) = (1,1,1,1)
_SpecColor ("Spec Color", Color) = (1,1,1,1)
_Emission ("Emissive Color", Color) = (0,0,0,0)
_Shininess ("Shininess", Range(0.01, 1)) = 0.7
_MainTex ("Base (RGB)", 2D) = "white" { }
}
SubShader {
 LOD 100
 Tags { "RenderType" = "Opaque" }
 Pass {
  LOD 100
  Tags { "LIGHTMODE" = "Vertex" "RenderType" = "Opaque" }
  ZClip Off
  GpuProgramID 84581
Program "vp" {
SubProgram "d3d11_9x " {
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11_9x " {
Keywords { "POINT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11_9x " {
Keywords { "SPOT" }
"// shader disassembly not supported on DXBC"
}
}
Program "fp" {
SubProgram "d3d11_9x " {
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11_9x " {
Keywords { "POINT" }
"// shader disassembly not supported on DXBC"
}
SubProgram "d3d11_9x " {
Keywords { "SPOT" }
"// shader disassembly not supported on DXBC"
}
}
}
 Pass {
  LOD 100
  Tags { "LIGHTMODE" = "VertexLM" "RenderType" = "Opaque" }
  ZClip Off
  GpuProgramID 175433
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
 Pass {
  LOD 100
  Tags { "LIGHTMODE" = "VertexLMRGBM" "RenderType" = "Opaque" }
  ZClip Off
  GpuProgramID 207922
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
 Pass {
  Name "SHADOWCASTER"
  LOD 100
  Tags { "LIGHTMODE" = "SHADOWCASTER" "RenderType" = "Opaque" "SHADOWSUPPORT" = "true" }
  ZClip Off
  GpuProgramID 37263
Program "vp" {
}
Program "fp" {
}
}
}
}