using UnityEngine.Rendering.Universal;

namespace UnityEngine.Rendering.Universal
{
    public class CustomBloomRenderFeature : ScriptableRendererFeature
    {
        [System.Serializable]
        public class CustomBloomSettings
        {
            public RenderPassEvent Event = RenderPassEvent.AfterRenderingOpaques;

            public Shader shader;
        }

        public CustomBloomSettings settings = new CustomBloomSettings();

        CustomBloomRenderPass _bloomRenderPass;

        public override void Create()
        {
            _bloomRenderPass = new CustomBloomRenderPass(settings.Event, settings.shader, name);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            var src = renderer.cameraColorTarget;
            var dest =  RenderTargetHandle.CameraTarget;

            if (settings.shader == null)
            {
                Debug.LogWarningFormat("Missing Shader");
                return;
            }

            _bloomRenderPass.Setup(src, dest);
            renderer.EnqueuePass(_bloomRenderPass);
        }
    }
}

