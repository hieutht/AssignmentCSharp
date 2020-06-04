using System;
using System.Text;
using DatabaseHandler.Entity;
using DatabaseHandler.Helper;
using MySql.Data.MySqlClient;

namespace DatabaseHandler.Model
{
    public class TransactionModel
    {
        private static string WithDrawMessage = "Withdraw at Spring Hero ATM - 8 Ton That Thuyet";
        private static string DepositMessage = "Deposit at Spring Hero Transaction Station.";

        // Hàm này thực hiện rút tiền với 2 tham số là: số tài khoản và số tiền cần rút.
        // Hàm sẽ check sự tồn tại và trạng thái của tài khoản,
        // check số dư mới nhất của tài khoản cũng như kiểm tra để tài khoản còn ít nhất 50k.
        // Update số dư tài khoản.
        // Lưu log transaction.
        // Tất cả được thực thi trong block transaction của MySQL, trường hợp lỗi sẽ rollback.
        public bool WithDraw(string accountNumber, double amount) // tài khoản nào rút và rút bao nhiêu.
        {
            // tạo biến kết quả trả về, mặc định = false.
            bool result = false;
            // mở kết nối đến db.
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            // tạo transaction.
            var mySqlTransaction = cnn.BeginTransaction();
            try
            {
                // 1. Kiểm tra sự tồn tại và trạng thái của tài khoản, lấy ra số dư mới nhất.
                // 1.1. Tạo câu lệnh truy vấn theo account Number và status, lấy ra balance.
                var cmdGetBalance =
                    new MySqlCommand(
                        $"select balance from `sh-accounts` where accountNumber = '{accountNumber}' and status = {(int) AccountStatus.ACTIVE}",
                        cnn);
                // 1.2. Lấy ra reader để check kết quả trả về.
                var readerGetBalance = cmdGetBalance.ExecuteReader();
                // trong trường hợp không có dữ liệu trả về thì thông báo lỗi.
                if (!readerGetBalance.Read())
                {
                    readerGetBalance.Close(); // đặc biệt lưu ý là đóng reader để có thể thực hiện các câu lệnh tiếp theo.
                    throw new Exception("Account is not found or has been locked!");
                }
                // lấy ra số dư mới nhất.
                var balance = readerGetBalance.GetDouble("balance");
                readerGetBalance.Close();
                // 2. udpate số dư.
                var updateBalance = balance - amount;
                // kiểm số dư còn ít nhất 50k.
                if (updateBalance <= 50000)
                {
                    throw new Exception("Account is not enough money!");
                }
                // 2.1 Tạo câu lệnh udpate số dư theo tài khoản.
                var cmdUpdateBalance =
                    new MySqlCommand(
                        $"update `sh-accounts` set balance = {updateBalance} where accountNumber = '{accountNumber}'",
                        cnn);
                // 2.2 Thực thi câu lệnh update.
                cmdUpdateBalance.ExecuteNonQuery();
                // 3. lưu transaction.
                // 3.1 Tạo đối tượng transaction.
                var transactionHistory = new SHTransactionHistory()
                {
                    TransactionCode = Guid.NewGuid().ToString(), // code được sinh ra unique
                    CreatedAt = DateTime.Now, // Thời gian hiện tại.
                    UpdatedAt = DateTime.Now,
                    Type = SHTransactionType.WITHDRAW,
                    Amount = amount,
                    Fee = 0,
                    Message = WithDrawMessage,
                    Status = SHTransactionStatus.DONE
                };
                // 3.2 Xây dựng câu lệnh insert sử dụng StringBuilder, tránh + chuỗi.
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append($"insert into `sh-transaction-history` ");
                stringBuilder.Append(
                    $"(transactionCode, createdByAccountNumber, createdAt, updatedAt, senderAccountNumber, receiverAccountNumber, type, amount, fee, message, status) ");
                stringBuilder.Append("values");
                stringBuilder.Append(" (");
                stringBuilder.Append($"'{transactionHistory.TransactionCode}',");
                stringBuilder.Append($"'{accountNumber}',");
                // Thời gian khi insert phải format theo yyyy-MM-dd hh:mm:ss
                stringBuilder.Append($"'{transactionHistory.CreatedAt:yyyy-MM-dd hh:mm:ss}',");
                stringBuilder.Append($"'{transactionHistory.UpdatedAt:yyyy-MM-dd hh:mm:ss}',");
                stringBuilder.Append($"'{null}',");
                stringBuilder.Append($"'{null}',");
                stringBuilder.Append($"{(int)transactionHistory.Type},");
                stringBuilder.Append($"{(double)transactionHistory.Amount},");
                stringBuilder.Append($"{(double)transactionHistory.Fee},");
                stringBuilder.Append($"'{transactionHistory.Message}',");
                stringBuilder.Append($"{(int)transactionHistory.Status}");
                stringBuilder.Append(" )");
                var cmdInsertTransaction = new MySqlCommand(stringBuilder.ToString(), cnn);
                // Thực thi câu lệnh, lưu transaction.
                cmdInsertTransaction.ExecuteNonQuery();
                // kết thúc transaction ở trạng thái thành công, đảm bảo tất cả các câu lệnh được thực thi.
                // update thông tin vào database.
                mySqlTransaction.Commit();
                result = true; // set kết quả = true.
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                mySqlTransaction.Rollback(); // đưa tất cả về xuất phát điểm bản đầu.
                result = false;
            }
            cnn.Close();
            return result;
        }
    }
}