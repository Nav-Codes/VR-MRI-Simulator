Shader "Custom/RippleCurtain"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WaveSpeed ("Wave Speed", Float) = 2
        _Amplitude ("Amplitude", Float) = 0.1
        _Frequency ("Frequency", Float) = 2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard vertex:vert

        sampler2D _MainTex;
        float _WaveSpeed;
        float _Amplitude;
        float _Frequency;

        struct Input
        {
            float2 uv_MainTex;
        };

void vert(inout appdata_full v)
{
    // Ripple varies along width (X)
    // Displacement moves vertices along X (side to side)
    float wave = sin(v.vertex.y * _Frequency + _Time.y * _WaveSpeed) * _Amplitude;
    v.vertex.x += wave;  // Move vertices LEFT and RIGHT (folds)
}

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            half4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
