Shader "Mshader/CircleOfPoison" {
	Properties{
		[HDR] _TintColor("Tint Color", Color) = (0.5,0.5,0.5,1)
		_BackColor("Back Color", Color) = (0,1,0,0)//背景底色
	_TimeScale("Time Scale", Vector) = (1,1,1,1)
		_MainTex("Noise Texture", 2D) = "white" {}
	_BorderScale("Border Scale (XY) Offset (Z)", Vector) = (0.5,0.05,1,0)
		_ahRang("ahRang",Range(0,4)) = 1.5//毒圈整体高度
		_ahValue("ahValue",Range(-1,1)) = 0//毒圈整体透明度
	}

		Category{
			Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
						Blend SrcAlpha OneMinusSrcAlpha
						Cull Off
						Lighting Off
						ZWrite Off

			SubShader {
				Pass {

					CGPROGRAM
					#pragma vertex vert
					#pragma fragment frag

					#include "UnityCG.cginc"

					sampler2D _MainTex;
					float4 _TintColor;
					float4 _TimeScale;
					float4 _BorderScale;
					float4 _BackColor;

					struct appdata_t {
						float4 vertex : POSITION;
						fixed4 color : COLOR;
						float2 texcoord : TEXCOORD0;
						float3 normal : NORMAL;
					};

					struct v2f {
						float4 vertex : POSITION;
						fixed4 color : COLOR;
						float2 texcoord : TEXCOORD0;
						float3 normal : NORMAL;
						float3 worldPosScaled : TEXCOORD1;
						float2 uv : TEXCOORD2;


					};

					float4 _MainTex_ST;
					float _ahRang;
					float _ahValue;


					v2f vert(appdata_t v)
					{
						v2f o;
						v.vertex.xyz += v.normal / 100 * _BorderScale.z;
						o.vertex = UnityObjectToClipPos(v.vertex);
						o.color = v.color;
						o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
						o.worldPosScaled = v.vertex.xyz * _MainTex_ST.x;
						o.normal = abs(v.normal);
						o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

						return o;
					}

					half2 tex2DTriplanar(sampler2D tex, float2 offset, float3 worldPos, float3 normal)
					{
						half2 yDiff = tex2D(tex, worldPos.xz + offset);
						half2 xDiff = tex2D(tex, worldPos.zy + offset);
						half2 zDiff = tex2D(tex, worldPos.xy + offset);
						normal = normal / (normal.x + normal.y + normal.z);
						return xDiff * normal.x + yDiff * normal.y + zDiff * normal.z;
					}

					half4 frag(v2f i) : COLOR
					{
						half2 mask = tex2DTriplanar(_MainTex, _Time.xx * _TimeScale.xy, i.worldPosScaled, i.normal);
						half2 tex = tex2DTriplanar(_MainTex, mask + _Time.xx * _TimeScale.zw, i.worldPosScaled, i.normal);
						float4 res = 0;
						res.r = step(tex.r, _BorderScale.x);
						res.r -= step(tex.r, _BorderScale.x - _BorderScale.y);
						res.r *= tex.g;
						res = i.color * res.r * _TintColor + float4(normalize(_BackColor.rgb),0);//加上底色的混合色
						res.a = saturate(res.a + (1 - _ahRang * sqrt(i.uv.y)) + _ahValue);//透明控制
						//res.a = saturate(res.a);
						return  res;
					}
					ENDCG
				}
			}
	}

}