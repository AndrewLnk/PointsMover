using Architecture.Components;
using Architecture.Main.Dots;

namespace Architecture.Main
{
    public class ModulesLocation
    {
        private static ModulesLocation instance;
        public static ModulesLocation Instance 
            => instance ?? (instance = new ModulesLocation());

        public Player Player;
        public DotsHolder DotsHolder;
        public DotsPositionCreator DotsPositionCreator;
        public DotsLineRenderer DotsLineRenderer;
        
        
        private ModulesLocation() { }
    }
}
