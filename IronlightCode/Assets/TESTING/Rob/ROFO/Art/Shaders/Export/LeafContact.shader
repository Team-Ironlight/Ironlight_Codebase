// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "LeafContact"
{
	Properties
	{
		_Albedo("Albedo", 2D) = "white" {}
		_NormalTexture("Normal Texture", 2D) = "white" {}
		_NormalIntensity("Normal Intensity", Range( 0 , 10)) = 0
		_EmissionTexture("Emission Texture", 2D) = "white" {}
		_EmissionColour("Emission Colour", Color) = (0,0,0,0)
		_EmissionIntensity("Emission Intensity", Range( 0 , 10)) = 0
		_ContactLocation("ContactLocation", Vector) = (0,0,0,0)
		_RingWidth("Ring Width", Float) = 0
		[HideInInspector]_Length("Length", Float) = 0
		_Intensity("Intensity", Float) = 0
		[HideInInspector]_NormalExtrude("NormalExtrude", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 4.6
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform float3 _ContactLocation;
		uniform float _Length;
		uniform float _RingWidth;
		uniform float _Intensity;
		uniform float _NormalExtrude;
		uniform sampler2D _NormalTexture;
		uniform float4 _NormalTexture_ST;
		uniform float _NormalIntensity;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform sampler2D _EmissionTexture;
		uniform float4 _EmissionTexture_ST;
		uniform float4 _EmissionColour;
		uniform float _EmissionIntensity;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float temp_output_21_0 = abs( ( ase_worldPos.x - _ContactLocation.x ) );
			float temp_output_20_0 = abs( ( ase_worldPos.z - _ContactLocation.z ) );
			float RadiusFromCollision26 = sqrt( ( ( temp_output_21_0 * temp_output_21_0 ) + ( temp_output_20_0 * temp_output_20_0 ) ) );
			float temp_output_85_0 = (( _RingWidth < _Length ) ? _RingWidth :  _Length );
			float smoothstepResult50 = smoothstep( 0.0 , temp_output_85_0 , ( temp_output_85_0 - abs( ( RadiusFromCollision26 - _Length ) ) ));
			float Mask61 = (( RadiusFromCollision26 >= saturate( ( _Length - temp_output_85_0 ) ) && RadiusFromCollision26 <= ( _Length + temp_output_85_0 ) ) ? ( smoothstepResult50 * _Intensity ) :  0.0 );
			float3 ase_worldNormal = UnityObjectToWorldNormal( v.normal );
			float3 VertexOffset71 = ( Mask61 * ( ase_worldNormal * _NormalExtrude ) );
			v.vertex.xyz += VertexOffset71;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_NormalTexture = i.uv_texcoord * _NormalTexture_ST.xy + _NormalTexture_ST.zw;
			float3 tex2DNode76 = UnpackNormal( tex2D( _NormalTexture, uv_NormalTexture ) );
			float4 appendResult80 = (float4(( (tex2DNode76).xy * _NormalIntensity ) , tex2DNode76.b , 0.0));
			float4 Normal81 = appendResult80;
			o.Normal = Normal81.xyz;
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float4 Albedo66 = tex2D( _Albedo, uv_Albedo );
			o.Albedo = Albedo66.rgb;
			float2 uv_EmissionTexture = i.uv_texcoord * _EmissionTexture_ST.xy + _EmissionTexture_ST.zw;
			float3 ase_worldPos = i.worldPos;
			float temp_output_21_0 = abs( ( ase_worldPos.x - _ContactLocation.x ) );
			float temp_output_20_0 = abs( ( ase_worldPos.z - _ContactLocation.z ) );
			float RadiusFromCollision26 = sqrt( ( ( temp_output_21_0 * temp_output_21_0 ) + ( temp_output_20_0 * temp_output_20_0 ) ) );
			float temp_output_85_0 = (( _RingWidth < _Length ) ? _RingWidth :  _Length );
			float smoothstepResult50 = smoothstep( 0.0 , temp_output_85_0 , ( temp_output_85_0 - abs( ( RadiusFromCollision26 - _Length ) ) ));
			float Mask61 = (( RadiusFromCollision26 >= saturate( ( _Length - temp_output_85_0 ) ) && RadiusFromCollision26 <= ( _Length + temp_output_85_0 ) ) ? ( smoothstepResult50 * _Intensity ) :  0.0 );
			float4 Emission68 = ( tex2D( _EmissionTexture, uv_EmissionTexture ) * _EmissionColour * Mask61 * _EmissionIntensity );
			o.Emission = Emission68.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17500
-735.5;783.5;1368;811;6537.016;1233.974;5.87163;True;False
Node;AmplifyShaderEditor.CommentaryNode;27;-2375.5,2409.023;Inherit;False;2043.135;659.0764;Hypothenus from Center;11;26;16;17;18;19;21;20;23;22;24;25;Hypothenus from Center;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector3Node;17;-2271.83,2726.034;Inherit;False;Property;_ContactLocation;ContactLocation;6;0;Create;True;0;0;False;0;0,0,0;-0.21,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldPosInputsNode;16;-2320.094,2519.429;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleSubtractOpNode;18;-1988.338,2553.579;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;19;-1982.813,2781.561;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;20;-1711.252,2778.954;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;21;-1714.014,2554.959;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-1462.545,2559.554;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-1459.784,2785.395;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;24;-1112.904,2671.862;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SqrtOpNode;25;-851.3179,2672.63;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;26;-621.7245,2670.938;Inherit;False;RadiusFromCollision;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;38;-3105.471,850.0978;Inherit;False;2606.999;1298.966;Ring Mask;15;61;15;53;50;49;36;35;29;39;31;32;54;30;85;28;Ring Mask;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;29;-3022.211,1572.252;Inherit;False;26;RadiusFromCollision;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-2994.677,904.0692;Inherit;False;Property;_Length;Length;8;1;[HideInInspector];Create;True;0;0;False;0;0;1.92;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;28;-2994.839,1174.016;Inherit;False;Property;_RingWidth;Ring Width;7;0;Create;True;0;0;False;0;0;0.72;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;35;-2635.083,1630.456;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;36;-2382.063,1706.381;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCCompareLower;85;-2685.663,1177.075;Inherit;False;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;49;-2144.773,1683.8;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;32;-2363.573,921.5052;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;84;-2674.7,-226.8032;Inherit;False;1843.973;719.8547;Comment;7;75;76;78;77;79;80;81;Normal;1,1,1,1;0;0
Node;AmplifyShaderEditor.SmoothstepOpNode;50;-1842.154,1513.326;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;53;-1788.087,1748.929;Inherit;False;Property;_Intensity;Intensity;9;0;Create;True;0;0;False;0;0;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;39;-2046.262,921.7228;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;31;-2364.176,1143.8;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;-1531.46,1512.387;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;75;-2624.7,-176.8032;Inherit;True;Property;_NormalTexture;Normal Texture;1;0;Create;True;0;0;False;0;None;7ddcba51d9fc0894d98b4ba77fbdfbd7;True;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;76;-2314.721,-153.8419;Inherit;True;Property;_TextureSample1;Texture Sample 1;10;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;72;865.2341,1434.062;Inherit;False;1211.676;786.1875;Comment;6;57;62;59;58;60;71;Vertex Offset;1,1,1,1;0;0
Node;AmplifyShaderEditor.TFHCCompareWithRange;15;-1221.557,1368.049;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;74;-3235.977,-1289.539;Inherit;False;1454.59;945.6476;Comment;8;83;55;68;10;12;63;9;69;Emission;1,1,1,1;0;0
Node;AmplifyShaderEditor.TexturePropertyNode;69;-3179.101,-1236.1;Inherit;True;Property;_EmissionTexture;Emission Texture;3;0;Create;True;0;0;False;0;None;f7e96904e8667e1439548f0f86389447;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.WorldNormalVector;57;915.2341,1714.066;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;60;954.8277,1962.25;Inherit;False;Property;_NormalExtrude;NormalExtrude;11;1;[HideInInspector];Create;True;0;0;False;0;1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;78;-1995.994,235.0515;Inherit;False;Property;_NormalIntensity;Normal Intensity;2;0;Create;True;0;0;False;0;0;1.8;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;77;-1962.554,8.352417;Inherit;False;True;True;False;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;61;-797.7081,1363.347;Inherit;False;Mask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;79;-1654.421,114.5973;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;59;1247.354,1706.98;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;12;-2798.546,-1020.872;Inherit;False;Property;_EmissionColour;Emission Colour;4;0;Create;True;0;0;False;0;0,0,0,0;1,0.1660023,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;64;-494.7463,-985.4793;Inherit;True;Property;_Albedo;Albedo;0;0;Create;True;0;0;False;0;None;00d034bb5072d8043a98b8a4aae5a40d;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.GetLocalVarNode;63;-2795.735,-831.2104;Inherit;False;61;Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;62;929.3383,1487.984;Inherit;False;61;Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;9;-2889.196,-1236.179;Inherit;True;Property;_Emission;Emission;0;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;83;-2850.06,-613.136;Inherit;False;Property;_EmissionIntensity;Emission Intensity;5;0;Create;True;0;0;False;0;0;0.8;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;65;-197.6017,-985.4792;Inherit;True;Property;_TextureSample0;Texture Sample 0;10;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-2334.514,-1150.502;Inherit;True;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;80;-1389.554,-104.6476;Inherit;False;FLOAT4;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;58;1534.151,1494.291;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;71;1825.41,1564.063;Inherit;False;VertexOffset;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;81;-1064.727,-107.6395;Inherit;False;Normal;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;68;-2038.995,-1154.539;Inherit;False;Emission;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;66;196.1147,-985.4792;Inherit;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;55;-3161.272,-889.1514;Inherit;False;Property;_Fade;Fade;10;0;Create;True;0;0;False;0;0;0.57;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;73;230.3634,422.5252;Inherit;False;71;VertexOffset;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;82;85.91602,-51.29474;Inherit;False;81;Normal;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;70;103.5478,185.0413;Inherit;False;68;Emission;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;67;240.0386,-290.2737;Inherit;False;66;Albedo;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;641.9153,18.12674;Float;False;True;-1;6;ASEMaterialInspector;0;0;Standard;LeafContact;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;18;0;16;1
WireConnection;18;1;17;1
WireConnection;19;0;16;3
WireConnection;19;1;17;3
WireConnection;20;0;19;0
WireConnection;21;0;18;0
WireConnection;22;0;21;0
WireConnection;22;1;21;0
WireConnection;23;0;20;0
WireConnection;23;1;20;0
WireConnection;24;0;22;0
WireConnection;24;1;23;0
WireConnection;25;0;24;0
WireConnection;26;0;25;0
WireConnection;35;0;29;0
WireConnection;35;1;30;0
WireConnection;36;0;35;0
WireConnection;85;0;28;0
WireConnection;85;1;30;0
WireConnection;85;2;28;0
WireConnection;85;3;30;0
WireConnection;49;0;85;0
WireConnection;49;1;36;0
WireConnection;32;0;30;0
WireConnection;32;1;85;0
WireConnection;50;0;49;0
WireConnection;50;2;85;0
WireConnection;39;0;32;0
WireConnection;31;0;30;0
WireConnection;31;1;85;0
WireConnection;54;0;50;0
WireConnection;54;1;53;0
WireConnection;76;0;75;0
WireConnection;15;0;29;0
WireConnection;15;1;39;0
WireConnection;15;2;31;0
WireConnection;15;3;54;0
WireConnection;77;0;76;0
WireConnection;61;0;15;0
WireConnection;79;0;77;0
WireConnection;79;1;78;0
WireConnection;59;0;57;0
WireConnection;59;1;60;0
WireConnection;9;0;69;0
WireConnection;65;0;64;0
WireConnection;10;0;9;0
WireConnection;10;1;12;0
WireConnection;10;2;63;0
WireConnection;10;3;83;0
WireConnection;80;0;79;0
WireConnection;80;2;76;3
WireConnection;58;0;62;0
WireConnection;58;1;59;0
WireConnection;71;0;58;0
WireConnection;81;0;80;0
WireConnection;68;0;10;0
WireConnection;66;0;65;0
WireConnection;0;0;67;0
WireConnection;0;1;82;0
WireConnection;0;2;70;0
WireConnection;0;11;73;0
ASEEND*/
//CHKSM=2A2998E3A8D47068A5231080F80866E441BC89C9