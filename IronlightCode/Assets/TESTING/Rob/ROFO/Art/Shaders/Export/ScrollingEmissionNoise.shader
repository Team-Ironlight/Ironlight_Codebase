// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ScrollingEmissionTexture"
{
	Properties
	{
		_Albedo("Albedo", 2D) = "white" {}
		_Normal("Normal", 2D) = "white" {}
		_NormalIntensity("Normal Intensity", Range( 0 , 10)) = 1
		_Emission("Emission", 2D) = "white" {}
		_EmissionColour("EmissionColour", Color) = (0,0,0,0)
		_EmissionIntensity("Emission Intensity", Range( 0 , 10)) = 1
		_EspeedX("EspeedX", Float) = 1
		_EspeedY("EspeedY", Float) = 1
		_Etime("Etime", Float) = 1
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_NoiseContrast("NoiseContrast", Range( 0 , 10)) = 1
		_Speed("Speed", Range( 0 , 100)) = 0
		_NoiseSpeedX("NoiseSpeedX", Float) = 0
		_NoiseSpeedY("NoiseSpeedY", Float) = 0
		_TilingX("TilingX", Float) = 0
		_TilingY("TilingY", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform float _NormalIntensity;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform sampler2D _Emission;
		uniform float _Etime;
		uniform float _EspeedX;
		uniform float _EspeedY;
		uniform float4 _EmissionColour;
		uniform float _EmissionIntensity;
		uniform float _NoiseSpeedX;
		uniform float _NoiseSpeedY;
		uniform float _Speed;
		uniform float _TilingX;
		uniform float _TilingY;
		uniform float _NoiseContrast;
		uniform float _Metallic;
		uniform float _Smoothness;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			float3 tex2DNode42 = UnpackNormal( tex2D( _Normal, uv_Normal ) );
			float4 appendResult48 = (float4(( (tex2DNode42).xy * _NormalIntensity ) , tex2DNode42.b , 0.0));
			float4 Normal49 = appendResult48;
			o.Normal = Normal49.xyz;
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float4 Albedo29 = tex2D( _Albedo, uv_Albedo );
			o.Albedo = Albedo29.rgb;
			float4 appendResult58 = (float4(_EspeedX , _EspeedY , 0.0 , 0.0));
			float2 panner54 = ( ( _Time.y * _Etime * 0.1 ) * appendResult58.xy + i.uv_texcoord);
			float2 Epanner62 = panner54;
			float4 Emission33 = ( tex2D( _Emission, Epanner62 ) * _EmissionColour * _EmissionIntensity );
			float4 appendResult23 = (float4(_NoiseSpeedX , _NoiseSpeedY , 0.0 , 0.0));
			float4 appendResult21 = (float4(_TilingX , _TilingY , 0.0 , 0.0));
			float2 uv_TexCoord17 = i.uv_texcoord * appendResult21.xy;
			float2 panner14 = ( 1.0 * _Time.y * ( appendResult23 * _Speed * 0.1 ).xy + uv_TexCoord17);
			float simplePerlin2D4 = snoise( panner14 );
			simplePerlin2D4 = simplePerlin2D4*0.5 + 0.5;
			float Noise32 = pow( simplePerlin2D4 , _NoiseContrast );
			o.Emission = ( Emission33 * Noise32 ).rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17500
0;0;1368;851;5944.03;1871.654;4.791045;True;False
Node;AmplifyShaderEditor.CommentaryNode;63;-5212.049,969.8859;Inherit;False;1220.763;1618.186;Comment;10;65;60;61;54;62;55;58;56;57;59;Epanner;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;61;-5140.779,2088.708;Inherit;False;Property;_Etime;Etime;8;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;38;-3346.367,906.8702;Inherit;False;2219.61;1354.701;Comment;15;53;18;22;51;52;23;32;24;4;26;14;17;21;19;20;Noise;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;65;-5136.344,2325.964;Inherit;False;Constant;_Float1;Float 1;17;0;Create;True;0;0;False;0;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;57;-5156.732,1578.227;Inherit;False;Property;_EspeedY;EspeedY;7;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;56;-5162.049,1337.165;Inherit;False;Property;_EspeedX;EspeedX;6;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;59;-5147.868,1842.33;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-3275.185,1522.11;Inherit;False;Property;_NoiseSpeedX;NoiseSpeedX;13;0;Create;True;0;0;False;0;0;1.21;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;60;-4910.534,1842.833;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-3158.966,1034.791;Inherit;False;Property;_TilingX;TilingX;15;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;58;-4951.122,1340.71;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;55;-5002.523,1025.204;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;22;-3269.423,1751.535;Inherit;False;Property;_NoiseSpeedY;NoiseSpeedY;14;0;Create;True;0;0;False;0;0;1.68;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-3158.268,1268.102;Inherit;False;Property;_TilingY;TilingY;16;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;66;-1454.924,-1576.729;Inherit;False;1746.725;719.9437;Comment;7;43;42;45;47;46;48;49;Normal;1,1,1,1;0;0
Node;AmplifyShaderEditor.PannerNode;54;-4614.646,1027.077;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;21;-2899.241,1039.385;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;53;-2917.747,2016.504;Inherit;False;Constant;_Float0;Float 0;14;0;Create;True;0;0;False;0;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;51;-3023.592,1770.96;Inherit;False;Property;_Speed;Speed;12;0;Create;True;0;0;False;0;0;7.6;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;23;-2962.101,1524.419;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TexturePropertyNode;43;-1404.924,-1526.729;Inherit;True;Property;_Normal;Normal;1;0;Create;True;0;0;False;0;None;0bebe40e9ebbecc48b8e9cfea982da7e;True;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.CommentaryNode;37;-3332.427,-69.31213;Inherit;False;1333.594;749.9031;Comment;7;33;2;1;7;9;31;64;Emission;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;17;-2663.463,1016.438;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;20,20;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;-2666.412,1520.868;Inherit;False;3;3;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;62;-4291.748,1019.886;Inherit;False;Epanner;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;64;-3261.433,203.921;Inherit;False;62;Epanner;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;14;-2392.279,1016.34;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-1,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;31;-3282.427,-19.31213;Inherit;True;Property;_Emission;Emission;3;0;Create;True;0;0;False;0;None;5798ded558355430c8a9b13ee12a847c;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;42;-1108.269,-1516.411;Inherit;True;Property;_TextureSample1;Texture Sample 1;11;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;47;-769.5548,-1114.785;Inherit;False;Property;_NormalIntensity;Normal Intensity;2;0;Create;True;0;0;False;0;1;0.52;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;45;-738.9117,-1340.959;Inherit;False;True;True;False;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;39;-3318.236,-743.3254;Inherit;False;908.041;412.7313;Comment;3;29;28;27;Albedo;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-2054.306,1263.943;Inherit;False;Property;_NoiseContrast;NoiseContrast;11;0;Create;True;0;0;False;0;1;3.21;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;4;-2025.859,1012.002;Inherit;False;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;7;-2910.819,243.673;Inherit;False;Property;_EmissionColour;EmissionColour;4;0;Create;True;0;0;False;0;0,0,0,0;0,0.7138028,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-3007.953,20.79045;Inherit;True;Property;_EmissionTexture;EmissionTexture;0;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;9;-2910.244,428.591;Inherit;False;Property;_EmissionIntensity;Emission Intensity;5;0;Create;True;0;0;False;0;1;0.6;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;27;-3268.236,-693.3254;Inherit;True;Property;_Albedo;Albedo;0;0;Create;True;0;0;False;0;None;fd81e0290e232ea4592fb734173c77d5;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.PowerNode;24;-1689.484,1018.303;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-2550.352,169.3168;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;46;-461.6677,-1336.581;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;28;-2994.216,-651.7423;Inherit;True;Property;_TextureSample0;Texture Sample 0;10;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;33;-2249.649,166.5023;Inherit;False;Emission;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;32;-1374.778,1010.148;Inherit;False;Noise;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;48;-204.8518,-1463.53;Inherit;False;FLOAT4;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;29;-2644.195,-588.5941;Inherit;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;35;-779.1526,385.3228;Inherit;False;32;Noise;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;34;-790.3443,150.2332;Inherit;False;33;Emission;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;49;57.80101,-1469.366;Inherit;False;Normal;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;40;-395.2375,707.9787;Inherit;False;Property;_Metallic;Metallic;9;0;Create;True;0;0;False;0;0;0.68;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;41;-395.9064,953.1758;Inherit;False;Property;_Smoothness;Smoothness;10;0;Create;True;0;0;False;0;0;0.6;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;50;-485.8662,-76.4971;Inherit;False;49;Normal;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;30;-452.1091,-294.4379;Inherit;False;29;Albedo;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-505.311,154.948;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;ScrollingEmissionTexture;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;60;0;59;0
WireConnection;60;1;61;0
WireConnection;60;2;65;0
WireConnection;58;0;56;0
WireConnection;58;1;57;0
WireConnection;54;0;55;0
WireConnection;54;2;58;0
WireConnection;54;1;60;0
WireConnection;21;0;19;0
WireConnection;21;1;20;0
WireConnection;23;0;18;0
WireConnection;23;1;22;0
WireConnection;17;0;21;0
WireConnection;52;0;23;0
WireConnection;52;1;51;0
WireConnection;52;2;53;0
WireConnection;62;0;54;0
WireConnection;14;0;17;0
WireConnection;14;2;52;0
WireConnection;42;0;43;0
WireConnection;45;0;42;0
WireConnection;4;0;14;0
WireConnection;1;0;31;0
WireConnection;1;1;64;0
WireConnection;24;0;4;0
WireConnection;24;1;26;0
WireConnection;2;0;1;0
WireConnection;2;1;7;0
WireConnection;2;2;9;0
WireConnection;46;0;45;0
WireConnection;46;1;47;0
WireConnection;28;0;27;0
WireConnection;33;0;2;0
WireConnection;32;0;24;0
WireConnection;48;0;46;0
WireConnection;48;2;42;3
WireConnection;29;0;28;0
WireConnection;49;0;48;0
WireConnection;36;0;34;0
WireConnection;36;1;35;0
WireConnection;0;0;30;0
WireConnection;0;1;50;0
WireConnection;0;2;36;0
WireConnection;0;3;40;0
WireConnection;0;4;41;0
ASEEND*/
//CHKSM=147F57A623F5106CF481F56EBA1DFC7FAF60B959