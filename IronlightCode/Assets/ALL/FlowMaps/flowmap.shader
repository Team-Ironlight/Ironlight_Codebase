// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "flowmap_teste"
{
	Properties
	{
		_flow_speed("flow_speed", Float) = 1
		_flowmap("flowmap", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _flowmap;
		uniform float4 _flowmap_ST;
		uniform float _flow_speed;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_flowmap = i.uv_texcoord * _flowmap_ST.xy + _flowmap_ST.zw;
			float2 temp_output_8_0 = ( (float2( -0.5,-0.5 ) + ((tex2D( _flowmap, uv_flowmap )).rg - float2( 0,0 )) * (float2( 0.5,0.5 ) - float2( -0.5,-0.5 )) / (float2( 1,1 ) - float2( 0,0 ))) * ( 0.5 * -1.0 ) );
			float temp_output_13_0 = ( _Time.x * _flow_speed );
			float temp_output_16_0 = frac( temp_output_13_0 );
			float2 temp_output_21_0 = ( i.uv_texcoord + ( temp_output_8_0 * temp_output_16_0 ) );
			float2 temp_output_22_0 = ( i.uv_texcoord + ( temp_output_8_0 * frac( ( temp_output_13_0 + -0.5 ) ) ) );
			float temp_output_31_0 = abs( ( ( 0.5 - temp_output_16_0 ) / 0.5 ) );
			float4 lerpResult42 = lerp( tex2D( _flowmap, temp_output_21_0 ) , tex2D( _flowmap, temp_output_22_0 ) , temp_output_31_0);
			o.Normal = lerpResult42.rgb;
			float4 lerpResult32 = lerp( tex2D( _flowmap, temp_output_21_0 ) , tex2D( _flowmap, temp_output_22_0 ) , temp_output_31_0);
			o.Albedo = lerpResult32.rgb;
			o.Metallic = 0.0;
			o.Smoothness = 0.2;
			float4 lerpResult48 = lerp( tex2D( _flowmap, temp_output_21_0 ) , tex2D( _flowmap, temp_output_22_0 ) , temp_output_31_0);
			o.Occlusion = lerpResult48.r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15303
938;92;982;966;4503.476;629.8049;2.181547;True;False
Node;AmplifyShaderEditor.SamplerNode;2;-3564.248,44.5867;Float;True;Property;_flowmap;flowmap;1;0;Create;True;0;0;False;0;None;f53512d44b91e954dae7bf028209df1a;True;0;True;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TimeNode;38;-3434.274,690.9586;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;11;-2965.463,978.6617;Float;False;Property;_flow_speed;flow_speed;0;0;Create;True;0;0;False;0;1;22;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-2899.084,423.009;Float;False;Constant;_Float0;Float 0;2;0;Create;True;0;0;False;0;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;3;-2926.198,27.03719;Float;True;True;True;False;False;1;0;COLOR;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-2717.63,809.9785;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-2897.49,330.7139;Float;False;Constant;_flowpower;flowpower;2;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-2972.32,1228.288;Float;False;Constant;_Float2;Float 2;2;0;Create;True;0;0;False;0;-0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;15;-2534.323,1148.351;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;5;-2627.939,118.2813;Float;True;5;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT2;1,1;False;3;FLOAT2;-0.5,-0.5;False;4;FLOAT2;0.5,0.5;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-2615.392,366.4027;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;16;-2338.508,595.5522;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-2226.704,92.73773;Float;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;29;-2203.799,1245.699;Float;False;Constant;_Float3;Float 3;3;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;19;-2299.641,921.0083;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;20;-2007.931,-277.2079;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-1891.945,62.99599;Float;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;28;-1969.278,779.2803;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-1881.427,327.4211;Float;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;21;-1564.65,8.920986;Float;True;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;30;-1711.215,829.378;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;22;-1583.102,318.6493;Float;True;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;40;267.5115,385.1901;Float;True;Property;_TextureSample3;Texture Sample 3;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Instance;2;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;26;-1216.844,220.2478;Float;True;Property;_TextureSample1;Texture Sample 1;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Instance;2;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-1226.722,-46.42866;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Instance;2;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;47;240.6331,1381.945;Float;True;Property;_TextureSample7;Texture Sample 7;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Instance;2;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.AbsOpNode;31;-1428.271,826.0427;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;41;269.6399,630.4026;Float;True;Property;_TextureSample4;Texture Sample 4;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Instance;2;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;46;245.2668,1022.847;Float;True;Property;_TextureSample6;Texture Sample 6;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Instance;2;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;48;611.3151,1191.971;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;42;729.7056,581.3604;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;43;794.6158,187.8568;Float;False;Constant;_Float5;Float 5;4;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;44;793.6158,274.8568;Float;False;Constant;_Float6;Float 6;4;0;Create;True;0;0;False;0;0.2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;32;-645.7189,56.58806;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1143.098,51.22921;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;flowmap_teste;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;0;False;-1;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;2;0
WireConnection;13;0;38;1
WireConnection;13;1;11;0
WireConnection;15;0;13;0
WireConnection;15;1;12;0
WireConnection;5;0;3;0
WireConnection;7;0;4;0
WireConnection;7;1;6;0
WireConnection;16;0;13;0
WireConnection;8;0;5;0
WireConnection;8;1;7;0
WireConnection;19;0;15;0
WireConnection;17;0;8;0
WireConnection;17;1;16;0
WireConnection;28;0;29;0
WireConnection;28;1;16;0
WireConnection;18;0;8;0
WireConnection;18;1;19;0
WireConnection;21;0;20;0
WireConnection;21;1;17;0
WireConnection;30;0;28;0
WireConnection;30;1;29;0
WireConnection;22;0;20;0
WireConnection;22;1;18;0
WireConnection;40;1;21;0
WireConnection;26;1;22;0
WireConnection;1;1;21;0
WireConnection;47;1;22;0
WireConnection;31;0;30;0
WireConnection;41;1;22;0
WireConnection;46;1;21;0
WireConnection;48;0;46;0
WireConnection;48;1;47;0
WireConnection;48;2;31;0
WireConnection;42;0;40;0
WireConnection;42;1;41;0
WireConnection;42;2;31;0
WireConnection;32;0;1;0
WireConnection;32;1;26;0
WireConnection;32;2;31;0
WireConnection;0;0;32;0
WireConnection;0;1;42;0
WireConnection;0;3;43;0
WireConnection;0;4;44;0
WireConnection;0;5;48;0
ASEEND*/
//CHKSM=3AC92A871C1C17F0445C9B4E75001EF47F7923CC