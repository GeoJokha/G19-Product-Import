namespace G19_ProductImport
{
    public class DatabaseImporter
    {
        private readonly string _connectionString;

        public DatabaseImporter(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void ImportData(IEnumerable<Category> categories)
        {
            throw new NotImplementedException();
        }
        static void FileStream()
        {
            string ProductsFile = @"D:\products2.txt";
            string[] lines = File.ReadAllLines(ProductsFile);
            foreach (string line in lines)
            {
                string[] parts = line.Split('\t');
                Console.WriteLine(line);
            }
        }
    }
}