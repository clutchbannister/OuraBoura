using Microsoft.Xna.Framework;
using Nez;

namespace ouraboura.Src.Scenes
{
    class titleScene : Scene
    {

        public override void initialize()
        {
            base.initialize();
            clearColor = new Color(255, 225, 230, 100);
            addRenderer(new DefaultRenderer());
        }
    }
}
