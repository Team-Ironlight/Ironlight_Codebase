// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BurningShader"
{
	Properties
	{
		_Mask("Mask", 2D) = "white" {}
		_DistortionMap("DistortionMap", 2D) = "white" {}
		_DistortionAmount("DistortionAmount", Range( 0 , 1)) = 1
		_ScrollSpeed("ScrollSpeed", Range( 0 , 2)) = 0
		_Warm("Warm", Color) = (0,0,0,0)
		_Hot("Hot", Color) = (0,0,0,0)
		_Power("Power", Float) = 1
		_Step("Step", Range( 0 , 1)) = 0
		_MainTexture("MainTexture", 2D) = "white" {}
		_Wave("Wave", Range( 0 , 1)) = 0
		_TexturesCom_Rock_Porous_1K_normal1("TexturesCom_Rock_Porous_1K_normal (1)", 2D) = "bump" {}
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_XY("XY", Vector) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		AlphaToMask On
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _TexturesCom_Rock_Porous_1K_normal1;
		uniform float2 _XY;
		uniform sampler2D _MainTexture;
		uniform float4 _Warm;
		uniform float4 _Hot;
		uniform sampler2D _Mask;
		uniform sampler2D _DistortionMap;
		uniform float _DistortionAmount;
		uniform float _ScrollSpeed;
		uniform float _Power;
		uniform float _Wave;
		uniform float _Step;
		uniform float _Metallic;
		uniform float _Smoothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 panner49 = ( 1.0 * _Time.y * _XY + i.uv_texcoord);
			o.Normal = UnpackNormal( tex2D( _TexturesCom_Rock_Porous_1K_normal1, panner49 ) );
			o.Albedo = tex2D( _MainTexture, panner49 ).rgb;
			float2 panner46 = ( 1.0 * _Time.y * _XY + i.uv_texcoord);
			float2 temp_cast_1 = (_ScrollSpeed).xx;
			float2 panner12 = ( 1.0 * _Time.y * temp_cast_1 + float2( 0,0 ));
			float2 uv_TexCoord9 = i.uv_texcoord + panner12;
			float4 lerpResult19 = lerp( _Warm , _Hot , tex2D( _Mask, ( ( (UnpackNormal( tex2D( _DistortionMap, panner46 ) )).xy * _DistortionAmount ) + uv_TexCoord9 ) ).r);
			float4 temp_cast_2 = (_Power).xxxx;
			float2 panner35 = ( 1.0 * _Time.y * _XY + i.uv_texcoord);
			float4 tex2DNode23 = tex2D( _Mask, ( ( (UnpackNormal( tex2D( _DistortionMap, panner35 ) )).xy * _Wave ) + i.uv_texcoord ) );
			float temp_output_25_0 = step( tex2DNode23.r , _Step );
			o.Emission = ( ( pow( lerpResult19 , temp_cast_2 ) * _Power ) * ( temp_output_25_0 + ( temp_output_25_0 - step( tex2DNode23.r , ( _Step / 1.1 ) ) ) ) ).rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			AlphaToMask Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
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
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float4 tSpace0 : TEXCOORD3;
				float4 tSpace1 : TEXCOORD4;
				float4 tSpace2 : TEXCOORD5;
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
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17500
0;0;1368;851;3695.283;1673.953;4.468977;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;36;-1888.795,659.0991;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;47;-1785.129,124.991;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;52;-1476.432,-848.272;Inherit;False;Property;_XY;XY;13;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;46;-1485.176,87.9843;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;4;-1708.516,-238.7284;Inherit;True;Property;_DistortionMap;DistortionMap;1;0;Create;True;0;0;False;0;None;f7e96904e8667e1439548f0f86389447;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.PannerNode;35;-1578.001,659.3757;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;5;-1239.818,-95.2617;Inherit;True;Property;_TextureSample1;Texture Sample 1;3;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;15;-991.3717,222.8581;Inherit;False;Property;_ScrollSpeed;ScrollSpeed;3;0;Create;True;0;0;False;0;0;0.27;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;34;-1250.504,629.6075;Inherit;True;Property;_TextureSample3;Texture Sample 3;10;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;8;-937.3253,7.30228;Inherit;False;Property;_DistortionAmount;DistortionAmount;2;0;Create;True;0;0;False;0;1;0.76;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;12;-676.9126,206.5948;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;6;-877.2233,-94.64092;Inherit;False;True;True;False;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;37;-856.5605,630.574;Inherit;False;True;True;False;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-887.6093,868.7305;Inherit;False;Property;_Wave;Wave;9;0;Create;True;0;0;False;0;0;0.08;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;11;-443.0853,6.489622;Inherit;False;483.0511;312.3378;guarentees that we at least get our omal map;2;10;9;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-570.3541,-87.78633;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;40;-513.5654,896.7751;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;9;-393.0853,162.8273;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;-490.6003,645.9667;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;31;159.2815,654.5151;Inherit;False;Constant;_DivideAmount;DivideAmount;10;0;Create;True;0;0;False;0;1.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;41;-164.1802,645.1453;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;2;-255.4412,-283.6757;Inherit;True;Property;_Mask;Mask;0;0;Create;True;0;0;False;0;f5c9fe7efb4ab2f49b92845fedf6cd00;f5c9fe7efb4ab2f49b92845fedf6cd00;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.RangedFloatNode;26;153.369,407.845;Inherit;False;Property;_Step;Step;7;0;Create;True;0;0;False;0;0;0.61;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;10;-112.0341,56.48962;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;23;160.6343,162.6687;Inherit;True;Property;_TextureSample2;Texture Sample 2;8;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;30;542.0222,432.8299;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;17;156.8556,-588.4726;Inherit;False;Property;_Warm;Warm;4;0;Create;True;0;0;False;0;0,0,0,0;1,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;18;165.5222,-399.4742;Inherit;False;Property;_Hot;Hot;5;0;Create;True;0;0;False;0;0,0,0,0;1,0.6628425,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;156.8315,-165.0205;Inherit;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;25;546.5754,169.9896;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;19;486.1859,-467.1404;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;22;596.1246,-203.401;Inherit;False;Property;_Power;Power;6;0;Create;True;0;0;False;0;1;1.99;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;29;808.0051,347.4259;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;48;912.8639,-832.6123;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;32;1072.202,226.7687;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;20;803.3499,-387.9169;Inherit;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;1067.349,-316.9492;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;33;1318.807,141.7003;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;49;1231.731,-879.0756;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;28;1542.834,-597.4094;Inherit;True;Property;_MainTexture;MainTexture;8;0;Create;True;0;0;False;0;-1;None;fd81e0290e232ea4592fb734173c77d5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;43;1540.633,-383.1629;Inherit;True;Property;_TexturesCom_Rock_Porous_1K_normal1;TexturesCom_Rock_Porous_1K_normal (1);10;0;Create;True;0;0;False;0;-1;ebb6cd8e0a017164ab632b4ea0b90f6b;ebb6cd8e0a017164ab632b4ea0b90f6b;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;1617.649,-134.2888;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;44;1619.142,116.4449;Inherit;False;Property;_Metallic;Metallic;11;0;Create;True;0;0;False;0;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;45;1621.918,355.1902;Inherit;False;Property;_Smoothness;Smoothness;12;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1948.96,-234.5735;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;BurningShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;True;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;True;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;46;0;47;0
WireConnection;46;2;52;0
WireConnection;35;0;36;0
WireConnection;35;2;52;0
WireConnection;5;0;4;0
WireConnection;5;1;46;0
WireConnection;34;0;4;0
WireConnection;34;1;35;0
WireConnection;12;2;15;0
WireConnection;6;0;5;0
WireConnection;37;0;34;0
WireConnection;7;0;6;0
WireConnection;7;1;8;0
WireConnection;9;1;12;0
WireConnection;38;0;37;0
WireConnection;38;1;39;0
WireConnection;41;0;38;0
WireConnection;41;1;40;0
WireConnection;10;0;7;0
WireConnection;10;1;9;0
WireConnection;23;0;2;0
WireConnection;23;1;41;0
WireConnection;30;0;26;0
WireConnection;30;1;31;0
WireConnection;3;0;2;0
WireConnection;3;1;10;0
WireConnection;25;0;23;1
WireConnection;25;1;26;0
WireConnection;19;0;17;0
WireConnection;19;1;18;0
WireConnection;19;2;3;1
WireConnection;29;0;23;1
WireConnection;29;1;30;0
WireConnection;32;0;25;0
WireConnection;32;1;29;0
WireConnection;20;0;19;0
WireConnection;20;1;22;0
WireConnection;21;0;20;0
WireConnection;21;1;22;0
WireConnection;33;0;25;0
WireConnection;33;1;32;0
WireConnection;49;0;48;0
WireConnection;49;2;52;0
WireConnection;28;1;49;0
WireConnection;43;1;49;0
WireConnection;27;0;21;0
WireConnection;27;1;33;0
WireConnection;0;0;28;0
WireConnection;0;1;43;0
WireConnection;0;2;27;0
WireConnection;0;3;44;0
WireConnection;0;4;45;0
ASEEND*/
//CHKSM=306BE18088B53A3347D5AF77D312B23251AF10B3