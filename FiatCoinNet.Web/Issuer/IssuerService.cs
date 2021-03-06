﻿using System;
using FiatCoinNet.Common;
using FiatCoinNet.Domain;
using FiatCoinNet.Domain.Requests;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;

namespace FiatCoinNetWeb.Controllers
{
    public class IssuerService
    {
        private const int MAX_TRANSACTION = 100;
        public Issuer issuer1;
        public Issuer issuer2;

        //public static readonly ConcurrentDictionary<int, List<LowerLevelBlock>> s_Blocks;
        //public static readonly Queue<PaymentTransaction> s_PaymentPool;
        //public static int epoch;
        //public static int version;

        public IssuerService()
        {
            int Id = 1010;
            string Name = "菲特银行北海分行";
            string PublicKey = "RUNTMSAAAAB9kGHlnnUY0FGYBCzd7WdcZifnx+wfPVswjSrxYqAw7sC92RYMLB2iJA9f7utNbhX7WGUgzWwKd+Y4wscGA98G";
            string PrivateKey = "RUNTMiAAAAB9kGHlnnUY0FGYBCzd7WdcZifnx+wfPVswjSrxYqAw7sC92RYMLB2iJA9f7utNbhX7WGUgzWwKd+Y4wscGA98G65oCkARn28CZeMfTC9i84DMlfc7vwSABoRVRmWlaLR4=";
            string SignatureToCertifyIssuer = "";
            issuer1 = new Issuer(Id, Name, PublicKey, PrivateKey, SignatureToCertifyIssuer);

            Id = 1942;
            Name = "菲特银行南海分行";
            PublicKey = "RUNTMSAAAADn7HBQqfSnjcD2R3UFKyirGIAqk65+NPWMIlX3Ilp95HpZLWt9DqSYowSbCQ1wUienJ9wQ2GEoYKWOEwMF9jl6";
            PrivateKey = "RUNTMiAAAADn7HBQqfSnjcD2R3UFKyirGIAqk65+NPWMIlX3Ilp95HpZLWt9DqSYowSbCQ1wUienJ9wQ2GEoYKWOEwMF9jl6bLAWC+sFREWvj1rfY97K49tosn9eg5CQ9Q3e1i59zgQ=";
            SignatureToCertifyIssuer = "";
            issuer2 = new Issuer(Id, Name, PublicKey, PrivateKey, SignatureToCertifyIssuer);

        }

        public void CreateLowerLevelBlockForIssuer1(object source, ElapsedEventArgs e)
        {
            CreateLowerLevelBlockForIssuer1();
        }

        public void CreateLowerLevelBlockForIssuer2(object source, ElapsedEventArgs e)
        {
            CreateLowerLevelBlockForIssuer2();
        }

        public void CreateLowerLevelBlockForIssuer1()
        {
            if (issuer1.s_PaymentPool.Count == 0)
                return;
            //Construct low level block
            LowerLevelBlock block = new LowerLevelBlock();
            block.TransactionCounter = 0;
            
            //Construct the block header
            block.blockHeader.hashPrevBlock = GetPreviousTransactionHash();
            block.blockHeader.Issuer = issuer1.Id.ToString();
            block.blockHeader.Time = GetTime(DateTime.Now);
            block.blockHeader.Version = issuer1.version;

            while(issuer1.s_PaymentPool.Count != 0 && block.TransactionCounter < MAX_TRANSACTION)
            {
                PaymentTransaction transaction = issuer1.s_PaymentPool.Dequeue();
                if(VerifyTransaction(transaction, issuer1) == true)
                {
                    block.TransactionSet.Add(transaction);
                    block.TransactionCounter++;

                    //Write the verified transaction to DB
                    DataAccess.DataAccessor.FiatCoinRepository.AddTransaction(transaction);
                }
            }
            //Add the Merkle root
            block.blockHeader.hashMerkleRoot = ConstructMerkleTree(block);

            //Add the hash & size of this block
            block.Hash = CryptoHelper.Hash(JsonHelper.Serialize(block));
            block.blockSize = Marshal.SizeOf(block);

            //Call api to post the lowerlevelblock to Bank
            string requestUri = string.Format("certifier/api/postLowerLevelBlock");
            HttpContent content = new StringContent(JsonHelper.Serialize(block));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = RestApiHelper.HttpClient.PostAsync(requestUri, content).Result;
            response.EnsureSuccessStatusCode();

            //Write the lowerlevelblock to DB
            DataAccess.DataAccessor.FiatCoinRepository.AddLowerLevelBlock(block);
        }

