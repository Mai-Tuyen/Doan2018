using DigitalShop.Entity;
using DigitalShop.Service;
using DigitalShop.Service.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitialShop.Service.Repository
{
    public class ImportRepository : IImportRepository
    {
        private readonly DigitalDBContext context;
        public ImportRepository(DigitalDBContext context)
        {
            this.context = context;
        }
        public void Add(Import import)
        {
            context.Import.Add(import);
            context.SaveChanges();
        }
        public void AddImportDetail(ImportDetail importDetail)
        {
            context.ImportDetail.Add(importDetail);
            context.SaveChanges();
        }

        public Import GetById(int id)
        {
            return context.Import.Find(id);
        }

        public List<Import> GetListImport()
        {
            return context.Import.Include(p => p.Admin)
                .OrderBy(x => x.CreateAt)
                .ToList();
        }

        public List<ImportDetail> GetListImportDetail(int id)
        {
            return context.ImportDetail.Include(p => p.Product)
                .Include(q => q.Import)
                .Where(x=>x.ImportId==id)
                .OrderBy(x => x.Quantity)
                .ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
