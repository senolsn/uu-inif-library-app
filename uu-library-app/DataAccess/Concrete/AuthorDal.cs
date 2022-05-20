﻿using MySql.Data.MySqlClient;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.DataAccess.Abstract;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.DataAccess.Concrete
{
    public class AuthorDal : IAuthorDal
    {
        MySqlConnection conn = new MySqlConnection("Server=172.21.54.3;uid=ASSEMSoft;pwd=Assemsoft1320..!;database=ASSEMSoft");
        public void Add(Author author)
        {
            conn.Open();
            MySqlCommand commandToAdd = new MySqlCommand("INSERT INTO Author (id, firstName, lastName) VALUES (@p1, @p2, @p3)", conn);
            try
            {
                commandToAdd.Parameters.AddWithValue("@p1", author.Id);
                commandToAdd.Parameters.AddWithValue("@p2", author.FirstName);
                commandToAdd.Parameters.AddWithValue("@p3", author.LastName);
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

        public void Delete(string id)
        {
            conn.Open();
            try
            {
                MySqlCommand commandToUpdate = new MySqlCommand("UPDATE Author SET deleted=1 WHERE id=@p1 ", conn);
                commandToUpdate.Parameters.AddWithValue("@p1", id);
                commandToUpdate.ExecuteNonQuery();

            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
            conn.Close();
        }

        List<Author> authors;
        public List<Author> getAll()
        {
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Author WHERE deleted=false", conn);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    Author author = new Author();
                    author.Id= reader[0].ToString();
                    author.FirstName = reader[1].ToString();
                    author.LastName = reader[2].ToString();
                    author.CreatedAt = Convert.ToDateTime(reader[3]);
                    author.Deleted = Convert.ToBoolean(reader[4]);
                    authors.Add(author);
                }
                conn.Close();
                return authors;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }

        }

        public void Update(Author author)
        {
            conn.Open();
            try
            {
                MySqlCommand commandToUpdate = new MySqlCommand("UPDATE Author SET (firstName=@p2, lastName=@p3) WHERE id=@p1 ", conn);
                commandToUpdate.Parameters.AddWithValue("@p1", author.Id);
                commandToUpdate.Parameters.AddWithValue("@p2", author.FirstName);
                commandToUpdate.Parameters.AddWithValue("@p3", author.LastName);
                commandToUpdate.ExecuteNonQuery();

            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }

            conn.Close();
        }
    }
}
