// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/WallCutout"
{
	Properties
	{
		_Albedo("Albedo", 2D) = "white" {}
		_Normal("Normal", 2D) = "white" {}
		_SmoothBorder("Smooth Border", Range( 0 , 1)) = 0
		_CutoutSizee("Cutout Sizee", Range( 0 , 1)) = 1
		_Opacity("Opacity", Range( 0 , 1)) = 0
		_PlayerPosition("PlayerPosition", Vector) = (0,0,0,0)
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_CenterCutout("CenterCutout", Vector) = (0.03,-0.32,0,0)
		_Metallic("Metallic", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 4.0
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float _Metallic;
		uniform float _Smoothness;
		uniform float _SmoothBorder;
		uniform float3 _CenterCutout;
		uniform float3 _PlayerPosition;
		uniform float _CutoutSizee;
		uniform float _Opacity;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			o.Normal = saturate( tex2D( _Normal, uv_Normal ) ).rgb;
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			o.Albedo = saturate( tex2D( _Albedo, uv_Albedo ) ).rgb;
			o.Metallic = saturate( _Metallic );
			o.Smoothness = saturate( _Smoothness );
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float4 ScreenPosition429 = ase_screenPosNorm;
			float Width435 = ( ( _ScreenParams.y / _ScreenParams.x ) * _CutoutSizee );
			float clampResult426 = clamp( _CutoutSizee , 0.0 , 1.0 );
			float Height433 = clampResult426;
			float2 appendResult4_g2 = (float2(Width435 , Height433));
			float smoothstepResult384 = smoothstep( 0.0 , _SmoothBorder , saturate( ( 1.0 - length( ( ((( float4( _CenterCutout , 0.0 ) + ScreenPosition429 )*1.0 + ( float4( _PlayerPosition , 0.0 ) + ScreenPosition429 )).xy*2.0 + -1.0) / appendResult4_g2 ) ) ) ));
			float Opacity437 = saturate( ( 1.0 - ( smoothstepResult384 * _Opacity ) ) );
			o.Alpha = Opacity437;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows exclude_path:deferred 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 4.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float4 screenPos : TEXCOORD3;
				float4 tSpace0 : TEXCOORD4;
				float4 tSpace1 : TEXCOORD5;
				float4 tSpace2 : TEXCOORD6;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.screenPos = ComputeScreenPos( o.pos );
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.screenPos = IN.screenPos;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
0;73;1008;682;3400.945;995.1097;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;447;-2929.971,-852.3972;Inherit;False;1987.903;1148.01;;19;427;384;195;388;222;430;421;431;226;423;434;432;387;436;373;374;437;448;452;Mascara y Posicion;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;449;-2046.032,-98.51666;Inherit;False;459.9185;262.4907;;2;220;429;Variables;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;448;-2894.44,-116.2936;Inherit;False;808.3242;385.2599;Comment;7;303;435;433;426;229;230;231;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ScreenParams;303;-2839.632,-61.99695;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenPosInputsNode;220;-1996.033,-48.026;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;429;-1812.115,-48.51676;Inherit;False;ScreenPosition;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;229;-2657.313,-60.62357;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;231;-2844.44,104.1283;Float;False;Property;_CutoutSizee;Cutout Sizee;3;0;Create;True;0;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;426;-2504.163,107.9662;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;452;-2880.618,-491.7236;Inherit;False;Property;_PlayerPosition;PlayerPosition;5;0;Create;True;0;0;0;False;0;False;0,0,0;-0.485082,-0.581287,-13.973;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;421;-2874.49,-718.2174;Inherit;False;Property;_CenterCutout;CenterCutout;7;0;Create;True;0;0;0;False;0;False;0.03,-0.32,0;0.03,0.12,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;230;-2443.79,-62.19049;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;430;-2860.024,-338.8219;Inherit;False;429;ScreenPosition;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;431;-2874.129,-567.9095;Inherit;False;429;ScreenPosition;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleAddOpNode;423;-2577.48,-587.3091;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;435;-2310.116,-66.29361;Inherit;False;Width;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;433;-2302.076,103.6852;Inherit;False;Height;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;226;-2581.223,-487.4165;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;432;-2327.299,-371.4359;Inherit;False;435;Width;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;434;-2327.937,-294.3152;Inherit;False;433;Height;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;222;-2404.528,-591.8674;Inherit;True;3;0;FLOAT4;0,0,0,0;False;1;FLOAT;1;False;2;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;195;-2062.96,-375.7837;Inherit;False;Property;_SmoothBorder;Smooth Border;2;0;Create;True;0;0;0;False;0;False;0;0.4;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;388;-2063.204,-302.4924;Inherit;False;Property;_Opacity;Opacity;4;0;Create;True;0;0;0;False;0;False;0;0.9;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;427;-2037.979,-590.4906;Inherit;True;ScreenSphereMask;-1;;2;ec4daf865ef039f4c8b13a0738be21e2;0;3;13;FLOAT2;0,0;False;7;FLOAT;0.5;False;6;FLOAT;0.5;False;1;FLOAT;3
Node;AmplifyShaderEditor.SmoothstepOpNode;384;-1785.095,-589.5153;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;436;-1647.254,-508.7672;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;387;-1620.565,-591.3671;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;373;-1486.288,-590.2449;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;374;-1322.036,-590.2739;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;450;-895.8528,-861.6038;Inherit;False;551.0276;626.7863;;8;442;441;443;444;27;439;440;1;Texturas y valores;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;1;-845.8528,-811.6038;Inherit;True;Property;_Albedo;Albedo;0;0;Create;True;0;0;0;False;0;False;-1;e6998966218df754cbb41fc1c187c33c;e6998966218df754cbb41fc1c187c33c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;440;-838.5037,-623.6258;Inherit;True;Property;_Normal;Normal;1;0;Create;True;0;0;0;False;0;False;-1;e6998966218df754cbb41fc1c187c33c;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;441;-813.7906,-421.1313;Inherit;False;Property;_Metallic;Metallic;8;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;437;-1166.068,-596.3912;Inherit;False;Opacity;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;442;-813.4422,-350.8176;Inherit;False;Property;_Smoothness;Smoothness;6;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;443;-509.8252,-415.9453;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;439;-552.3491,-618.9663;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;444;-510.0352,-347.4171;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;438;-452.1994,-227.5099;Inherit;False;437;Opacity;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;27;-555.9982,-806.6443;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;37;-287.931,-468.5215;Float;False;True;-1;4;ASEMaterialInspector;0;0;Standard;Custom/WallCutout;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;ForwardOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;429;0;220;0
WireConnection;229;0;303;2
WireConnection;229;1;303;1
WireConnection;426;0;231;0
WireConnection;230;0;229;0
WireConnection;230;1;231;0
WireConnection;423;0;421;0
WireConnection;423;1;431;0
WireConnection;435;0;230;0
WireConnection;433;0;426;0
WireConnection;226;0;452;0
WireConnection;226;1;430;0
WireConnection;222;0;423;0
WireConnection;222;2;226;0
WireConnection;427;13;222;0
WireConnection;427;7;432;0
WireConnection;427;6;434;0
WireConnection;384;0;427;3
WireConnection;384;2;195;0
WireConnection;436;0;388;0
WireConnection;387;0;384;0
WireConnection;387;1;436;0
WireConnection;373;0;387;0
WireConnection;374;0;373;0
WireConnection;437;0;374;0
WireConnection;443;0;441;0
WireConnection;439;0;440;0
WireConnection;444;0;442;0
WireConnection;27;0;1;0
WireConnection;37;0;27;0
WireConnection;37;1;439;0
WireConnection;37;3;443;0
WireConnection;37;4;444;0
WireConnection;37;9;438;0
ASEEND*/
//CHKSM=A6A2A833D3507A534B787841EA0E201B93DBC987