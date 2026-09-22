using HN_Backend.Data;
using HN_Backend.Helpers;
using HN_Backend.Interface;
using Microsoft.EntityFrameworkCore;

namespace HN_Backend.Repository
{
    public class EventNoOrCodeGenerationRepository : IEventNoOrCodeGeneration
    {
        private readonly ApplicationDbContext _db;
        private readonly CurrentSessionData _currentSessionData;
        public EventNoOrCodeGenerationRepository(ApplicationDbContext db,CurrentSessionData currentSessionData)
        {
            _db = db;
            _currentSessionData = currentSessionData;
        }

        public async Task<string> EventNoGeneration(string eventType, string prefix, long companyId, long locationId)
        {
            int currentYear = DateTime.Now.Year;
            var eventGeneration = await _db.EventNoGenerations.FromSqlInterpolated($@"
                SELECT *
                FROM EventNoGeneration WITH (UPDLOCK, ROWLOCK)
                WHERE EventType = {eventType}
                AND CompanyId = {companyId}
                AND LocationId = {locationId}
                AND EntryYear = {currentYear}").FirstOrDefaultAsync(); 
            if (eventGeneration == null)
            {
                var eventNoOrCodeGeneration = new EventNoGeneration
                {
                    EventType = eventType,
                    Prefix = prefix,
                    CompanyId = companyId,
                    LocationId = locationId,
                    EntryYear = currentYear,
                    CurrentNumber = 000001
                }; 
                await _db.EventNoGenerations.AddAsync(eventNoOrCodeGeneration); 
                return $"{eventNoOrCodeGeneration.Prefix}-{currentYear}-{eventNoOrCodeGeneration.CurrentNumber:D6}";
            }
            else
            {
                eventGeneration.CurrentNumber += 1;
                eventGeneration.UpdateOn = DateTime.UtcNow;
                eventGeneration.UpdateBy = _currentSessionData.UserId;
                _db.EventNoGenerations.Update(eventGeneration);
                return $"{eventGeneration.Prefix}-{eventGeneration.CompanyCode}{eventGeneration.LocationCode}{currentYear}-{eventGeneration.CurrentNumber:D6}";
            } 
        }


        public async Task<string> EventCodeGeneration(string eventType, string prefix, long companyId)
        {
            int currentYear = DateTime.Now.Year;
            var eventGeneration = await _db.EventCodeGenerations.FromSqlInterpolated($@"
                SELECT *
                FROM EventCodeGeneration WITH (UPDLOCK, ROWLOCK)
                WHERE EventType = {eventType}
                AND CompanyId = {companyId}").FirstOrDefaultAsync();
            if (eventGeneration == null)
            {
                var eventNoOrCodeGeneration = new EventCodeGeneration
                {
                    EventType = eventType,
                    Prefix = prefix,
                    CompanyId = companyId,  
                    CurrentNumber = 000001
                };
                await _db.EventCodeGenerations.AddAsync(eventNoOrCodeGeneration);
                return $"{eventNoOrCodeGeneration.Prefix}-{eventNoOrCodeGeneration.CurrentNumber:D6}";
            }
            else
            {
                eventGeneration.CurrentNumber += 1;
                eventGeneration.UpdateOn = DateTime.Now;
                eventGeneration.UpdateBy = _currentSessionData.UserId; 
                _db.EventCodeGenerations.Update(eventGeneration);
                return $"{eventGeneration.Prefix}{eventGeneration.CompanyCode}-{eventGeneration.CurrentNumber:D6}";
            }
        }

    }
}
