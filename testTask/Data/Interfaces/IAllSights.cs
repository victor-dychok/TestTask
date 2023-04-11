using testTask.Data.Models;

namespace testTask.Data.Interfaces
{
    public interface IAllSights
    {
        IEnumerable<Sight> Sights { get; }

        void AddSight(Sight sight);
        void UpdateSight(Sight sight);
        void DeleteSight(Sight sight);

        List<Location> GetOriginalRegions();
    }
}
