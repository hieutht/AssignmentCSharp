using System;

namespace DatabaseHandler.Entity
{
    public class SHTransactionHistory
    {
        // int tự tăng.
        public string TransactionCode { get; set; } // Transaction-00005 // Transaction Count
        public string CreatedByAccountNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string SenderAccountNumber { get; set; }
        public string ReceiverAccountNumber { get; set; }
        public SHTransactionType Type { get; set; }
        public double Amount { get; set; }
        public double Fee { get; set; } // default 0
        public string Message { get; set; } // Rút tiền tại ATM, số tiền là 10.000 (VND)
        public SHTransactionStatus Status { get; set; } // 1
    }

    public enum SHTransactionType
    {
        DEPOSIT = 1, // gửi 
        WITHDRAW = 2, // rút
        TRANSFER = 3 // chuyển khoản.
    }

    public enum SHTransactionStatus
    {
        DONE = 1,
        FAILS = 2,
        PROCESSING = 3
    }
}