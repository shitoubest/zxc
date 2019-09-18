// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

#warning Upgrade NOTE: unity_Scale shader variable was removed; replaced 'unity_Scale.w' with '1.0'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
//limiao 2016/09/06 fix scale issue in unity5 , add fog support

Shader "Unique/Character/Character"
{
	Properties
	{
        _Alpha("Alpha", Range(0.0, 1.0)) = 1.0
		_MainTex("Base (RGB)", 2D) = "white" {}
		_ToonRampTex("Toon Ramp (RGB)", 2D) = "gray" {}
        _ToonShadowColor("Toon Shadow Color", Color) = (0.5, 0.5, 0.5, 1.0)
        _ToonHighlightColor("Toon Highlight Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _RimColor("Rim Color", Color) = (0.8, 0.8, 0.8, 0.5)
        _RimPower("Rim Power", Range(0.1, 5.0)) = 3.0
        _RimSharpness("Rim Sharpness", Range(0.0,5.0)) = 1.5
        _Emission("Emission Color", Color) = (0.0, 0.0, 0.0, 0.0)
        _XRayColor("XRay Color", Color) = (0.0, 0.5, 1.0, 1.0)
	}

    SubShader
	{
        Tags
        {
            "Queue" = "Geometry+499"
        }

        Pass
        {
            Tags
            {
                "IgnoreProjector" = "True"
                "RenderType" = "Transparent"
                "LightMode" = "Always"
            }

            ColorMask RGBA
            Lighting Off
            Fog {Mode Off}
            Cull Back
            ZWrite Off
            ZTest Greater
            AlphaTest Off
            Blend SrcAlpha One

            CGPROGRAM
			#pragma enable_d3d11_debug_symbols
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata_t
			{
				half4 vertex : POSITION;
                half3 normal : NORMAL;
			};

			struct v2f
			{
				half4 vertex : POSITION;
                half3 position : TEXCOORD1;
                half3 normal : TEXCOORD2;
			};

			half4 _XRayColor;

			v2f vert(appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
                o.position = mul(unity_ObjectToWorld, v.vertex).xyz;
                //In unity5  you always have to use normalize on the transformed normal 
                o.normal =normalize(mul(half4(v.normal, 0.0), unity_WorldToObject).xyz);
				return o;
			}

			half4 frag(v2f i) : COLOR
			{
                half4 color = _XRayColor;
                half3 V = normalize(_WorldSpaceCameraPos - i.position);
                color.a = 1.0 - saturate(dot(V, i.normal));
				return color;
			}
			ENDCG
        }

        Pass
        {
            Tags
            {
                "RenderType" = "Opaque"
                "LightMode" = "ForwardBase"
            }

            ColorMask RGBA
            Lighting On
            Cull Back
            ZWrite On
            ZTest LEqual
            AlphaTest Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
            #pragma multi_compile_fog
			#include "UnityCG.cginc"

			struct appdata_t
			{
				half4 vertex : POSITION;
                half3 normal : NORMAL;
                half2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				half4 vertex : POSITION;
                half2 texcoord : TEXCOORD0;
                half3 position : TEXCOORD1;
                half3 normal : TEXCOORD2;
                UNITY_FOG_COORDS(3)//fog have to use it's  own channel
			};

			half _Alpha;
            sampler2D _MainTex;
            sampler2D _ToonRampTex;
            half4 _LightColor0;
            half4 _ToonShadowColor;
			half4 _ToonHighlightColor;
			half4 _RimColor;
            half _RimPower;
            half _RimSharpness;
			half4 _Emission;

			v2f vert(appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                o.position = mul(unity_ObjectToWorld, v.vertex).xyz;
                //In unity5  you always have to use normalize on the transformed normal 
                o.normal =normalize(mul(half4(v.normal, 0.0), unity_WorldToObject).xyz);
                UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			half4 frag(v2f i) : SV_Target
			{
                half4 main = tex2D(_MainTex, i.texcoord);
                half3 L = normalize(UnityWorldSpaceLightDir(i.position));
                half d = dot(L, i.normal) * 0.5 + 0.5;
                half4 ramp = tex2D(_ToonRampTex, half2(d, d));
                half4 toon = lerp(_ToonShadowColor, _ToonHighlightColor, ramp);
                half4 color = main * _LightColor0 * toon;
                half3 V = normalize(_WorldSpaceCameraPos - i.position);
                half rim = 1.0 - saturate(dot(V, i.normal)) * _RimSharpness;
                half Irim = pow(_RimPower , rim);
                color.rgb += _RimColor.rgb * Irim *(1.0 - saturate(dot(V, i.normal))) * _RimColor.a;
                color.rgb += _Emission.rgb * _Emission.a;
                color.a = _Alpha;
                UNITY_APPLY_FOG(i.fogCoord, color);
				return color;
			}
			ENDCG
        }
	}
}