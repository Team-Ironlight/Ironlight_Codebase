// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "JohnLauron/Templates/Legacy/Particles Alpha Blended"
{
	Properties
	{
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
		_Emission("Emission", Float) = 2
		[Toggle]_UseCenterGlow("Use Center Glow?", Float) = 0
		_Opacity("Opacity", Range( 0 , 1)) = 1
		_TextureSample0("MainTex", 2D) = "white" {}
		_TextureSample1("Noise", 2D) = "white" {}
		_Color0("Color 0", Color) = (0.5019608,0.5019608,0.5019608,1)
		_SpeedMainTexUVNoiseZW("Speed MainTex U/V + Noise Z/W", Vector) = (0,0,0,0)
		_TextureSample2("Flow", 2D) = "white" {}
		_TextureSample3("Texture Sample 3", 2D) = "white" {}
		_DistortionSpeedXYPowerZ("Distortion Speed XY Power Z", Vector) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}


	Category 
	{
		SubShader
		{
			Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMask RGB
			Cull Off
			Lighting Off 
			ZWrite Off
			ZTest LEqual
			
			Pass {
			
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0
				#pragma multi_compile_particles
				#pragma multi_compile_fog
				#include "UnityShaderVariables.cginc"


				#include "UnityCG.cginc"

				struct appdata_t 
				{
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
					
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
				uniform float _UseCenterGlow;
				uniform sampler2D _TextureSample0;
				uniform float4 _SpeedMainTexUVNoiseZW;
				uniform float4 _TextureSample0_ST;
				uniform sampler2D _TextureSample2;
				uniform float4 _DistortionSpeedXYPowerZ;
				uniform float4 _TextureSample2_ST;
				uniform sampler2D _TextureSample3;
				uniform float4 _TextureSample3_ST;
				uniform sampler2D _TextureSample1;
				uniform float4 _TextureSample1_ST;
				uniform float4 _Color0;
				uniform float _Emission;
				uniform float _Opacity;

				v2f vert ( appdata_t v  )
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					

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
					#ifdef SOFTPARTICLES_ON
						float sceneZ = LinearEyeDepth (SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
						float partZ = i.projPos.z;
						float fade = saturate (_InvFade * (sceneZ-partZ));
						i.color.a *= fade;
					#endif

					float2 appendResult23 = (float2(_SpeedMainTexUVNoiseZW.x , _SpeedMainTexUVNoiseZW.y));
					float2 uv0_TextureSample0 = i.texcoord.xy * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
					float2 panner20 = ( 1.0 * _Time.y * appendResult23 + uv0_TextureSample0);
					float2 appendResult36 = (float2(_DistortionSpeedXYPowerZ.x , _DistortionSpeedXYPowerZ.y));
					float3 uv0_TextureSample2 = i.texcoord.xyz;
					uv0_TextureSample2.xy = i.texcoord.xyz.xy * _TextureSample2_ST.xy + _TextureSample2_ST.zw;
					float2 panner34 = ( 1.0 * _Time.y * appendResult36 + (uv0_TextureSample2).xyz.xy);
					float2 uv_TextureSample3 = i.texcoord.xy * _TextureSample3_ST.xy + _TextureSample3_ST.zw;
					float4 tex2DNode33 = tex2D( _TextureSample3, uv_TextureSample3 );
					float4 tex2DNode10 = tex2D( _TextureSample0, ( float4( panner20, 0.0 , 0.0 ) - ( (( tex2D( _TextureSample2, panner34 ) * tex2DNode33 )).rgba * float4( 0,0,0,0 ) ) ).rg );
					float2 appendResult24 = (float2(_SpeedMainTexUVNoiseZW.z , _SpeedMainTexUVNoiseZW.w));
					float2 uv0_TextureSample1 = i.texcoord.xy * _TextureSample1_ST.xy + _TextureSample1_ST.zw;
					float2 panner21 = ( 1.0 * _Time.y * appendResult24 + uv0_TextureSample1);
					float4 tex2DNode11 = tex2D( _TextureSample1, panner21 );
					float4 temp_output_7_0 = (( tex2DNode10 * tex2DNode11 * _Color0 * i.color )).rgba;
					float4 temp_cast_3 = ((0.0 + (uv0_TextureSample2.z - 0.0) * (1.0 - 0.0) / (1.0 - 0.0))).xxxx;
					float4 clampResult16 = clamp( ( tex2DNode33 * ( tex2DNode33 - temp_cast_3 ) ) , float4( 0,0,0,0 ) , float4( 1,0,0,0 ) );
					float4 temp_output_2_0 = ( lerp(temp_output_7_0,( temp_output_7_0 * (clampResult16).rgba ),_UseCenterGlow) * _Emission );
					float4 appendResult1 = (float4(temp_output_2_0.r , temp_output_2_0.r , temp_output_2_0.r , ( tex2DNode10.a * tex2DNode11.a * _Color0.a * i.color.a * _Opacity )));
					

					fixed4 col = appendResult1;
					UNITY_APPLY_FOG(i.fogCoord, col);
					return col;
				}
				ENDCG 
			}
		}	
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16400
-1920;0;1920;1019;8528.531;2100.49;5.99133;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;38;-4172.026,992.6552;Float;False;0;32;3;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;39;-4336.021,661.9966;Float;False;Property;_DistortionSpeedXYPowerZ;Distortion Speed XY Power Z;9;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;36;-3830.392,757.1609;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;35;-3876.826,644.389;Float;False;True;True;True;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.PannerNode;34;-3569.417,677.5912;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;32;-3354.166,649.4718;Float;True;Property;_TextureSample2;Flow;7;0;Create;False;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;33;-3350.043,856.2452;Float;True;Property;_TextureSample3;Texture Sample 3;8;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-2962.458,652.3593;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector4Node;26;-3448.821,-139.4776;Float;False;Property;_SpeedMainTexUVNoiseZW;Speed MainTex U/V + Noise Z/W;6;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;22;-2999.884,-305.0213;Float;False;0;10;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;23;-2906.096,-155.7259;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;29;-2791.996,644.4969;Float;False;True;True;True;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-2552.792,649.9854;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;19;-2391.313,1670.232;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;24;-2904.181,106.4982;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;25;-2996.057,-33.22697;Float;False;0;11;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;20;-2578.796,-251.4281;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;14;-2067.327,-147.2028;Float;False;2;0;FLOAT2;0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;18;-2125.349,1616.863;Float;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.PannerNode;21;-2576.881,-35.14104;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;11;-1800.767,49.09912;Float;True;Property;_TextureSample1;Noise;4;0;Create;False;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;12;-1759.439,283.2879;Float;False;Property;_Color0;Color 0;5;0;Create;True;0;0;False;0;0.5019608,0.5019608,0.5019608,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;10;-1806.321,-168.8395;Float;True;Property;_TextureSample0;MainTex;3;0;Create;False;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-1865.547,1493.427;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;13;-1712.725,506.8176;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;16;-1674.212,1482.698;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-1228.109,-55.8246;Float;False;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ComponentMaskNode;7;-1012.787,-18.97101;Float;False;True;True;True;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ComponentMaskNode;15;-1466.784,1486.275;Float;False;True;True;True;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-785.7875,86.02899;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-851.2942,595.5651;Float;False;Property;_Opacity;Opacity;2;0;Create;True;0;0;False;0;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-530.0846,125.2966;Float;False;Property;_Emission;Emission;0;0;Create;True;0;0;False;0;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;4;-611.0846,-17.7034;Float;False;Property;_UseCenterGlow;Use Center Glow?;1;0;Create;True;0;0;False;0;0;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-530.2339,340.1209;Float;False;5;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-340.0845,2.296692;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;37;-3875.17,896.4676;Float;False;myVarName;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;1;-165.1707,1.646708;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;31;-2773.842,753.6452;Float;False;-1;;1;0;OBJECT;0;False;1;OBJECT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;7;JohnLauron/Templates/Legacy/Particles Alpha Blended;0b6a9f8b4f707c74ca64c0be8e590de0;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;2;False;-1;True;True;True;True;False;0;False;-1;False;True;2;False;-1;True;3;False;-1;False;True;4;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;False;0;False;False;False;False;False;False;False;False;False;False;True;0;0;;0;0;Standard;0;0;1;True;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;36;0;39;1
WireConnection;36;1;39;2
WireConnection;35;0;38;0
WireConnection;34;0;35;0
WireConnection;34;2;36;0
WireConnection;32;1;34;0
WireConnection;30;0;32;0
WireConnection;30;1;33;0
WireConnection;23;0;26;1
WireConnection;23;1;26;2
WireConnection;29;0;30;0
WireConnection;28;0;29;0
WireConnection;19;0;38;3
WireConnection;24;0;26;3
WireConnection;24;1;26;4
WireConnection;20;0;22;0
WireConnection;20;2;23;0
WireConnection;14;0;20;0
WireConnection;14;1;28;0
WireConnection;18;0;33;0
WireConnection;18;1;19;0
WireConnection;21;0;25;0
WireConnection;21;2;24;0
WireConnection;11;1;21;0
WireConnection;10;1;14;0
WireConnection;17;0;33;0
WireConnection;17;1;18;0
WireConnection;16;0;17;0
WireConnection;8;0;10;0
WireConnection;8;1;11;0
WireConnection;8;2;12;0
WireConnection;8;3;13;0
WireConnection;7;0;8;0
WireConnection;15;0;16;0
WireConnection;6;0;7;0
WireConnection;6;1;15;0
WireConnection;4;0;7;0
WireConnection;4;1;6;0
WireConnection;5;0;10;4
WireConnection;5;1;11;4
WireConnection;5;2;12;4
WireConnection;5;3;13;4
WireConnection;5;4;9;0
WireConnection;2;0;4;0
WireConnection;2;1;3;0
WireConnection;37;0;39;3
WireConnection;1;0;2;0
WireConnection;1;1;2;0
WireConnection;1;2;2;0
WireConnection;1;3;5;0
WireConnection;0;0;1;0
ASEEND*/
//CHKSM=A743E501B79AFE63007DBC5C6609B0C493680F09