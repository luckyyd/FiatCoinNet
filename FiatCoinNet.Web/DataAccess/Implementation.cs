﻿using FiatCoinNet.Domain;
using FiatCoinNet.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiatCoinNetWeb.DataAccess
{
    public partial class DataAccessor : 
        IFiatCoinRepository
    {
        public PaymentAccount AddAccount(PaymentAccount newAccount)
        {
            var result = QueryStoreProcedure("AddAccount", new Dictionary<string, object>
                                                          {
                                                              {"@address", newAccount.Address},
                                                              {"@issuerId", newAccount.IssuerId},
                                                              {"@currencyCode", newAccount.CurrencyCode},
                                                              {"@publicKey", newAccount.PublicKey},
                                                          });
            if (result.Tables[0].Rows.Count > 0)
            {
                var acct = new PaymentAccount().FromRow(result.Tables[0].Rows[0]);
                return acct;
            }
            return null;
        }

        public HigherLevelBlock AddHigherLevelBlock(HigherLevelBlock newHigherLevelBlock)
        {
            var result = QueryStoreProcedure("AddHigherLevelBlock", new Dictionary<string, object>
                                                        {
                                                            {"@hash", newHigherLevelBlock.Hash },
                                                            {"@blockSize", newHigherLevelBlock.blockSize },
                                                            {"@blockHeader", newHigherLevelBlock.blockHeader },
                                                            {"@LowerLevelBlockCounter", newHigherLevelBlock.LowerLevelBlockCounter },
                                                            {"@LowerLevelBlockSet", newHigherLevelBlock.LowerLevelBlockSet },
                                                            {"@period", newHigherLevelBlock.Period },
                                                            {"@signature", newHigherLevelBlock.Signature },
                                                        });
            if (result.Tables[0].Rows.Count > 0)
            {
                var hlb = new HigherLevelBlock().FromRow(result.Tables[0].Rows[0]);
                return hlb;
            }
            return null;
        }

        public LowerLevelBlock AddLowerLevelBlock(LowerLevelBlock newLowerLevelBlock)
        {
            var result = QueryStoreProcedure("AddLowerLevelBlock", new Dictionary<string, object>
                                                        {
                                                            {"@hash", newLowerLevelBlock.Hash },
                                                            {"@blockSize", newLowerLevelBlock.blockSize },
                                                            {"@blockHeader", newLowerLevelBlock.blockHeader },
                                                            {"@transactionCounter", newLowerLevelBlock.TransactionCounter },
                                                            {"@transactions", newLowerLevelBlock.TransactionSet },
                                                            {"@period", newLowerLevelBlock.Epoch },
                                                            {"@hash", newLowerLevelBlock.Hash },
                                                            {"@signature", newLowerLevelBlock.Signature },
                                                            {"@signatureToCertifyIssuer", newLowerLevelBlock.SignatureToCertifyIssuer },
                                                        });
            if (result.Tables[0].Rows.Count > 0)
            {
                var llb = new LowerLevelBlock().FromRow(result.Tables[0].Rows[0]);
                return llb;
            }
            return null;
        }

        public PaymentTransaction AddTransaction(PaymentTransaction newTransaction)
        {
            var result = QueryStoreProcedure("AddTransaction", new Dictionary<string, object>
                                                          {
                                                              {"@transactionId", newTransaction.TransactionId},
                                                              {"@amount", newTransaction.Amount},
                                                              {"@currencyCode", newTransaction.CurrencyCode},
                                                              {"@scriptSig", newTransaction.scriptSig},
                                                              {"@scriptSigPubKey", newTransaction.scriptSigPubkey},
                                                              {"@in_counter", newTransaction.In_counter},
                                                              {"@scriptPubKey", newTransaction.scriptPubKey},
                                                              {"@out_counter", newTransaction.Out_counter},
                                                              {"@source", newTransaction.Source},
                                                              {"@dest", newTransaction.Dest},
                                                              {"@issuerId", newTransaction.IssuerId},
                                                              {"@PreviousTransactionHash", newTransaction.PreviousTransactionHash},
                                                              {"@PreviousTransactionIndex", newTransaction.PreviousTransactionIndex},
                                                              {"@memoData", newTransaction.MemoData},
                                                          });
            if (result.Tables[0].Rows.Count > 0)
            {
                var trans = new PaymentTransaction().FromRow(result.Tables[0].Rows[0]);
                return trans;
            }
            return null;
        }

        public void CloseAccount(string address)
        {
            QueryStoreProcedure("CloseAccount", new Dictionary<string, object>
                                                          {
                                                              {"@address", address},
                                                          });
        }

        public PaymentAccount GetAccount(int issuerId, string address)
        {
            var result = QueryStoreProcedure("GetAccount", new Dictionary<string, object>
                                                          {
                                                              {"@issuerId", issuerId},
                                                              {"@address", address},
                                                          });
            if (result.Tables[0].Rows.Count > 0)
            {
                var acct = new PaymentAccount().FromRow(result.Tables[0].Rows[0]);
                return acct;
            }
            return null;
        }

        public List<PaymentAccount> GetAccounts(int issuerId)
        {
            var list = new List<PaymentAccount>();

            var result = QueryStoreProcedure("GetAccounts", new Dictionary<string, object>
                                                          {
                                                              {"@issuerId", issuerId},
                                                          });
            if (result.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in result.Tables[0].Rows)
                {
                    list.Add(new PaymentAccount().FromRow(row));
                }
            }
            return list;
        }

        public HigherLevelBlock GetHigherLevelBlock()
        {
            throw new NotImplementedException();
        }

        public LowerLevelBlock GetLowerLevelBlock()
        {
            throw new NotImplementedException();
        }

        public List<PaymentTransaction> GetTransactions(int issuerId, string address)
        {
            var list = new List<PaymentTransaction>();

            var result = QueryStoreProcedure("GetTransactions", new Dictionary<string, object>
                                                          {
                                                              {"@issuerId", issuerId},
                                                              {"@address", address},
                                                          });
            if (result.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in result.Tables[0].Rows)
                {
                    list.Add(new PaymentTransaction().FromRow(row));
                }
            }
            return list;
        }
    }
}