        public void CreateLowerLevelBlockForIssuer2()
        {
            if (issuer2.s_PaymentPool.Count == 0)
                return;
            //Construct low level block
            LowerLevelBlock block = new LowerLevelBlock();
            block.TransactionCounter = 0;

            //Construct the block header
            block.blockHeader.hashPrevBlock = GetPreviousTransactionHash();
            block.blockHeader.Issuer = issuer2.Id.ToString();
            block.blockHeader.Time = GetTime(DateTime.Now);
            block.blockHeader.Version = issuer2.version;

            while (issuer2.s_PaymentPool.Count != 0 && block.TransactionCounter < MAX_TRANSACTION)
            {
                PaymentTransaction transaction = issuer2.s_PaymentPool.Dequeue();
                if (VerifyTransaction(transaction, issuer2) == true)
                {
                    block.TransactionSet.Add(transaction);
                    block.TransactionCounter++;

                    //Write the verified transaction to DB
                    DataAccess.DataAccessor.FiatCoinRepository.AddTransaction(transaction);
                }
            }
            //Add the Merkle root
            block.blockHeader.hashMerkleRoot = ConstructMerkleTree(block);

            //Add the hash & size of this block
            block.Hash = CryptoHelper.Hash(JsonHelper.Serialize(block));
            block.blockSize = Marshal.SizeOf(block);

            //Call api to post the lowerlevelblock to Bank
            string requestUri = string.Format("certifier/api/postLowerLevelBlock");
            HttpContent content = new StringContent(JsonHelper.Serialize(block));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = RestApiHelper.HttpClient.PostAsync(requestUri, content).Result;
            response.EnsureSuccessStatusCode();

            //Write the lowerlevelblock to DB
            DataAccess.DataAccessor.FiatCoinRepository.AddLowerLevelBlock(block);
        }

        private string ConstructMerkleTree(LowerLevelBlock block)
        {
            int numberofPayment = block.TransactionCounter;
            int counter = 0;
            Queue<string> MerkleTree = new Queue<string>();
            foreach (var transaction in block.TransactionSet)
            {
                MerkleTree.Enqueue(CryptoHelper.Hash(JsonHelper.Serialize(transaction)));
            }
            if (numberofPayment % 2 == 1)
            {
                MerkleTree.Enqueue(CryptoHelper.Hash(JsonHelper.Serialize(block.TransactionSet[numberofPayment - 1])));
                numberofPayment++;
            }
            while (MerkleTree.Count != 1)
            {
                string hash = MerkleTree.Dequeue();
                hash += MerkleTree.Dequeue();
                MerkleTree.Enqueue(CryptoHelper.Hash(JsonHelper.Serialize(hash)));
                counter += 2;
                if(counter + 1 == numberofPayment)
                {
                    hash = MerkleTree.Dequeue();
                    hash += hash;
                    MerkleTree.Enqueue(CryptoHelper.Hash(JsonHelper.Serialize(hash)));
                    counter++;
                }
                if(counter == numberofPayment)
                {
                    counter = 0;
                    numberofPayment /= 2;
                }
            }
            string result = MerkleTree.Dequeue();
            MerkleTree = null;
            return result;
        }

        public string GetPreviousTransactionHash()
        {
            string requestUri = "certifier/api/prevBlockHash";
            HttpResponseMessage response = RestApiHelper.HttpClient.GetAsync(requestUri).Result;
            response.EnsureSuccessStatusCode();
            string hash = response.Content.ReadAsAsync<string>().Result;
            return hash;
        }

        private static double GetTime(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            double result = (time - startTime).TotalSeconds;
            return result;
        }

        public bool VerifyTransaction(PaymentTransaction transaction, Issuer issuer)
        {
            //TODO: Verify the transaction
            for(int i = 0; i < transaction.Amount.Count; i++)
            {
                //Step1: PayFrom provide the <sig><pubkey>
                string scriptSig = transaction.scriptSig[i];
                string scriptSigPubKey = transaction.scriptSigPubkey[i];
                //Step2: Through trid trace last transaction scriptPubKey
                string txid = transaction.PreviousTransactionHash[i];
                int txidIndex = transaction.PreviousTransactionIndex[i];
                //Find the transaction match txid and receive its pubKeyHash
                //traverse the blocks in the issuer
                string pubkeyHash = "";
                foreach (var block in issuer.s_Blocks)
                {
                    foreach (var trans in block.TransactionSet)
                    {
                        if(trans.TransactionId == txid)
                        {
                            pubkeyHash = trans.Dest[txidIndex];
                        }
                    }
                }
                //Step3: Verification script
                if (CryptoHelper.Hash(JsonHelper.Serialize(scriptSigPubKey)) == pubkeyHash)
                {
                    //Step4: CheckSig
                    //TODO: CheckSig
                } 
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
