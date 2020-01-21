// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Aurora"
{
	Properties
	{
		_MainTexture("MainTexture", 2D) = "white" {}
		_Offset("Offset", Float) = 0
		_WaveSpeed("Wave Speed", Float) = 0
		_SmallLinesSpeed("Small Lines Speed", Float) = 0
		_BigLinesSpeed("Big Lines Speed", Float) = 0
		_Intensity("Intensity", Float) = 0
		_IntensityBigLines("Intensity Big Lines", Float) = 0
		_TilingBigLines("Tiling Big Lines", Float) = 2
		_TilingSmallLines("Tiling Small Lines", Float) = 1
		_TilingWave("Tiling Wave", Float) = 1
		_Slider("Slider", Range( 0 , 1)) = 0
		_Colour1("Colour1", Color) = (0,1,0.9441659,0)
		_Colour2("Colour2", Color) = (0.7050457,0,1,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Off
		Blend One One
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _MainTexture;
		uniform float _WaveSpeed;
		uniform float _TilingWave;
		uniform float _Offset;
		uniform float4 _Colour1;
		uniform float4 _Colour2;
		uniform float _BigLinesSpeed;
		uniform float _TilingBigLines;
		uniform float _IntensityBigLines;
		uniform float _Intensity;
		uniform float _SmallLinesSpeed;
		uniform float _TilingSmallLines;
		uniform float4 _MainTexture_ST;
		uniform float _Slider;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float4 appendResult13 = (float4(( _WaveSpeed * 0.1 ) , 0.0 , 0.0 , 0.0));
			float2 panner9 = ( 1.0 * _Time.y * appendResult13.xy + v.texcoord.xy);
			float2 break52 = panner9;
			float4 appendResult53 = (float4(( break52.x * _TilingWave ) , break52.y , 0.0 , 0.0));
			float4 appendResult5 = (float4(0.0 , 0.0 , ( tex2Dlod( _MainTexture, float4( appendResult53.xy, 0, 0.0) ).r * _Offset ) , 0.0));
			v.vertex.xyz += appendResult5.xyz;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 appendResult28 = (float4(_BigLinesSpeed , 0.0 , 0.0 , 0.0));
			float2 panner29 = ( 1.0 * _Time.y * appendResult28.xy + i.uv_texcoord);
			float2 break42 = panner29;
			float4 appendResult43 = (float4(( break42.x * _TilingBigLines ) , break42.y , 0.0 , 0.0));
			float4 appendResult16 = (float4(( _SmallLinesSpeed * 0.1 ) , 0.0 , 0.0 , 0.0));
			float2 panner18 = ( 1.0 * _Time.y * appendResult16.xy + i.uv_texcoord);
			float2 break59 = panner18;
			float4 appendResult60 = (float4(( break59.x * _TilingSmallLines ) , break59.y , 0.0 , 0.0));
			float2 uv_MainTexture = i.uv_texcoord * _MainTexture_ST.xy + _MainTexture_ST.zw;
			float temp_output_22_0 = ( ( ( tex2D( _MainTexture, appendResult43.xy ).a * _IntensityBigLines ) + ( _Intensity * tex2D( _MainTexture, appendResult60.xy ).b ) ) * tex2D( _MainTexture, uv_MainTexture ).g );
			float4 lerpResult47 = lerp( _Colour1 , _Colour2 , ( temp_output_22_0 * _Slider ));
			o.Emission = ( lerpResult47 * temp_output_22_0 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17500
0;0;1368;851;5801.117;1773.804;5.676143;True;False
Node;AmplifyShaderEditor.RangedFloatNode;15;-3062.386,471.9979;Inherit;False;Property;_SmallLinesSpeed;Small Lines Speed;3;0;Create;True;0;0;False;0;0;1.18;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;61;-3044.959,731.7831;Inherit;False;Constant;_Float1;Float 1;13;0;Create;True;0;0;False;0;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-2832.782,-454.145;Inherit;False;Property;_BigLinesSpeed;Big Lines Speed;4;0;Create;True;0;0;False;0;0;0.41;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-2830.968,-219.1121;Inherit;False;Constant;_LinesV;Lines V;7;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;62;-2765.572,534.39;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-2699.191,804.2093;Inherit;False;Constant;_SmallLinesV;SmallLines V;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;16;-2440.306,463.5065;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;17;-3058.627,193.2149;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;27;-2837.979,-742.6888;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;28;-2572.083,-559.8148;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.PannerNode;18;-2152.875,234.5772;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;29;-2313.02,-742.6469;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;35;-1937.302,-514.579;Inherit;False;Property;_TilingBigLines;Tiling Big Lines;7;0;Create;True;0;0;False;0;2;0.21;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;42;-1966.201,-791.1973;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;55;-3056.721,1662.983;Inherit;False;Constant;_Float2;Float 2;13;0;Create;True;0;0;False;0;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-3056.133,1412.82;Inherit;False;Property;_WaveSpeed;Wave Speed;2;0;Create;True;0;0;False;0;0;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;37;-1859.828,530.9421;Inherit;False;Property;_TilingSmallLines;Tiling Small Lines;8;0;Create;True;0;0;False;0;1;1.15;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;59;-1809.724,235.7363;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;11;-2749.827,1815.79;Inherit;False;Constant;_WaveV;Wave V;5;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;56;-2799.326,1557.66;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-1550.105,-830.6324;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;58;-1511.508,519.7416;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;43;-1186.49,-798.7109;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;60;-1253.316,146.7265;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;8;-2789.914,1249.126;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;13;-2533.278,1434.315;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TexturePropertyNode;1;-1240.387,482.5448;Inherit;True;Property;_MainTexture;MainTexture;0;0;Create;True;0;0;False;0;None;e1644f661226c0e4b9ca06afff977448;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.PannerNode;9;-2279.559,1216.75;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;20;-864.255,104.9462;Inherit;True;Property;_TextureSample2;Texture Sample 2;6;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;30;-856.3762,-785.3776;Inherit;True;Property;_TextureSample3;Texture Sample 3;9;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;33;-741.8069,-470.9759;Inherit;False;Property;_IntensityBigLines;Intensity Big Lines;6;0;Create;True;0;0;False;0;0;0.63;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-751.9772,-202.2775;Inherit;True;Property;_Intensity;Intensity;5;0;Create;True;0;0;False;0;0;4.64;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;40;-2201.958,1489.56;Inherit;False;Property;_TilingWave;Tiling Wave;9;0;Create;True;0;0;False;0;1;1.16;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;41;-927.9969,356.2641;Inherit;False;370;280;MASK;1;19;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;-386.4852,74.02539;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-304.2687,-197.8041;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;52;-1931.827,1217.915;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode;32;-66.90627,54.53257;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;19;-877.9969,406.2641;Inherit;True;Property;_TextureSample1;Texture Sample 1;6;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;-1638.636,1371.603;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;48;254.5101,-38.81568;Inherit;False;Property;_Slider;Slider;10;0;Create;True;0;0;False;0;0;0.91;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;170.4177,436.8011;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;53;-1373.254,1217.04;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-653.1677,1108.672;Inherit;False;Property;_Offset;Offset;1;0;Create;True;0;0;False;0;0;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;45;400.0835,-469.7541;Inherit;False;Property;_Colour1;Colour1;11;0;Create;True;0;0;False;0;0,1,0.9441659,0;0.2713501,0.8962264,0.2240566,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-893.4554,714.2585;Inherit;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;46;398.6275,-294.1975;Inherit;False;Property;_Colour2;Colour2;12;0;Create;True;0;0;False;0;0.7050457,0,1,0;0.05473477,0.7357505,0.7735849,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;51;-406.9025,732.4777;Inherit;False;248;308.0001;Make movement Y only;1;6;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;50;600.061,89.42658;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;47;862.8125,-372.6145;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-356.9025,782.4777;Inherit;False;Constant;_Float0;Float 0;2;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-315.5584,1097.851;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;49;1202.019,410.6832;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;5;114.4544,1090.718;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1645.437,395.0605;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Aurora;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;4;1;False;-1;1;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;62;0;15;0
WireConnection;62;1;61;0
WireConnection;16;0;62;0
WireConnection;16;1;14;0
WireConnection;28;0;26;0
WireConnection;28;1;25;0
WireConnection;18;0;17;0
WireConnection;18;2;16;0
WireConnection;29;0;27;0
WireConnection;29;2;28;0
WireConnection;42;0;29;0
WireConnection;59;0;18;0
WireConnection;56;0;10;0
WireConnection;56;1;55;0
WireConnection;36;0;42;0
WireConnection;36;1;35;0
WireConnection;58;0;59;0
WireConnection;58;1;37;0
WireConnection;43;0;36;0
WireConnection;43;1;42;1
WireConnection;60;0;58;0
WireConnection;60;1;59;1
WireConnection;13;0;56;0
WireConnection;13;1;11;0
WireConnection;9;0;8;0
WireConnection;9;2;13;0
WireConnection;20;0;1;0
WireConnection;20;1;60;0
WireConnection;30;0;1;0
WireConnection;30;1;43;0
WireConnection;57;0;23;0
WireConnection;57;1;20;3
WireConnection;34;0;30;4
WireConnection;34;1;33;0
WireConnection;52;0;9;0
WireConnection;32;0;34;0
WireConnection;32;1;57;0
WireConnection;19;0;1;0
WireConnection;54;0;52;0
WireConnection;54;1;40;0
WireConnection;22;0;32;0
WireConnection;22;1;19;2
WireConnection;53;0;54;0
WireConnection;53;1;52;1
WireConnection;2;0;1;0
WireConnection;2;1;53;0
WireConnection;50;0;22;0
WireConnection;50;1;48;0
WireConnection;47;0;45;0
WireConnection;47;1;46;0
WireConnection;47;2;50;0
WireConnection;4;0;2;1
WireConnection;4;1;3;0
WireConnection;49;0;47;0
WireConnection;49;1;22;0
WireConnection;5;0;6;0
WireConnection;5;1;6;0
WireConnection;5;2;4;0
WireConnection;0;2;49;0
WireConnection;0;11;5;0
ASEEND*/
//CHKSM=3749D47923826B3288CC0E7EC090FFBC44F6FE73