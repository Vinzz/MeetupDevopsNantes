namespace FabrikamFiber.DAL.Data
{
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    using FabrikamFiber.DAL.Models;
    using System.Collections.Generic;

    public interface IAlertRepository
    {
        IQueryable<Alert> All { get; }

        IQueryable<Alert> AllIncluding(params Expression<Func<Alert, object>>[] includeProperties);

        Alert Find(int id);

        void InsertOrUpdate(Alert alert);

        void Delete(int id);

        void Save();
    }

    public class AlertRepository : IAlertRepository
    {
        private readonly FabrikamFiberWebContext context = new FabrikamFiberWebContext();

        public IQueryable<Alert> All
        {
            get { return new List<Alert>().AsQueryable(); }
        }

        public IQueryable<Alert> AllIncluding(params Expression<Func<Alert, object>>[] includeProperties)
        {
            return new List<Alert>().AsQueryable();
        }

        public Alert Find(int id)
        {
            return this.context.Alerts.Find(id);
        }

        public void InsertOrUpdate(Alert alert)
        {
            if (alert.ID == default(int))
            {
                this.context.Alerts.Add(alert);
            }
            else
            {
                this.context.Entry(alert).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var alert = this.context.Alerts.Find(id);
            this.context.Alerts.Remove(alert);
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}