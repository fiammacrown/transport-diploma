using Abeslamidze_Kursovaya7.Interfaces;
using Abeslamidze_Kursovaya7.Models;
using Abeslamidze_Kursovaya7.Repos;
using System;

namespace Abeslamidze_Kursovaya7
{
    public class UnitOfWork : IDisposable
    {
        private readonly EntityContext context = new EntityContext("DbConnection");
        private readonly DbContextLock dbContextLock = new DbContextLock();

        private IDeliveriesRepo? deliveriesRepo;
        private ITransportsRepo? transportsRepo;
        private ILocationsRepo? locationsRepo;
        private IOrdersRepo? ordersRepo;
        public IDeliveriesRepo DeliveryRepository
        {
            get
            {

                if (deliveriesRepo == null)
                {
                    deliveriesRepo = new DatabaseDeliveriesRepo(context);
                }
                return deliveriesRepo;
            }
        }

        public ITransportsRepo TransportRepository
        {
            get
            {

                if (transportsRepo == null)
                {
                    transportsRepo = new DatabaseTransportsRepo(context);
                }
                return transportsRepo;
            }
        }
        public ILocationsRepo LocationRepository
        {
            get
            {

                if (locationsRepo == null)
                {
                    locationsRepo = new DatabaseLocationRepo(context);
                }
                return locationsRepo;
            }
        }


        public IOrdersRepo OrderRepository
        {
            get
            {

                if (ordersRepo == null)
                {
                    ordersRepo = new DatabaseOrdersRepo(context);
                }
                return ordersRepo;
            }
        }

        public void Save()
        {
            if (!dbContextLock.IsLocked)
            {
                dbContextLock.Lock();
                try
                {
                    context.SaveChanges();
                }
                finally
                {
                    dbContextLock.Unlock();
                }
            }
        }

        private bool disposed = false;


        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            dbContextLock.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
