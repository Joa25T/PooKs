Shader "Unlit/UI_Noise"
{
    Properties
    {
        _BlinkSpeed("BlinkSpeed", Float) = 1
        _LineIntensity("LineIntensity", Range(0, 10)) = 4
        _LineDistance("LineDistance", Float) = 7
        _LineThickness("LineThickness", Range(0, 1)) = 0.06
        _LineSpeed("LineSpeed", Float) = 0.4
        _NoiseSpeed("NoiseSpeed", Range(0, 0.1)) = 0.08
        [HideInInspector]_QueueOffset("_QueueOffset", Float) = 0
        [HideInInspector]_QueueControl("_QueueControl", Float) = -1
        [HideInInspector][NoScaleOffset]unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "RenderType"="Transparent"
            "UniversalMaterialType" = "Unlit"
            "Queue"="Transparent"
            "DisableBatching"="False"
            "ShaderGraphShader"="true"
            "ShaderGraphTargetId"="UniversalUnlitSubTarget"
        }
        Pass
        {
            Name "Universal Forward"
            Tags
            {
                // LightMode: <None>
            }
        
        // Render State
        Cull Back
        Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
        ZTest LEqual
        ZWrite Off
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 2.0
        #pragma multi_compile_instancing
        #pragma multi_compile_fog
        #pragma instancing_options renderinglayer
        #pragma vertex vert
        #pragma fragment frag
        
        // Keywords
        #pragma multi_compile _ LIGHTMAP_ON
        #pragma multi_compile _ DIRLIGHTMAP_COMBINED
        #pragma shader_feature _ _SAMPLE_GI
        #pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
        #pragma multi_compile_fragment _ DEBUG_DISPLAY
        #pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define ATTRIBUTES_NEED_COLOR
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define VARYINGS_NEED_COLOR
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_UNLIT
        #define _FOG_FRAGMENT 1
        #define _SURFACE_TYPE_TRANSPARENT 1
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/RenderingLayers.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DBuffer.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
             float4 color : COLOR;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float3 normalWS;
             float4 texCoord0;
             float4 color;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float4 uv0;
             float4 VertexColor;
             float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float4 texCoord0 : INTERP0;
             float4 color : INTERP1;
             float3 positionWS : INTERP2;
             float3 normalWS : INTERP3;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.texCoord0.xyzw = input.texCoord0;
            output.color.xyzw = input.color;
            output.positionWS.xyz = input.positionWS;
            output.normalWS.xyz = input.normalWS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.texCoord0 = input.texCoord0.xyzw;
            output.color = input.color.xyzw;
            output.positionWS = input.positionWS.xyz;
            output.normalWS = input.normalWS.xyz;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _BlinkSpeed;
        float _LineIntensity;
        float _LineDistance;
        float _LineThickness;
        float _LineSpeed;
        float _NoiseSpeed;
        CBUFFER_END
        
        
        // Object and Global properties
        
        // Graph Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Hashes.hlsl"
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_Fraction_float(float In, out float Out)
        {
            Out = frac(In);
        }
        
        void Unity_Step_float(float Edge, float In, out float Out)
        {
            Out = step(Edge, In);
        }
        
        void Unity_OneMinus_float(float In, out float Out)
        {
            Out = 1 - In;
        }
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        float Unity_SimpleNoise_ValueNoise_Deterministic_float (float2 uv)
        {
            float2 i = floor(uv);
            float2 f = frac(uv);
            f = f * f * (3.0 - 2.0 * f);
            uv = abs(frac(uv) - 0.5);
            float2 c0 = i + float2(0.0, 0.0);
            float2 c1 = i + float2(1.0, 0.0);
            float2 c2 = i + float2(0.0, 1.0);
            float2 c3 = i + float2(1.0, 1.0);
            float r0; Hash_Tchou_2_1_float(c0, r0);
            float r1; Hash_Tchou_2_1_float(c1, r1);
            float r2; Hash_Tchou_2_1_float(c2, r2);
            float r3; Hash_Tchou_2_1_float(c3, r3);
            float bottomOfGrid = lerp(r0, r1, f.x);
            float topOfGrid = lerp(r2, r3, f.x);
            float t = lerp(bottomOfGrid, topOfGrid, f.y);
            return t;
        }
        
        void Unity_SimpleNoise_Deterministic_float(float2 UV, float Scale, out float Out)
        {
            float freq, amp;
            Out = 0.0f;
            freq = pow(2.0, float(0));
            amp = pow(0.5, float(3-0));
            Out += Unity_SimpleNoise_ValueNoise_Deterministic_float(float2(UV.xy*(Scale/freq)))*amp;
            freq = pow(2.0, float(1));
            amp = pow(0.5, float(3-1));
            Out += Unity_SimpleNoise_ValueNoise_Deterministic_float(float2(UV.xy*(Scale/freq)))*amp;
            freq = pow(2.0, float(2));
            amp = pow(0.5, float(3-2));
            Out += Unity_SimpleNoise_ValueNoise_Deterministic_float(float2(UV.xy*(Scale/freq)))*amp;
        }
        
        void Unity_Subtract_float(float A, float B, out float Out)
        {
            Out = A - B;
        }
        
        void Unity_Multiply_float4_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A * B;
        }
        
        float2 Unity_GradientNoise_Deterministic_Dir_float(float2 p)
        {
            float x; Hash_Tchou_2_1_float(p, x);
            return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
        }
        
        void Unity_GradientNoise_Deterministic_float (float2 UV, float3 Scale, out float Out)
        {
            float2 p = UV * Scale.xy;
            float2 ip = floor(p);
            float2 fp = frac(p);
            float d00 = dot(Unity_GradientNoise_Deterministic_Dir_float(ip), fp);
            float d01 = dot(Unity_GradientNoise_Deterministic_Dir_float(ip + float2(0, 1)), fp - float2(0, 1));
            float d10 = dot(Unity_GradientNoise_Deterministic_Dir_float(ip + float2(1, 0)), fp - float2(1, 0));
            float d11 = dot(Unity_GradientNoise_Deterministic_Dir_float(ip + float2(1, 1)), fp - float2(1, 1));
            fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
            Out = lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x) + 0.5;
        }
        
        void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        
        void Unity_Add_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A + B;
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float3 BaseColor;
            float Alpha;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float _Property_d6688d0803924fb38dee8e136cc3cadd_Out_0_Float = _LineSpeed;
            float _Multiply_fa06dfbff0384f80afa949a976c3391e_Out_2_Float;
            Unity_Multiply_float_float(IN.TimeParameters.x, _Property_d6688d0803924fb38dee8e136cc3cadd_Out_0_Float, _Multiply_fa06dfbff0384f80afa949a976c3391e_Out_2_Float);
            float4 _UV_36f471af2cb54fda9e1d37e9071e0e30_Out_0_Vector4 = IN.uv0;
            float _Split_dd57b12fb4eb44ae91568655cf3f4a32_R_1_Float = _UV_36f471af2cb54fda9e1d37e9071e0e30_Out_0_Vector4[0];
            float _Split_dd57b12fb4eb44ae91568655cf3f4a32_G_2_Float = _UV_36f471af2cb54fda9e1d37e9071e0e30_Out_0_Vector4[1];
            float _Split_dd57b12fb4eb44ae91568655cf3f4a32_B_3_Float = _UV_36f471af2cb54fda9e1d37e9071e0e30_Out_0_Vector4[2];
            float _Split_dd57b12fb4eb44ae91568655cf3f4a32_A_4_Float = _UV_36f471af2cb54fda9e1d37e9071e0e30_Out_0_Vector4[3];
            float _Property_4d4fe74a47bf40229853729d71261e7c_Out_0_Float = _LineDistance;
            float _Multiply_58b0ec757a42479ba9f42979605cf9ce_Out_2_Float;
            Unity_Multiply_float_float(_Split_dd57b12fb4eb44ae91568655cf3f4a32_G_2_Float, _Property_4d4fe74a47bf40229853729d71261e7c_Out_0_Float, _Multiply_58b0ec757a42479ba9f42979605cf9ce_Out_2_Float);
            float _Add_b5a76a364bac44ec84ffbbdc23b73547_Out_2_Float;
            Unity_Add_float(_Multiply_fa06dfbff0384f80afa949a976c3391e_Out_2_Float, _Multiply_58b0ec757a42479ba9f42979605cf9ce_Out_2_Float, _Add_b5a76a364bac44ec84ffbbdc23b73547_Out_2_Float);
            float _Fraction_42a78b2d67ea4b9787dd14b06c46772c_Out_1_Float;
            Unity_Fraction_float(_Add_b5a76a364bac44ec84ffbbdc23b73547_Out_2_Float, _Fraction_42a78b2d67ea4b9787dd14b06c46772c_Out_1_Float);
            float _Property_f353072389df41b29c17bc4ef51f8d21_Out_0_Float = _LineThickness;
            float _Step_4f2f93fe176245f6aff32d2e5119907a_Out_2_Float;
            Unity_Step_float(_Fraction_42a78b2d67ea4b9787dd14b06c46772c_Out_1_Float, _Property_f353072389df41b29c17bc4ef51f8d21_Out_0_Float, _Step_4f2f93fe176245f6aff32d2e5119907a_Out_2_Float);
            float _Multiply_3d63573ab8e74e8a843c49c075a5aee9_Out_2_Float;
            Unity_Multiply_float_float(IN.TimeParameters.x, 0.2, _Multiply_3d63573ab8e74e8a843c49c075a5aee9_Out_2_Float);
            float _OneMinus_385627a825f14c35858f7979cf4db484_Out_1_Float;
            Unity_OneMinus_float(_Multiply_3d63573ab8e74e8a843c49c075a5aee9_Out_2_Float, _OneMinus_385627a825f14c35858f7979cf4db484_Out_1_Float);
            float2 _TilingAndOffset_f883c71c436c4c3f95205cf36e0b0cfd_Out_3_Vector2;
            Unity_TilingAndOffset_float(IN.uv0.xy, float2 (1, 1), (_OneMinus_385627a825f14c35858f7979cf4db484_Out_1_Float.xx), _TilingAndOffset_f883c71c436c4c3f95205cf36e0b0cfd_Out_3_Vector2);
            float _SimpleNoise_8cc8d7f439624ba4ba3a4af753fa53b6_Out_2_Float;
            Unity_SimpleNoise_Deterministic_float(_TilingAndOffset_f883c71c436c4c3f95205cf36e0b0cfd_Out_3_Vector2, float(50), _SimpleNoise_8cc8d7f439624ba4ba3a4af753fa53b6_Out_2_Float);
            float _Property_55ef1f9a8db94817974ca1aee6b072ef_Out_0_Float = _LineIntensity;
            float _Subtract_063c65694e434e31ad9f3a6c0a83041b_Out_2_Float;
            Unity_Subtract_float(float(10), _Property_55ef1f9a8db94817974ca1aee6b072ef_Out_0_Float, _Subtract_063c65694e434e31ad9f3a6c0a83041b_Out_2_Float);
            float _Multiply_1e7811574aba4204a2e0824377bcf4a0_Out_2_Float;
            Unity_Multiply_float_float(_SimpleNoise_8cc8d7f439624ba4ba3a4af753fa53b6_Out_2_Float, _Subtract_063c65694e434e31ad9f3a6c0a83041b_Out_2_Float, _Multiply_1e7811574aba4204a2e0824377bcf4a0_Out_2_Float);
            float _OneMinus_e6f07690a06f401b9d0f3d51556b4229_Out_1_Float;
            Unity_OneMinus_float(_Multiply_1e7811574aba4204a2e0824377bcf4a0_Out_2_Float, _OneMinus_e6f07690a06f401b9d0f3d51556b4229_Out_1_Float);
            float _Multiply_998b9393023a4951b460ef427665cc70_Out_2_Float;
            Unity_Multiply_float_float(_Step_4f2f93fe176245f6aff32d2e5119907a_Out_2_Float, _OneMinus_e6f07690a06f401b9d0f3d51556b4229_Out_1_Float, _Multiply_998b9393023a4951b460ef427665cc70_Out_2_Float);
            float _Step_734de3caca7d495190ddc6a8abda031a_Out_2_Float;
            Unity_Step_float(float(0.1), _Multiply_998b9393023a4951b460ef427665cc70_Out_2_Float, _Step_734de3caca7d495190ddc6a8abda031a_Out_2_Float);
            float4 _Multiply_87ba905ec60a4df68aa79c0fe17bcadc_Out_2_Vector4;
            Unity_Multiply_float4_float4(IN.VertexColor, (_Step_734de3caca7d495190ddc6a8abda031a_Out_2_Float.xxxx), _Multiply_87ba905ec60a4df68aa79c0fe17bcadc_Out_2_Vector4);
            float _Property_8354d3ed5df048e6a6b82e32cefe6443_Out_0_Float = _BlinkSpeed;
            float _GradientNoise_640e14a5944246558657b67696338a45_Out_2_Float;
            Unity_GradientNoise_Deterministic_float((_Property_8354d3ed5df048e6a6b82e32cefe6443_Out_0_Float.xx), IN.TimeParameters.y, _GradientNoise_640e14a5944246558657b67696338a45_Out_2_Float);
            float _Remap_7c44592094cb4a95a9a13f2aa8ecaef8_Out_3_Float;
            Unity_Remap_float(_GradientNoise_640e14a5944246558657b67696338a45_Out_2_Float, float2 (0, 1), float2 (0.2, 0.8), _Remap_7c44592094cb4a95a9a13f2aa8ecaef8_Out_3_Float);
            float4 _Multiply_afd488a8fcd2411a9afde4c0f75bd784_Out_2_Vector4;
            Unity_Multiply_float4_float4((_Remap_7c44592094cb4a95a9a13f2aa8ecaef8_Out_3_Float.xxxx), IN.VertexColor, _Multiply_afd488a8fcd2411a9afde4c0f75bd784_Out_2_Vector4);
            float4 _Add_082095f9938b4e99a5750f8fa69c59c2_Out_2_Vector4;
            Unity_Add_float4(_Multiply_87ba905ec60a4df68aa79c0fe17bcadc_Out_2_Vector4, _Multiply_afd488a8fcd2411a9afde4c0f75bd784_Out_2_Vector4, _Add_082095f9938b4e99a5750f8fa69c59c2_Out_2_Vector4);
            float _Property_110dcc6a93cc4be8b2d7a50b4a720500_Out_0_Float = _NoiseSpeed;
            float _Multiply_1a5b8fe72aea4382b7b5ed76c75f7d9a_Out_2_Float;
            Unity_Multiply_float_float(IN.TimeParameters.x, _Property_110dcc6a93cc4be8b2d7a50b4a720500_Out_0_Float, _Multiply_1a5b8fe72aea4382b7b5ed76c75f7d9a_Out_2_Float);
            float2 _Vector2_e2bceb305bd04794b63ec44782ea60c8_Out_0_Vector2 = float2(float(0), _Multiply_1a5b8fe72aea4382b7b5ed76c75f7d9a_Out_2_Float);
            float2 _TilingAndOffset_a3c681b7eb35488e88810b61d192babe_Out_3_Vector2;
            Unity_TilingAndOffset_float(IN.uv0.xy, _Vector2_e2bceb305bd04794b63ec44782ea60c8_Out_0_Vector2, _Vector2_e2bceb305bd04794b63ec44782ea60c8_Out_0_Vector2, _TilingAndOffset_a3c681b7eb35488e88810b61d192babe_Out_3_Vector2);
            float _SimpleNoise_866cdbae09054c9bbaf6b88cfeaa13fe_Out_2_Float;
            Unity_SimpleNoise_Deterministic_float(_TilingAndOffset_a3c681b7eb35488e88810b61d192babe_Out_3_Vector2, float(400), _SimpleNoise_866cdbae09054c9bbaf6b88cfeaa13fe_Out_2_Float);
            float _Remap_f9983e6cfce1466291bb1bb8eb4464cf_Out_3_Float;
            Unity_Remap_float(_SimpleNoise_866cdbae09054c9bbaf6b88cfeaa13fe_Out_2_Float, float2 (-1, 1), float2 (0.65, 1), _Remap_f9983e6cfce1466291bb1bb8eb4464cf_Out_3_Float);
            float4 _Multiply_54ca128f4b7d4d4aa0d31195cec74b27_Out_2_Vector4;
            Unity_Multiply_float4_float4(_Add_082095f9938b4e99a5750f8fa69c59c2_Out_2_Vector4, (_Remap_f9983e6cfce1466291bb1bb8eb4464cf_Out_3_Float.xxxx), _Multiply_54ca128f4b7d4d4aa0d31195cec74b27_Out_2_Vector4);
            float _Split_230fcfdafd6a46fdafa9142a902700ab_R_1_Float = IN.VertexColor[0];
            float _Split_230fcfdafd6a46fdafa9142a902700ab_G_2_Float = IN.VertexColor[1];
            float _Split_230fcfdafd6a46fdafa9142a902700ab_B_3_Float = IN.VertexColor[2];
            float _Split_230fcfdafd6a46fdafa9142a902700ab_A_4_Float = IN.VertexColor[3];
            surface.BaseColor = (_Multiply_54ca128f4b7d4d4aa0d31195cec74b27_Out_2_Vector4.xyz);
            surface.Alpha = _Split_230fcfdafd6a46fdafa9142a902700ab_A_4_Float;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
        
            #if UNITY_UV_STARTS_AT_TOP
            #else
            #endif
        
        
            output.uv0 = input.texCoord0;
            output.VertexColor = input.color;
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/UnlitPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }  
    }
}