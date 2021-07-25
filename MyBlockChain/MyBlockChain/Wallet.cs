using System;
using System.Security.Cryptography;
using System.Text;

namespace MyBlockChain
{
    partial class Program
    {
        public class Wallet
        {
           // public DateTime TimeStamp { get; set; } = DateTime.Now;
            public string PublicKey;
            public string Valuta { get; set; } = "MolCoin";
            public double Bilancio { get; set; } = 0;


            public Wallet(string firstName, string lastName)
            {
                SHA256 cifra = SHA256.Create();
                byte[] input = Encoding.ASCII.GetBytes($"{firstName} - {lastName} - {DateTime.Now}");
                byte[] output = cifra.ComputeHash(input);
                PublicKey = Convert.ToBase64String(output);
            }

            public bool Send(double importo)
            {
                if (Bilancio < importo)
                    return false;
                else
                {
                    Bilancio = Bilancio - importo;
                    return true;
                }
            }
            public void Receive(double importo)
            {
                Bilancio += importo;
            }
        }
    }
    
}
