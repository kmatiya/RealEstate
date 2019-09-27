using Generic_Repository_and_Unit_of_Work.Unit_of_Work;
using TipezeNyumbaService.Models;

namespace TipezeNyumbaRESTAPI.Models
{
    public class UnitOfWorkInstance
    {
        internal  readonly GenericUoW TipezeNyumbaUnitOfWork = new GenericUoW(new FindAHouseEntities());
    }
    public enum SystemCodes
    {
        Successful,
        ItemNotAvailable,
        NoDatabaseConnection,
        InvalidLoginCredintials
    }
}
