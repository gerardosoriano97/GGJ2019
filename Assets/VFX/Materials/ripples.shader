// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Particles/Ripples"
{
	Properties
	{
		[Header(Refraction)]
		_ChromaticAberration("Chromatic Aberration", Range( 0 , 0.3)) = 0.1
		_RefractionMap("RefractionMap", 2D) = "bump" {}
		_NormalIntencity("Normal Intencity", Range( 0 , 3)) = 1.5
		_RotationSpeed("Rotation Speed", Range( 0 , 5)) = 1
		_RefractionValue("Refraction Value", Range( -0.5 , 0.5)) = 0
		_Opacity("Opacity", 2D) = "white" {}
		_Value("Value", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Off
		GrabPass{ }
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma multi_compile _ALPHAPREMULTIPLY_ON
		#pragma surface surf Lambert alpha:fade keepalpha finalcolor:RefractionF noshadow exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
			float3 worldPos;
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _RefractionMap;
		uniform float _RotationSpeed;
		uniform float _NormalIntencity;
		uniform float _Value;
		uniform sampler2D _Opacity;
		uniform float4 _Opacity_ST;
		uniform sampler2D _GrabTexture;
		uniform float _ChromaticAberration;
		uniform float _RefractionValue;

		inline float4 Refraction( Input i, SurfaceOutput o, float indexOfRefraction, float chomaticAberration ) {
			float3 worldNormal = o.Normal;
			float4 screenPos = i.screenPos;
			#if UNITY_UV_STARTS_AT_TOP
				float scale = -1.0;
			#else
				float scale = 1.0;
			#endif
			float halfPosW = screenPos.w * 0.5;
			screenPos.y = ( screenPos.y - halfPosW ) * _ProjectionParams.x * scale + halfPosW;
			#if SHADER_API_D3D9 || SHADER_API_D3D11
				screenPos.w += 0.00000000001;
			#endif
			float2 projScreenPos = ( screenPos / screenPos.w ).xy;
			float3 worldViewDir = normalize( UnityWorldSpaceViewDir( i.worldPos ) );
			float3 refractionOffset = ( ( ( ( indexOfRefraction - 1.0 ) * mul( UNITY_MATRIX_V, float4( worldNormal, 0.0 ) ) ) * ( 1.0 / ( screenPos.z + 1.0 ) ) ) * ( 1.0 - dot( worldNormal, worldViewDir ) ) );
			float2 cameraRefraction = float2( refractionOffset.x, -( refractionOffset.y * _ProjectionParams.x ) );
			float4 redAlpha = tex2D( _GrabTexture, ( projScreenPos + cameraRefraction ) );
			float green = tex2D( _GrabTexture, ( projScreenPos + ( cameraRefraction * ( 1.0 - chomaticAberration ) ) ) ).g;
			float blue = tex2D( _GrabTexture, ( projScreenPos + ( cameraRefraction * ( 1.0 + chomaticAberration ) ) ) ).b;
			return float4( redAlpha.r, green, blue, redAlpha.a );
		}

		void RefractionF( Input i, SurfaceOutput o, inout half4 color )
		{
			#ifdef UNITY_PASS_FORWARDBASE
			float mulTime125 = _Time.y * _RotationSpeed;
			float cos123 = cos( mulTime125 );
			float sin123 = sin( mulTime125 );
			float2 rotator123 = mul( i.uv_texcoord - float2( 0.5,0.5 ) , float2x2( cos123 , -sin123 , sin123 , cos123 )) + float2( 0.5,0.5 );
			float3 tex2DNode121 = UnpackNormal( tex2D( _RefractionMap, rotator123 ) );
			float2 uv_Opacity = i.uv_texcoord * _Opacity_ST.xy + _Opacity_ST.zw;
			float4 tex2DNode135 = tex2D( _Opacity, uv_Opacity );
			float4 color141 = IsGammaSpace() ? float4(1,1,1,1) : float4(1,1,1,1);
			color.rgb = color.rgb + Refraction( i, o, ( ( ( (tex2DNode121).xy * ( _NormalIntencity * _RefractionValue ) ) * tex2DNode135.r ) * ( i.vertexColor.a * color141.a ) ).x, _ChromaticAberration ) * ( 1 - color.a );
			color.a = 1;
			#endif
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			o.Normal = float3(0,0,1);
			float mulTime125 = _Time.y * _RotationSpeed;
			float cos123 = cos( mulTime125 );
			float sin123 = sin( mulTime125 );
			float2 rotator123 = mul( i.uv_texcoord - float2( 0.5,0.5 ) , float2x2( cos123 , -sin123 , sin123 , cos123 )) + float2( 0.5,0.5 );
			float3 tex2DNode121 = UnpackNormal( tex2D( _RefractionMap, rotator123 ) );
			float3 lerpResult129 = lerp( float3(0,0,1) , tex2DNode121 , _NormalIntencity);
			o.Normal = lerpResult129;
			float2 uv_Opacity = i.uv_texcoord * _Opacity_ST.xy + _Opacity_ST.zw;
			float4 tex2DNode135 = tex2D( _Opacity, uv_Opacity );
			o.Alpha = ( _Value * tex2DNode135.r );
			o.Normal = o.Normal + 0.00001 * i.screenPos * i.worldPos;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16200
219;163;1585;1010;-401.2861;-345.8466;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;124;208.4399,621.6332;Float;False;Property;_RotationSpeed;Rotation Speed;4;0;Create;True;0;0;False;0;1;2.32;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;122;469.4399,272.6332;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;125;516.4399,635.6332;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;123;741.4399,537.6332;Float;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;121;1047.44,510.6332;Float;True;Property;_RefractionMap;RefractionMap;2;0;Create;True;0;0;False;0;e2f9479433c558243ae57f937d374837;e2f9479433c558243ae57f937d374837;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;126;870.4399,882.6332;Float;False;Property;_NormalIntencity;Normal Intencity;3;0;Create;True;0;0;False;0;1.5;1;0;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;130;871.4786,993.3605;Float;False;Property;_RefractionValue;Refraction Value;5;0;Create;True;0;0;False;0;0;0;-0.5;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;133;1455.159,791.0504;Float;False;True;True;False;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;131;1314.159,956.0504;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;141;2038.529,1402.539;Float;False;Constant;_Color0;Color 0;6;0;Create;True;0;0;False;0;1,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;135;1657.159,1011.05;Float;True;Property;_Opacity;Opacity;6;0;Create;True;0;0;False;0;28f65f6c1ccd25647805fbaaeda0dc55;28f65f6c1ccd25647805fbaaeda0dc55;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;134;1723.159,773.0504;Float;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.VertexColorNode;140;2079.159,1193.05;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;128;1248.44,325.6332;Float;False;Constant;_Vector0;Vector 0;2;0;Create;True;0;0;False;0;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;142;2319.159,1317.05;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;137;1988.159,664.0504;Float;False;Property;_Value;Value;7;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;138;2122.159,1008.05;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;136;2121.159,847.0504;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;129;1600.44,520.6332;Float;True;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;143;2556.159,881.0504;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;120;2809.868,510.0213;Float;False;True;2;Float;ASEMaterialInspector;0;0;Lambert;Particles/Ripples;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;0;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;125;0;124;0
WireConnection;123;0;122;0
WireConnection;123;2;125;0
WireConnection;121;1;123;0
WireConnection;133;0;121;0
WireConnection;131;0;126;0
WireConnection;131;1;130;0
WireConnection;134;0;133;0
WireConnection;134;1;131;0
WireConnection;142;0;140;4
WireConnection;142;1;141;4
WireConnection;138;0;134;0
WireConnection;138;1;135;1
WireConnection;136;0;137;0
WireConnection;136;1;135;1
WireConnection;129;0;128;0
WireConnection;129;1;121;0
WireConnection;129;2;126;0
WireConnection;143;0;138;0
WireConnection;143;1;142;0
WireConnection;120;1;129;0
WireConnection;120;8;143;0
WireConnection;120;9;136;0
ASEEND*/
//CHKSM=0F190881DCF50FF4E813EF5EF49D01562AEDBF7E