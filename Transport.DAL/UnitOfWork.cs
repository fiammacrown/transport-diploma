using Microsoft.EntityFrameworkCore;
using Transport.DAL.Data;
using Transport.DAL.Interfaces;

namespace Transport.DAL
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationDbContext _context;

		private bool _disposed = false;

		public UnitOfWork(
            ApplicationDbContext context,
            IDeliveriesRepo deliveriesRepo,
            ITransportsRepo transportsRepo,
            ILocationsRepo locationsRepo,
            IOrdersRepo ordersRepo)
        {
            _context = context;
            DeliveryRepository = deliveriesRepo;
            TransportRepository = transportsRepo;
            LocationRepository = locationsRepo;
            OrderRepository = ordersRepo;
        }

        public IDeliveriesRepo DeliveryRepository { get; }

        public ITransportsRepo TransportRepository { get; }

        public ILocationsRepo LocationRepository { get; }

        public IOrdersRepo OrderRepository { get; }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //// Обработка конфликта
                //var entry = ex.Entries.Single();
                //var databaseValues = entry.GetDatabaseValues();
                //if (databaseValues != null)
                //{
                //    // Перезагрузка сущности из базы данных
                //    entry.OriginalValues.SetValues(databaseValues);
                //}
                //else
                //{
                //    // Сущность была удалена в базе данных
                //    // Ваша логика обработки удаления
                //}
                //// Попытка обновления сущности снова
                //_context.SaveChanges();
            }
        }

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}
			_disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
