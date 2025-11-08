//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Hidden/FrameDebuggerRenderTargetDisplay" {
Properties {
_MainTex ("", any) = "white" { }
}
SubShader {
 Tags { "ForceSupported" = "true" }
 Pass {
  Tags { "ForceSupported" = "true" }
  ZClip Off
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 50908
Program "vp" {
SubProgram "glcore " {
"#ifdef VERTEX
#version 150
#extension GL_ARB_explicit_attrib_location : require
#extension GL_ARB_shader_bit_encoding : enable

uniform 	vec4 hlslcc_mtx4x4glstate_matrix_mvp[4];
in  vec4 in_POSITION0;
in  vec3 in_TEXCOORD0;
out vec3 vs_TEXCOORD0;
vec4 u_xlat0;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4glstate_matrix_mvp[1];
    u_xlat0 = hlslcc_mtx4x4glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = hlslcc_mtx4x4glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
    gl_Position = u_xlat0 + hlslcc_mtx4x4glstate_matrix_mvp[3];
    vs_TEXCOORD0.xyz = in_TEXCOORD0.xyz;
    return;
}

#endif
#ifdef FRAGMENT
#version 150
#extension GL_ARB_explicit_attrib_location : require
#extension GL_ARB_shader_bit_encoding : enable

uniform 	vec4 _Channels;
uniform 	vec4 _Levels;
uniform  sampler2D _MainTex;
in  vec3 vs_TEXCOORD0;
layout(location = 0) out vec4 SV_Target0;
vec4 u_xlat0;
lowp vec4 u_xlat10_0;
float u_xlat1;
float u_xlat3;
bool u_xlatb3;
void main()
{
    u_xlat10_0 = texture(_MainTex, vs_TEXCOORD0.xy);
    u_xlat0 = u_xlat10_0 + (-_Levels.xxxx);
    u_xlat1 = (-_Levels.x) + _Levels.y;
    u_xlat0 = u_xlat0 / vec4(u_xlat1);
    u_xlat0 = u_xlat0 * _Channels;
    u_xlat1 = dot(u_xlat0, vec4(1.0, 1.0, 1.0, 1.0));
    u_xlat3 = dot(_Channels, vec4(1.0, 1.0, 1.0, 1.0));
    u_xlatb3 = u_xlat3==1.0;
    SV_Target0 = (bool(u_xlatb3)) ? vec4(u_xlat1) : u_xlat0;
    return;
}

#endif
"
}
}
Program "fp" {
SubProgram "glcore " {
""
}
}
}
 Pass {
  Tags { "ForceSupported" = "true" }
  ZClip Off
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 106630
Program "vp" {
SubProgram "glcore " {
"#ifdef VERTEX
#version 150
#extension GL_ARB_explicit_attrib_location : require
#extension GL_ARB_shader_bit_encoding : enable

uniform 	vec4 hlslcc_mtx4x4glstate_matrix_mvp[4];
in  vec4 in_POSITION0;
in  vec3 in_TEXCOORD0;
out vec3 vs_TEXCOORD0;
vec4 u_xlat0;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4glstate_matrix_mvp[1];
    u_xlat0 = hlslcc_mtx4x4glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = hlslcc_mtx4x4glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
    gl_Position = u_xlat0 + hlslcc_mtx4x4glstate_matrix_mvp[3];
    vs_TEXCOORD0.xyz = in_TEXCOORD0.xyz;
    return;
}

#endif
#ifdef FRAGMENT
#version 150
#extension GL_ARB_explicit_attrib_location : require
#extension GL_ARB_shader_bit_encoding : enable

uniform 	vec4 _Channels;
uniform 	vec4 _Levels;
uniform  samplerCube _MainTex;
in  vec3 vs_TEXCOORD0;
layout(location = 0) out vec4 SV_Target0;
vec4 u_xlat0;
lowp vec4 u_xlat10_0;
float u_xlat1;
float u_xlat3;
bool u_xlatb3;
void main()
{
    u_xlat10_0 = texture(_MainTex, vs_TEXCOORD0.xyz);
    u_xlat0 = u_xlat10_0 + (-_Levels.xxxx);
    u_xlat1 = (-_Levels.x) + _Levels.y;
    u_xlat0 = u_xlat0 / vec4(u_xlat1);
    u_xlat0 = u_xlat0 * _Channels;
    u_xlat1 = dot(u_xlat0, vec4(1.0, 1.0, 1.0, 1.0));
    u_xlat3 = dot(_Channels, vec4(1.0, 1.0, 1.0, 1.0));
    u_xlatb3 = u_xlat3==1.0;
    SV_Target0 = (bool(u_xlatb3)) ? vec4(u_xlat1) : u_xlat0;
    return;
}

#endif
"
}
}
Program "fp" {
SubProgram "glcore " {
""
}
}
}
}
}