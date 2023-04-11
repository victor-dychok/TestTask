using Microsoft.EntityFrameworkCore;
using testTask.Data.Interfaces;
using testTask.Data.Models;

namespace testTask.Data.Repository
{
    public class SightsRepository : IAllSights
    {
        private readonly AppContext _context;
        
        public SightsRepository(AppContext context) 
        {
            _context = context;
        }

        public IEnumerable<Sight> Sights => _context.Sights.Include(l => l.Location);
        
        public void AddSight(Sight sight)
        {
            if(sight != null)
            {
                if(sight.Location != null)
                {
                    var location = _context.Locations.First(s => s.GeoPosition == sight.Location.GeoPosition);
                    if(location == null)
                    {
                        _context.Locations.Add(sight.Location);
                        sight.Location = _context.Locations.First(s => s.GeoPosition == sight.Location.GeoPosition);
                    }
                    else
                    {
                        sight.Location = location;
                    }
                    _context.Sights.Add(sight);
                    _context.SaveChanges();
                }
            }
        }

        public void DeleteSight(Sight sight)
        {
            if(sight != null)
            {
                _context.Sights.Remove(sight);
                _context.SaveChanges();
            }
        }

        public void UpdateSight(Sight sight)
        {
            if(sight != null)
            {
                var location = _context.Locations.First(s => s.GeoPosition == sight.Location.GeoPosition);
                if (location == null)
                {
                    _context.Locations.Add(sight.Location);
                    sight.Location = _context.Locations.First(s => s.GeoPosition == sight.Location.GeoPosition);
                }
                else
                {
                    sight.Location = location;
                }
                
                _context.Sights.Update(sight);
                _context.SaveChanges();
            }
        }
    }
}
