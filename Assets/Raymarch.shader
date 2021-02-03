Shader "Unlit/Raymarch"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            //#pragma multi_compile_fog

            #include "UnityCG.cginc"

            #define MaxIter 100
            #define MaxDist 100
            #define SurfaceDist 1e-2

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                //UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 ro : TEXCOORD1;
                float3 hitPos: TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                // wworldSpace coords:
                //o.ro = _WorldSpaceCameraPos;
                //o.hitPos = mul(unity_ObjectToWorld, v.vertex);

                // object space coords:
                o.ro = mul(unity_WorldToObject, float4(_WorldSpaceCameraPos,1));
                o.hitPos = v.vertex;

                //UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float mandelbulb(float3 position) {
                float powe = sin(_Time)*2;
                float3 z = position;
                float dr = 1;
                float r;

                for (int i = 0; i < 15; i++) {
                    r = length(z);
                    if (r > 2)
                        break;

                    float theta = acos(z.z / r) * powe;
                    float phi = atan2(z.y, z.x) * powe;
                    float zr = pow(r, powe);
                    dr = pow(r, powe - 1) * powe * dr + 1;

                    z = zr * float3(sin(theta) * cos(phi), sin(phi) * sin(theta), cos(theta));
                    z += position;

                }
                return 0.5 * log(r) * r / dr;
            }

            float GetDist(float3 p) {
                return mandelbulb(p);
                //float d = length(p) - 0.5;
                float d = length(p)-0.5;
                return d;
            }

            float Raymarch(float3 ro, float3 rd) {
                float dO = 0;
                float ds;
                for (int i = 0; i < MaxIter; i++) {
                    float3 p = ro + dO * rd;
                    ds = GetDist(p);
                    dO += ds;
                    if (ds<SurfaceDist || dO>MaxDist) {
                        break;
                    }


                }
                return dO;

            }

            float3 getNormal(float3 p) {
                float2 e = float2(1e-2, 0);
                float3 n = GetDist(p) - float3(GetDist(p - e.xyy), GetDist(p - e.yxy), GetDist(p - e.yyx));
                return normalize(n);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                //fixed4 col = tex2D(_MainTex, i.uv);
                float2 uv = i.uv - 0.5f;
                float3 ro = i.ro;
                float3 rd = normalize(i.hitPos - ro);

                float d = Raymarch(ro, rd);
                fixed4 col = 0;

                if (d >= MaxDist) {
                    discard;
                }
                else {
                    float3 p = ro + rd * d;
                    float3 n = getNormal(p);
                    col.rgb = n;
                    //col.rgb = float3(1,1,1);
                }

                // apply fog      Mathf.PerlinNoise(xCoord, yCoord);
                //UNITY_APPLY_FOG(i.fogCoord, col);

                return col;
            }
            ENDCG
        }
    }
}
