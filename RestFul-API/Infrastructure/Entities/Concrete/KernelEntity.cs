
using RestFul_API.Infrastructure.Entities.Abstract;
using System;


namespace RestFul_API.Infrastructure.Entities.Concrete
{
    public class KernelEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }

        private DateTime _createDate = DateTime.Now;
        public DateTime CreateDate { get { return _createDate; } set { _createDate = value; } }

        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }



        private Status _status = Status.Active;
        public Status Status { get { return _status; } set { _status = value; } }
    }
}
