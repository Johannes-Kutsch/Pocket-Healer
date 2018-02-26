Shader "Custom/Greyscale" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);

			//Last number is the greyscale proportion
			o.Albedo = lerp(c.rgb, dot(c.rgb, float3(0.3, 0.59, 0.11)), 0.875);
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
