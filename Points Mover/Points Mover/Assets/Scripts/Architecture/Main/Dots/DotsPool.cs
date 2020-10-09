using System.Collections.Generic;
using Modules;

namespace Architecture.Main.Dots
{
    public class DotsPool
    {
        private readonly Stack<DotItem> pool = new Stack<DotItem>();
        private const string elementName = "Dot";

        public DotItem GetDot()
        {
            return pool.Count == 0 ? CreateNewDot() : pool.Pop();
        }

        public void SetDot(DotItem dotItem)
        {
            SceneResources.Set(dotItem.Transform.gameObject);
            pool.Push(dotItem);
        }
        
        private static DotItem CreateNewDot()
        {
            var element = SceneResources.GetPreparedCopy(elementName).transform;
            var result = new DotItem(element);

            return result;
        }
    }
}
