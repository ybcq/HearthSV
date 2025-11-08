//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Unlit/Transparent" {
Properties {
_MainTex ("Base (RGB) Trans (A)", 2D) = "white" { }
}
SubShader {
 LOD 100
 Tags { "IGNOREPROJECTOR" = "true" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
 Pass {
  LOD 100
  Tags { "IGNOREPROJECTOR" = "true" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
  ZClip Off
  ZWrite Off
  GpuProgramID 8419
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
}