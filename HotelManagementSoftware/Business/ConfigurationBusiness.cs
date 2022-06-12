using HotelManagementSoftware.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HotelManagementSoftware.Business
{
    public class ConfigurationBusiness
    {
        public async Task<int?> GetConfig(string name)
        {
            using (var db = new Database())
            {
                Configuration? config = await db.Configurations.FirstOrDefaultAsync(i => i.Name == name);
                if (config == null)
                    return null;
                return config.Value;
            }
        }

        public async Task SetConfig(string name, int value)
        {
            using (var db = new Database())
            {
                Configuration config = await db.Configurations.FirstAsync(i => i.Name == name);
                config.Value = value;
                await db.SaveChangesAsync();
            }
        }
    }
}
