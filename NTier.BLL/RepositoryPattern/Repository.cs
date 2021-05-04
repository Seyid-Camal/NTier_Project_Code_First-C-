﻿using NTier.BLL.SingletonPattern;
using NTier.DAL.Context;
using NTier.Model.Entity;
using NTier.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace NTier.BLL.RepositoryPattern
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        internal readonly DataContext _db;

        public Repository()
        {
            _db = Tools.DbInstance;
        }

        public virtual ResultModel<T> Delete(int Id)
        {
            ResultModel<T> result = new ResultModel<T>();
            T deleted = _db.Set<T>().Remove(this.SelectById(Id));
            if (_db.SaveChanges() > 0)
            {
                result.IsSuccess = true;
                result.Message = typeof(T).Name + " " + "Silme işlemi Başarılı";
                result.Data = deleted;
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "Hata";
            }
            return result;
        }

        public virtual ResultModel<T> Insert(T item)
        {
            ResultModel<T> result = new ResultModel<T>();
            _db.Set<T>().Add(item);
            if (_db.SaveChanges() > 0)
            {
                result.IsSuccess = true;
                result.Message = typeof(T).Name + " Ekleme İşlemi Başarılı";
                result.Data = item;
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "Hata";
            }
            return result;
        }

        public virtual T SearchEntity(Expression<Func<T, bool>> predicate)
        {
            return _db.Set<T>().FirstOrDefault(predicate);
        }

        public virtual List<T> SearchList(Expression<Func<T, bool>> predicate)
        {
            return _db.Set<T>().Where(predicate).ToList();
        }

        public virtual List<T> SelectAll()
        {
            return _db.Set<T>().ToList();
        }

        public virtual T SelectById(int Id)
        {
            return _db.Set<T>().Find(Id);
        }

        public virtual ResultModel<T> Update(T item)
        {
            ResultModel<T> result = new ResultModel<T>();
            _db.Set<T>().Attach(item);
            _db.Entry(item).State = EntityState.Modified;
            if (_db.SaveChanges() > 0)
            {
                result.IsSuccess = true;
                result.Message = typeof(T).Name + " Güncelleme İşlemi Başarılı";
                result.Data = item;
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "Hata";
            }
            return result;
        }
    }
}
