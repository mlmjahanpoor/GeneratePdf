namespace CreateFactor.Utils
{
    public class UploasX
    {
        private readonly IWebHostEnvironment _environment;
        public UploasX(IWebHostEnvironment environment)
        {
            _environment = environment; 
        }

        public bool ByteArrayToFile(string fileName, byte[] byteArray)
        {
            try
            {
                using (var fs = new FileStream(Path.Combine(_environment.ContentRootPath, fileName), FileMode.Create, FileAccess.Write))
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

    }
    
}

