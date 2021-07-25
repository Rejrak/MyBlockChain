using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyBlockChain
{
    public class Block
    {   
        //Indice del blocck
        //public int Indice { get; set; }
        //TImeStamp del blocco fino al ms
        public DateTime TimeStamp { get; set; }
        //Hash del blocco corrente
        public string Hash { get; set; }
        //Hash del blocco precedente
        public string PrevHash { get; set; }
        //Lista di transazioni ( Mittente, Destinatario, Importo )
        public IList<Transazione> Transazioni { get; set; }
        //Assicura che i dati scambiati non possano essere alterati
        private int Nonce { get; set; } = 0;

        public Block(DateTime timeStamp, string prevHash, IList<Transazione> transazioni)
        {
           // Indice = 0;
            Transazioni = transazioni;
            TimeStamp = timeStamp;
            PrevHash = prevHash;
        }

        public string CalcolaHash()
        {
            SHA256 cifra = SHA256.Create();

            //L'Hash e' dato dal TimeStamp, dall'hash del blocco precedente (nel caso non ci fosse, uso una stringa vuota)
            //dai dati delle transazioni presenti e da Nonce
            byte[] input = Encoding.ASCII.GetBytes($"{TimeStamp}-{PrevHash ?? ""}-{JsonConvert.SerializeObject(Transazioni)}-{Nonce}");
            byte[] output = cifra.ComputeHash(input);

            return Convert.ToBase64String(output);
        }

        public void Mina(int difficolta)
        {
            //assegno il numero di zeri iniziali come vincolo dell'hash
            string zeroIniziali = new string('0', difficolta);
            //Finche' non trova un Hash che soddisfa la ricerca continuo a provare
            
            while(Hash == null || Hash.Substring(0,difficolta) != zeroIniziali)
            {
                //ad ogni tentativo incremento Nonce
                Nonce++;
                Hash = CalcolaHash();
                /*Stampa di tutti gli hash che prova e del relativo Nonce*/
                //Console.WriteLine($"{Hash}  Nonce = {Nonce}\n");
            }
        }
    }
}
