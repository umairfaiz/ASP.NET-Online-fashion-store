using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CorrectTemp.Models;

namespace CorrectTemp.DataAccess
{
    public class DA
    {

        public static bool UserIsValid(string username, string password)
        {
            BWAEntities context = new BWAEntities();
            try
            {
                Account a = context.Accounts.Single(x => x.username == username && x.password == password);
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
        public static bool adminIsValid(string username, string password)
        {
            BWAEntities context = new BWAEntities();
            string userType = "admin";
            try
            {
                Account a = context.Accounts.Single(x => x.username == username && x.password == password && x.userType == userType);
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
        public static bool AddUser(int userID, string username, string shippingAddress, string cardNumber, string email, string password, string userType)
        {
            BWAEntities context = new BWAEntities();
            try
            {
                Account a = new Account();
                a.userID = random_numberGen.randNumber();
                a.username = username;
                a.shippingAddress = shippingAddress;
                a.cardNumber = cardNumber;
                a.email = email;
                a.password = password;
                a.userType = "customer";

                context.Accounts.Add(a);
                context.SaveChanges();
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        public static bool userCart(List<Product> products, string username)
        {
            BWAEntities context = new BWAEntities();
            try
            {
                foreach(var item in products)
                {
                    Cart cartItem = new Cart
                    {
                        cost = item.Price,
                        productName = item.ItemName,
                        itemID = item.ItemID,
                        quantity = item.Quantity,
                        username = username,
                        total = item.Price * item.Quantity
                    };
                    context.Carts.Add(cartItem);
                    context.SaveChanges();
                }
                
                return true;
            }
            catch (InvalidOperationException ex)
            {
                ex.ToString();
                return false;
            }
        }
        public static bool message_portal(int messageID, string username, string subject, string messageBody, string receive_end)
        {
            BWAEntities context = new BWAEntities();
            try
            {
                Message m = new Message();

                m.msgID = random_numberGen.randNumber();
                m.username = username;
                m.subject = subject;
                m.messageDetail = messageBody;
                m.reciever = "admin@dailyshop.com";


                context.Messages.Add(m);
                context.SaveChanges();

                return true;
            }
            catch (InvalidOperationException) { return false; }
        }
        public static bool remove_mail(int id)
        {

            BWAEntities context = new BWAEntities();
            try
            {
                Message m = context.Messages.Single(a => a.msgID == id);
                context.Messages.Remove(m);
                context.SaveChanges();
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
        public static bool admin_Message(int messageID, string username, string subject, string messageBody, string receive_end)
        {
            BWAEntities context = new BWAEntities();
            try
            {

                Message m = new Message();

                m.msgID = random_numberGen.randNumber();
                m.username = username;
                m.subject = subject;
                m.messageDetail = messageBody;
                m.reciever = receive_end;


                context.Messages.Add(m);
                context.SaveChanges();

                return true;
            }
            catch (InvalidOperationException) { return false; }
        }
        public static bool addItem(string itemID, string itemName, string itemDescription, int Stock, int discount, int price, string itemAvailability, string itemCategory)
        {
            BWAEntities context = new BWAEntities();
            try
            {

                Product p = new Product();
                p.ItemID = itemID;
                p.ItemName = itemName;
                p.ItemDescription = itemDescription;
                p.Stock = Stock;
                p.Discount = discount;
                p.Price = price;
                p.ItemAvailability = itemAvailability;
                p.ItemCategory = itemCategory;

                context.Products.Add(p);
                context.SaveChanges();
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
        public static bool AddtoWlist(List<Product> products, string username)
        {
            
            BWAEntities context = new BWAEntities();
            try
            {
                foreach (var item in products)
                {
                    Wlist wishlistItem = new Wlist
                    {
                        itemID = item.ItemID,
                        username = username,
                        itemName= item.ItemName,
                        itemPrice = item.Price
                    };
                    context.Wlists.Add(wishlistItem);
                    context.SaveChanges();
                }
                return true;
            }

            catch (InvalidOperationException)
            {
                return false;
            }
        }
        public static bool remove_product(string id)
        {

            BWAEntities context = new BWAEntities();
            try
            {
                Product m = context.Products.Single(a => a.ItemID == id);
                context.Products.Remove(m);
                context.SaveChanges();
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
    }
}