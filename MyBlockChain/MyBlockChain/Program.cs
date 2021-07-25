using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MyBlockChain
{
    partial class Program
    {
        static void Main(string[] args)
        {
            //TODO : Creare utenti per la BlockChain
            //Creare un wallet, assegnare al wallet del nodo miner la ricompensa
            //Gestire le transazioni Tra untenti tramite il wallet


            DateTime tempoInizio = DateTime.Now;

            User Chiara = new User("Chiara", "Mattioli");
            User Roberto = new User("Roberto", "Lucchetti");

            //instanzia un oggetto che rappresenta il mining della moneta
            BlockChain MolCoin = new BlockChain();

            //implementazione con gestione transazioni
            Roberto.wallet.Receive(10);

            if (Roberto.wallet.Send(5))
            {
                
                try
                {
                    MolCoin.CreaTransazione(new Transazione(Roberto.wallet.PublicKey, Chiara.wallet.PublicKey, 5));
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Transazione fallita : {e.Message}");
                    Roberto.wallet.Receive(5);
                }
                Console.WriteLine("\nTransazione salvata correttamente\n");
                try
                {
                    MolCoin.ConfermaBlocco("Miner");
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Errore nel nodo miner {e.Message}");
                }
                Chiara.wallet.Receive(5);
                StampaTransazioneconfermata(MolCoin);
            }
            else
            {
                Console.WriteLine("\n******************************************************\n");
                Console.WriteLine("Bilancio non sufficiente per effettuare la transazione");
                Console.WriteLine("\n******************************************************\n");
            }
            // Console.WriteLine($"Durata: {tempoFine - tempoInizio}");

            // Rifare l bilancio basato sul wallet

            /*Console.WriteLine("----------------------------");

            Console.WriteLine("\nRiepilogo bilanci:");
            Console.WriteLine($"\tBilancio Roberto: { MolCoin.GetBilancio("Roberto")}");
            Console.WriteLine($"\tBilancio Chiara: { MolCoin.GetBilancio("Chiara")}");
            Console.WriteLine($"\tBilancio Marioner: { MolCoin.GetBilancio("Marioner")}");

            Console.WriteLine("----------------------------");*/

            Console.WriteLine("\nMolCoin:");
            Console.WriteLine($"MolCoin");
           // Console.WriteLine(JsonConvert.SerializeObject(MolCoin, Formatting.Indented));

            MolCoin.Difficolta = 3;
            if (Roberto.wallet.Send(6))
            {
                Chiara.wallet.Receive(6);
                MolCoin.CreaTransazione(new Transazione(Roberto.wallet.PublicKey, Chiara.wallet.PublicKey, 5));
                Console.WriteLine("\nTransazione salvata correttamente\n");
                MolCoin.ConfermaBlocco("Marioner");
                Console.WriteLine("\nTransazione Confermata\n");
            }
            else
            {
                Console.WriteLine("\n******************************************************\n");
                Console.WriteLine("Bilancio non sufficiente per effettuare la transazione");
                Console.WriteLine("\n******************************************************\n");
            }
            

            /*   MolCoin.CreaTransazione(new Transazione("Chiara", "Roberto", 5));
               MolCoin.CreaTransazione(new Transazione("Roberto", "Chiara", 5));
               MolCoin.ConfermaBlocco("Marioner"); */


         /*   DateTime tempoFine = DateTime.Now;

            Console.WriteLine($"Durata: {tempoFine - tempoInizio}");

            Console.WriteLine("\nMolCoin:");
            Console.WriteLine($"MolCoin");
            Console.WriteLine(JsonConvert.SerializeObject(MolCoin, Formatting.Indented)); */

            //aspetta la pressione di un tasto per la terminazione del programma
            Console.WriteLine("\nEsecuzione terminata.\nPremere un tasto per uscire...");

            Console.ReadKey();

        }
        public static void StampaTransazioneconfermata(BlockChain MolCoin)
        {
            Console.WriteLine("\nTransazione Confermata\n");
            // Console.WriteLine($"Durata: {tempoFine - tempoInizio}");
            Console.WriteLine("\nMolCoin:");
            Console.WriteLine($"MolCoin");
            Console.WriteLine(JsonConvert.SerializeObject(MolCoin, Formatting.Indented));
        }
    }
}
