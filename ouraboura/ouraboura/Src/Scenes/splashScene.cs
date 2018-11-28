using System.Collections;
using Microsoft.Xna.Framework;
using Nez;

namespace ouraboura.Src.Scenes
{
    class splashScene : Scene
    {
        //public Texture2D title;
        //private Entity pretendoTitle;
        //private Vector2 pos;
        //public const int SCREEN_SPACE_RENDER_LAYER = 999;
        //private const int POSTPROCESSLAYER = 50;
        //private HeatDistortionPostProcessor _heatDistortion;
        //private RenderLayerRenderer _ppLayer;
        //ScreenSpaceRenderer _screenSpaceRenderer;

        public override void initialize()
        {
            base.initialize();
            clearColor = new Color(133, 224, 229, 100);
            addRenderer(new DefaultRenderer());
            //_screenSpaceRenderer = addRenderer(new ScreenSpaceRenderer(-1, SCREEN_SPACE_RENDER_LAYER));
            //addRenderer(new RenderLayerExcludeRenderer(0, SCREEN_SPACE_RENDER_LAYER, POSTPROCESSLAYER));

            //_ppLayer = addRenderer(new RenderLayerRenderer(-1, POSTPROCESSLAYER));
            //_ppLayer.renderTexture = new RenderTexture();
            //_ppLayer.renderTargetClearColor = new Color(133, 224, 229, 100);
            //_ppLayer.renderTexture = new RenderTexture();

            //_heatDistortion = addPostProcessor(new HeatDistortionPostProcessor(-1));

            ////addRenderer(new DefaultRenderer());
            //gameManager.Instance.currentScene = (int)gameManager.GameScenes.splashScene;

            //pretendoTitle = createEntity("pretendo_title");
            //pretendoTitle.addComponent(new pretenDropComponent());
            Core.startCoroutine(transition());
        }

        IEnumerator transition()
        {
            yield return Coroutine.waitForSeconds(2f);
            gameManager.Instance.LoadSceneWithTransition(gameManager.Transitions.fade, new titleScene());
        }
    }
}
