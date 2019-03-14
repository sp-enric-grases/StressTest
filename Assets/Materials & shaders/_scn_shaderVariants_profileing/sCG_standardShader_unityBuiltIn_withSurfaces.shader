// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "socialPointCG/sCG_standardShader_unityBuiltIn_withSurfaces"
{
	Properties
	{
		_MainTex("DF", 2D) = "white" {}
		_MetallicGlossMap("MK", 2D) = "white" {}
		
		_MetallicFactor("Metallic Factor",Range(0, 1)) = 0
		_GlossMapScale("Smoothness", Range(0, 1)) = 0

		_BumpMap("NM", 2D) = "bump" {}
		_BumpScale("Scale", Range(0, 3)) = 1.0

		_EmissiveColor ("Emissive color", Color) = (1,0.5,0,1) 
		_EmissiveScale("Emissive Scale", Range(0, 3)) = 1.0
	}
	
	SubShader
	{
		Tags
		{ 
			"RenderType" = "Opaque" 
		}
		
		LOD 200

		CGPROGRAM

		/*
		finalcolor:myFinalColor 
				...works only in forward!
		exclude_path:deferred 
				...forces forward rendering for the shader.
		*/

		// Physically based Standard lighting model, and enable shadows on all light types
		// #pragma surface surf Standard fullforwardshadows vertex:vert finalcolor:myFinalColor exclude_path:deferred 
		//#pragma surface surf Standard fullforwardshadows vertex:vert
		

		//----skipping variants...

		//pragma skips: SHADOWS
			//#pragma skip_variants SHADOWS_SCREEN
			#pragma skip_variants SHADOWS_SOFT
			#pragma skip_variants SHADOWS_DEPTH
			#pragma skip_variants SHADOWS_CUBE
			#pragma skip_variants SHADOWS_SHADOWMASK

		//pragma skips: LIGHTING
			#pragma skip_variants LIGHTMAP_ON
			#pragma skip_variants LIGHTMAP_SHADOW_MIXING
			#pragma skip_variants DYNAMICLIGHTMAP_ON
			#pragma skip_variants DIRLIGHTMAP_COMBINED
			//#pragma skip_variants DIRECTIONAL
			#pragma skip_variants POINT
			#pragma skip_variants SPOT
			#pragma skip_variants POINT_COOKIE
			#pragma skip_variants DIRECTIONAL_COOKIE
			//#pragma skip_variants LIGHTPROBE_SH
			//#pragma skip_variants VERTEXLIGHT_ON

		//pragma skips: FOG
			#pragma skip_variants FOG_EXP
			#pragma skip_variants FOG_EXP2

		//pragma skips: OTHER
			#pragma skip_variants UNITY_HDR_ON
	
		//----excluding unnecessary renderers...
		#pragma exclude_renderers glcore d3d11_9x xboxone ps4 n3ds wiiu

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
				
		#pragma surface surf Standard addshadow vertex:vert

		fixed4 _BaseColor;
		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _MetallicGlossMap;

		float _MetallicFactor;
		float _BumpScale;
		float _GlossMapScale;
		fixed4 _EmissiveColor;
		float _EmissiveScale;

		struct Input 
		{
			float2 uv_MainTex;
		};

		/*
		struct appdata_full 
		{
			float4 vertex : POSITION;
			float4 tangent : TANGENT;
			float3 normal : NORMAL;
			fixed4 color : COLOR;
			float4 texcoord : TEXCOORD0;
			float4 texcoord1 : TEXCOORD1;
			half4 texcoord2 : TEXCOORD2;
			half4 texcoord3 : TEXCOORD3;
			half4 texcoord4 : TEXCOORD4;
			half4 texcoord5 : TEXCOORD5;
		};
		*/

		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);		
		}
		
		/*
		struct SurfaceOutputStandard
		{
			fixed3 Albedo;      // base (diffuse or specular) color
			fixed3 Normal;      // tangent space normal, if written
			half3 Emission;
			half Metallic;      // 0=non-metal, 1=metal
			half Smoothness;    // 0=rough, 1=smooth
			half Occlusion;     // occlusion (default 1)
			fixed Alpha;        // alpha for transparencies
		};
		*/

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
		
			// Albedo
			fixed4 DF = tex2D(_MainTex, IN.uv_MainTex);

			o.Albedo = DF;
			
			// Normal
			fixed4 NM = tex2D(_BumpMap, IN.uv_MainTex);

			o.Normal = lerp(fixed3(0.0,0.0,1.0), UnpackNormal(NM), _BumpScale).rgb;

			// Metallic & Smoothness
			/*
				R = AO mask
				G = Emissive mask
				B = Smoothness mask
				A = Metallic mask
			*/
			fixed4 MK = tex2D(_MetallicGlossMap, IN.uv_MainTex); 
			o.Occlusion = MK.r;
			o.Emission = (DF.rgb + _EmissiveColor) * MK.g * _EmissiveScale;
			o.Smoothness = MK.b * _GlossMapScale;
			o.Metallic = MK.a * _MetallicFactor;

			o.Alpha = DF.a;
		}

	ENDCG

	}
	
	FallBack "Diffuse"
}