

using System;

namespace RestFul_API.Infrastructure.Entities.Abstract
{

    public enum Status
    {
        None,
        Active,
        Modified,
        Passive
    }

    public interface IEntity<T>
    {
        T Id { get; set; }

        DateTime CreateDate { get; set; }
        DateTime? UpdateDate { get; set; }
        DateTime? DeleteDate { get; set; }

        Status Status { get; set; }
    }
}
