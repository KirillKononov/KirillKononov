using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.DataAccess;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BLL.Repositories
{
    //class HomeWorkRepository : IRepository<HomeWork>
    //{
    //    private readonly DataBaseContext _db;
    //    private readonly ILogger _logger;

    //    public HomeWorkRepository(DataBaseContext context, ILogger logger)
    //    {
    //        _db = context;
    //        _logger = logger;
    //    }

    //    public IEnumerable<HomeWork> GetAll()
    //    {
    //        if (!_db.HomeWorks.Any())
    //        {
    //            _logger.LogError("There is no home works in data base");
    //            throw new ValidationException("There is no home works in data base");
    //        }

    //        return _db.HomeWorks;
    //    }

    //    public HomeWork Get(int? id)
    //    {
    //        if (id == null)
    //        {
    //            _logger.LogError("Home work's id hasn't entered");
    //            throw new ValidationException("Home work's id hasn't entered");
    //        }

    //        if (_db.HomeWorks.Find(id) == null)
    //        {
    //            _logger.LogError("There is no home work in data base with this id");
    //            throw new ValidationException("There is no home work in data base with this id");
    //        }

    //        return _db.HomeWorks.Find(id);
    //    }

    //    public void Create(HomeWork item)
    //    {
    //        _db.HomeWorks.Add(item);
    //    }

    //    public void Update(HomeWork item)
    //    {
    //        _db.Entry(item).State = EntityState.Modified;
    //    }

    //    public IEnumerable<HomeWork> Find(Func<HomeWork, bool> predicate)
    //    {
    //        return _db.HomeWorks
    //            .Where(predicate)
    //            .ToList();
    //    }

    //    public void Delete(int? id)
    //    {
    //        if (id == null)
    //        {
    //            _logger.LogError("Home work's id hasn't entered");
    //            throw new ValidationException("Home work's id hasn't entered");
    //        }

    //        var attendance = _db.HomeWorks.Find(id);

    //        if (attendance != null)
    //            _db.HomeWorks.Remove(attendance);
    //    }
    //}
}
