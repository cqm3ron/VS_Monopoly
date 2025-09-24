namespace VS_Monopoly
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //try
            //{
                Console.SetWindowSize(1, 1);
                Console.SetBufferSize(72, 45);
                Console.SetWindowSize(72, 45);
            //    Console.Write(Properties.Resources.standard_uk);
            //    Console.WriteLine(Properties.Resources.standard_uk);
            List<Property> propertyData = BoardGenerator.Generate();
            BoardGenerator.Assemble(propertyData);
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.ReadKey(true);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);    
            //}

        }
    }
}