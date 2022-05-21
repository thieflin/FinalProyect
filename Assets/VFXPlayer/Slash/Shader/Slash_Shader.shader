// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Slash_Shader"
{
	Properties
	{
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
		_Main("Main", 2D) = "white" {}
		_BrilloTextura("Brillo Textura", 2D) = "white" {}
		_Opacidad("Opacidad", Float) = 20
		_Disolvente("Disolvente", 2D) = "white" {}
		_Vector1("Vector 1", Vector) = (0,0,0,0)
		_Emission("Emission", Float) = 2
		_BordesNegroColor("Bordes Negro Color", Color) = (0,0,0,0)
		[ASEEnd]_Desaturacion("Desaturacion", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}


	Category 
	{
		SubShader
		{
		LOD 0

			Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMask RGB
			Cull Off
			Lighting Off 
			ZWrite Off
			ZTest LEqual
			
			Pass {
			
				CGPROGRAM
				
				#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
				#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
				#endif
				
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0
				#pragma multi_compile_instancing
				#pragma multi_compile_particles
				#pragma multi_compile_fog
				#include "UnityShaderVariables.cginc"
				#define ASE_NEEDS_FRAG_COLOR


				#include "UnityCG.cginc"

				struct appdata_t 
				{
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
					float4 ase_texcoord1 : TEXCOORD1;
				};

				struct v2f 
				{
					float4 vertex : SV_POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_FOG_COORDS(1)
					#ifdef SOFTPARTICLES_ON
					float4 projPos : TEXCOORD2;
					#endif
					UNITY_VERTEX_INPUT_INSTANCE_ID
					UNITY_VERTEX_OUTPUT_STEREO
					float4 ase_texcoord3 : TEXCOORD3;
				};
				
				
				#if UNITY_VERSION >= 560
				UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
				#else
				uniform sampler2D_float _CameraDepthTexture;
				#endif

				//Don't delete this comment
				// uniform sampler2D_float _CameraDepthTexture;

				uniform sampler2D _MainTex;
				uniform fixed4 _TintColor;
				uniform float4 _MainTex_ST;
				uniform float _InvFade;
				uniform float4 _BordesNegroColor;
				uniform sampler2D _BrilloTextura;
				uniform float4 _BrilloTextura_ST;
				uniform float _Desaturacion;
				uniform float _Emission;
				uniform sampler2D _Main;
				uniform float4 _Main_ST;
				uniform float _Opacidad;
				uniform sampler2D _Disolvente;
				uniform float4 _Vector1;
				float3 HSVToRGB( float3 c )
				{
					float4 K = float4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
					float3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
					return c.z * lerp( K.xxx, saturate( p - K.xxx ), c.y );
				}
				
				float3 RGBToHSV(float3 c)
				{
					float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
					float4 p = lerp( float4( c.bg, K.wz ), float4( c.gb, K.xy ), step( c.b, c.g ) );
					float4 q = lerp( float4( p.xyw, c.r ), float4( c.r, p.yzx ), step( p.x, c.r ) );
					float d = q.x - min( q.w, q.y );
					float e = 1.0e-10;
					return float3( abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
				}


				v2f vert ( appdata_t v  )
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					o.ase_texcoord3 = v.ase_texcoord1;

					v.vertex.xyz +=  float3( 0, 0, 0 ) ;
					o.vertex = UnityObjectToClipPos(v.vertex);
					#ifdef SOFTPARTICLES_ON
						o.projPos = ComputeScreenPos (o.vertex);
						COMPUTE_EYEDEPTH(o.projPos.z);
					#endif
					o.color = v.color;
					o.texcoord = v.texcoord;
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				fixed4 frag ( v2f i  ) : SV_Target
				{
					UNITY_SETUP_INSTANCE_ID( i );
					UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( i );

					#ifdef SOFTPARTICLES_ON
						float sceneZ = LinearEyeDepth (SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
						float partZ = i.projPos.z;
						float fade = saturate (_InvFade * (sceneZ-partZ));
						i.color.a *= fade;
					#endif

					float2 uv_BrilloTextura = i.texcoord.xy * _BrilloTextura_ST.xy + _BrilloTextura_ST.zw;
					float3 hsvTorgb43 = RGBToHSV( tex2D( _BrilloTextura, uv_BrilloTextura ).rgb );
					float4 texCoord31 = i.ase_texcoord3;
					texCoord31.xy = i.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
					float3 hsvTorgb42 = HSVToRGB( float3(( hsvTorgb43.x + texCoord31.z ),hsvTorgb43.y,hsvTorgb43.z) );
					float3 desaturateInitialColor45 = hsvTorgb42;
					float desaturateDot45 = dot( desaturateInitialColor45, float3( 0.299, 0.587, 0.114 ));
					float3 desaturateVar45 = lerp( desaturateInitialColor45, desaturateDot45.xxx, _Desaturacion );
					float4 _ColorSaturacion = float4(0,1.3,-2,1.5);
					float3 temp_cast_1 = (_ColorSaturacion.x).xxx;
					float3 temp_cast_2 = (_ColorSaturacion.y).xxx;
					float3 temp_cast_3 = (_ColorSaturacion.z).xxx;
					float3 temp_cast_4 = (_ColorSaturacion.w).xxx;
					float3 clampResult36 = clamp( (temp_cast_3 + (desaturateVar45 - temp_cast_1) * (temp_cast_4 - temp_cast_3) / (temp_cast_2 - temp_cast_1)) , float3( 0,0,0 ) , float3( 1,1,1 ) );
					float2 uv_Main = i.texcoord.xy * _Main_ST.xy + _Main_ST.zw;
					float clampResult6 = clamp( ( tex2D( _Main, uv_Main ).a * _Opacidad ) , 0.0 , 1.0 );
					float4 appendResult23 = (float4(_Vector1.z , _Vector1.w , 0.0 , 0.0));
					float4 texCoord16 = i.texcoord;
					texCoord16.xy = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					float2 panner24 = ( 1.0 * _Time.y * appendResult23.xy + texCoord16.xy);
					float2 break28 = panner24;
					float2 appendResult29 = (float2(break28.x , ( texCoord31.w + break28.y )));
					float t18 = texCoord16.w;
					float w17 = texCoord16.z;
					float3 _Vector0 = float3(0.3,0,1);
					float ifLocalVar12 = 0;
					UNITY_BRANCH 
					if( ( tex2D( _Disolvente, appendResult29 ).r * t18 ) >= w17 )
					ifLocalVar12 = _Vector0.y;
					else
					ifLocalVar12 = _Vector0.z;
					float4 appendResult7 = (float4(( ( _BordesNegroColor * i.color ) + ( float4( clampResult36 , 0.0 ) * _Emission * i.color ) ).rgb , ( i.color.a * clampResult6 * ifLocalVar12 )));
					

					fixed4 col = appendResult7;
					UNITY_APPLY_FOG(i.fogCoord, col);
					return col;
				}
				ENDCG 
			}
		}	
	}
	
	
	
}
/*ASEBEGIN
Version=18900
303;73;138;570;582.5581;1330.753;3.982025;False;False
Node;AmplifyShaderEditor.Vector4Node;21;-1748.734,-114.5751;Inherit;False;Property;_Vector1;Vector 1;4;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,1,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;32;-1314.082,-1242.934;Inherit;True;Property;_BrilloTextura;Brillo Textura;1;0;Create;True;0;0;0;False;0;False;-1;None;776495090b22ebc49af55b62923f4a68;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;23;-1392.344,102.4184;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;16;-1419.066,-112.1122;Inherit;False;0;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RGBToHSVNode;43;-987.9342,-1248.066;Float;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TextureCoordinatesNode;31;-1108.848,-357.6801;Inherit;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;24;-1123.553,158.4168;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;44;-755.799,-978.0406;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;28;-906.4003,182.7515;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;46;-588.6029,-939.0697;Inherit;False;Property;_Desaturacion;Desaturacion;7;0;Create;True;0;0;0;False;0;False;0;2.09;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;30;-739.601,258.9891;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;42;-619.1659,-1248.355;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector4Node;35;-473.4182,-827.9747;Inherit;False;Constant;_ColorSaturacion;Color / Saturacion;5;0;Create;True;0;0;0;False;0;False;0,1.3,-2,1.5;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DesaturateOpNode;45;-317.5707,-1055.769;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;29;-638.4669,74.73785;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;18;-1109.012,10.4372;Inherit;False;t;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;20;-452.2419,156.8354;Inherit;True;18;t;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;3;-604.8412,-488.9547;Inherit;True;Property;_Main;Main;0;0;Create;True;0;0;0;False;0;False;-1;None;d2d212ea9b69a8345bd6b7debdf07fcd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;10;-488.4037,-105.7277;Inherit;True;Property;_Disolvente;Disolvente;3;0;Create;True;0;0;0;False;0;False;-1;1eae6143ce30e2849b37136524e197f0;1eae6143ce30e2849b37136524e197f0;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;17;-1116.978,-117.8296;Inherit;False;w;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;33;-190.2988,-858.2087;Inherit;False;5;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;1,1,1;False;3;FLOAT3;0,0,0;False;4;FLOAT3;1,1,1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-317.2143,-233.5129;Float;False;Property;_Opacidad;Opacidad;2;0;Create;True;0;0;0;False;0;False;20;20;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;36;-52.64641,-1015.609;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;1,1,1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-143.2144,-389.5129;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;9;-178.4264,-644.5909;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;38;148.9444,-657.4966;Float;False;Property;_Emission;Emission;5;0;Create;True;0;0;0;False;0;False;2;2.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;15;-164.0435,129.6454;Inherit;False;Constant;_Vector0;Vector 0;3;0;Create;True;0;0;0;False;0;False;0.3,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-145.3164,-51.44374;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;19;-163.0563,55.46178;Inherit;False;17;w;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;39;-74.89348,-1234.301;Inherit;False;Property;_BordesNegroColor;Bordes Negro Color;6;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.4198113,0.949517,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;6;50.78557,-426.5129;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;12;113.1023,-55.77603;Inherit;False;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;197.0445,-916.1964;Inherit;False;3;3;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;192.9068,-1104.301;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;304.5531,-528.7042;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;40;403.5064,-961.3008;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;22;-1362.329,-353.2412;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;7;528.6745,-681.7849;Inherit;True;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;1;772.676,-683.3936;Float;False;True;-1;2;;0;7;Slash_Shader;0b6a9f8b4f707c74ca64c0be8e590de0;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;False;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;True;True;True;True;False;0;False;-1;False;False;False;False;False;False;False;False;False;True;2;False;-1;True;3;False;-1;False;True;4;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;0;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;23;0;21;3
WireConnection;23;1;21;4
WireConnection;43;0;32;0
WireConnection;24;0;16;0
WireConnection;24;2;23;0
WireConnection;44;0;43;1
WireConnection;44;1;31;3
WireConnection;28;0;24;0
WireConnection;30;0;31;4
WireConnection;30;1;28;1
WireConnection;42;0;44;0
WireConnection;42;1;43;2
WireConnection;42;2;43;3
WireConnection;45;0;42;0
WireConnection;45;1;46;0
WireConnection;29;0;28;0
WireConnection;29;1;30;0
WireConnection;18;0;16;4
WireConnection;10;1;29;0
WireConnection;17;0;16;3
WireConnection;33;0;45;0
WireConnection;33;1;35;1
WireConnection;33;2;35;2
WireConnection;33;3;35;3
WireConnection;33;4;35;4
WireConnection;36;0;33;0
WireConnection;4;0;3;4
WireConnection;4;1;5;0
WireConnection;11;0;10;1
WireConnection;11;1;20;0
WireConnection;6;0;4;0
WireConnection;12;0;11;0
WireConnection;12;1;19;0
WireConnection;12;2;15;2
WireConnection;12;3;15;2
WireConnection;12;4;15;3
WireConnection;37;0;36;0
WireConnection;37;1;38;0
WireConnection;37;2;9;0
WireConnection;41;0;39;0
WireConnection;41;1;9;0
WireConnection;8;0;9;4
WireConnection;8;1;6;0
WireConnection;8;2;12;0
WireConnection;40;0;41;0
WireConnection;40;1;37;0
WireConnection;22;0;21;1
WireConnection;22;1;21;2
WireConnection;7;0;40;0
WireConnection;7;3;8;0
WireConnection;1;0;7;0
ASEEND*/
//CHKSM=76F3D2D1BEB774E194E09A19CDAD9937657A5745