using System.Collections.Generic;

namespace CodeBase.Domain.Location.View
{
    public interface ILevelActivator
    {
        void ActivateLevels(List<LevelView> levelViewsList, Location location);
    }
}
