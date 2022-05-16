﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql;
using MySql.Data.MySqlClient;
using uu_library_app.DataAccess.Abstract;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.DataAccess.Concrete.EntityFramework
{

    public class DepositBookDal : IDepositBookDal
    {
        MySqlConnection conn = new MySqlConnection("Server=172.21.54.3;uid=ASSEMSoft;pwd=Assemsoft1320..!;database=ASSEMSoft");
        public void Add(DepositBook depositBook)
        {
            conn.Open();
            MySqlCommand commandToAdd = new MySqlCommand("INSERT INTO DepositBook (id,studentId,bookId,depositDate,createdAt,deleted) VALUES (@p1, @p2, @p3, @p4, @p5, @p6)", conn);
            try
            {
                commandToAdd.Parameters.AddWithValue("@p1", depositBook.Id);
                commandToAdd.Parameters.AddWithValue("@p2", depositBook.StudentId);
                commandToAdd.Parameters.AddWithValue("@p3", depositBook.BookId);
                commandToAdd.Parameters.AddWithValue("@p4", depositBook.DepositDate);
                commandToAdd.Parameters.AddWithValue("@p5", depositBook.CreatedAt);
                commandToAdd.Parameters.AddWithValue("@p6", depositBook.Deleted);
                commandToAdd.ExecuteNonQuery();
                Console.WriteLine("Başarıyla eklendi!");
            }
            catch (Exception)
            {
                Console.WriteLine("Hatalı ekleme!");
                throw;
            }

            conn.Close();
        }

        public void Delete(DepositBook depositBook)
        {
            throw new NotImplementedException();
        }

        public List<DepositBook> getAll()
        {
            throw new NotImplementedException();
        }

        public DepositBook getById(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(DepositBook depositBook)
        {
            throw new NotImplementedException();
        }
    }
}