using CovertCode.Services.Secret.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovertCode.Services.Secret.Infrastructure.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Add(T item);
        void Remove(int id);
        void Update(T item);
        T FindByID(int id);
        IEnumerable<T> FindAll();
    }

}