using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
 
[Serializable]
[PostProcess(typeof(RainEffectRenderer), PostProcessEvent.AfterStack, "Custom/RainEffect")]
public sealed class RainEffect : PostProcessEffectSettings
{
    [Range(0f, 1f), Tooltip("Grayscale effect intensity.")]
    public FloatParameter blend = new FloatParameter { value = 0.5f };
    [Range(0, 30), Tooltip("Max Blur Distance.")]
    public IntParameter maxBlurDistance = new IntParameter { value = 1 };
    public ColorParameter rainColor = new ColorParameter{ value = Color.white };
    public TextureParameter rainTexture = new TextureParameter{ value = null };
}
 
public sealed class RainEffectRenderer : PostProcessEffectRenderer<RainEffect>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/RainEffect"));
        sheet.properties.SetFloat("_Blend", settings.blend);
        sheet.properties.SetInt("_MaxBlurDistance", settings.maxBlurDistance);
        sheet.properties.SetColor("_RainColor", settings.rainColor);
        var rainTexture = settings.rainTexture.value == null
            ? RuntimeUtilities.whiteTexture
            : settings.rainTexture.value;
        sheet.properties.SetTexture("_RainTexture", rainTexture);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}