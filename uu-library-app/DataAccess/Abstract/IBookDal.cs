﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.DataAccess.Abstract
{
    public interface IBookDal
    {
        List<Book> getAll();
        List<Book> getAllByCategory(string categoryId);
        List<Book> getAllSortedByName();
        List<Book> getAllSortedByAddedDate();
        Book getById(string id);
        Book getByCategoryId(string categoryId);
        Book getByAuthorId(string authorId);
        Book getByLanguageId(string languageId);
        Book getByLocationId(string locationId);
        Book getByPublisherId(string publisherId);

        List<Book> getAllByName(string name);
        void Add(Book book);
        void Update(Book book);
        void Delete(Book book);

        

    }
}
