using System;
using System.Threading.Tasks;
using WINConnect.Data.Configuration.EntityFramework;
using WINConnect.Models;

namespace WINConnect.Data
{
    public class UnitOfWork : IDisposable
    {
        private readonly WINContext _context;

        public UnitOfWork()
        {
            _context = new WINContext();
        }

        public UnitOfWork(WINContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private Repository<ListValues> _listvaluesRepo { get; set; }
        public Repository<ListValues> ListValuesRepository
        {
            get { return _listvaluesRepo ?? (_listvaluesRepo = new Repository<ListValues>(_context)); }
        }

        private Repository<Role> _roleRepo { get; set; }
        public Repository<Role> RolesRepository
        {
            get { return _roleRepo ?? (_roleRepo = new Repository<Role>(_context)); }
        }

        private Repository<Agent> _agentRepo { get; set; }
        public Repository<Agent> AgentRepository
        {
            get { return _agentRepo ?? (_agentRepo = new Repository<Agent>(_context)); }
        }

        private Repository<Shipment> _shipmentRepo { get; set; }
        public Repository<Shipment> ShipmentRepository
        {
            get { return _shipmentRepo ?? (_shipmentRepo = new Repository<Shipment>(_context)); }
        }

        private Repository<Contact> _contactRepo { get; set; }
        public Repository<Contact> ContactRepository
        {
            get { return _contactRepo ?? (_contactRepo = new Repository<Contact>(_context)); }
        }

        private Repository<SeaBooking> _seaBookingRepo { get; set; }
        public Repository<SeaBooking> SeaBookingRepository
        {
            get { return _seaBookingRepo ?? (_seaBookingRepo = new Repository<SeaBooking>(_context)); }
        }

        private Repository<AirBooking> _airBookingRepo { get; set; }
        public Repository<AirBooking> AirBookingRepository
        {
            get { return _airBookingRepo ?? (_airBookingRepo = new Repository<AirBooking>(_context)); }
        }

        private Repository<ViewMAWB> _airFreightRepo { get; set; }
        public Repository<ViewMAWB> AirFreightRepository
        {
            get { return _airFreightRepo ?? (_airFreightRepo = new Repository<ViewMAWB>(_context)); }
        }
    }
}