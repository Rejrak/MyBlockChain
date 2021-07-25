using System;
using System.Collections.Generic;

namespace MyBlockChain
{
    public class BlockChain
    {
        //Lista di transazioni in attesa di essere confermate
        private IList<Transazione> TransazioniInAttesa = new List<Transazione>();
        //"Catena di blocchi"
        public IList<Block> Chain { get; set; }
        //Scelgo la difficolta, ovvero il numero di zeri iniziali da usare come vincolo per calcolare l'hash
        public int Difficolta { get; set; } = 3;
        //Con l'aggiunta della blockchain s'inserisce anche il concetto di ricompensa
        //Ogni volta che un miner riesce a trovare un hash, gli viene data una "ricompensa"
        public int Ricompensa = 1;



        public BlockChain()
        {
            InizializzaCatena();
            AddInitialBlock();
        }
        //Istanzio il blocco iniziale
        public Block CreaBlockIniziale()
        {
            Block block = new Block(DateTime.Now, null, TransazioniInAttesa);
            block.Mina(Difficolta);
            TransazioniInAttesa = new List<Transazione>();
            return block;
        }
        //Inizializzo la catena
        public void InizializzaCatena()
        {
            Chain = new List<Block>();
        }
        //Aggiungo il blocco iniziale alla catena
        public void AddInitialBlock()
        {
            Chain.Add(CreaBlockIniziale());
        }
        //Restituisce l'ultimo blocco della catena
        public Block GetLastBlock()
        {
            return Chain[Chain.Count-1];
        }
        //Aggiungo un blocco alla catena
        public void AddBlock(Block block)
        {
            Block lastBlock = GetLastBlock();
            //block.Indice = lastBlock.Indice + 1;
            block.PrevHash = lastBlock.Hash;
           // block.Hash = block.CalcolaHash();
           
            block.Mina(Difficolta);
            Chain.Add(block);
        }
        //Controllo se la catena e' valida, ovvero che ogni blocco sia coerente con il successivo
        /*Devo Ragionare al contrario? Partire dall'ultimo blocco ed arrivare al primo?*/
        public bool IsValid()
        {
            for (int pos = 1; pos < Chain.Count; pos++)
            {
                Block block = Chain[pos];
                Block prevblock = Chain[pos - 1];
                if (block.CalcolaHash() != block.Hash)
                    return false;
                if (block.PrevHash != prevblock.Hash)
                    return false;
            }
            return true;
        }
        //Creo nuove transazioni da confermare
        public void CreaTransazione(Transazione transazione)
        {
            TransazioniInAttesa.Add(transazione);
        }
        //funzione che, passato l'indirizzo del miner che deve gestire le transazioni, calcola l'hash e se lo trova, conferma le transazioni
        public void ConfermaBlocco(string indirizzoMiner)
        {
            Block block = new Block(DateTime.Now, GetLastBlock().Hash, TransazioniInAttesa);
            AddBlock(block);
            TransazioniInAttesa = new List<Transazione>();
            CreaTransazione(new Transazione(null, indirizzoMiner, Ricompensa));
        }


        /*Restituisce il bilancio delle transazioni e del miner
         Da modificare nel caso in cui creo utenti con relativo wallet*/
        public int GetBilancio(string indirizzo)
        {
            int bilancio = 0;

            for (int posBlocco = 0; posBlocco < Chain.Count; posBlocco++)
            {
                for (int posTransazione = 0; posTransazione < Chain[posBlocco].Transazioni.Count; posTransazione++)
                {
                    Transazione transazione = Chain[posBlocco].Transazioni[posTransazione];

                    if (transazione.IndirizzoSorgente == indirizzo)
                    {
                        bilancio -= transazione.Valore;
                    }

                    if (transazione.IndirizzoDestinazione == indirizzo)
                    {
                        bilancio += transazione.Valore;
                    }

                }
            }

            return bilancio;
        }
    }
}
