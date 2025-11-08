//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Hidden/Internal-DeferredReflections" {
Properties {
_SrcBlend ("", Float) = 1
_DstBlend ("", Float) = 1
}
SubShader {
 Pass {
  ZClip Off
  ZWrite Off
  GpuProgramID 2208
Program "vp" {
}
Program "fp" {
}
}
 Pass {
  ZClip Off
  ZTest Always
  ZWrite Off
  GpuProgramID 128161
Program "vp" {
}
Program "fp" {
}
}
}
}