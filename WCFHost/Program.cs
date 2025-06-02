using System;
using System.ServiceModel;
using QuanLyHocSinhWCF; // Namespace chứa QuanLyService

namespace WCFHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (ServiceHost host = new ServiceHost(typeof(QuanLyService)))
                {
                    host.Open();
                    Console.WriteLine("✅ WCF Service is running at http://localhost:8000/QuanLyService");
                    Console.ReadLine();
                    host.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ WCF failed to start:");
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("💡 Inner exception:");
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
        }
    }
}

    
