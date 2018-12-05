using DigitalShop.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalShop.Service.IRepository
{
    public interface IImportRepository
    {
        void Add(Import import);
        void AddImportDetail(ImportDetail importDetail);
        List<Import> GetListImport();
        List<ImportDetail> GetListImportDetail(int id);
        Import GetById(int id);
        void Save();

    }
}
