Shader "Hidden/Custom/RainEffect"
{
    HLSLINCLUDE

        #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

        TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
        TEXTURE2D_SAMPLER2D(_RainTexture, sampler_RainTexture);
        float _Blend;
        int _MaxBlurDistance;
        float4 _RainColor;

        int numDirections;

        float4 Frag(VaryingsDefault i) : SV_Target
        {
            float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
            float4 blurStrength = SAMPLE_TEXTURE2D(_RainTexture, sampler_RainTexture, i.texcoord);
            float4 result = color.rbga;

            if(_MaxBlurDistance == 0) return color;
            
            int duplicateCount = 1;
            for(int j = -_MaxBlurDistance; j <= _MaxBlurDistance; j++)
            {
                float blurDistanceX = blurStrength.r * j * 0.001f;
                for(int k = -_MaxBlurDistance; k <= _MaxBlurDistance; k++)
                {
                    float blurDistanceY = blurStrength.r * k * 0.001f;
                    
                    float4 c = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + float2(blurDistanceX, blurDistanceY));
                    if(blurStrength.r < 0.2f) c = lerp(c, _RainColor, _Blend * blurStrength);
                    result += c;
                    duplicateCount ++;
                }
            }
            result /= duplicateCount;
            //Vertical
            
            color = result;
            return color;
        }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

                #pragma vertex VertDefault
                #pragma fragment Frag

            ENDHLSL
        }
    }
}