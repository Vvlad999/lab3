namespace lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            Console.SetWindowSize(100,100);
            var tmp = new txt_tojson();
            tmp.Serialization();
            var g = new Launch();
        }
    }
}
