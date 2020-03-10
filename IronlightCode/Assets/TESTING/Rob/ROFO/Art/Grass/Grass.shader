// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Grass"
{
	Properties
	{
		_Albedo("Albedo", 2D) = "white" {}
		_NoiseScale("Noise Scale", Range( 0 , 2)) = 0.5
		_Normal("Normal", 2D) = "white" {}
		_NormalIntensity("Normal Intensity", Range( 0 , 10)) = 0
		_PannerDirection("Panner Direction", Vector) = (0,0,0,0)
		_PannerTime("Panner Time", Float) = 0
		_WindStrength("Wind Strength", Float) = 0
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_AmbientOcclusion("Ambient Occlusion", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 4.6
		#pragma surface surf Standard alpha:fade keepalpha noshadow vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform float _PannerTime;
		uniform float2 _PannerDirection;
		uniform float _NoiseScale;
		uniform float _WindStrength;
		uniform float _NormalIntensity;
		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float _Metallic;
		uniform float _Smoothness;
		uniform float _AmbientOcclusion;


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


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float4 appendResult54 = (float4(ase_worldPos.x , ase_worldPos.z , 0.0 , 0.0));
			float2 panner23 = ( ( _PannerTime * _Time.y ) * _PannerDirection + appendResult54.xy);
			float simplePerlin2D31 = snoise( panner23 );
			float4 appendResult45 = (float4(( ase_vertex3Pos.x + ( ( ( simplePerlin2D31 * _NoiseScale ) - 0.5 ) * _WindStrength ) ) , ase_vertex3Pos.y , ase_vertex3Pos.z , 0.0));
			float4 lerpResult46 = lerp( float4( ase_vertex3Pos , 0.0 ) , appendResult45 , saturate( ( v.texcoord.xy.y - 0.0 ) ));
			float4 VertexDisplacement42 = lerpResult46;
			v.vertex.xyz += VertexDisplacement42.xyz;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			float3 break15 = UnpackNormal( tex2D( _Normal, uv_Normal ) );
			float4 appendResult17 = (float4(break15.x , break15.y , 0.0 , 0.0));
			float4 appendResult16 = (float4(( _NormalIntensity * appendResult17 ).x , 0.0 , break15.z , 0.0));
			float4 Normal19 = appendResult16;
			o.Normal = Normal19.xyz;
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float4 tex2DNode6 = tex2D( _Albedo, uv_Albedo );
			o.Albedo = tex2DNode6.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Occlusion = _AmbientOcclusion;
			o.Alpha = tex2DNode6.a;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
0;0;1368;851;3980.349;-1942.965;3.190834;True;False
Node;AmplifyShaderEditor.CommentaryNode;56;-3073.34,2479.887;Float;False;3179.732;1246.806;Comment;22;53;26;27;29;28;54;23;31;34;36;35;48;37;44;40;38;51;50;45;46;42;57;Grass Wave;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;27;-2964.806,3190.95;Float;False;Property;_PannerTime;Panner Time;5;0;Create;True;0;0;False;0;0;0.24;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;26;-2957.995,3433.641;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;53;-3023.34,2619.586;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector2Node;29;-2995.188,2888.131;Float;False;Property;_PannerDirection;Panner Direction;4;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.DynamicAppendNode;54;-2759.116,2640.301;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-2725.471,3108.769;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;23;-2412.373,2867.077;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-2116.864,3116.444;Float;False;Property;_NoiseScale;Noise Scale;1;0;Create;True;0;0;False;0;0.5;0.65;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;31;-2141.663,2860.877;Float;False;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;55;-3112.231,1567.885;Float;False;2059.903;579.2761;Comment;8;10;11;15;17;18;12;16;19;Normal;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;-1908.19,2867.859;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;10;-3062.231,1617.885;Float;True;Property;_Normal;Normal;2;0;Create;True;0;0;False;0;None;f5453dca2ac649e4182c56a3966ad395;True;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;11;-2770.392,1734.304;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;35;-1682.508,2868.224;Float;False;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;36;-1656.628,3097.154;Float;False;Property;_WindStrength;Wind Strength;6;0;Create;True;0;0;False;0;0;0.16;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;15;-2400.175,1880.629;Float;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.PosVertexDataNode;44;-1367.03,2567.386;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;-1354.042,2868.224;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;40;-1393.766,3195.613;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;48;-1393.188,3469.193;Float;False;Constant;_GradientStrength;Gradient Strength;2;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;38;-1077.335,2844.334;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-2409.291,1617.251;Float;False;Property;_NormalIntensity;Normal Intensity;3;0;Create;True;0;0;False;0;0;1.4;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;51;-950.3011,3301.727;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;17;-2091.626,1721.158;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-1802.325,1630.868;Float;False;2;2;0;FLOAT;0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;45;-805.0681,2669.206;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SaturateNode;50;-698.6705,3306.348;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;46;-491.7216,2530.904;Float;False;3;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;16;-1566.483,1894.661;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;19;-1296.828,1888.413;Float;False;Normal;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TexturePropertyNode;5;-848.166,-436.5679;Float;True;Property;_Albedo;Albedo;0;0;Create;True;0;0;False;0;None;33380ee4dda78c048807eec8ca167051;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;42;-173.1085,2529.887;Float;False;VertexDisplacement;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-659.9254,763.9745;Float;False;Property;_AmbientOcclusion;Ambient Occlusion;9;0;Create;True;0;0;False;0;0;0.38;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;43;-205.3825,892.3438;Float;False;42;VertexDisplacement;1;0;OBJECT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SamplerNode;6;-518.9873,-396.4756;Float;True;Property;_TextureSample1;Texture Sample 1;3;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;8;-662.6187,514.2603;Float;False;Property;_Smoothness;Smoothness;8;0;Create;True;0;0;False;0;0;0.42;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-654.7205,271.7969;Float;False;Property;_Metallic;Metallic;7;0;Create;True;0;0;False;0;0;0.07;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;20;-371.2407,-9.901494;Float;True;19;Normal;1;0;OBJECT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;213,-18;Float;False;True;6;Float;ASEMaterialInspector;0;0;Standard;Grass;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;54;0;53;1
WireConnection;54;1;53;3
WireConnection;28;0;27;0
WireConnection;28;1;26;0
WireConnection;23;0;54;0
WireConnection;23;2;29;0
WireConnection;23;1;28;0
WireConnection;31;0;23;0
WireConnection;57;0;31;0
WireConnection;57;1;34;0
WireConnection;11;0;10;0
WireConnection;35;0;57;0
WireConnection;15;0;11;0
WireConnection;37;0;35;0
WireConnection;37;1;36;0
WireConnection;38;0;44;1
WireConnection;38;1;37;0
WireConnection;51;0;40;2
WireConnection;51;1;48;0
WireConnection;17;0;15;0
WireConnection;17;1;15;1
WireConnection;12;0;18;0
WireConnection;12;1;17;0
WireConnection;45;0;38;0
WireConnection;45;1;44;2
WireConnection;45;2;44;3
WireConnection;50;0;51;0
WireConnection;46;0;44;0
WireConnection;46;1;45;0
WireConnection;46;2;50;0
WireConnection;16;0;12;0
WireConnection;16;2;15;2
WireConnection;19;0;16;0
WireConnection;42;0;46;0
WireConnection;6;0;5;0
WireConnection;0;0;6;0
WireConnection;0;1;20;0
WireConnection;0;3;7;0
WireConnection;0;4;8;0
WireConnection;0;5;9;0
WireConnection;0;9;6;4
WireConnection;0;11;43;0
ASEEND*/
//CHKSM=B43DD93A2E2D1770C19DD1811AABD71EEE1D72AB