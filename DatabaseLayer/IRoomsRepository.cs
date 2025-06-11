using Model;

namespace DatabaseLayer
{
    public interface IRoomsRepository : IRepository<Room>
    {
        public int GetMaxId();

    }
}
