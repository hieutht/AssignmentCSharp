using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using DatabaseHandler.Controller;
using DatabaseHandler.Entity;
using DatabaseHandler.Helper;
using DatabaseHandler.Model;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Math;

namespace DatabaseHandler
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Order order = new Order()
            {
                Id = "Order001",
                CreatedAt = DateTime.Now,
                TotalMoney = 20000,
                CreatedById = "User01",
                OrderDetails = new List<OrderDetail>()
                {
                    new OrderDetail()
                    {
                        OrderId = "Order001",
                        ProductId = "Product001",
                        Quantity = 3,
                        UnitPrice = 1000
                    },
                    new OrderDetail()
                    {
                        OrderId = "Order001",
                        ProductId = "Product002",
                        Quantity = 2,
                        UnitPrice = 2000
                    },
                    new OrderDetail()
                    {
                        OrderId = "Order001",
                        ProductId = "Product003",
                        Quantity = 5,
                        UnitPrice = 1500
                    }
                }
            };

        
            // var clazz = new ClassRoom();
            // clazz.Name = "T1908M";
            // clazz.RoomName = "A10";
            // clazz.Students = new List<Student>();
            // // var student = new Student()
            // // {
            // //     Username = "hacongmanh",
            // //     Password = "congmanh",
            // //     Salt = "iot",
            // //     Status = 1
            // // };
            // // clazz.Students.Add(student);
            // clazz.Students.Add(new Student()
            // {
            //     Username = "hacongmanh",
            //     Password = "congmanh",
            //     Salt = "iot",
            //     Status = 1,
            //     Detail = new StudentDetail()
            //     {
            //         FirstName = "Hạ",
            //         LastName = "Mành Công",
            //         Email = "hacongmanh@gmail.com"
            //     }
            // });
            // clazz.Students.Add(new Student()
            // {
            //     Username = "manhkhongyeu",
            //     Password = "yeukhong",
            //     Salt = "bien",
            //     Status = 1,
            //     Detail = new StudentDetail()
            //     {
            //         FirstName = "Yều",
            //         LastName = "Không Mánh",
            //         Email = "yeukhongmanh@gmail.com"
            //     }
            // });
            // clazz.Students.Add(new Student()
            // {
            //     Username = "vuonghathanh",
            //     Password = "calopbiet",
            //     Salt = "iot",
            //     Status = 1,
            //     Detail = new StudentDetail()
            //     {
            //         FirstName = "Thành",
            //         LastName = "Trấn",
            //         Email = "tranthanh@gmail.com"
            //     }
            // });
            // foreach (var st in clazz.Students)
            // {
            //     Console.WriteLine(st.Username);
            //     Console.WriteLine(st.Detail.FirstName);
            //     Console.WriteLine(st.Detail.LastName);
            //     Console.WriteLine(st.Status);
            // }
        }
    }
}