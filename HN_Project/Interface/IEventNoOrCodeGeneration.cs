using HN_Backend.Data;

namespace HN_Backend.Interface
{
    public interface IEventNoOrCodeGeneration
    {
        public Task<string> EventNoGeneration(string eventType,string prefix, long companyId, long locationId);
        public Task<string> EventCodeGeneration(string eventType,string prefix, long companyId);
    }
}
