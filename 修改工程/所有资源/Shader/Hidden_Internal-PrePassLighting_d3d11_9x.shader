//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Hidden/Internal-PrePassLighting" {
Properties {
_LightTexture0 ("", any) = "" { }
_LightTextureB0 ("", 2D) = "" { }
_ShadowMapTexture ("", any) = "" { }
}
SubShader {
 Pass {
  Tags { "SHADOWSUPPORT" = "true" }
  ZClip Off
  ZWrite Off
  GpuProgramID 13514
Program "vp" {
}
Program "fp" {
}
}
 Pass {
  Tags { "SHADOWSUPPORT" = "true" }
  ZClip Off
  ZWrite Off
  GpuProgramID 86331
Program "vp" {
}
Program "fp" {
}
}
}
}