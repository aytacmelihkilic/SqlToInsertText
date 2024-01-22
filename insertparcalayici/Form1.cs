using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace insertparcalayici
{
    public partial class Form1 : Form
    {
        private const int BatchSize = 950; // Her bir INSERT batch'inin boyutu

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Kullanıcı arayüzünden girişleri al
            string serverName = "ITOCH\\ITOCH"; // Veritabanı sunucu adı
            string databaseName = "Turkey"; // Veritabanı adı
            string tableName = "Vatandaslar"; // Hedef tablo adı
            string outputFileName = "C:\\veriler\\data2.sql"; // Çıktı dosyası adı

            // Bağlantı dizesini oluştur
            string connectionString = $"Data Source={serverName};Initial Catalog={databaseName};Integrated Security=true";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Verilerin bulunduğu SQL sorgusu
                string selectQuery = "SELECT * FROM Vatandaslar"; // YourSourceTable tablo adını değiştirin

                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        StringBuilder insertQuery = new StringBuilder();
                        int recordCount = 0;

                        while (reader.Read())
                        {
                            // Veriyi al ve insert sorgusunu oluştur
                            string insertValues = GetInsertValues(reader);
                            insertQuery.AppendLine($"INSERT INTO {tableName} VALUES ({insertValues});");
                            recordCount++;

                            // Belirlenen batch boyutuna ulaşıldığında veya tüm veriler işlendiyse INSERT yap
                            if (recordCount % BatchSize == 0 || reader.IsClosed)
                            {
                                // INSERT ifadelerini dosyaya eklerken, her bir grup için bir satır boşluk bırak
                                WriteToFile(outputFileName, insertQuery.ToString() + Environment.NewLine);

                                // Yeni bir batch için StringBuilder'ı sıfırla
                                insertQuery.Clear();
                            }
                        }
                    }
                }
            }

            MessageBox.Show($"Insert ifadeleri {outputFileName} dosyasına yazıldı.");
        }

        private string GetInsertValues(SqlDataReader reader)
        {
            StringBuilder values = new StringBuilder();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                // Her bir sütunu uygun formatta ekle
                string value = reader[i].ToString();

                // Tek tırnaklı alanlarda tek tırnakları escape et
                if (reader.GetFieldType(i) == typeof(string))
                {
                    value = $"'{value.Replace("'", "''")}'";
                }

                values.Append(value);

                // Son sütun değilse virgül ekle
                if (i < reader.FieldCount - 1)
                {
                    values.Append(", ");
                }
            }

            return values.ToString();
        }

        private void WriteToFile(string filePath, string content)
        {
            // Dosyaya yaz
            using (StreamWriter writer = new StreamWriter(filePath, true)) // true parametresi dosyaya ekleme yapılmasını sağlar
            {
                writer.Write(content);
            }
        }
    }
}
