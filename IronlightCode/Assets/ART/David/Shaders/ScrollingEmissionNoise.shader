// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "EmissionNoiseShader"
{
	Properties
	{
		_EmissionTexture("EmissionTexture", 2D) = "white" {}
		_EmissionColour("EmissionColour", Color) = (0,0,0,0)
		_EmissionStrength("EmissionStrength", Float) = 0
		_NoiseSpeedX("NoiseSpeedX", Float) = 0
		_Albedo("Albedo", Color) = (0.6415094,0.239053,0.239053,0)
		_TilingX("TilingX", Float) = 0
		_TilingY("TilingY", Float) = 0
		_NoiseSpeedY("NoiseSpeedY", Float) = 0
		_NoiseContrast("NoiseContrast", Float) = 0
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

		uniform float4 _Albedo;
		uniform sampler2D _EmissionTexture;
		uniform float4 _EmissionTexture_ST;
		uniform float _NoiseSpeedX;
		uniform float _NoiseSpeedY;
		uniform float _TilingX;
		uniform float _TilingY;
		uniform float _NoiseContrast;
		uniform float4 _EmissionColour;
		uniform float _EmissionStrength;


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
			o.Albedo = _Albedo.rgb;
			float2 uv_EmissionTexture = i.uv_texcoord * _EmissionTexture_ST.xy + _EmissionTexture_ST.zw;
			float4 appendResult23 = (float4(_NoiseSpeedX , _NoiseSpeedY , 0.0 , 0.0));
			float4 appendResult21 = (float4(_TilingX , _TilingY , 0.0 , 0.0));
			float2 uv_TexCoord17 = i.uv_texcoord * appendResult21.xy;
			float2 panner14 = ( 1.0 * _Time.y * appendResult23.xy + uv_TexCoord17);
			float simplePerlin2D4 = snoise( panner14 );
			simplePerlin2D4 = simplePerlin2D4*0.5 + 0.5;
			o.Emission = ( tex2D( _EmissionTexture, uv_EmissionTexture ) * pow( simplePerlin2D4 , _NoiseContrast ) * _EmissionColour * _EmissionStrength ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17500
479;575.5;889;274;1703.038;375.6865;2.233659;True;False
Node;AmplifyShaderEditor.RangedFloatNode;19;-1688.331,179.7802;Inherit;False;Property;_TilingX;TilingX;5;0;Create;True;0;0;False;0;0;20;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-1687.633,413.0917;Inherit;False;Property;_TilingY;TilingY;6;0;Create;True;0;0;False;0;0;20;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;21;-1428.606,184.3742;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-1439.608,561.9231;Inherit;False;Property;_NoiseSpeedX;NoiseSpeedX;3;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-1433.878,789.3127;Inherit;False;Property;_NoiseSpeedY;NoiseSpeedY;7;0;Create;True;0;0;False;0;0;-1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;17;-1192.828,161.4277;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;20,20;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;23;-1175.61,459.0284;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.PannerNode;14;-921.644,161.3294;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-1,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;4;-621.5613,161.9822;Inherit;False;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-375.6447,530.4062;Inherit;False;Property;_NoiseContrast;NoiseContrast;8;0;Create;True;0;0;False;0;0;1.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-628.1622,-49.43155;Inherit;True;Property;_EmissionTexture;EmissionTexture;0;0;Create;True;0;0;False;0;-1;None;a9466649578b0524e8542809021a0899;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;7;-631.1589,395.7724;Inherit;False;Property;_EmissionColour;EmissionColour;1;0;Create;True;0;0;False;0;0,0,0,0;0.04224169,1,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;9;-626.9388,575.1315;Inherit;False;Property;_EmissionStrength;EmissionStrength;2;0;Create;True;0;0;False;0;0;12.6;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;24;-304.7369,282.3315;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-240.927,46.77214;Inherit;False;4;4;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;15;-548.786,-253.1545;Inherit;False;Property;_Albedo;Albedo;4;0;Create;True;0;0;False;0;0.6415094,0.239053,0.239053,0;0.6415094,0.239053,0.239053,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;EmissionNoiseShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;21;0;19;0
WireConnection;21;1;20;0
WireConnection;17;0;21;0
WireConnection;23;0;18;0
WireConnection;23;1;22;0
WireConnection;14;0;17;0
WireConnection;14;2;23;0
WireConnection;4;0;14;0
WireConnection;24;0;4;0
WireConnection;24;1;26;0
WireConnection;2;0;1;0
WireConnection;2;1;24;0
WireConnection;2;2;7;0
WireConnection;2;3;9;0
WireConnection;0;0;15;0
WireConnection;0;2;2;0
ASEEND*/
//CHKSM=1023CE50D93D4DAB60DD9D2587C40F1E338760FA