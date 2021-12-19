namespace CreateFactor.Utils
{
    public class Utilities
    {
        private readonly IWebHostEnvironment environment;
        public Utilities(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }
        public bool ByteArrayToFile(string fileName, byte[] byteArray)
        {
            try
            {
                using (var fs = new FileStream(Path.Combine(environment.WebRootPath, fileName), FileMode.Create, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }
        }


        public void SaveBytesToFile(Byte[] byteArray, string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(byteArray, 0, byteArray.Length);
            fileStream.Close();
        }

    }
}
