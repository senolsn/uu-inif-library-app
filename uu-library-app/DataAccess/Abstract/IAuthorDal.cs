﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.DataAccess.Abstract
{
    public interface IAuthorDal
    {
        List<Author> getAll();
        void Add(Author author);
        void Update(Author author);
        void Delete(Author author);
        Author getById(string id);
    }
}
