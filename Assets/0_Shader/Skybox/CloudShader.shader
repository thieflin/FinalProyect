// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "CloudShader"
{
	Properties
	{
		_Vertex("Vertex", Float) = 0
		_Sub("Sub", Range( 0 , 0.5)) = 0
		_sub2("sub2", Range( 0 , 0.5)) = 0
		_Tilling("Tilling", Float) = 1
		_Tilling2("Tilling2", Float) = 1
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Multiplay("Multiplay", Color) = (0,0,0,0)
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_Vector0("Vector 0", Vector) = (0,0.1,0,0)
		_Velocidad2("Velocidad2", Vector) = (0,0.1,0,0)
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
		#pragma target 4.6
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _TextureSample0;
		uniform float3 _Vector0;
		uniform float _Tilling;
		uniform float _Sub;
		uniform sampler2D _TextureSample1;
		uniform float3 _Velocidad2;
		uniform float _Tilling2;
		uniform float _sub2;
		uniform float _Vertex;
		uniform float4 _Multiplay;


		struct Gradient
		{
			int type;
			int colorsLength;
			int alphasLength;
			float4 colors[8];
			float2 alphas[8];
		};


		Gradient NewGradient(int type, int colorsLength, int alphasLength, 
		float4 colors0, float4 colors1, float4 colors2, float4 colors3, float4 colors4, float4 colors5, float4 colors6, float4 colors7,
		float2 alphas0, float2 alphas1, float2 alphas2, float2 alphas3, float2 alphas4, float2 alphas5, float2 alphas6, float2 alphas7)
		{
			Gradient g;
			g.type = type;
			g.colorsLength = colorsLength;
			g.alphasLength = alphasLength;
			g.colors[ 0 ] = colors0;
			g.colors[ 1 ] = colors1;
			g.colors[ 2 ] = colors2;
			g.colors[ 3 ] = colors3;
			g.colors[ 4 ] = colors4;
			g.colors[ 5 ] = colors5;
			g.colors[ 6 ] = colors6;
			g.colors[ 7 ] = colors7;
			g.alphas[ 0 ] = alphas0;
			g.alphas[ 1 ] = alphas1;
			g.alphas[ 2 ] = alphas2;
			g.alphas[ 3 ] = alphas3;
			g.alphas[ 4 ] = alphas4;
			g.alphas[ 5 ] = alphas5;
			g.alphas[ 6 ] = alphas6;
			g.alphas[ 7 ] = alphas7;
			return g;
		}


		float4 SampleGradient( Gradient gradient, float time )
		{
			float3 color = gradient.colors[0].rgb;
			UNITY_UNROLL
			for (int c = 1; c < 8; c++)
			{
			float colorPos = saturate((time - gradient.colors[c-1].w) / ( 0.00001 + (gradient.colors[c].w - gradient.colors[c-1].w)) * step(c, (float)gradient.colorsLength-1));
			color = lerp(color, gradient.colors[c].rgb, lerp(colorPos, step(0.01, colorPos), gradient.type));
			}
			#ifndef UNITY_COLORSPACE_GAMMA
			color = half3(GammaToLinearSpaceExact(color.r), GammaToLinearSpaceExact(color.g), GammaToLinearSpaceExact(color.b));
			#endif
			float alpha = gradient.alphas[0].x;
			UNITY_UNROLL
			for (int a = 1; a < 8; a++)
			{
			float alphaPos = saturate((time - gradient.alphas[a-1].y) / ( 0.00001 + (gradient.alphas[a].y - gradient.alphas[a-1].y)) * step(a, (float)gradient.alphasLength-1));
			alpha = lerp(alpha, gradient.alphas[a].x, lerp(alphaPos, step(0.01, alphaPos), gradient.type));
			}
			return float4(color, alpha);
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float2 temp_cast_1 = (_Tilling).xx;
			float2 uv_TexCoord12 = v.texcoord.xy * temp_cast_1;
			float2 panner15 = ( 1.0 * _Time.y * _Vector0.xy + uv_TexCoord12);
			float4 temp_cast_2 = (_Sub).xxxx;
			float2 temp_cast_4 = (_Tilling2).xx;
			float2 uv_TexCoord44 = v.texcoord.xy * temp_cast_4;
			float2 panner45 = ( 1.0 * _Time.y * _Velocidad2.xy + uv_TexCoord44);
			float4 temp_cast_5 = (_sub2).xxxx;
			float4 temp_output_41_0 = ( ( tex2Dlod( _TextureSample0, float4( panner15, 0, 0.0) ) - temp_cast_2 ) + ( tex2Dlod( _TextureSample1, float4( panner45, 0, 0.0) ) - temp_cast_5 ) );
			v.vertex.xyz += saturate( ( temp_output_41_0 * float4( float3(0,0.5,0) , 0.0 ) * _Vertex ) ).rgb;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			Gradient gradient47 = NewGradient( 0, 4, 2, float4( 0.1336329, 0.2414071, 0.3679245, 0 ), float4( 0.137371, 0.304208, 0.4622642, 0.4088197 ), float4( 0.4485582, 0.7138407, 0.9056604, 0.8882429 ), float4( 0.6876557, 0.8454977, 0.9528302, 1 ), 0, 0, 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
			float2 temp_cast_1 = (_Tilling).xx;
			float2 uv_TexCoord12 = i.uv_texcoord * temp_cast_1;
			float2 panner15 = ( 1.0 * _Time.y * _Vector0.xy + uv_TexCoord12);
			float4 temp_cast_2 = (_Sub).xxxx;
			float2 temp_cast_4 = (_Tilling2).xx;
			float2 uv_TexCoord44 = i.uv_texcoord * temp_cast_4;
			float2 panner45 = ( 1.0 * _Time.y * _Velocidad2.xy + uv_TexCoord44);
			float4 temp_cast_5 = (_sub2).xxxx;
			float4 temp_output_41_0 = ( ( tex2D( _TextureSample0, panner15 ) - temp_cast_2 ) + ( tex2D( _TextureSample1, panner45 ) - temp_cast_5 ) );
			float4 temp_output_25_0 = saturate( temp_output_41_0 );
			o.Albedo = saturate( ( SampleGradient( gradient47, temp_output_25_0.r ) * _Multiplay ) ).rgb;
			o.Alpha = temp_output_25_0.r;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 4.6
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
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
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
0;73;879;681;2507.983;2064.606;4.802055;True;False
Node;AmplifyShaderEditor.RangedFloatNode;42;-1334.26,-300.3934;Inherit;False;Property;_Tilling2;Tilling2;4;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-1303.25,-606.9218;Inherit;False;Property;_Tilling;Tilling;3;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;44;-1207.231,-320.0403;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;43;-1156.889,-197.1776;Inherit;False;Property;_Velocidad2;Velocidad2;9;0;Create;True;0;0;0;False;0;False;0,0.1,0;0.02,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TextureCoordinatesNode;12;-1175.869,-615.1805;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;16;-1130.997,-495.0521;Inherit;False;Property;_Vector0;Vector 0;8;0;Create;True;0;0;0;False;0;False;0,0.1,0;0.01,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.PannerNode;45;-975.5965,-300.9778;Inherit;True;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;15;-961.167,-562.6009;Inherit;True;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;20;-717.6687,-580.8812;Inherit;True;Property;_TextureSample0;Texture Sample 0;5;0;Create;True;0;0;0;False;0;False;-1;None;04d1ce46b4de6414282eec5e6da971d0;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;35;-680.6567,-380.9085;Inherit;False;Property;_Sub;Sub;1;0;Create;True;0;0;0;False;0;False;0;0.22;0;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;40;-714.9126,-313.4625;Inherit;True;Property;_TextureSample1;Texture Sample 1;7;0;Create;True;0;0;0;False;0;False;-1;04d1ce46b4de6414282eec5e6da971d0;3de8a22a9e2cd534e9583f61a09a10b3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;50;-696.3335,-115.119;Inherit;False;Property;_sub2;sub2;2;0;Create;True;0;0;0;False;0;False;0;0.14;0;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;51;-383.9731,-274.6272;Inherit;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;34;-380.569,-497.699;Inherit;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;41;-220.1811,-291.1711;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;25;-43.65556,-297.5359;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GradientNode;47;-139.6964,-604.3061;Inherit;False;0;4;2;0.1336329,0.2414071,0.3679245,0;0.137371,0.304208,0.4622642,0.4088197;0.4485582,0.7138407,0.9056604,0.8882429;0.6876557,0.8454977,0.9528302,1;1,0;1,1;0;1;OBJECT;0
Node;AmplifyShaderEditor.Vector3Node;23;-135.2801,-13.76302;Inherit;False;Constant;_Vector1;Vector 1;4;0;Create;True;0;0;0;False;0;False;0,0.5,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;24;-97.94,162.2409;Inherit;False;Property;_Vertex;Vertex;0;0;Create;True;0;0;0;False;0;False;0;0.36;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GradientSampleNode;48;76.32402,-604.1329;Inherit;True;2;0;OBJECT;;False;1;FLOAT;0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;54;159.655,-420.1478;Inherit;False;Property;_Multiplay;Multiplay;6;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.9245283,0.9245283,0.9245283,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;100.8052,16.17821;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;53;408.1515,-605.6245;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;27;234.3347,16.58471;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;49;547.0071,-600.2634;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;870.6635,-236.0305;Float;False;True;-1;6;ASEMaterialInspector;0;0;Standard;CloudShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;50;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;44;0;42;0
WireConnection;12;0;18;0
WireConnection;45;0;44;0
WireConnection;45;2;43;0
WireConnection;15;0;12;0
WireConnection;15;2;16;0
WireConnection;20;1;15;0
WireConnection;40;1;45;0
WireConnection;51;0;40;0
WireConnection;51;1;50;0
WireConnection;34;0;20;0
WireConnection;34;1;35;0
WireConnection;41;0;34;0
WireConnection;41;1;51;0
WireConnection;25;0;41;0
WireConnection;48;0;47;0
WireConnection;48;1;25;0
WireConnection;22;0;41;0
WireConnection;22;1;23;0
WireConnection;22;2;24;0
WireConnection;53;0;48;0
WireConnection;53;1;54;0
WireConnection;27;0;22;0
WireConnection;49;0;53;0
WireConnection;0;0;49;0
WireConnection;0;9;25;0
WireConnection;0;11;27;0
ASEEND*/
//CHKSM=742FF4BC943D8AB610BCF5ED24F81A393793F8E9