using NTier.DAL.Context;

namespace NTier.BLL.SingletonPattern
{
    public class Tools
    {
        private static DataContext _dbInstance;

        public static DataContext DbInstance
        {
            get
            {
                if (_dbInstance == null)
                    _dbInstance = new DataContext();
                return _dbInstance;
            }
        }
    }
}
