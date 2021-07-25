using System;
using System.Security.Cryptography;
using System.Text;

namespace MyBlockChain
{
    partial class Program
    {
        public class User
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            private string PrivateKey { get; set; }
            public Wallet wallet { get; set; }
            public User(string firstname,string lastname)
            {
                FirstName = firstname;
                LastName = lastname;
                wallet = new Wallet(FirstName, LastName);
                SHA256 cifra = SHA256.Create();
                byte[] input = Encoding.ASCII.GetBytes($"{FirstName} - {LastName} - {DateTime.Now} - {wallet.PublicKey}");
                byte[] output = cifra.ComputeHash(input);
                PrivateKey = Convert.ToBase64String(output);
            }
        }
    }
    
}
