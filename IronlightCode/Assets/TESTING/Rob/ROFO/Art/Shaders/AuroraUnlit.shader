// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "AuroraUnlit"
{
	Properties
	{
		_MainTexture1("MainTexture", 2D) = "white" {}
		_Offset1("Offset", Float) = 0
		_WaveSpeed1("Wave Speed", Float) = 0
		_SmallLinesSpeed1("Small Lines Speed", Float) = 0
		_BigLinesSpeed1("Big Lines Speed", Float) = 0
		_Intensity1("Intensity", Float) = 0
		_IntensityBigLines1("Intensity Big Lines", Float) = 0
		_TilingBigLines1("Tiling Big Lines", Float) = 2
		_TilingSmallLines1("Tiling Small Lines", Float) = 1
		_TilingWave1("Tiling Wave", Float) = 1
		_Slider1("Slider", Range( 0 , 1)) = 0
		_Colour2("Colour1", Color) = (0,1,0.9441659,0)
		_Colour3("Colour2", Color) = (0.7050457,0,1,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Opaque" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		Cull Off
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		
		
		Pass
		{
			Name "Unlit"
			Tags { "LightMode"="ForwardBase" }
			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord : TEXCOORD0;
			};

			uniform sampler2D _MainTexture1;
			uniform float _WaveSpeed1;
			uniform float _TilingWave1;
			uniform float _Offset1;
			uniform float4 _Colour2;
			uniform float4 _Colour3;
			uniform float _BigLinesSpeed1;
			uniform float _TilingBigLines1;
			uniform float _IntensityBigLines1;
			uniform float _Intensity1;
			uniform float _SmallLinesSpeed1;
			uniform float _TilingSmallLines1;
			uniform float4 _MainTexture1_ST;
			uniform float _Slider1;

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float4 appendResult26 = (float4(( _WaveSpeed1 * 0.1 ) , 0.0 , 0.0 , 0.0));
				float2 uv025 = v.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner28 = ( 1.0 * _Time.y * appendResult26.xy + uv025);
				float2 break37 = panner28;
				float4 appendResult43 = (float4(( break37.x * _TilingWave1 ) , break37.y , 0.0 , 0.0));
				float4 appendResult54 = (float4(0.0 , 0.0 , ( tex2Dlod( _MainTexture1, float4( appendResult43.xy, 0, 0.0) ).r * _Offset1 ) , 0.0));
				
				o.ase_texcoord.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = appendResult54.xyz;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				float4 appendResult10 = (float4(_BigLinesSpeed1 , 0.0 , 0.0 , 0.0));
				float2 uv09 = i.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner12 = ( 1.0 * _Time.y * appendResult10.xy + uv09);
				float2 break14 = panner12;
				float4 appendResult23 = (float4(( break14.x * _TilingBigLines1 ) , break14.y , 0.0 , 0.0));
				float4 appendResult7 = (float4(( _SmallLinesSpeed1 * 0.1 ) , 0.0 , 0.0 , 0.0));
				float2 uv08 = i.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner11 = ( 1.0 * _Time.y * appendResult7.xy + uv08);
				float2 break18 = panner11;
				float4 appendResult24 = (float4(( break18.x * _TilingSmallLines1 ) , break18.y , 0.0 , 0.0));
				float2 uv_MainTexture1 = i.ase_texcoord.xy * _MainTexture1_ST.xy + _MainTexture1_ST.zw;
				float temp_output_42_0 = ( ( ( tex2D( _MainTexture1, appendResult23.xy ).a * _IntensityBigLines1 ) + ( _Intensity1 * tex2D( _MainTexture1, appendResult24.xy ).b ) ) * tex2D( _MainTexture1, uv_MainTexture1 ).g );
				float4 lerpResult50 = lerp( _Colour2 , _Colour3 , ( temp_output_42_0 * _Slider1 ));
				
				
				finalColor = ( lerpResult50 * temp_output_42_0 );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=17500
65.5;471;1356;345;1615.812;1328.656;1.972101;True;False
Node;AmplifyShaderEditor.RangedFloatNode;1;-3894.3,-461.855;Inherit;False;Property;_SmallLinesSpeed1;Small Lines Speed;3;0;Create;True;0;0;False;0;0;1.18;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;2;-3876.873,-202.0698;Inherit;False;Constant;_Float2;Float 1;13;0;Create;True;0;0;False;0;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-3664.696,-1387.998;Inherit;False;Property;_BigLinesSpeed1;Big Lines Speed;4;0;Create;True;0;0;False;0;0;0.41;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-3662.882,-1152.965;Inherit;False;Constant;_LinesV1;Lines V;7;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-3597.486,-399.4629;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-3531.105,-129.6436;Inherit;False;Constant;_SmallLinesV1;SmallLines V;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;7;-3272.22,-470.3464;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;8;-3890.541,-740.638;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;9;-3669.893,-1676.542;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;10;-3403.997,-1493.668;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.PannerNode;11;-2984.789,-699.2757;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;12;-3144.934,-1676.5;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-2769.216,-1448.432;Inherit;False;Property;_TilingBigLines1;Tiling Big Lines;7;0;Create;True;0;0;False;0;2;0.21;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;14;-2798.115,-1725.05;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;15;-3888.635,729.1301;Inherit;False;Constant;_Float3;Float 2;13;0;Create;True;0;0;False;0;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-3888.047,478.967;Inherit;False;Property;_WaveSpeed1;Wave Speed;2;0;Create;True;0;0;False;0;0;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-2691.742,-402.9108;Inherit;False;Property;_TilingSmallLines1;Tiling Small Lines;8;0;Create;True;0;0;False;0;1;1.15;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;18;-2641.638,-698.1166;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;19;-3581.741,881.9371;Inherit;False;Constant;_WaveV1;Wave V;5;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-3631.24,623.8071;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-2382.019,-1764.485;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-2343.422,-414.1113;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;23;-2018.404,-1732.564;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;24;-2085.23,-787.1264;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;25;-3621.828,315.2731;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;26;-3365.192,500.462;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TexturePropertyNode;27;-2072.301,-451.3081;Inherit;True;Property;_MainTexture1;MainTexture;0;0;Create;True;0;0;False;0;None;e1644f661226c0e4b9ca06afff977448;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;30;-1688.29,-1719.23;Inherit;True;Property;_TextureSample4;Texture Sample 3;9;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;29;-1696.169,-828.9067;Inherit;True;Property;_TextureSample3;Texture Sample 2;6;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;32;-1583.891,-1136.13;Inherit;True;Property;_Intensity1;Intensity;5;0;Create;True;0;0;False;0;0;4.64;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-1573.721,-1404.829;Inherit;False;Property;_IntensityBigLines1;Intensity Big Lines;6;0;Create;True;0;0;False;0;0;0.63;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;28;-3111.473,282.8971;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-3033.872,555.7072;Inherit;False;Property;_TilingWave1;Tiling Wave;9;0;Create;True;0;0;False;0;1;1.16;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;34;-1759.911,-577.5888;Inherit;False;370;280;MASK;1;39;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-1218.399,-859.8275;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-1136.183,-1131.657;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;37;-2763.741,284.0621;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode;38;-898.8204,-879.3203;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;39;-1709.911,-527.5888;Inherit;True;Property;_TextureSample2;Texture Sample 1;6;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;-2470.55,437.7501;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;43;-2205.168,283.1871;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;41;-577.4041,-972.6686;Inherit;False;Property;_Slider1;Slider;10;0;Create;True;0;0;False;0;0;0.91;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-661.4965,-497.0518;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;44;-1485.082,174.8191;Inherit;False;Property;_Offset1;Offset;1;0;Create;True;0;0;False;0;0;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;45;-431.8307,-1403.607;Inherit;False;Property;_Colour2;Colour1;11;0;Create;True;0;0;False;0;0,1,0.9441659,0;0.2713501,0.8962264,0.2240566,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;46;-1725.37,-219.5944;Inherit;True;Property;_TextureSample1;Texture Sample 0;1;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;47;-433.2867,-1228.05;Inherit;False;Property;_Colour3;Colour2;12;0;Create;True;0;0;False;0;0.7050457,0,1,0;0.05473477,0.7357505,0.7735849,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;48;-1238.817,-201.3752;Inherit;False;248;308.0001;Make movement Y only;1;51;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;49;-231.8532,-844.4263;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;50;30.89832,-1306.467;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;51;-1188.817,-151.3752;Inherit;False;Constant;_Float1;Float 0;2;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;-1147.473,163.998;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;53;370.1049,-523.1697;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;54;-717.4598,156.8651;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;607.6392,-312.6617;Float;False;True;-1;2;ASEMaterialInspector;100;1;AuroraUnlit;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;0;1;False;-1;1;False;-1;0;1;False;-1;1;False;-1;True;0;False;-1;0;False;-1;True;False;True;2;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;0
WireConnection;5;0;1;0
WireConnection;5;1;2;0
WireConnection;7;0;5;0
WireConnection;7;1;6;0
WireConnection;10;0;3;0
WireConnection;10;1;4;0
WireConnection;11;0;8;0
WireConnection;11;2;7;0
WireConnection;12;0;9;0
WireConnection;12;2;10;0
WireConnection;14;0;12;0
WireConnection;18;0;11;0
WireConnection;20;0;16;0
WireConnection;20;1;15;0
WireConnection;21;0;14;0
WireConnection;21;1;13;0
WireConnection;22;0;18;0
WireConnection;22;1;17;0
WireConnection;23;0;21;0
WireConnection;23;1;14;1
WireConnection;24;0;22;0
WireConnection;24;1;18;1
WireConnection;26;0;20;0
WireConnection;26;1;19;0
WireConnection;30;0;27;0
WireConnection;30;1;23;0
WireConnection;29;0;27;0
WireConnection;29;1;24;0
WireConnection;28;0;25;0
WireConnection;28;2;26;0
WireConnection;35;0;32;0
WireConnection;35;1;29;3
WireConnection;36;0;30;4
WireConnection;36;1;31;0
WireConnection;37;0;28;0
WireConnection;38;0;36;0
WireConnection;38;1;35;0
WireConnection;39;0;27;0
WireConnection;40;0;37;0
WireConnection;40;1;33;0
WireConnection;43;0;40;0
WireConnection;43;1;37;1
WireConnection;42;0;38;0
WireConnection;42;1;39;2
WireConnection;46;0;27;0
WireConnection;46;1;43;0
WireConnection;49;0;42;0
WireConnection;49;1;41;0
WireConnection;50;0;45;0
WireConnection;50;1;47;0
WireConnection;50;2;49;0
WireConnection;52;0;46;1
WireConnection;52;1;44;0
WireConnection;53;0;50;0
WireConnection;53;1;42;0
WireConnection;54;0;51;0
WireConnection;54;1;51;0
WireConnection;54;2;52;0
WireConnection;0;0;53;0
WireConnection;0;1;54;0
ASEEND*/
//CHKSM=2E7D372FA3A501F7E5B12676193C7176A89C79E8