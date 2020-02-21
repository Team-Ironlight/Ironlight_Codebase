// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "TinyUnlit"
{
	Properties
	{
		[HDR]_Albedo1("Albedo", Color) = (1,1,1,0)
		_RimIntensity1("RimIntensity", Range( 0 , 1)) = 0
		_Threshold1("Threshold", Range( 0.01 , 0.5)) = 0
		[HDR]_TopColour1("TopColour", Color) = (1,0.8820389,0,0)
		[HDR]_BottomColour1("BottomColour", Color) = (1,0.496859,0,0)
		_GradientBottom1("GradientBottom", Float) = 0.4642553
		_EmissionIntensity1("EmissionIntensity", Float) = 1

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Opaque" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		Cull Back
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
			

			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float3 ase_normal : NORMAL;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
			};

			uniform float4 _Albedo1;
			uniform float _RimIntensity1;
			uniform float _Threshold1;
			uniform float4 _TopColour1;
			uniform float4 _BottomColour1;
			uniform float _GradientBottom1;
			uniform float _EmissionIntensity1;

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float3 ase_worldNormal = UnityObjectToWorldNormal(v.ase_normal);
				o.ase_texcoord.xyz = ase_worldNormal;
				float3 ase_worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.ase_texcoord1.xyz = ase_worldPos;
				
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.w = 0;
				o.ase_texcoord1.w = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
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
				float3 ase_worldNormal = i.ase_texcoord.xyz;
				float3 ase_worldPos = i.ase_texcoord1.xyz;
				float3 ase_worldViewDir = UnityWorldSpaceViewDir(ase_worldPos);
				ase_worldViewDir = normalize(ase_worldViewDir);
				float dotResult13 = dot( ase_worldNormal , ase_worldViewDir );
				float smoothstepResult21 = smoothstep( ( _RimIntensity1 - _Threshold1 ) , ( _RimIntensity1 + _Threshold1 ) , ( 1.0 - dotResult13 ));
				float4 transform31 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
				float smoothstepResult34 = smoothstep( 0.0 , 1.0 , ( ( transform31.y - ase_worldPos.y ) + _GradientBottom1 ));
				float4 lerpResult35 = lerp( _TopColour1 , _BottomColour1 , smoothstepResult34);
				
				
				finalColor = ( ( ( _Albedo1 * smoothstepResult21 ) + lerpResult35 ) * _EmissionIntensity1 );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=17500
479;546;889;307;3845.493;302.8032;6.934548;True;False
Node;AmplifyShaderEditor.WorldNormalVector;7;-1434.758,-634.4731;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;8;-1435.951,-397.5964;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;31;-602.8849,1608.619;Inherit;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldPosInputsNode;32;-598.6169,1816.823;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleSubtractOpNode;33;-333.3538,1698.344;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-1216.781,-889.2454;Inherit;False;Constant;_Float1;Float 0;0;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-1308.572,749.9363;Inherit;False;Property;_GradientBottom1;GradientBottom;5;0;Create;True;0;0;False;0;0.4642553;1.84;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-1433.416,-178.2094;Inherit;False;Property;_RimIntensity1;RimIntensity;1;0;Create;True;0;0;False;0;0;0.59;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-1431.595,59.12698;Inherit;False;Property;_Threshold1;Threshold;2;0;Create;True;0;0;False;0;0;0.01;0.01;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;13;-1066.852,-411.3459;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;36;-31.37586,1624.009;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;17;-1059.235,-162.0247;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;15;-820.7803,-432.3763;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;18;-1053.613,55.89767;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;34;176.5656,1515.443;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;24;-1307.678,568.5184;Inherit;False;Property;_BottomColour1;BottomColour;4;1;[HDR];Create;True;0;0;False;0;1,0.496859,0,0;1,0.496859,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;21;-568.7246,-434.3777;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;23;-937.4763,-937.0692;Inherit;False;Property;_Albedo1;Albedo;0;1;[HDR];Create;True;0;0;False;0;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;20;-1311.3,389.9525;Inherit;False;Property;_TopColour1;TopColour;3;1;[HDR];Create;True;0;0;False;0;1,0.8820389,0,0;1,0.8820389,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-207.0166,-448.3025;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;35;575.0279,1329.265;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;28;1115.194,-1.040156;Inherit;True;Property;_EmissionIntensity1;EmissionIntensity;6;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;27;1078.623,-237.9708;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;1498.689,-232.0741;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-42.55363,501.3134;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;26;715.3345,337.5255;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;9;-318.1321,823.2324;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;4;-1034.795,1272.274;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-644.1577,879.7884;Inherit;False;Constant;_Float2;Float 1;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-1307.53,984.7774;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMinOpNode;22;292.2545,471.2342;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-3.308334,723.1464;Inherit;False;Constant;_Float4;Float 3;6;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;10;-352.6981,579.8834;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;6;-681.1147,627.6403;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;2;-1305.656,1257.8;Inherit;False;Constant;_Float3;Float 2;6;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;1728.944,-271.7109;Float;False;True;-1;2;ASEMaterialInspector;100;1;TinyUnlit;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;False;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;0
WireConnection;33;0;31;2
WireConnection;33;1;32;2
WireConnection;13;0;7;0
WireConnection;13;1;8;0
WireConnection;36;0;33;0
WireConnection;36;1;3;0
WireConnection;17;0;14;0
WireConnection;17;1;12;0
WireConnection;15;0;11;0
WireConnection;15;1;13;0
WireConnection;18;0;14;0
WireConnection;18;1;12;0
WireConnection;34;0;36;0
WireConnection;21;0;15;0
WireConnection;21;1;17;0
WireConnection;21;2;18;0
WireConnection;25;0;23;0
WireConnection;25;1;21;0
WireConnection;35;0;20;0
WireConnection;35;1;24;0
WireConnection;35;2;34;0
WireConnection;27;0;25;0
WireConnection;27;1;35;0
WireConnection;29;0;27;0
WireConnection;29;1;28;0
WireConnection;19;0;10;0
WireConnection;19;1;9;0
WireConnection;26;0;20;0
WireConnection;26;1;24;0
WireConnection;26;2;22;0
WireConnection;9;0;2;0
WireConnection;9;1;3;0
WireConnection;4;0;2;0
WireConnection;4;1;1;2
WireConnection;22;0;19;0
WireConnection;22;1;16;0
WireConnection;10;0;6;0
WireConnection;10;1;5;0
WireConnection;6;0;4;0
WireConnection;6;1;3;0
WireConnection;0;0;29;0
ASEEND*/
//CHKSM=4A0B723D45526F115D3D8A3E43FF9110BB205B0A