using CheckVarMttq;
using EntityFrameworkCore.SqlServer.SimpleBulks.BulkInsert;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

var transactions = LoadTransactions();

using (var dbct = new CheckVarMttqDbContext())
{
    dbct.Database.Migrate();
    dbct.BulkInsert(transactions);
}


static IEnumerable<Transaction> LoadTransactions()
{
    var fileName = Path.Combine(@"C:\Users\Phong.NguyenDoan\Downloads\Thong_tin_ung_ho_qua_tsk_vcb_0011001932418_tu_01_09_den10_09_2024.xlsx");
    using var fileStream = File.OpenRead(fileName);
    using var reader = ExcelReaderFactory.CreateReader(fileStream);

    var result = new List<Transaction>();

    do
    {
        if (reader.VisibleState != "visible")
        {
            continue;
        }

        var rowIndex = 0;
        while (reader.Read())
        {
            if (rowIndex <= 13)
            {
                rowIndex++;
                continue;
            }

            var transaction = new Transaction();
            transaction.DocNo = reader.GetValue(0)?.ToString();

            decimal.TryParse(reader.GetValue(2)?.ToString(), out decimal amount);
            transaction.Amount = amount;

            transaction.Note = reader.GetValue(4)?.ToString();
            transaction.Index = rowIndex + 1;

            result.Add(transaction);

            rowIndex++;
        }

        break;

    } while (reader.NextResult());

    return result;
}
