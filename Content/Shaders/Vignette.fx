#if OPENGL
  #define SV_POSITION POSITION
  #define VS_SHADERMODEL vs_3_0
  #define PS_SHADERMODEL ps_3_0
#else
  #define VS_SHADERMODEL vs_4_0_level_9_1
  #define PS_SHADERMODEL ps_4_0_level_9_1
#endif

float2 VignetteCenter;
float2 VignetteDimensions;
float2 VignetteMaxTopLeft;
float2 VignetteMaxBottomRight;
float2 VignetteNoneDistSq;
float2 VignetteFullDistSq;
float4 VignetteColor;

Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state {
  Texture = <SpriteTexture>;
};

struct VertexShaderOutput {
  float4 Position : SV_POSITION;
  float4 Color : COLOR0;
  float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR {
  bool applyVignette =
    input.TextureCoordinates.x >= VignetteMaxTopLeft.x &&
    input.TextureCoordinates.x <= VignetteMaxBottomRight.x &&
    input.TextureCoordinates.y >= VignetteMaxTopLeft.y &&
    input.TextureCoordinates.y <= VignetteMaxBottomRight.y;
  
  float vignetteRelCoords = (input.TextureCoordinates - VignetteCenter) / VignetteDimensions;

  float vignetteCenterDistSq = vignetteRelCoords.x * vignetteRelCoords.x + vignetteRelCoords.y * vignetteRelCoords.y;
  
  float vignetteMultiplier =
    applyVignette ?
      (
        vignetteCenterDistSq < VignetteNoneDistSq ?
        0.0f :
        (
          vignetteCenterDistSq < VignetteFullDistSq ?
          1.0f - (vignetteCenterDistSq - VignetteNoneDistSq) / (VignetteFullDistSq - VignetteNoneDistSq) :
          1.0f
        )
      ) :
      0.0f;

  return lerp(
    tex2D(SpriteTextureSampler, input.TextureCoordinates) * input.Color,
    VignetteColor,
    vignetteMultiplier
  );
}

technique SpriteDrawing {
  pass P0 {
    PixelShader = compile PS_SHADERMODEL MainPS();
  }
};
