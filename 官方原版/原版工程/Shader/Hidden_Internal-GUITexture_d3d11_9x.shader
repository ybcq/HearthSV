//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Hidden/Internal-GUITexture" {
Properties {
_MainTex ("Texture", any) = "" { }
}
SubShader {
 Tags { "ForceSupported" = "true" "RenderType" = "Overlay" }
 Pass {
  Tags { "ForceSupported" = "true" "RenderType" = "Overlay" }
  ZClip Off
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 11810
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