Shader "Telexistence/Demo/UnlitTexture" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color("",Color)=(1,1,1,1)
		_FlipCoordinate("Coord Factor", Vector) = (0,0,0,0)
	}
	SubShader {
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
		Pass{
			LOD 200

			Cull Off
			Lighting Off
			ZWrite Off
			ZTest Off
			Fog { Mode Off }
			Offset -1, -1
			//Blend SrcAlpha DstAlpha
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			sampler2D _MainTex;
			float4 _Color=float4(1,1,1,1);
			float2 _FlipCoordinate=float2(0,0);

			struct appdata_t
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				fixed4 color : COLOR;
			};
			struct v2f {
			    float4 pos : SV_POSITION;
			    float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
			};
			v2f vert(appdata_t  v) {
			    v2f o;
			    o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
			    o.uv = v.texcoord;
			    if(_FlipCoordinate.y>0)
			    	o.uv.y=1-o.uv.y;
			    if(_FlipCoordinate.x>0)
			    	o.uv.x=1-o.uv.x;
			    o.color=v.color;
			    return o;
			}
			half4 frag(v2f IN) : SV_Target {
				float4 c;
				c= tex2D (_MainTex, IN.uv)*_Color*IN.color;
				return c;
			}

	        ENDCG
		}
	} 
}
