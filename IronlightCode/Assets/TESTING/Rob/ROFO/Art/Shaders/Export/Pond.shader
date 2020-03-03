// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Pond"
{
	Properties
	{
		_Albedo("Albedo", 2D) = "white" {}
		_Normal("Normal", 2D) = "white" {}
		_NormalIntensity("Normal Intensity", Range( 0 , 10)) = 0
		_Emission("Emission", 2D) = "white" {}
		_EmissionColour("Emission Colour", Color) = (0,0,0,0)
		_EmissionIntensity("Emission Intensity", Range( 0 , 5)) = 1.424822
		_Depth("Depth", Range( 0 , 5)) = 0
		_RefractNormal("Refract Normal", 2D) = "white" {}
		_RefractNormalIntensity("Refract Normal Intensity", Range( 0 , 10)) = 0
		_RefractAmount("RefractAmount", Range( 0 , 20)) = 0.1
		_RefractColour("Refract Colour", Color) = (0,0,0,0)
		_RefractColourIntensity("Refract Colour Intensity", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		GrabPass{ }
		CGPROGRAM
		#include "UnityCG.cginc"
		#pragma target 4.6
		#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex);
		#else
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex)
		#endif
		#pragma surface surf Standard alpha:fade keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform float _NormalIntensity;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		ASE_DECLARE_SCREENSPACE_TEXTURE( _GrabTexture )
		uniform float _RefractAmount;
		uniform sampler2D _RefractNormal;
		uniform float4 _RefractNormal_ST;
		uniform float _RefractNormalIntensity;
		uniform float4 _RefractColour;
		uniform float _RefractColourIntensity;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _Depth;
		uniform sampler2D _Emission;
		uniform float4 _Emission_ST;
		uniform float _EmissionIntensity;
		uniform float4 _EmissionColour;


		inline float4 ASE_ComputeGrabScreenPos( float4 pos )
		{
			#if UNITY_UV_STARTS_AT_TOP
			float scale = -1.0;
			#else
			float scale = 1.0;
			#endif
			float4 o = pos;
			o.y = pos.w * 0.5f;
			o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
			return o;
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			float3 tex2DNode67 = UnpackNormal( tex2D( _Normal, uv_Normal ) );
			float4 appendResult71 = (float4(( (tex2DNode67).xy * _NormalIntensity ) , tex2DNode67.b , 0.0));
			float4 Normal72 = appendResult71;
			o.Normal = Normal72.xyz;
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float4 Albedo17 = tex2D( _Albedo, uv_Albedo );
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( ase_screenPos );
			float4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
			float2 uv_RefractNormal = i.uv_texcoord * _RefractNormal_ST.xy + _RefractNormal_ST.zw;
			float3 tex2DNode78 = UnpackNormal( tex2D( _RefractNormal, uv_RefractNormal ) );
			float4 appendResult80 = (float4(( (tex2DNode78).xy * _RefractNormalIntensity ) , tex2DNode78.b , 0.0));
			float4 screenColor28 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,( float4( (ase_grabScreenPosNorm).xy, 0.0 , 0.0 ) + ( _RefractAmount * appendResult80 ) ).xy);
			float4 clampResult29 = clamp( screenColor28 , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float4 Refraction90 = clampResult29;
			float4 lerpResult131 = lerp( Refraction90 , _RefractColour , _RefractColourIntensity);
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth44 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth44 = abs( ( screenDepth44 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _Depth ) );
			float clampResult46 = clamp( ( 1.0 - distanceDepth44 ) , 0.0 , 1.0 );
			float Depth47 = clampResult46;
			float4 lerpResult74 = lerp( Albedo17 , lerpResult131 , Depth47);
			o.Albedo = lerpResult74.rgb;
			float2 uv_Emission = i.uv_texcoord * _Emission_ST.xy + _Emission_ST.zw;
			float4 Emission15 = ( tex2D( _Emission, uv_Emission ) * _EmissionIntensity * _EmissionColour );
			float4 temp_output_149_0 = Emission15;
			o.Emission = temp_output_149_0.rgb;
			float clampResult115 = clamp( exp( ( 1.0 - saturate( Depth47 ) ) ) , 0.0 , 1.0 );
			float Opacity98 = clampResult115;
			o.Alpha = Opacity98;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17500
0;0;1368;851;647.5536;314.2529;2.491301;True;False
Node;AmplifyShaderEditor.CommentaryNode;84;-3398.508,1781.981;Inherit;False;2913.385;1101.467;Comment;14;80;78;77;82;81;83;29;28;27;25;26;22;24;90;Refraction;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;42;-2592.421,356.0923;Inherit;False;1651.3;378.0659;Depth;5;47;46;45;44;43;Depth;1,1,1,1;0;0
Node;AmplifyShaderEditor.TexturePropertyNode;77;-3336.302,2248.44;Inherit;True;Property;_RefractNormal;Refract Normal;7;0;Create;True;0;0;False;0;None;ebb6cd8e0a017164ab632b4ea0b90f6b;True;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.RangedFloatNode;43;-2515.676,467.1073;Inherit;False;Property;_Depth;Depth;6;0;Create;True;0;0;False;0;0;1.04;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;78;-3034.302,2247.44;Inherit;True;Property;_TextureSample3;Texture Sample 3;10;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DepthFade;44;-2101.216,449.4804;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;83;-2689.654,2619.834;Inherit;False;Property;_RefractNormalIntensity;Refract Normal Intensity;8;0;Create;True;0;0;False;0;0;0.18;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;81;-2658.094,2411.15;Inherit;False;True;True;False;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;45;-1754.89,449.6865;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;82;-2325.094,2415.15;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;80;-2050.66,2295.813;Inherit;False;FLOAT4;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-2099.767,2033.852;Inherit;False;Property;_RefractAmount;RefractAmount;9;0;Create;True;0;0;False;0;0.1;0.19;0;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.GrabScreenPosition;22;-2089.468,1832.717;Inherit;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;46;-1479.538,449.3939;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;65;-2591.627,-792.8942;Inherit;False;1900.952;799.007;;7;72;71;70;69;68;67;66;Normal;1,1,1,1;0;0
Node;AmplifyShaderEditor.ComponentMaskNode;26;-1784.009,1831.981;Inherit;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-1749.031,2038.344;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;47;-1164.433,444.2064;Inherit;False;Depth;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;117;-210.7507,2542.806;Inherit;False;1691.374;310.1648;Opa;6;109;110;111;98;116;115;Opacity;1,1,1,1;0;0
Node;AmplifyShaderEditor.TexturePropertyNode;66;-2541.627,-742.8942;Inherit;True;Property;_Normal;Normal;1;0;Create;True;0;0;False;0;None;2a7b4c9b0ca26f84bb76c23d1a691037;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.GetLocalVarNode;109;-160.7507,2592.806;Inherit;False;47;Depth;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;27;-1487.302,1927.219;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ScreenColorNode;28;-1242.097,1921.778;Inherit;False;Global;_GrabScreen0;Grab Screen 0;23;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;41;-3381.937,3147.436;Inherit;False;1415.388;744.9944;Comment;6;12;13;40;19;20;15;Emission;1,1,1,1;0;0
Node;AmplifyShaderEditor.SaturateNode;110;129.6307,2597.685;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;67;-2213.84,-740.8431;Inherit;True;Property;_TextureSample2;Texture Sample 2;2;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;69;-1873.741,-363.8873;Inherit;False;Property;_NormalIntensity;Normal Intensity;2;0;Create;True;0;0;False;0;0;0.96;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;6;1585.304,1214.988;Inherit;True;Property;_Albedo;Albedo;0;0;Create;True;0;0;False;0;None;4e9f338126105ab4c882fc8f8a14df42;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.OneMinusNode;111;385.6273,2597.941;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;29;-1031.19,1926.866;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TexturePropertyNode;12;-3331.937,3199.506;Inherit;True;Property;_Emission;Emission;3;0;Create;True;0;0;False;0;None;cba10a92f655f5642b4589f5c5f64765;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.ComponentMaskNode;68;-1842.182,-572.5714;Inherit;False;True;True;False;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;90;-721.7781,1921.906;Inherit;False;Refraction;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;70;-1509.182,-568.5714;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;7;1940.682,1216.208;Inherit;True;Property;_TextureSample0;Texture Sample 0;5;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;13;-3053.268,3199.336;Inherit;True;Property;_TextureSample1;Texture Sample 1;9;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;19;-3033.25,3411.082;Inherit;False;Property;_EmissionIntensity;Emission Intensity;5;0;Create;True;0;0;False;0;1.424822;1.39;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.ExpOpNode;116;654.1931,2599.119;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;40;-2964.402,3648.431;Inherit;False;Property;_EmissionColour;Emission Colour;4;0;Create;True;0;0;False;0;0,0,0,0;0,0.6497359,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-2558.99,3203.162;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;115;927.0844,2599.119;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;127;17.65693,-282.8623;Inherit;False;Property;_RefractColour;Refract Colour;12;0;Create;True;0;0;False;0;0,0,0,0;0.8018868,0.688574,0.3290762,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;76;12.88987,-490.6969;Inherit;False;90;Refraction;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;71;-1234.748,-687.908;Inherit;False;FLOAT4;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;128;-39.16537,-90.20746;Inherit;False;Property;_RefractColourIntensity;Refract Colour Intensity;13;0;Create;True;0;0;False;0;0;0.16;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;17;2321.856,1216.998;Inherit;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;75;655.7695,-98.134;Inherit;False;47;Depth;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;131;381.2838,-300.7374;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;73;652.1796,-508.0721;Inherit;False;17;Albedo;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;98;1246.623,2594.971;Inherit;False;Opacity;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;72;-919.6744,-695.4435;Inherit;True;Normal;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;15;-2239.735,3197.436;Inherit;False;Emission;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;79;1380.384,-134.1356;Inherit;False;72;Normal;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;119;-504.103,-739.9946;Inherit;False;Property;_DepthOneRange;Depth One Range;10;0;Create;True;0;0;False;0;0;0.5881748;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;89;1729.415,421.6668;Inherit;False;98;Opacity;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCCompareLower;120;-156.1883,-947.0037;Inherit;False;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;126;205.6432,-1267.085;Inherit;False;17;Albedo;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;149;532.2747,580.9611;Inherit;False;15;Emission;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;123;200.4244,-1056.597;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;121;-69.20957,-1213.158;Inherit;False;Property;_Test1;Test1;11;0;Create;True;0;0;False;0;0.3066038,1,0.9642429,0;0.3066038,1,0.9642429,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;118;-504.8287,-956.2224;Inherit;False;47;Depth;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;154;534.1649,814.9053;Inherit;False;47;Depth;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;74;990.8905,-318.1667;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;155;980.7085,581.9749;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2117.36,-161.4979;Float;False;True;-1;6;ASEMaterialInspector;0;0;Standard;Pond;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;78;0;77;0
WireConnection;44;0;43;0
WireConnection;81;0;78;0
WireConnection;45;0;44;0
WireConnection;82;0;81;0
WireConnection;82;1;83;0
WireConnection;80;0;82;0
WireConnection;80;2;78;3
WireConnection;46;0;45;0
WireConnection;26;0;22;0
WireConnection;25;0;24;0
WireConnection;25;1;80;0
WireConnection;47;0;46;0
WireConnection;27;0;26;0
WireConnection;27;1;25;0
WireConnection;28;0;27;0
WireConnection;110;0;109;0
WireConnection;67;0;66;0
WireConnection;111;0;110;0
WireConnection;29;0;28;0
WireConnection;68;0;67;0
WireConnection;90;0;29;0
WireConnection;70;0;68;0
WireConnection;70;1;69;0
WireConnection;7;0;6;0
WireConnection;13;0;12;0
WireConnection;116;0;111;0
WireConnection;20;0;13;0
WireConnection;20;1;19;0
WireConnection;20;2;40;0
WireConnection;115;0;116;0
WireConnection;71;0;70;0
WireConnection;71;2;67;3
WireConnection;17;0;7;0
WireConnection;131;0;76;0
WireConnection;131;1;127;0
WireConnection;131;2;128;0
WireConnection;98;0;115;0
WireConnection;72;0;71;0
WireConnection;15;0;20;0
WireConnection;120;0;118;0
WireConnection;120;1;119;0
WireConnection;120;3;118;0
WireConnection;123;0;121;0
WireConnection;123;1;120;0
WireConnection;74;0;73;0
WireConnection;74;1;131;0
WireConnection;74;2;75;0
WireConnection;155;0;149;0
WireConnection;155;2;154;0
WireConnection;0;0;74;0
WireConnection;0;1;79;0
WireConnection;0;2;149;0
WireConnection;0;9;89;0
ASEEND*/
//CHKSM=23B85B410F880D6B712A566701D88F88938D9512