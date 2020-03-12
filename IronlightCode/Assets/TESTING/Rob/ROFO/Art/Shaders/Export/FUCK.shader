// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ContactVertexDisplacementRing"
{
	Properties
	{
		_Albedo("Albedo", 2D) = "white" {}
		_Normal("Normal", 2D) = "white" {}
		_NormalIntensity("Normal Intensity", Range( 0 , 10)) = 0
		_Emission("Emission", 2D) = "white" {}
		_EmissionColour("Emission Colour", Color) = (0,0,0,0)
		_EmissionIntensity("Emission Intensity", Range( 0 , 10)) = 1
		[HideInInspector]_Fade("Fade", Float) = 0
		_ContactLocation("Contact Location", Vector) = (0,0,0,0)
		_RingWidth("Ring Width", Float) = 0
		_Intensity("Intensity", Range( 0 , 10)) = 0
		[HideInInspector]_ExtrudeAmount("Extrude Amount", Float) = 0
		[HideInInspector]_Length("Length", Float) = 0
		_Metallic("Metallic", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 4.6
		#pragma surface surf Standard keepalpha noshadow vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform float _ExtrudeAmount;
		uniform float3 _ContactLocation;
		uniform float _Length;
		uniform float _RingWidth;
		uniform float _Intensity;
		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform float _NormalIntensity;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform sampler2D _Emission;
		uniform float4 _Emission_ST;
		uniform float4 _EmissionColour;
		uniform float _EmissionIntensity;
		uniform float _Fade;
		uniform sampler2D _Metallic;
		uniform float4 _Metallic_ST;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_worldNormal = UnityObjectToWorldNormal( v.normal );
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float temp_output_32_0 = abs( ( ase_worldPos.x - _ContactLocation.x ) );
			float temp_output_33_0 = abs( ( ase_worldPos.z - _ContactLocation.z ) );
			float RadiusFromCenter38 = sqrt( ( ( temp_output_32_0 * temp_output_32_0 ) + ( temp_output_33_0 * temp_output_33_0 ) ) );
			float smoothstepResult20 = smoothstep( 0.0 , _RingWidth , ( _RingWidth - abs( ( RadiusFromCenter38 - _Length ) ) ));
			float Mask26 = (( RadiusFromCenter38 >= saturate( ( _Length - _RingWidth ) ) && RadiusFromCenter38 <= ( _Length + _RingWidth ) ) ? ( smoothstepResult20 * _Intensity ) :  0.0 );
			float dotResult88 = dot( ase_worldNormal , float3(0,1,0) );
			float3 VertexDisplacement59 = ( ( ase_worldNormal * _ExtrudeAmount ) * Mask26 * (( dotResult88 > 0.0 ) ? 1.0 :  0.0 ) );
			v.vertex.xyz += VertexDisplacement59;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			float3 tex2DNode72 = UnpackNormal( tex2D( _Normal, uv_Normal ) );
			float4 appendResult76 = (float4(( (tex2DNode72).xy * _NormalIntensity ) , tex2DNode72.b , 0.0));
			float4 Normal77 = appendResult76;
			o.Normal = Normal77.xyz;
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float4 Albedo62 = tex2D( _Albedo, uv_Albedo );
			o.Albedo = Albedo62.rgb;
			float2 uv_Emission = i.uv_texcoord * _Emission_ST.xy + _Emission_ST.zw;
			float3 ase_worldPos = i.worldPos;
			float temp_output_32_0 = abs( ( ase_worldPos.x - _ContactLocation.x ) );
			float temp_output_33_0 = abs( ( ase_worldPos.z - _ContactLocation.z ) );
			float RadiusFromCenter38 = sqrt( ( ( temp_output_32_0 * temp_output_32_0 ) + ( temp_output_33_0 * temp_output_33_0 ) ) );
			float smoothstepResult20 = smoothstep( 0.0 , _RingWidth , ( _RingWidth - abs( ( RadiusFromCenter38 - _Length ) ) ));
			float Mask26 = (( RadiusFromCenter38 >= saturate( ( _Length - _RingWidth ) ) && RadiusFromCenter38 <= ( _Length + _RingWidth ) ) ? ( smoothstepResult20 * _Intensity ) :  0.0 );
			float4 Emission7 = ( tex2D( _Emission, uv_Emission ) * _EmissionColour * Mask26 * _EmissionIntensity * _Fade );
			o.Emission = Emission7.rgb;
			float2 uv_Metallic = i.uv_texcoord * _Metallic_ST.xy + _Metallic_ST.zw;
			float4 Metallic84 = tex2D( _Metallic, uv_Metallic );
			o.Metallic = Metallic84.r;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
0;0;1368;851;5213.163;-1949.153;3.269217;True;False
Node;AmplifyShaderEditor.CommentaryNode;39;-3627.349,1997.503;Float;False;2022.369;581.605;Comment;11;28;29;30;31;32;33;34;35;36;37;38;Hypothenous;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector3Node;28;-3533.473,2257.033;Float;False;Property;_ContactLocation;Contact Location;7;0;Create;True;0;0;False;0;0,0,0;0.2,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldPosInputsNode;29;-3552.484,2051.891;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleSubtractOpNode;30;-3241.531,2306.71;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;31;-3238.28,2077.265;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;33;-2971.432,2308.491;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;32;-2975.658,2077.183;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-2717.04,2307.619;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-2719.8,2081.777;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;36;-2380.397,2194.085;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SqrtOpNode;37;-2104.185,2193.391;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;27;-4123.714,501.9306;Float;False;2519.003;1152.36;Comment;14;11;12;13;14;15;17;18;19;20;21;22;23;24;26;Ring;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;38;-1864.354,2187.311;Float;False;RadiusFromCenter;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-4046.181,551.9305;Float;False;Property;_Length;Length;11;1;[HideInInspector];Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;12;-4055.666,1223.121;Float;False;38;RadiusFromCenter;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;14;-3686.587,1278.317;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-4046.343,819.2775;Float;False;Property;_RingWidth;Ring Width;8;0;Create;True;0;0;False;0;0;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;15;-3433.567,1354.242;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;17;-3196.277,1331.661;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-2907.871,1404.943;Float;False;Property;_Intensity;Intensity;9;0;Create;True;0;0;False;0;0;1;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;18;-3415.077,569.3665;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;79;-3100.953,-1821.137;Float;False;1859.099;693.9418;Comment;7;71;72;74;73;75;76;77;Normal;1,1,1,1;0;0
Node;AmplifyShaderEditor.SmoothstepOpNode;20;-2893.658,1161.187;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-2582.964,1160.248;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;71;-3050.953,-1771.137;Float;True;Property;_Normal;Normal;1;0;Create;True;0;0;False;0;None;2f1fc2610420fb0418d722141a8d7481;True;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SaturateNode;22;-3097.766,569.5841;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;23;-3415.68,809.6616;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;89;-2988.016,3836.771;Float;False;Constant;_VectorUp;Vector Up;13;0;Create;True;0;0;False;0;0,1,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.CommentaryNode;10;-3081.102,-840.7518;Float;False;1469.112;948.0507;Comment;8;5;7;6;4;3;2;1;81;Emission;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldNormalVector;87;-3058.215,3600.628;Float;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TFHCCompareWithRange;24;-2273.061,1015.91;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;80;-2808.417,2975.463;Float;False;1202.796;555.6143;Comment;6;55;56;54;57;58;59;Vertex Displacement;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;72;-2741.597,-1767.364;Float;True;Property;_TextureSample2;Texture Sample 2;10;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DotProductOpNode;88;-2697.682,3598.192;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;26;-1849.212,1011.208;Float;False;Mask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;55;-2758.417,3028.728;Float;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TexturePropertyNode;1;-3039.856,-767.9126;Float;True;Property;_Emission;Emission;3;0;Create;True;0;0;False;0;None;0b2e50a315eb95f4181a0c844bfa357e;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.RangedFloatNode;74;-2417.336,-1384.695;Float;False;Property;_NormalIntensity;Normal Intensity;2;0;Create;True;0;0;False;0;0;1.9;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;56;-2735.564,3273.577;Float;False;Property;_ExtrudeAmount;Extrude Amount;10;1;[HideInInspector];Create;True;0;0;False;0;0;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;73;-2391.083,-1602.799;Float;False;True;True;False;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;86;-1009.521,1619.951;Float;False;955.094;309.8594;Comment;3;82;83;84;Metallic;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;70;-3088.129,-2351.374;Float;False;944.6865;307.5;Comment;3;60;61;62;Albedo;1,1,1,1;0;0
Node;AmplifyShaderEditor.TFHCCompareGreater;92;-2456.465,3600.262;Float;False;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;5;-2745.208,-767.9916;Float;True;Property;_TextureSample1;Texture Sample 1;3;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;81;-2320,-176;Float;False;Property;_Fade;Fade;6;1;[HideInInspector];Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;3;-2651.747,-363.0232;Float;False;26;Mask;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;60;-3038.129,-2299.25;Float;True;Property;_Albedo;Albedo;0;0;Create;True;0;0;False;0;None;ee36cffcf5540894dac72b0a33e6c2b8;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.ColorNode;4;-2654.558,-552.6846;Float;False;Property;_EmissionColour;Emission Colour;4;0;Create;True;0;0;False;0;0,0,0,0;1,0,0.8378444,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;-2454.804,3027.095;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;2;-2706.072,-144.9488;Float;False;Property;_EmissionIntensity;Emission Intensity;5;0;Create;True;0;0;False;0;1;3;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;54;-2451.54,3265.416;Float;False;26;Mask;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;-2080.083,-1499.805;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;82;-959.5206,1669.951;Float;True;Property;_Metallic;Metallic;12;0;Create;True;0;0;False;0;None;3b62b2197bf622f448927482553a6fa8;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;83;-643.379,1669.951;Float;True;Property;_TextureSample3;Texture Sample 3;13;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-2190.526,-682.3146;Float;False;5;5;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;76;-1793.316,-1713.87;Float;False;FLOAT4;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SamplerNode;61;-2738.743,-2299.657;Float;True;Property;_TextureSample0;Texture Sample 0;9;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;58;-2159.353,3028.727;Float;False;3;3;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;59;-1883.945,3023.739;Float;False;VertexDisplacement;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;84;-298.9265,1672.31;Float;False;Metallic;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;7;-1895.007,-686.3516;Float;False;Emission;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;62;-2387.942,-2301.374;Float;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;77;-1486.354,-1719.93;Float;False;Normal;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;85;-266.8129,428.1887;Float;False;84;Metallic;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;9;-206.7398,143.1984;Float;False;7;Emission;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;53;-7.765743,612.2368;Float;False;59;VertexDisplacement;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;63;-179.6159,-303.0373;Float;False;62;Albedo;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;78;-257.3491,-79.49203;Float;False;77;Normal;1;0;OBJECT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;385.361,-29.64315;Float;False;True;6;Float;ASEMaterialInspector;0;0;Standard;ContactVertexDisplacementRing;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;False;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;30;0;29;3
WireConnection;30;1;28;3
WireConnection;31;0;29;1
WireConnection;31;1;28;1
WireConnection;33;0;30;0
WireConnection;32;0;31;0
WireConnection;34;0;33;0
WireConnection;34;1;33;0
WireConnection;35;0;32;0
WireConnection;35;1;32;0
WireConnection;36;0;35;0
WireConnection;36;1;34;0
WireConnection;37;0;36;0
WireConnection;38;0;37;0
WireConnection;14;0;12;0
WireConnection;14;1;11;0
WireConnection;15;0;14;0
WireConnection;17;0;13;0
WireConnection;17;1;15;0
WireConnection;18;0;11;0
WireConnection;18;1;13;0
WireConnection;20;0;17;0
WireConnection;20;2;13;0
WireConnection;21;0;20;0
WireConnection;21;1;19;0
WireConnection;22;0;18;0
WireConnection;23;0;11;0
WireConnection;23;1;13;0
WireConnection;24;0;12;0
WireConnection;24;1;22;0
WireConnection;24;2;23;0
WireConnection;24;3;21;0
WireConnection;72;0;71;0
WireConnection;88;0;87;0
WireConnection;88;1;89;0
WireConnection;26;0;24;0
WireConnection;73;0;72;0
WireConnection;92;0;88;0
WireConnection;5;0;1;0
WireConnection;57;0;55;0
WireConnection;57;1;56;0
WireConnection;75;0;73;0
WireConnection;75;1;74;0
WireConnection;83;0;82;0
WireConnection;6;0;5;0
WireConnection;6;1;4;0
WireConnection;6;2;3;0
WireConnection;6;3;2;0
WireConnection;6;4;81;0
WireConnection;76;0;75;0
WireConnection;76;2;72;3
WireConnection;61;0;60;0
WireConnection;58;0;57;0
WireConnection;58;1;54;0
WireConnection;58;2;92;0
WireConnection;59;0;58;0
WireConnection;84;0;83;0
WireConnection;7;0;6;0
WireConnection;62;0;61;0
WireConnection;77;0;76;0
WireConnection;0;0;63;0
WireConnection;0;1;78;0
WireConnection;0;2;9;0
WireConnection;0;3;85;0
WireConnection;0;11;53;0
ASEEND*/
//CHKSM=23C39CEB57C95BA8924A430DF2E51C013B712892